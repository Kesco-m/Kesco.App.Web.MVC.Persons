using System;
using System.Web.Mvc;
using Kesco.Territories.Controls.DataAccess;
using Kesco.Territories.ObjectModel;
using Kesco.Web.Mvc;
using Kesco.Web.Mvc.UI.Infrastructure;
using System.Web;

namespace Kesco.Territories.Controls.Controllers
{
	public class TerritorySelectController : KescoSelectBaseController<TerritorySelectAccessor, Territory, TerritorySelectAccessor.SearchParameters, int>
	{

		public TerritorySelectController() : base()
		{
			UseCompressHtml = true;
		}

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
		/// <param name="parameters">Параметры поиска/фильтрации.</param>
		/// <returns>
		/// URL-адрес для расширенного поиска лица
		/// </returns>
		protected override string GetAdvancedSearchUrl(TerritorySelectAccessor.SearchParameters parameters)
		{
			string фильтрПоКодуСтраныИлиКодуВСтране = String.Empty;
			int кодСтраныИлиКодВСтране = 0;
			int areaID = 0;
			if (parameters.AreaIDs != null && parameters.AreaIDs.Count > 0) areaID = parameters.AreaIDs[0];
			if (areaID > 0 && Int32.TryParse(parameters.Search ?? String.Empty, out кодСтраныИлиКодВСтране)) {
				if (areaID == 2) фильтрПоКодуСтраныИлиКодуВСтране = String.Format("&_TELCODECOUNTRY=1{0}", кодСтраныИлиКодВСтране);
				if (areaID == 4) фильтрПоКодуСтраныИлиКодуВСтране = String.Format("&_TELCODEINCOUNTRY=0{0}", кодСтраныИлиКодВСтране);
				if (фильтрПоКодуСтраныИлиКодуВСтране != String.Empty) parameters.Search = String.Empty;
			}
			return Configuration.AppSettings.URI_area_search
				+ "&return=1&clid={0}&areatype={1}{2}&search={3}&mvc=1&control=c&callbackKey=c1&callbackUrl={{0}}".FormatWith( 
						parameters.CLID, 
						areaID, 
						фильтрПоКодуСтраныИлиКодуВСтране,
						Server.UrlEncode(parameters.Search ?? String.Empty)
					);
		}

		/// <summary>
		/// Возвращает URL-адрес для просмотра досье на лицо
		/// </summary>
		/// <returns>
		/// URL-адрес для просмотра досье на лицо
		/// </returns>
		protected override string GetDetailsUrl()
		{
			return Configuration.AppSettings.URI_area_form;
		}

		protected override void AdjustSearchParameters(TerritorySelectAccessor.SearchParameters parameters)
		{
			base.AdjustSearchParameters(parameters);
			if (parameters != null)
			{
				parameters.MaxEntries = 9;
			}
		}

		/// <summary>
		/// Возвращает локализованное название территории в зависимости от языка пользователя
		/// </summary>
		/// <param name="entry"></param>
		/// <returns></returns>
		protected override string GetEntryLabel(Territory entry)
		{
			switch (UserContext.Lang)
			{
				case "ru": return entry.Name;
				case "ee": return entry.Caption;
				default: return entry.Caption;
			}
		}

		/// <summary>
		/// Возвращает английское название территории.
		/// </summary>
		/// <param name="entry">Запись.</param>
		/// <returns>Английское название территории.</returns>
		protected string GetEntryLabelEn(Territory entry)
		{
			return entry.Caption;
		}

		/// <summary>
		/// Выполняет диспетчеризацию команд.
		/// </summary>
		/// <param name="command">Команда</param>
		/// <param name="control">Идентификатор элемента управления на стороне клиента</param>
		/// <param name="clid">Идентификатор клиента.</param>
		/// <param name="id">Идентификатор сущности.</param>
		/// <param name="parameters">Критерии поиска.</param>
		/// <returns></returns>
		/// <exception cref="System.Exception">Неизвестная команда</exception>
		public override ActionResult Dispatch(string command, string control, int mode, int? id, TerritorySelectAccessor.SearchParameters parameters)
		{
			var cmd = command.ToLower();
			switch (cmd)
			{
				case "getitemen":
					return GetItemEn(control, mode, id);
				default:
					return base.Dispatch(command, control, mode, id, parameters);
			}
		}

		/// <summary>
		/// Устанавливает значение элемента управления, используя английское название страны
		/// </summary>
		/// <param name="control">Идентификатор контрола.</param>
		/// <param name="mode">Режим (1 - просмотр, иначе редактирование).</param>
		/// <param name="id">Идентификатор</param>
		/// <returns>
		/// Должен устанавливать значение, включая отображаемое, для элемента управления
		/// </returns>
		public virtual ActionResult GetItemEn(string control, int mode, int? id)
		{
			string widget = mode == 1 ? "dynamicLink" : "selectBox";
			string value = "";
			string label = "";
			Territory entry = Territory.CreateInstance();
			if (id.HasValue)
			{
				value = id.Value.ToString();
				entry = GetEntry(id.Value);
				if (entry != null)
					label = GetEntryLabelEn(entry);
				else label = String.Format("#{0}", value);
			}
			string script = String.Format(@"(function() {{ // closure/замыкание
					var $control = $('#{0}');
					var item = {1};
					$control.{2}('setValue', item );
				}})();
				",
				control,
				Kesco.Web.Mvc.Json.Serialize(new
				{
					value = value,
					label = label,
					data = entry
				}),
				widget
			);
			return JavaScript(script);
		}

		public virtual ActionResult GetItemLang(string control, int mode, int? id)
		{
			string widget = mode == 1 ? "dynamicLink" : "selectBox";
			string value = "";
			string label = "";
			Territory entry = Territory.CreateInstance();
			if (id.HasValue)
			{
				value = id.Value.ToString();
				entry = GetEntry(id.Value);
				if (entry != null)
					label = GetEntryLabel(entry);
				else label = String.Format("#{0}", value);
			}
			string script = String.Format(@"(function() {{ // closure/замыкание
					var $control = $('#{0}');
					var item = {1};
					$control.{2}('setValue', item );
				}})();
				",
				control,
				Kesco.Web.Mvc.Json.Serialize(new
				{
					value = value,
					label = label,
					data = entry
				}),
				widget
			);
			return JavaScript(script);
		}
	}
}