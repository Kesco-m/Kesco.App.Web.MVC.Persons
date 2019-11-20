using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLToolkit.Reflection;
using Kesco;
using Kesco.Web.Mvc;

namespace Kesco.Web.Mvc.UI.ComponentModel.DataAnnotations
{

	/// <summary>
	/// Класс-атрибут, указывающий для свойства модели, 
	/// что он будет представлен элементом управления <see cref="KescoSelect" />.
	/// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple=false)]
	public class KescoSelectAttribute : UIHintAttribute, IMetadataAware
	{
        /// <summary>
        /// Указывает ключ для дескриптора элемента управления SelectBox 
        /// в словаре AdditionalValues из метаданных
        /// </summary>
        public const string AdditionalValuesKey_SelectBox = "KescoSelect";

		/// <summary>
		/// Указывает ключ для параметров поиска, установленных для элемента элемента управления SelectBox 
		/// в словаре AdditionalValues из метаданных
		/// </summary>
		public const string AdditionalValuesKey_SelectBoxSearchParameters = "KescoSelectSearchParameters";

        /// <summary>
        /// Указывает ключ для массива команд, которые должнен поддерживать 
        /// элемент управления SelectBox в словаре AdditionalValues из метаданных
        /// </summary>
		public const string AdditionalValuesKey_SelectBoxCommands = "KescoSelectCommands";

        /// <summary>
        /// Идентификатор скрипта, который инициализирует 
        /// тултип для элемента управления SELECT
        /// </summary>
		public const string ScriptTemplate_InitSelectBoxToolTip_ScriptID = "initSelectBoxToolTip";
        
        /// <summary>
        /// Cкрипт, который инициализирует 
        /// тултип для элемента управления SELECT
        /// </summary>
		public const string ScriptTemplate_InitSelectBoxToolTip = @"
			function initSelectBoxToolTip(controlSelector, url) {{
				$(controlSelector).selectBox('getInput').initToolTip(function() {{
					var uri = ''+url;
					var item = $(controlSelector).selectBox('getValue');
					if (item && item.value)
						uri = url.replace('/0', '/' + item.value);
					else return '';
					return uri;
				}}, $(controlSelector).parent());
			}}
		";

		/// <summary>
		/// Возвращает или устанавливает CLID (идентификатор клиента)
		/// для расширенного поиска лица
		/// </summary>
		/// <value>
		/// CLID (идентификатор клиента)
		/// </value>
		public int CLID { get; set; }

		/// <summary>
		/// Возвращает или устанавливает проводник данных.
		/// </summary>
		/// <value>
		/// Проводник данных.
		/// </value>
		public Type EntityAccessorType { get; set; }

		/// <summary>
		/// Возвращает или устанавливает тип SELECT-контроллера.
		/// </summary>
		/// <value>
		///	SELECT-контроллера.
		/// </value>
		public Type SelectControllerType { get; set; }

		/// <summary>
		/// Возвращает или устанавливает имя ключевого поля 
        /// из набора записей, предложенных для выбора.
		/// </summary>
		/// <value>
		///		Имя ключевого поля из набора записей, предложенных для выбора.
		/// </value>
        public string KeyField { get; set; }

		/// <summary>
		/// Возвращает или устанавливает имя отображаемого поля 
        /// из набора записей, предложенных для выбора.
		/// </summary>
		/// <value>
		/// Имя отображаемого поля из набора записей, предложенных для выбора.
		/// </value>
        public string DisplayField { get; set; }

		/// <summary>
		/// Возвращает или устанавливает имя контроллера,
		/// который обслуживает элемент управления SELECT.
		/// </summary>
		/// <value>
		/// Имя контроллера
		/// </value>
		public string AutocompleteController { get; set; }

        /// <summary>
        /// Возвращает или устанавливает действие на контроллере,
        /// которое возвращает список автозавершения.
        /// </summary>
        /// <value>
		/// Действие на контроллере,
		/// которое возвращает список автозавершения.
        /// </value>
        public string AutocompleteAction { get; set; }

        /// <summary>
        /// Возвращает или устанавливает максимальное количество
        /// записей, соответствующих критерию поиска.
        /// </summary>
        /// <value>
        /// Максимальное количество записей, соответствующих критерию поиска.
        /// </value>
        public int AutocompleteLimit { get; set; }

        /// <summary>
		/// Возвращает или устанавливает признак,
		/// следует ли показать кнопку поиска или нет
		/// </summary>
		/// <value>
		/// Показать кнопку поиска или нет
		/// </value>
		public bool ShowSearchButton { get; set; }

		/// <summary>
		/// Возвращает или устанавливает признак,
		/// следует ли показать кнопку просмотра или нет
		/// </summary>
		/// <value>
		/// Показать кнопку просмотра или нет
		/// </value>
		public bool ShowViewButton { get; set; }

