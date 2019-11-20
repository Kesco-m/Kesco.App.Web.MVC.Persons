using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Kesco.Web.Mvc;
using Kesco.Web.Mvc.SharedViews.ComponentModel;
using Kesco.Web.Mvc.UI.ComponentModel.DataAnnotations;

namespace Kesco.Web.Mvc.SharedViews.ComponentModel
{
	/// <summary>
	/// Атрибут определяет элемент управления Лицо 
	/// и свойства его отбражения
	/// </summary>
	public class EmployeeControlAttribute : KescoSelectAttribute
	{

		/// <summary>
		/// Представляет скрипт диспетчеризации команд для 
		/// элемента управления выбора лица, - SELECT
		/// {0} - callback Url
		/// {1} - SEARCH Url
		/// {2} - VIEW Url
		/// </summary>
		public const string DispatchCommandEmployeeControlScriptTemplate = @"
			function dispatchCommand_EmployeeControl(controlID, command) {{
				var $lookup = $('#'+controlID);
				var url; 
				var callbackUrl = encodeURIComponent('{0}');
				var item = $lookup.selectBox('getValue');
				switch(command) {{
					case 'advSearch':
						if (window.console) console.log('advSearch', item);
						url = '{1}';
						url = $.validator.format(url, callbackUrl, encodeURIComponent(item.label));
                        KescoLookup_AdvSearch('#'+controlID, url);
						return false;
						break;
					case 'view':
						ViewModel.showUser(item.value);
						break;
					default:
						break;
				}}
			}}	
		";

		/// <summary>
		/// Возвращает текущий HTTP-контекст
		/// </summary>
		internal HttpContextBase Context
		{
			get { return new HttpContextWrapper(HttpContext.Current); }
		}

		/// <summary>
		/// Возвращает идентификатор скрипта в элементах контекста (Context.Items["Scripts"])
		/// </summary>
		/// <value>
		/// The script ID.
		/// </value>
		public virtual string ScriptID
		{
			get { return "EmployeeSelectControl"; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="EmployeeControlAttribute"/> class.
		/// </summary>
		public EmployeeControlAttribute()
			: base("Employee")
		{
			CLID = 0;
		}

		#region Члены IMetadataAware

		/// <summary>
		/// При реализации в классе предоставляет метаданные для процесса создания метаданных модели.
		/// </summary>
		/// <param name="metadata">Метаданные модели.</param>
		public override void OnMetadataCreated(ModelMetadata metadata)
		{
			base.OnMetadataCreated(metadata);

			// Добавляем скрипт форматирования элемента
			Context.RegisterCommonScript(ScriptID, GetScript(metadata));
		}

		#endregion

		/// <summary>
		/// Возвращает скрипт для элемента управления Employee
		/// </summary>
		/// <param name="metadata">Метаданные модели.</param>
		/// <returns>скрипт для элемента управления Employee</returns>
		public virtual string GetScript(ModelMetadata metadata)
		{
			RequestContext requestContext = (HttpContext.Current.CurrentHandler as MvcHandler).RequestContext;
			
			UrlHelper urlHelper = new UrlHelper(requestContext);
			return string.Format(CultureInfo.InvariantCulture, DispatchCommandEmployeeControlScriptTemplate,
					// callback Url
					urlHelper.FullPathAction("DialogResult", "Default"),
					// расширенный поиск
					Configuration.AppSettings.URI_user_search 
						+ String.Format("?return=1&clid={0}&mvc=1&control=c&callbackKey=c1&callbackUrl={{0}}&search={{1}}", CLID)
				);
		}

	}
}