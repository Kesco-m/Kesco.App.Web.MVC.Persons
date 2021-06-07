using System;
using System.Collections.Generic;
using System.Linq;
using BLToolkit.DataAccess;
using BLToolkit.Mapping;
using Kesco.DataAccess;
using Kesco.Persons.ObjectModel;

namespace Kesco.Persons.BusinessLogic.DataAccess
{
	public abstract class PersonLinkAccessor : EntityAccessor<PersonLinkAccessor, DB, PersonLink, PersonLinkAccessor.SearchParameters, int>
	{
		/// <summary>
		/// Расширение для контрола выбора связи лица
		/// </summary>
		public class PersonLinkExtended : PersonLink
		{
			/// <summary>
			/// Кличка родителя
			/// </summary>
			[MapIgnore]
			public string FriendlyName { get { 
				return String.Format("{0} / {1}", Parent, Child);
			} }

			/// <summary>
			/// Кличка родителя
			/// </summary>
			[MapField("Родитель")]
			public string Parent { get; set; }

			/// <summary>
			/// Кличка потомка
			/// </summary>
			[MapField("Потомок")]
			public string Child { get; set; }

			/// <summary>
			/// Возвращает отображаемое имя.
			/// </summary>
			/// <returns>Отображаемое имя</returns>
			public override string GetInstanceFriendlyName()
			{
				return Child;
			}
		}

		/// <summary>
		/// Параметры поиска по связям лиц
		/// </summary>
		[MapField("СтрокаПоиска", "Search")]
		public class SearchParameters : Kesco.DataAccess.SearchParameters
		{
			/// <summary>
			/// Указывае, где искать совпадение, 0 - содержит, 1 - начинается с
			/// </summary>
			/// <value>
			/// Индикатор, как искать текст
			/// </value>
			[MapField("ГдеИскать")]
			public HowSearch HowSearch { get; set; }

			/// <summary>
			/// Указывает с какого порядкового номера 
			/// в наборе данных возвращать записи.
			/// </summary>
			/// <value>
			/// Порядковый номер результата.
			/// </value>
			[MapField("ВернутьНачинаяСПорядковогоНомера")]
			public int? StartFrom { get; set; }

			/// <summary>
			/// Устанавливает или возвращает список кодов связей лиц, 
			/// среди которых произвести поиск.
			/// </summary>
			/// <value>
			/// Список кодов связей лиц
			/// </value>
			[MapField("СписокКодов")]
			public string IDs { get; set; }

			/// <summary>
			/// Устанавливает или возвращает список кодов типов связей лиц, 
			/// среди которых произвести поиск.
			/// </summary>
			/// <value>
			/// Список кодов типов связей лиц
			/// </value>
			[MapField("СписокКодовТиповСвязейЛиц")]
			public string LinkTypeIDs { get; set; }

			/// <summary>
			/// Устанавливает или возвращает список кодов лиц-потомков, 
			/// среди которых произвести поиск.
			/// </summary>
			/// <value>
			/// Список кодов лиц-потомков
			/// </value>
			[MapField("СписокКодовЛицРодителей")]
			public string ParentIDs { get; set; }

			/// <summary>
			/// Устанавливает или возвращает список кодов лиц-потомков, 
			/// среди которых произвести поиск.
			/// </summary>
			/// <value>
			/// Список кодов лиц-потомков
			/// </value>
			[MapField("СписокКодовЛицПотомков")]
			public string ChildIDs { get; set; }

			/// <summary>
			/// Устанавливает или возвращает максимальное количество 
			/// записей, которое разрешено вернуть.
			/// если 0 - то все найденные записи.
			/// </summary>
			/// <value>
			/// Максимальное количество записей, 
			/// которое разрешено вернуть, 
			/// если 0 - то все найденные записи
			/// </value>
			[MapField("МаксимальноеКоличествоЗаписей")]
			public int? Limit { get; set; }
		}

