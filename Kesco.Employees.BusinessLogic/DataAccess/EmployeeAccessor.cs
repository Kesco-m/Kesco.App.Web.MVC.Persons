using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using BLToolkit.Common;
using BLToolkit.Data;
using BLToolkit.DataAccess;
using System.Linq;
using Kesco.DataAccess;
using Kesco.Employees.ObjectModel;
using BLToolkit.Mapping;

namespace Kesco.Employees.BusinessLogic.DataAccess
{

	public abstract class EmployeeAccessor : EntityAccessor<EmployeeAccessor, DB, Employee, EmployeeAccessor.SearchParameters, int>
	{
		public class SearchParameters : Kesco.DataAccess.SearchParameters
		{
			public int UserSelectTop { get; set; }
		}

		[SqlQuery(@" 
			/* -- FOR TEST
			DECLARE @keyword varchar(255)
			DECLARE @limit int
			DECLARE @en int

			SET @keyword = '%ФИЛ%'
			SET @limit = 8
			SET @en = 0*/

			DECLARE @Search NVARCHAR(100) 
			SET @Search = COALESCE(@keyword, '')
			DECLARE @Words TABLE(Word nvarchar(100), WordRL nvarchar(100))
			DECLARE @I int, @S nvarchar(100), @W nvarchar(100), @First bit
			SET @Search = Инвентаризация.dbo.fn_SplitWords(Инвентаризация.dbo.fn_ReplaceKeySymbols(@Search))
			SET @Search = RTRIM(LTRIM(@Search))
			WHILE CHARINDEX('  ', @Search) > 0 SET @Search = REPLACE(@Search,'  ',' ')	
			IF @Search <> ''
			BEGIN	
				SET @S = @Search
				SET @First = 1
				WHILE LEN(@S) > 0 
				BEGIN
					SET @I = CHARINDEX(' ', @S + ' ') 
					SET @W = LEFT(@S, @I-1)
					IF @First = 1
						INSERT INTO @Words VALUES(@W+'%', Инвентаризация.dbo.fn_ReplaceRusLat(@W)+'%' )
					ELSE
						INSERT INTO @Words VALUES('% '+@W+'%', Инвентаризация.dbo.fn_ReplaceRusLat(@W)+'%' )
					SET @S = SUBSTRING(@S, @I+1, 100)
					SET @First = 0
				END
			END	

			DECLARE @WC INT
			SELECT @WC = COALESCE(COUNT(*),0) FROM @Words
			PRINT @WC
			SET ROWCOUNT @limit;
			SELECT *
			FROM [Инвентаризация]..[Сотрудники]
			WHERE @WC = 0  or (@WC <> 0 and 
				@WC = (SELECT COUNT(*) FROM @Words 
						WHERE [Сотрудник] LIKE Word or [Employee] LIKE Word
							or [ФамилияRL] LIKE WordRL
							or [ИмяRL] LIKE WordRL
							or [ОтчествоRL] LIKE WordRL
					)
				)
			ORDER BY case @en when 1 then [Employee] else [Сотрудник] end;

			SET ROWCOUNT 0;
		")]
		public abstract List<Employee> SearchEmployees(string @keyword, int @limit, int @en);

		public override List<Employee>  Search(SearchParameters criteria)
		{
 			return SearchEmployees(criteria.Search, criteria.UserSelectTop,
				(Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName == "en")?1:0);
		}

		[SqlQuery(@"
			SELECT * FROM Сотрудники
			WHERE КодСотрудника IN( SELECT value FROM Инвентаризация.dbo.fn_SplitInts(@ids) )
			ORDER BY Сотрудник")]
		public abstract List<Employee> GetListByIds(string ids);


        [SqlQuery(@"
			SELECT * FROM Сотрудники
			WHERE КодОбщегоСотрудника = @id AND КодСотрудника != @id
			ORDER BY Сотрудник")]
        public abstract List<ObjectModel.Employee> GetListCommonEmployeesByIds(string id);

		public IDictionary<int, Employee> GetDictionary(string ids)
		{
			using (DbManager db = CreateDbManager())
			{
				Dictionary<int, Employee> dictionary = new Dictionary<int, Employee>();
				return db
					.SetCommand(@"SELECT * FROM Сотрудники
						WHERE КодСотрудника IN( SELECT value FROM Инвентаризация.dbo.fn_SplitInts(@ids) )
						ORDER BY Сотрудник",
						db.Parameter("ids", ids))
					.ExecuteDictionary<int, Employee>(dictionary, new NameOrIndexParameter("ID"), typeof(Employee));
			}
		}

		/// <summary>
		/// Возвращает список контактов сотрудника.
		/// </summary>
		/// <param name="КодСотрудника">The КОД СОТРУДНИКА.</param>
		/// <param name="КодТелефоннойСтанцииЗвонящего">The КОД ТЕЛЕФОННОЙ СТАНЦИИ ЗВОНЯЩЕГО.</param>
		/// <returns>Список контактов сотрудника</returns>
		[SprocName("sp_Сотрудники_Контакты")]
		public abstract List<EmployeeContact> GetEmployeeContacts(int @КодСотрудника, int? @КодТелефоннойСтанцииЗвонящего);

		/// <summary>
		/// Получение привязанных к сотруднику SIM карт
		/// </summary>
		/// <param name="КодСотрудника">код сотрудника</param>
		/// <returns>Список мобильных номеров сотрудника</returns>
		[SqlQuery(@"SELECT Sim.НомерТелефона, Sim.Изменено
FROM vwSimКарты Sim
	INNER JOIN ОборудованиеСотрудников Ob ON Sim.КодОборудования = Ob.КодОборудования
WHERE Ob.До IS NULL AND Ob.КодСотрудника = @КодСотрудника
ORDER BY Sim.Изменено DESC")]
		public abstract List<EmployeeSIM> GetEmployeeSimCards(int @КодСотрудника);

		[SqlQuery(@"SELECT * FROM Сотрудники WHERE КодЛица = @КодЛица")]
		public abstract Employee GetEmployeeByPersonID(int @КодЛица);
		
		[SqlQuery(@"
			SELECT Считыватель, ПоследнийПроход
			FROM Инвентаризация.dbo.ПоследнийПроходСотрудников 
			WHERE КодСотрудника = @КодСотрудника")]
		public abstract EmployeePassage GetEmployeeLastPassage(int @КодСотрудника);

		/// <summary>
		/// Determines whether [has roles for B project].
		/// </summary>
		/// <returns></returns>
        [SqlQuery(@"select КодРоли from fn_ТекущиеРоли() where КодРоли in (71,72,73,74,75,76)")]
        public abstract List<int> HasRolesForBProject();

        /// <summary>
        /// Текущий пользователь - секретарь или администратор лиц
        /// </summary>
        /// <returns></returns>
        [SqlQuery(@"select КодРоли from fn_ТекущиеРоли() where КодРоли in (11,12)")]
        public abstract List<int> HasRolesForPersonAdministration();

		/// <summary>
		/// Текущий пользватель имеет права пересаживать сотрудника
		/// </summary>
		/// <param name="КодСотрудника">код сотрудника, расположение которого меняется</param>
		/// <returns>1 - достаточно прав</returns>
		[SqlQuery(@"SELECT 1 FROM Инвентаризация.dbo.fn_ТекущиеРоли() WHERE КодРоли IN (31,43,32) AND (КодЛица =0 OR КодЛица IN (SELECT КодЛица FROM fn_КомпанииСотрудника(@КодСотрудника)))")]
		public abstract string HasRightsToChangeUserWorkPlace(int КодСотрудника);

		/// <summary>
		/// Текущие роли пользователя по указанному лицу
		/// </summary>
		/// <returns>список идентификаторов ролей</returns>
		[SqlQuery(@"select КодРоли from fn_ТекущиеРоли() where КодЛица in (0, @КодЛица)")]
		public abstract List<int> CurrentRoles(int @КодЛица);

		[SqlQuery(@"SELECT * FROM fn_ТекущийСотрудник()")]
		public abstract List<int> CurentEmployees();

		[SqlQuery(@"
			SELECT L.*
			FROM Инвентаризация..Сотрудники S
				INNER JOIN ЛицаЗаказчики L ON L.КодЛица = S.КодЛицаЗаказчика
			WHERE S.КодСотрудника = @КодСотрудника
		")]
		public abstract EmployeeCustomer GetEmployeeCustomer(int @КодСотрудника);

        [SqlQuery(@"SELECT * FROM Инвентаризация..Сотрудники WHERE КодСотрудника = dbo.fn_НепосредственныйРуководитель(@КодСотрудника)")]
        public abstract Employee GetEmployeeSupervisor(int @КодСотрудника);

		[SqlQuery(@"
			SELECT 
				Pst.КодЛица, ISNULL(NULLIF (L.КраткоеНазваниеРус, ''), L.КраткоеНазваниеЛат) as  Организация, Подразделение, Должность, Совместитель
			FROM Инвентаризация..vwДолжности Pst
				LEFT JOIN ЛицаЗаказчики L ON Pst.КодЛица = L.КодЛица
			WHERE КодСотрудника = @КодСотрудника
			ORDER BY Совместитель 
		")]
		public abstract List<EmployeePosition> GetEmployeePositions(int @КодСотрудника);

		[SqlQuery(@"
			SELECT 
				РабочиеМеста.КодРасположения, dbo.fn_Tree_Расположения_FullPath(РабочиеМеста.КодРасположения, 1) AS РасположениеPath, РабочееМесто,
                ISNULL(orm.КодРасположения,0) ОрганизованноеРабочееМесто
			FROM РабочиеМеста
				INNER JOIN vwРасположения ON РабочиеМеста.КодРасположения = vwРасположения.КодРасположения
                LEFT JOIN vwРасположенияОрганизованыРабочиеМеста orm ON orm.КодРасположения =  РабочиеМеста.КодРасположения
            WHERE КодСотрудника = @КодСотрудника
		")]
		public abstract List<EmployeeWorkPlace> GetEmployeeWorkPlaces(int @КодСотрудника);

		[SqlQuery(@"
			IF NOT EXISTS(SELECT * FROM vwРасположения WHERE КодРасположения = @КодРасположения) 
			BEGIN
				RAISERROR  (N'Указанное расположение в базе данных отсутствует - %s.', 12, 1, @КодРасположения);
				RETURN
			END

			IF NOT EXISTS(SELECT * FROM РабочиеМеста WHERE КодСотрудника = @КодСотрудника AND КодРасположения = @КодРасположения)
			INSERT INTO РабочиеМеста (КодСотрудника, КодРасположения) VALUES (@КодСотрудника, @КодРасположения)
		")]
		public abstract void SaveEmployeeWorkPlaces(
				int @КодСотрудника,
				int @КодРасположения
			);


        [SqlQuery(@"
			SELECT DISTINCT 
                РедактируемыйСотрудник.КодСотрудника, 
                РедактируемыйСотрудник.КодОбщегоСотрудника, 
                РабочееМесто.КодРасположения,
                РасположениеPath1 Расположение,
                ИнформацияОбщиеСотрудники.КодСотрудника КодОбщегоСотрудникаНаРабочемМесте,
                ИнформацияОбщиеСотрудники.ФИО ФИООбщегоСотрудника
            FROM [Инвентаризация].[dbo].[Сотрудники] РедактируемыйСотрудник 
                LEFT JOIN [Инвентаризация].[dbo].[РабочиеМеста] РабочееМесто
	            ON РабочееМесто.КодРасположения = @КодРасположения
                LEFT JOIN [Инвентаризация].[dbo].[Сотрудники] ОбщиеСотрудники
	            ON РабочееМесто.КодСотрудника = ОбщиеСотрудники.КодОбщегоСотрудника
                LEFT JOIN [Инвентаризация].[dbo].[Сотрудники] ИнформацияОбщиеСотрудники
	            ON ОбщиеСотрудники.КодОбщегоСотрудника = ИнформацияОбщиеСотрудники.КодСотрудника
                LEFT JOIN vwРасположения ON vwРасположения.КодРасположения = РабочееМесто.КодРасположения
            WHERE РедактируемыйСотрудник.КодСотрудника = @КодСотрудника
		")]
        public abstract List<EmployeeWorkPlaceChangeCheck> CheckCommonEmployeesWhilChangeWorkPlace(
                int @КодСотрудника,
                int @КодРасположения
            );

        [SqlQuery(@"
			  SELECT КодСотрудника, КодРасположения FROM [Инвентаризация].[dbo].[РабочиеМеста] WHERE КодСотрудника = @КодСотрудника
		")]
        public abstract List<EmployeeWorkPlaceWithEmployee> GetEmployeeWorkPlace(
                int @КодСотрудника
            );

        [SqlQuery(@"
			SELECT s.КодСотрудника,s.Сотрудник,s.Employee 
			FROM РабочиеМеста r 
				INNER JOIN Сотрудники s ON s.КодСотрудника = r.КодСотрудника 
			WHERE r.КодРасположения = @КодРасположения AND s.Состояние = 0  AND КодЛица IS NOT NULL
		")]
        public abstract List<EmployeeCoWorker> GetWorkPlaceEmployees(
                int @КодРасположения
            );

        [SqlQuery(@"
			  SELECT КодСотрудника, КодРасположения FROM [Инвентаризация].[dbo].[РабочиеМеста] WHERE КодСотрудника = @КодСотрудника
              AND КодРасположения IN (SELECT КодРасположения FROM [Инвентаризация].[dbo].[РабочиеМеста] WHERE КодСотрудника = @КодОбщегоСотрудника)
              AND КодРасположения != @КодРасположения
		")]
        public abstract List<EmployeeWorkPlaceWithEmployee> CheckCommonEmployeesWhilChangeWorkPlaceWithCommonEmployee(
                int @КодСотрудника,
                int @КодРасположения,
                string @КодОбщегоСотрудника
            );


        [SqlQuery(@"
			UPDATE ЗамещенияСотрудников
			SET До = CASE WHEN От > getdate() THEN ОТ ELSE FLOOR(CONVERT(float, getdate())) END
			WHERE КодЗамещенияСотрудников = @КодЗамеещения
		")]
        public abstract void DeleteAllEmployeeReplacements(
                string @КодЗамеещения
            );

		[SqlQuery(@"
			SELECT s.КодСотрудника,s.Сотрудник,s.Employee 
			FROM РабочиеМеста r 
				INNER JOIN Сотрудники s ON s.КодСотрудника = r.КодСотрудника 
			WHERE r.КодРасположения = @КодРасположения AND r.КодСотрудника <> @КодСотрудника AND s.Состояние = 0  AND КодЛица IS NOT NULL
			ORDER BY s.Сотрудник
		")]
		public abstract List<EmployeeCoWorker> GetEmployeeCoWorkersByWorkPlace(
				int @КодСотрудника,
				int @КодРасположения
			);

        [SqlQuery(@"
            SELECT s.КодСотрудника, s.ФИО Сотрудник, s.FIO Employee, s.КодЛица, s.Состояние, vwРасположения.РабочееМесто
            FROM РабочиеМеста r 
                INNER JOIN Сотрудники s ON s.КодСотрудника = r.КодСотрудника
                INNER JOIN vwРасположения ON vwРасположения.КодРасположения = r.КодРасположения
            WHERE r.КодРасположения = @КодРасположения AND r.КодСотрудника <> @КодСотрудника AND s.Состояние = 0 
            ORDER BY s.Сотрудник
		")]
        public abstract List<EmployeeCoWorker> GetEmployeeCoWorkersByOfficeWorkPlace(
                int @КодСотрудника,
                int @КодРасположения
            );

		/// <summary>
		/// Возвращает значение, является ли сотрудник администратором лица.
		/// </summary>
		/// <param name="КодСотрудника">КОД СОТРУДНИКА.</param>
		/// <returns></returns>
		[SqlQuery(@"
			DECLARE @ЯвляетсяАдминистраторомЛица bit SET @ЯвляетсяАдминистраторомЛица = 0
			
			SELECT @ЯвляетсяАдминистраторомЛица = 1 FROM Инвентаризация..fn_ТекущиеРоли() 
			WHERE	КодРоли IN (31,43,32) 
				AND (КодЛица =0 OR КодЛица IN (SELECT КодЛица FROM fn_КомпанииСотрудника(@КодСотрудника)))
			
			SELECT @ЯвляетсяАдминистраторомЛица ЯвляетсяАдминистраторомЛица
		")]
		public abstract bool BelongsToPersonAdministators(int @КодСотрудника);

        /// <summary>
        /// Возвращает значение, может ли сотрудник иметь доступ к карточкам сотрудников.
        /// </summary>
        /// <param name="КодСотрудника">КОД СОТРУДНИКА.</param>
        /// <param name="СписокРолей">Список ролей, имеющий доступ.</param>
        /// <returns></returns>
        [SqlQuery(@"
			DECLARE @ИмеетДоступККарточкам bit SET @ИмеетДоступККарточкам = 0
			
            SELECT @ИмеетДоступККарточкам = 1 FROM Инвентаризация..fn_ТекущиеРоли() 
            WHERE	КодРоли IN (SELECT value FROM dbo.fn_SplitInts(@СписокРолей)) 
	            AND (КодЛица=0 OR КодЛица IN (SELECT КодЛица FROM fn_КомпанииСотрудника(@КодСотрудника)))
			
            SELECT @ИмеетДоступККарточкам ИмеетДоступККарточкам
		")]
        public abstract bool BelongsToPersonCard(int @КодСотрудника, string @СписокРолей);

		/// <summary>
		/// Возвращает фотографии сотрудника.
		/// </summary>
		/// <param name="кодСотрудника">The КОД СОТРУДНИКА.</param>
		/// <param name="кодФотографииСотрудника">The КОД ФОТОГРАФИИ СОТРУДНИКА.</param>
		/// <param name="вернутьМиниФотографию">Если установлено в <c>true</c> [ВЕРНУТЬ МИНИ ФОТОГРАФИЮ].</param>
		/// <returns>Фотографии сотрудника</returns>
		[SqlQuery(@"
			IF (@ВернутьМиниФотографию = 1)
				SELECT TOP 1 Фотография as Фотография, Изменено 
				FROM Инвентаризация..ThumbnailPhotos
				WHERE КодСотрудника = @КодСотрудника OR КодСотрудника = @КодФотографииСотрудника
			ELSE										
				SELECT TOP 1 Фотография, Изменено 
				FROM Инвентаризация..ФотографииСотрудников
				WHERE	(@КодСотрудника IS NULL OR КодСотрудника = @КодСотрудника)
					AND	(@КодФотографииСотрудника IS NULL OR КодФотографииСотрудника = @КодФотографииСотрудника)
				ORDER BY КодФотографииСотрудника DESC			
		")]
		public abstract EmployeePhoto GetEmployeePhoto(int? @кодСотрудника, int? @кодФотографииСотрудника, bool @вернутьМиниФотографию);


		[SqlQuery(@"
			SELECT case when EXISTS(select TOP 1 КодСотрудника from Инвентаризация..ФотографииСотрудников where КодСотрудника = @КодСотрудника) then 1 else 0 END"
			)]
		public abstract bool EmployeeHasPhoto(int? @КодСотрудника);

		public class EmployeeSupervisor
		{
			public int МояОрганизация { get; set; }
			public int МойКодСотрудника {  get; set; }
			public int МойКодЛица { get; set; }
			public string Я { get; set; }
			public string МояДолжность { get; set; }
			public int КодСотрудникаРуководителя { get; set; }
			public int КодЛицаРуководителя { get; set; }
			public string Руководитель { get; set; }
			public string ДолжностьРуководителя { get; set; }
		}

		/// <summary>
		/// Возвращает информацию по руководителю сотрудника по коду сотрудника
		/// </summary>
		/// <param name="emplId">код сотрудника</param>
		/// <returns></returns>
		[SqlQuery(@"
SELECT МояДолжн.КодЛица МояОрганизация, Я.КодСотрудника AS МойКодСотрудника, Я.КодЛица МойКодЛица, Я.Сотрудник AS Я, МояДолжн.Должность AS МояДолжность,
	Рук.КодСотрудника КодСотрудникаРуководителя, Рук.КодЛица КодЛицаРуководителя, Рук.ФИО AS Руководитель, РукДолжн.Должность AS ДолжностьРуководителя
FROM Сотрудники Я
	INNER JOIN vwДолжности МояДолжн ON Я.КодСотрудника = МояДолжн.КодСотрудника
	INNER JOIN vwДолжности РукДолжн ON МояДолжн.Parent = РукДолжн.КодДолжности
	LEFT OUTER JOIN Сотрудники Рук ON РукДолжн.КодСотрудника = Рук.КодСотрудника
WHERE Я.КодСотрудника = @emplId AND (МояДолжн.Совместитель = 0)"
			)]
		public abstract EmployeeSupervisor GetSuperVisorEmployee(int emplId);

		/// <summary>
		/// Возвращает информацию по руководителю сотрудника по коду сотрудника и коду лица
		/// </summary>
		/// <param name="emplId">код сотрудника</param>
		/// <param name="employerId">код лица</param>
		/// <returns></returns>
		[SqlQuery(@"
SELECT МояДолжн.КодЛица МояОрганизация, Я.КодСотрудника AS МойКодСотрудника, Я.КодЛица МойКодЛица, Я.Сотрудник AS Я, МояДолжн.Должность AS МояДолжность,
	Рук.КодСотрудника КодСотрудникаРуководителя, Рук.КодЛица КодЛицаРуководителя, Рук.ФИО AS Руководитель, РукДолжн.Должность AS ДолжностьРуководителя
FROM Сотрудники Я
	INNER JOIN vwДолжности МояДолжн ON Я.КодСотрудника = МояДолжн.КодСотрудника
	INNER JOIN vwДолжности РукДолжн ON МояДолжн.Parent = РукДолжн.КодДолжности
	LEFT OUTER JOIN Сотрудники Рук ON РукДолжн.КодСотрудника = Рук.КодСотрудника
WHERE Я.КодСотрудника = @emplId AND (МояДолжн.КодЛица = @employerId)"
			)]
		public abstract EmployeeSupervisor GetSuperVisorEmployeeByOrg(int emplId, int employerId);

		[SqlQuery(
			@"SELECT Child.КодДолжности
		FROM	dbo.vwДолжности Parent
				INNER JOIN dbo.vwДолжности Child ON Parent.L <= Child.L AND Parent.R >= Child.R
		WHERE 	(Child.КодСотрудника=@employeeId AND Parent.КодСотрудника=@managerId) OR
			(Child.КодСотрудника=@employeeId AND
				(EXISTS(SELECT *
					FROM dbo.ПодчинениеАдминистративное Chief
						INNER JOIN dbo.ПодчинениеАдминистративное Slave ON Chief.L <= Slave.L AND Chief.R >= Slave.R
						INNER JOIN dbo.vwДолжности D ON Chief.КодДолжности = D.КодДолжности
					WHERE Slave.КодДолжности=Parent.КодДолжности AND D.КодСотрудника=@managerId
					) OR
				EXISTS(SELECT *
					FROM dbo.ПодчинениеФинансовое Chief
						INNER JOIN dbo.ПодчинениеФинансовое Slave ON Chief.L <= Slave.L AND Chief.R >= Slave.R
						INNER JOIN dbo.vwДолжности D ON Chief.КодДолжности = D.КодДолжности
					WHERE Slave.КодДолжности=Parent.КодДолжности AND D.КодСотрудника=@managerId
					) OR
				EXISTS(SELECT *
					FROM dbo.ПодчинениеТехническое Chief
						INNER JOIN dbo.ПодчинениеТехническое Slave ON Chief.L <= Slave.L AND Chief.R >= Slave.R
						INNER JOIN dbo.vwДолжности D ON Chief.КодДолжности = D.КодДолжности
					WHERE Slave.КодДолжности=Parent.КодДолжности AND D.КодСотрудника=@managerId
					) OR
				EXISTS(SELECT *
					FROM dbo.ПодчинениеЮридическое Chief
						INNER JOIN dbo.ПодчинениеЮридическое Slave ON Chief.L <= Slave.L AND Chief.R >= Slave.R
						INNER JOIN dbo.vwДолжности D ON Chief.КодДолжности = D.КодДолжности
					WHERE Slave.КодДолжности=Parent.КодДолжности AND D.КодСотрудника=@managerId
					)
				)
			)"
			)]
		public abstract List<int> GetSubordination(int employeeId, int managerId);

        [SqlQuery(@"
			SELECT
	        КодЗамещенияСотрудников,
	        До,
	        КодСотрудникаЗамещаемого,
	        Замещённый.ФИО AS Замещённый,
	        КодСотрудникаЗамещающего,
	        ИспОбязанности.ФИО AS ИспОбязанности,
	        T.Примечания,
	        Изменил.ФИО AS Изменил,
	        T.Изменено
                FROM dbo.ЗамещенияСотрудников T
	                INNER JOIN dbo.Сотрудники Замещённый ON T.КодСотрудникаЗамещаемого = Замещённый.КодСотрудника
	                INNER JOIN dbo.Сотрудники ИспОбязанности ON T.КодСотрудникаЗамещающего = ИспОбязанности.КодСотрудника
	                INNER JOIN dbo.Сотрудники Изменил ON T.Изменил = Изменил.КодСотрудника
                WHERE (КодСотрудникаЗамещаемого = @ids) AND До > getdate() AND До<>ОТ
        ")]
        public abstract List<EmployeeReplacement> GetEmployeeReplacementById(int ids);

        [SqlQuery(@"
			SELECT
	        КодЗамещенияСотрудников,
	        До,
	        КодСотрудникаЗамещаемого,
	        Замещённый.ФИО AS Замещённый,
	        КодСотрудникаЗамещающего,
	        ИспОбязанности.ФИО AS ИспОбязанности,
	        T.Примечания,
	        Изменил.ФИО AS Изменил,
	        T.Изменено
                FROM dbo.ЗамещенияСотрудников T
	                INNER JOIN dbo.Сотрудники Замещённый ON T.КодСотрудникаЗамещаемого = Замещённый.КодСотрудника
	                INNER JOIN dbo.Сотрудники ИспОбязанности ON T.КодСотрудникаЗамещающего = ИспОбязанности.КодСотрудника
	                INNER JOIN dbo.Сотрудники Изменил ON T.Изменил = Изменил.КодСотрудника
                WHERE (КодСотрудникаЗамещающего = @ids) AND До > getdate() AND До<>ОТ
        ")]
        public abstract List<EmployeeReplacement> GetEmployeeRepresentativeById(int ids);

		[SqlQuery(
			@"DECLARE @LName varchar(50), @FName varchar(50), @MName varchar(50), @Login varchar(50), @F tinyint

SELECT @LName=LastName, @FName=FirstName, @MName=MiddleName FROM Сотрудники WHERE КодСотрудника=@КодСотрудника

SET @Login=@LName
SET @F=0

WHILE EXISTS(SELECT * FROM Сотрудники WHERE Login=@Login AND КодСотрудника !=@КодСотрудника)
BEGIN
	SET @Login= LEFT(@FName,1)+ CASE WHEN @F=1 THEN LEFT(@MName,1) ELSE '' END +  @LName
	SET @F=1
	IF @F>1 BREAK
END

SELECT LOWER(@Login) Login"
			)]
		public abstract string GetEmptyLogin(int КодСотрудника);
	}
}