		/// <summary>
		/// Возвращает или устанавливает признак,
		/// является ли поле обязательным для заполнения или нет.
        /// В случае, если обязательно, полю добавляется класс
        /// jqueryui: ui-state-highlight
		/// </summary>
		/// <value>
		/// Является ли поле обязательным для заполнения или нет
		/// </value>
		public bool IsRequired { get; set; }

		/// <summary>
		/// Возвращает или устанавливает имя клиентской функции возврата,
		/// выполняющая форматирование элемента списка совпадений.
		/// </summary>
		/// <value>
		/// Имя клиентской функции возврата
		/// </value>
		public string OnFormatItemClientFunction { get; set; }

		/// <summary>
		/// Возвращает или устанавливает имя клиентской функции возврата,
		/// выполняющаяся перед отправкой запроса на сервер запроса.
		/// </summary>
		/// <remarks> 
		/// Данная функция может быть использована для внесения 
		/// дополнительных параметров в запрос на сервер
		/// </remarks>
		/// <value>
		/// Имя клиентской функции возврата
		/// </value>
		public string OnRequestClientFunction { get; set; }

		/// <summary>
		/// Иницилизирует новый экземпляр <see cref="KescoSelectAttribute" /> класса.
        /// </summary>
        /// <param name="uiHint">Название шаблона для отрисовки элемента управления</param>
		public KescoSelectAttribute(string uiHint)
			: base(uiHint, null)
		{
			CLID = 0;
            ShowSearchButton = true;
            ShowViewButton = true;
            IsRequired = false;
		}

        /// <summary>
        /// Иницилизирует новый экземпляр  <see cref="KescoSelectAttribute" /> класса
        /// с UIHint установленным в SelectBox.
        /// </summary>
        public KescoSelectAttribute() : this("SelectBox") {}

        /// <summary>
		/// Метод позволяет убедиться, 
		/// что сценарии функции инициализации подсказки 
		/// для элемента управления SelectBox
		/// будет выведен на страницу.
		/// </summary>
		public static void EnsureScriptsReadyForRender()
		{
			HttpContextBase сontext = new HttpContextWrapper(HttpContext.Current);
			// Добавляем скрипт функции инициализации подсказки для элемента управления SelectBox
			сontext.RegisterCommonScript(
					ScriptTemplate_InitSelectBoxToolTip_ScriptID, 
					() => String.Format(ScriptTemplate_InitSelectBoxToolTip, String.Empty)
				);
		}

		#region Члены IMetadataAware

		/// <summary>
		/// При реализации в классе предоставляет метаданные для процесса создания метаданных модели.
		/// </summary>
		/// <param name="metadata">Метаданные модели.</param>
		public virtual void OnMetadataCreated(ModelMetadata metadata)
		{
			var copy = TypeAccessor.Copy(this);
            metadata.AdditionalValues.Add(AdditionalValuesKey_SelectBox, copy);

			var searchParams = metadata.ContainerType.GetMetadataAttribute<KescoSelectSearchParametersAttribute>(metadata.PropertyName);

			if (searchParams != null)
			{
				metadata.AdditionalValues.Add(AdditionalValuesKey_SelectBoxSearchParameters, searchParams);
			}
			// Добавляем команды, определённые через атрибуты 
			// в качестве дополнительных значений для элемента управления
			AddCommands(metadata, metadata.ContainerType.GetProperty(metadata.PropertyName)
					.GetCustomAttributes(typeof(KescoSelectCommandAttribute), true) as KescoSelectCommandAttribute[]);

            EnsureScriptsReadyForRender();
            
		}

		/// <summary>
		/// Добавляет команды для элемента управления <see cref="Kesco.Web.Mvc.UI.KescoSelect"/>.
		/// </summary>
		/// <param name="metadata">Метаданные.</param>
		/// <param name="commandAttributes">Атрибуты, описывающие команды.</param>
		protected virtual void AddCommands(ModelMetadata metadata, params KescoSelectCommandAttribute[] commandAttributes)
		{
			// Объединим команды, если уже некоторые установлены
			if (metadata.AdditionalValues.ContainsKey(AdditionalValuesKey_SelectBoxCommands))
			{
				var existing = metadata.AdditionalValues[AdditionalValuesKey_SelectBoxCommands] as KescoSelectCommandAttribute[];
				metadata.AdditionalValues.Remove(AdditionalValuesKey_SelectBoxCommands);
				if (existing != null)
				{
					commandAttributes = commandAttributes.Concat(existing.AsEnumerable()).ToArray();
				}
			}

			metadata.AdditionalValues.Add(AdditionalValuesKey_SelectBoxCommands, commandAttributes);
		}

		#endregion
	}
}