		/// <summary>
		/// Параметры процедуры сохранения/изменения/удаления связанного лица
		/// </summary>
		public class PersonLinkForSave
		{
			/// <summary>
			/// 0 добавляем, 1 - обновляем, 2 - удаляем
			/// </summary>
			public int WhatDo;
			public int КодЛицаРодителя;
			public int КодЛицаПотомка;
			public int? КодСвязиЛиц;
			public int? КодТипаСвязиЛиц;
			public DateTime От = new DateTime(1980, 1, 1);
			public DateTime До = new DateTime(2050, 1, 1);
			public string Описание = "";
			public int? Параметр;
		}

        public override List<PersonLink> Search(SearchParameters criteria)
        {
            if (criteria.ChildIDs != null && criteria.ChildIDs.Split(',').Length > 0)
            {
                return SearchExtendedChildID<PersonLinkExtended>(criteria).Select(l => { return l as PersonLink; }).ToList();
            }

            if (criteria.ParentIDs != null && criteria.ParentIDs.Split(',').Length > 0)
            {
                return SearchExtendedParentID<PersonLinkExtended>(criteria).Select(l => { return l as PersonLink; }).ToList();
            }

            return SearchExtended<PersonLinkExtended>(criteria).Select(l => { return l as PersonLink; }).ToList();
        }

        public virtual List<TResult> Search<TResult>(SearchParameters criteria)
			where TResult: PersonLink
		{
			return SearchExtended<TResult>(criteria);
		}

