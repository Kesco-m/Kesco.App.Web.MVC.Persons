using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLToolkit.Reflection;
using Kesco.Persons.BusinessLogic;
using Kesco.Persons.ObjectModel;
using Kesco.Persons.Controls.ComponentModel;
using Kesco.Persons.Controls.DataAccess;
using Kesco.Web.Mvc;
using Kesco.Web.Mvc.SharedViews.ComponentModel;
using Kesco.Web.Mvc.UI;
using Kesco.Web.Mvc.UI.ComponentModel.DataAnnotations;
using Kesco.Web.Mvc.UI.Infrastructure;

namespace Kesco.Persons.Controls.Controllers
{
	/// <summary>
	/// Реализует контроллер для элемента управления "Выбор лица"
	/// </summary>
	public partial class PersonSimpleSelectController
		: KescoSelectBaseController<PersonSimpleSelectAccessor, PersonSimple, PersonSimpleSelectAccessor.SearchParameters, int>
	{

		/// <summary>
		/// Gets the corporate culture settings.
		/// </summary>
		/// <returns></returns>
		protected override CultureSettings GetCorporateCultureSettings()
		{
			return Configuration.AppSettings.Culture;
		}

		/// <summary>
		/// Возвращает URL-адрес для расширенного поиска лица
		/// </summary>
		/// <param name="clid">Идентификатор клиента.</param>
		/// <returns>URL-адрес для расширенного поиска лица</returns>
		protected override string GetAdvancedSearchUrl(PersonSimpleSelectAccessor.SearchParameters parameters)
		{
			return Configuration.AppSettings.URI_person_search
				+ String.Format("?return=1&clid={0}&mvc=1&control=c&callbackKey=c1&callbackUrl={{0}}&search={{1}}", parameters.CLID);
		}

		/// <summary>
		/// Возвращает URL-адрес для просмотра досье на лицо
		/// </summary>
		/// <param name="clid">Идентификатор клиента.</param>
		/// <returns>URL-адрес для просмотра досье на лицо</returns>
		protected override string GetDetailsUrl()
		{
			return Configuration.AppSettings.URI_person_form;
		}

		protected override void AdjustSearchParameters(PersonSimpleSelectAccessor.SearchParameters parameters)
		{
			base.AdjustSearchParameters(parameters);
			if (parameters != null)
			{
				parameters.MaxEntries = 9;
			}
		}

		/// <summary>
		/// Выполняет диспетчеризацию команд.
		/// </summary>
		/// <param name="command">Команда</param>
		/// <param name="control">Идентификатор элемента управления на стороне клиента</param>
		/// <param name="clid">Идентификатор клиента.</param>
		/// <param name="id">Идентификатор сущности</param>
		/// <param name="parameters">критерии поиска.</param>
		/// <returns></returns>
		public override ActionResult Dispatch(string command, string control, int mode, int? id, PersonSimpleSelectAccessor.SearchParameters parameters)
		{
			try
			{
				switch (command.ToLower())
				{
					case "createjuridical":
						return CreateJuridical(control);
					case "createnatural":
						return CreateNatural(control);
				}
				return base.Dispatch(command, control, mode, id, parameters);
			}
			catch (Exception ex)
			{
				Kesco.Logging.Logger.WriteEx(ex);
				return JavaScriptAlert(
						Kesco.Localization.Resources.Ajax_Alert_Title_ApplicationError,
						ex.Message
					);
			}
		}

		/// <summary>
		/// Creates the juridical.
		/// </summary>
		/// <param name="control">The control.</param>
		/// <returns></returns>
		public virtual ActionResult CreateJuridical(string control)
		{
			string script = String.Format(@"(function() {{ // closure/замыкание
				    var $lookup = $('#{0}');
				    var item = $lookup.selectBox('getValue');
				    if (window.console) console.log('{0}:CreateJuridical', item);
					openPopupWindow('{1}?id='+item.value, null, null, 'wnd_PersonCreateJuridical_{0}', 800, 600);
				}})();
                ",
				 control,
                 Configuration.AppSettings.URI_person_jp_add
			);
			return JavaScript(script);
		}

		/// <summary>
		/// Creates the natural.
		/// </summary>
		/// <param name="control">The control.</param>
		/// <returns></returns>
		public virtual ActionResult CreateNatural(string control)
		{
			string script = String.Format(@"(function() {{ // closure/замыкание
				    var $lookup = $('#{0}');
				    var item = $lookup.selectBox('getValue');
				    if (window.console) console.log('{0}:CreateNatural', item);
					openPopupWindow('{1}?id='+item.value, null, null, 'wnd_PersonCreateNatural_{0}', 800, 600);
				}})();
                ",
				 control,
                 Configuration.AppSettings.URI_person_np_add
			);
			return JavaScript(script);
		}

		/// <summary>
		/// Возвращает наименованине для лица
		/// </summary>
		/// <param name="entry">Лицо.</param>
		/// <returns></returns>
		protected override string GetEntryLabel(PersonSimple entry)
		{
			return entry.Nickname;
		}

		/// <summary>
		/// Возвращает HTML-представление об лице
		/// </summary>
		/// <param name="id">Код лица</param>
		/// <returns>HTML-представление об лица</returns>
		public virtual ActionResult PersonInfo(int id)
		{
			string viewName = "PersonInfo";
			if (IsAjaxRequest) viewName += "Ajax";
			return View(viewName, new Models.PersonInfo.ViewModel(id));
		}
	}
}