		[SqlQuery(@"
			DECLARE @I int, @S varchar(200), @W varchar(200), @WRL varchar(200)
			DECLARE @СписокСловДляПоиска TABLE(Слово varchar(200)) DECLARE @КоличествоСлов int
	
			SET @МаксимальноеКоличествоЗаписей = ISNULL(@МаксимальноеКоличествоЗаписей, 0)
			IF @МаксимальноеКоличествоЗаписей < 0 SET @МаксимальноеКоличествоЗаписей = 0

			SET @ВернутьНачинаяСПорядковогоНомера = ISNULL(@ВернутьНачинаяСПорядковогоНомера, 1)
			IF @ВернутьНачинаяСПорядковогоНомера <= 0 SET @ВернутьНачинаяСПорядковогоНомера = 1
	
 			SET @СписокКодов = LTRIM(RTRIM(COALESCE(@СписокКодов, '')))
 			SET @СписокКодовТиповСвязейЛиц = LTRIM(RTRIM(COALESCE(@СписокКодовТиповСвязейЛиц, '')))
 			SET @СписокКодовЛицРодителей = LTRIM(RTRIM(COALESCE(@СписокКодовЛицРодителей, '')))
 			SET @СписокКодовЛицПотомков = LTRIM(RTRIM(COALESCE(@СписокКодовЛицПотомков, '')))

			-- Обработка и разбивка строки поиска
			SET @СтрокаПоиска = RTRIM(LTRIM(ISNULL(@СтрокаПоиска,'')))
			IF @СтрокаПоиска <> '' 
			BEGIN
				SET @СтрокаПоиска = Инвентаризация.dbo.fn_SplitWords(Инвентаризация.dbo.fn_ReplaceKeySymbols(@СтрокаПоиска))
				SET @СтрокаПоиска = RTRIM(LTRIM(@СтрокаПоиска))
				WHILE CHARINDEX('  ', @СтрокаПоиска) > 0 SET @СтрокаПоиска = REPLACE(@СтрокаПоиска,'  ',' ')

				SET @S = @СтрокаПоиска
				WHILE LEN(@S) > 0 
				BEGIN
				SET @I = CHARINDEX(' ', @S + ' ') 
				SET @W = LEFT(@S, @I-1)	SET @S = SUBSTRING(@S, @I + 1, 100)
				SET @WRL = Инвентаризация.dbo.fn_ReplaceRusLat(@W) 	
				IF (@ГдеИскать = 1) SET @WRL = ' ' + @WRL
				INSERT @СписокСловДляПоиска SELECT @WRL
				END			

				-- Подсчёт слов
				SET @КоличествоСлов = (SELECT COUNT(*) FROM @СписокСловДляПоиска)
			END

			-- Выполним поиск	
			DECLARE @РезультатПоиска TABLE(Код INT, Порядок BIGINT IDENTITY(1,1))
			INSERT INTO @РезультатПоиска(Код)
			SELECT 
				СЛ.КодСвязиЛиц
			FROM Справочники..vwСвязиЛиц СЛ (nolock)
				INNER JOIN vwЛица Р (nolock) ON СЛ.КодЛицаРодителя = Р.КодЛица
				INNER JOIN vwЛица П (nolock) ON СЛ.КодЛицаПотомка = П.КодЛица
			WHERE	(@СтрокаПоиска = '' OR @СтрокаПоиска <> '' AND (SELECT COUNT(*) FROM @СписокСловДляПоиска WHERE 
									                                                            ' '+Р.КличкаRL LIKE '%'+Слово+'%'
									                                                            OR	' '+П.КличкаRL LIKE '%'+Слово+'%'
									                                                            ) = @КоличествоСлов)
				AND	(@СписокКодов = '' OR @СписокКодов <> '' AND СЛ.КодСвязиЛиц IN (SELECT * FROM Инвентаризация..[fn_SplitInts](@СписокКодов)))
				AND	(@СписокКодовТиповСвязейЛиц = '' OR @СписокКодовТиповСвязейЛиц <> '' AND СЛ.КодТипаСвязиЛиц IN (SELECT * FROM Инвентаризация..[fn_SplitInts](@СписокКодовТиповСвязейЛиц)))
				AND	(@СписокКодовЛицРодителей = '' OR @СписокКодовЛицРодителей <> '' AND Р.КодЛица IN (SELECT * FROM Инвентаризация..[fn_SplitInts](@СписокКодовЛицРодителей)))
				AND	(@СписокКодовЛицПотомков = '' OR @СписокКодовЛицПотомков <> '' AND П.КодЛица IN (SELECT * FROM Инвентаризация..[fn_SplitInts](@СписокКодовЛицПотомков)))
	
			-- Вернём результат
			SELECT 
				СЛ.*, Р.Кличка Родитель, П.Кличка Потомок
			FROM @РезультатПоиска РП INNER JOIN Справочники..vwСвязиЛиц СЛ (nolock) on СЛ.КодСвязиЛиц = РП.Код
				INNER JOIN vwЛица Р (nolock) ON СЛ.КодЛицаРодителя = Р.КодЛица
				INNER JOIN vwЛица П (nolock) ON СЛ.КодЛицаПотомка = П.КодЛица
			WHERE	РП.Порядок >= @ВернутьНачинаяСПорядковогоНомера
				AND (@МаксимальноеКоличествоЗаписей = 0 OR	@МаксимальноеКоличествоЗаписей <> 0 
								AND РП.Порядок < @ВернутьНачинаяСПорядковогоНомера+@МаксимальноеКоличествоЗаписей)
			ORDER BY РП.Порядок
		")]
		protected abstract List<TResult> SearchExtended<TResult>(SearchParameters parameters)
			where TResult : PersonLink;

        [SqlQuery(@"
			DECLARE @I int, @S varchar(200), @W varchar(200), @WRL varchar(200)
			DECLARE @СписокСловДляПоиска TABLE(Слово varchar(200)) DECLARE @КоличествоСлов int
	
			SET @МаксимальноеКоличествоЗаписей = ISNULL(@МаксимальноеКоличествоЗаписей, 0)
			IF @МаксимальноеКоличествоЗаписей < 0 SET @МаксимальноеКоличествоЗаписей = 0

			SET @ВернутьНачинаяСПорядковогоНомера = ISNULL(@ВернутьНачинаяСПорядковогоНомера, 1)
			IF @ВернутьНачинаяСПорядковогоНомера <= 0 SET @ВернутьНачинаяСПорядковогоНомера = 1
	
 			SET @СписокКодов = LTRIM(RTRIM(COALESCE(@СписокКодов, '')))
 			SET @СписокКодовТиповСвязейЛиц = LTRIM(RTRIM(COALESCE(@СписокКодовТиповСвязейЛиц, '')))
 			SET @СписокКодовЛицПотомков = LTRIM(RTRIM(COALESCE(@СписокКодовЛицПотомков, '')))

			-- Обработка и разбивка строки поиска
			SET @СтрокаПоиска = RTRIM(LTRIM(ISNULL(@СтрокаПоиска,'')))
			IF @СтрокаПоиска <> '' 
			BEGIN
				SET @СтрокаПоиска = Инвентаризация.dbo.fn_SplitWords(Инвентаризация.dbo.fn_ReplaceKeySymbols(@СтрокаПоиска))
				SET @СтрокаПоиска = RTRIM(LTRIM(@СтрокаПоиска))
				WHILE CHARINDEX('  ', @СтрокаПоиска) > 0 SET @СтрокаПоиска = REPLACE(@СтрокаПоиска,'  ',' ')

				SET @S = @СтрокаПоиска
				WHILE LEN(@S) > 0 
				BEGIN
				SET @I = CHARINDEX(' ', @S + ' ') 
				SET @W = LEFT(@S, @I-1)	SET @S = SUBSTRING(@S, @I + 1, 100)
				SET @WRL = Инвентаризация.dbo.fn_ReplaceRusLat(@W) 	
				IF (@ГдеИскать = 1) SET @WRL = ' ' + @WRL
				INSERT @СписокСловДляПоиска SELECT @WRL
				END			

				-- Подсчёт слов
				SET @КоличествоСлов = (SELECT COUNT(*) FROM @СписокСловДляПоиска)
			END

			-- Выполним поиск	
			DECLARE @РезультатПоиска TABLE(Код INT, Порядок BIGINT IDENTITY(1,1))
			INSERT INTO @РезультатПоиска(Код)
			SELECT 
				СЛ.КодСвязиЛиц
			FROM Справочники..vwСвязиЛиц СЛ (nolock)
				INNER JOIN vwЛица Р (nolock) ON СЛ.КодЛицаРодителя = Р.КодЛица
				INNER JOIN vwЛица П (nolock) ON СЛ.КодЛицаПотомка = П.КодЛица
			WHERE	(@СтрокаПоиска = '' OR @СтрокаПоиска <> '' AND (SELECT COUNT(*) FROM @СписокСловДляПоиска WHERE 
									                                                            ' '+Р.КличкаRL LIKE '%'+Слово+'%'
									                                                            OR	' '+П.КличкаRL LIKE '%'+Слово+'%'
									                                                            ) = @КоличествоСлов)
				AND	(@СписокКодов = '' OR @СписокКодов <> '' AND СЛ.КодСвязиЛиц IN (SELECT * FROM Инвентаризация..[fn_SplitInts](@СписокКодов)))
				AND	(@СписокКодовТиповСвязейЛиц = '' OR @СписокКодовТиповСвязейЛиц <> '' AND СЛ.КодТипаСвязиЛиц IN (SELECT * FROM Инвентаризация..[fn_SplitInts](@СписокКодовТиповСвязейЛиц)))
				AND	(@СписокКодовЛицПотомков = '' OR @СписокКодовЛицПотомков <> '' AND П.КодЛица IN (SELECT * FROM Инвентаризация..[fn_SplitInts](@СписокКодовЛицПотомков)))
	
			-- Вернём результат
			SELECT 
				СЛ.*, Р.Кличка Родитель, П.Кличка Потомок
			FROM @РезультатПоиска РП INNER JOIN Справочники..vwСвязиЛиц СЛ (nolock) on СЛ.КодСвязиЛиц = РП.Код
				INNER JOIN vwЛица Р (nolock) ON СЛ.КодЛицаРодителя = Р.КодЛица
				INNER JOIN vwЛица П (nolock) ON СЛ.КодЛицаПотомка = П.КодЛица
			WHERE	РП.Порядок >= @ВернутьНачинаяСПорядковогоНомера
				AND (@МаксимальноеКоличествоЗаписей = 0 OR	@МаксимальноеКоличествоЗаписей <> 0 
								AND РП.Порядок < @ВернутьНачинаяСПорядковогоНомера+@МаксимальноеКоличествоЗаписей)
			ORDER BY РП.Порядок
		")]
        protected abstract List<TResult> SearchExtendedChildID<TResult>(SearchParameters parameters)
            where TResult : PersonLink;

        [SqlQuery(@"
			DECLARE @I int, @S varchar(200), @W varchar(200), @WRL varchar(200)
			DECLARE @СписокСловДляПоиска TABLE(Слово varchar(200)) DECLARE @КоличествоСлов int
	
			SET @МаксимальноеКоличествоЗаписей = ISNULL(@МаксимальноеКоличествоЗаписей, 0)
			IF @МаксимальноеКоличествоЗаписей < 0 SET @МаксимальноеКоличествоЗаписей = 0

			SET @ВернутьНачинаяСПорядковогоНомера = ISNULL(@ВернутьНачинаяСПорядковогоНомера, 1)
			IF @ВернутьНачинаяСПорядковогоНомера <= 0 SET @ВернутьНачинаяСПорядковогоНомера = 1
	
 			SET @СписокКодов = LTRIM(RTRIM(COALESCE(@СписокКодов, '')))
 			SET @СписокКодовТиповСвязейЛиц = LTRIM(RTRIM(COALESCE(@СписокКодовТиповСвязейЛиц, '')))
 			SET @СписокКодовЛицРодителей = LTRIM(RTRIM(COALESCE(@СписокКодовЛицРодителей, '')))

			-- Обработка и разбивка строки поиска
			SET @СтрокаПоиска = RTRIM(LTRIM(ISNULL(@СтрокаПоиска,'')))
			IF @СтрокаПоиска <> '' 
			BEGIN
				SET @СтрокаПоиска = Инвентаризация.dbo.fn_SplitWords(Инвентаризация.dbo.fn_ReplaceKeySymbols(@СтрокаПоиска))
				SET @СтрокаПоиска = RTRIM(LTRIM(@СтрокаПоиска))
				WHILE CHARINDEX('  ', @СтрокаПоиска) > 0 SET @СтрокаПоиска = REPLACE(@СтрокаПоиска,'  ',' ')

				SET @S = @СтрокаПоиска
				WHILE LEN(@S) > 0 
				BEGIN
				SET @I = CHARINDEX(' ', @S + ' ') 
				SET @W = LEFT(@S, @I-1)	SET @S = SUBSTRING(@S, @I + 1, 100)
				SET @WRL = Инвентаризация.dbo.fn_ReplaceRusLat(@W) 	
				IF (@ГдеИскать = 1) SET @WRL = ' ' + @WRL
				INSERT @СписокСловДляПоиска SELECT @WRL
				END			

				-- Подсчёт слов
				SET @КоличествоСлов = (SELECT COUNT(*) FROM @СписокСловДляПоиска)
			END

			-- Выполним поиск	
			DECLARE @РезультатПоиска TABLE(Код INT, Порядок BIGINT IDENTITY(1,1))
			INSERT INTO @РезультатПоиска(Код)
			SELECT 
				СЛ.КодСвязиЛиц
			FROM Справочники..vwСвязиЛиц СЛ (nolock)
				INNER JOIN vwЛица Р (nolock) ON СЛ.КодЛицаРодителя = Р.КодЛица
				INNER JOIN vwЛица П (nolock) ON СЛ.КодЛицаПотомка = П.КодЛица
			WHERE	(@СтрокаПоиска = '' OR @СтрокаПоиска <> '' AND (SELECT COUNT(*) FROM @СписокСловДляПоиска WHERE 
									                                                            ' '+Р.КличкаRL LIKE '%'+Слово+'%'
									                                                            OR	' '+П.КличкаRL LIKE '%'+Слово+'%'
									                                                            ) = @КоличествоСлов)
				AND	(@СписокКодов = '' OR @СписокКодов <> '' AND СЛ.КодСвязиЛиц IN (SELECT * FROM Инвентаризация..[fn_SplitInts](@СписокКодов)))
				AND	(@СписокКодовТиповСвязейЛиц = '' OR @СписокКодовТиповСвязейЛиц <> '' AND СЛ.КодТипаСвязиЛиц IN (SELECT * FROM Инвентаризация..[fn_SplitInts](@СписокКодовТиповСвязейЛиц)))
				AND	(@СписокКодовЛицРодителей = '' OR @СписокКодовЛицРодителей <> '' AND Р.КодЛица IN (SELECT * FROM Инвентаризация..[fn_SplitInts](@СписокКодовЛицРодителей)))
	
			-- Вернём результат
			SELECT 
				СЛ.*, Р.Кличка Родитель, П.Кличка Потомок
			FROM @РезультатПоиска РП INNER JOIN Справочники..vwСвязиЛиц СЛ (nolock) on СЛ.КодСвязиЛиц = РП.Код
				INNER JOIN vwЛица Р (nolock) ON СЛ.КодЛицаРодителя = Р.КодЛица
				INNER JOIN vwЛица П (nolock) ON СЛ.КодЛицаПотомка = П.КодЛица
			WHERE	РП.Порядок >= @ВернутьНачинаяСПорядковогоНомера
				AND (@МаксимальноеКоличествоЗаписей = 0 OR	@МаксимальноеКоличествоЗаписей <> 0 
								AND РП.Порядок < @ВернутьНачинаяСПорядковогоНомера+@МаксимальноеКоличествоЗаписей)
			ORDER BY РП.Порядок
		")]
        protected abstract List<TResult> SearchExtendedParentID<TResult>(SearchParameters parameters)
            where TResult : PersonLink;

        [SqlQuery("SELECT ТипЛица FROM vwЛица WHERE @personID = КодЛица")]
        public abstract int GetPersonTypeByID(int personID);

        [SqlQuery("SELECT * FROM vwСвязиЛиц WHERE КодЛицаРодителя = @personID")]
		public abstract List<PersonLink> GetPersonChildsByID(string personID);

		[SqlQuery("SELECT * FROM vwСвязиЛиц WHERE КодТипаСвязиЛиц=1 AND @personID IN(КодЛицаРодителя, КодЛицаПотомка)")]
		public abstract List<PersonLink> GetPersonLinksByID(int personID);

		/// <summary>
		/// Добавляет, редактирует или удаляет связь лица
		/// </summary>
		/// <param name="personLinkForSave">Параметры вызова хранимой процедуры</param>
		[SprocName("sp_Лица_InsUpdDel_СвязиЛиц")]
		public abstract void MergePersonLink( PersonLinkForSave personLinkForSave );

		/// <summary>
		/// Сохранение изменений в свзяи лица
		/// </summary>
		/// <param name="instance">модель представления</param>
		public void Save(dynamic instance)
		{
			// мапим переданную модель представления на объектную модель
			PersonLink link = new PersonLink()
			{
				ID = instance.ID,
				PersonLinkTypeID = instance.PersonLinkTypeID,
				From = instance.From,
				To = instance.To,
				ParentPersonID = instance.ParentPersonID,
				ChildPersonID = instance.ChildPersonID,
				Description = instance.Description,
				Parameter = instance.Parameter,
				ChangedBy = instance.ChangedBy,
				ChangedDate = instance.ChangedDate
			};

			if (link.ID == 0)
				Repository.Links.Insert(link);
			else
				Repository.Links.Update(link);
		}
	}
}
