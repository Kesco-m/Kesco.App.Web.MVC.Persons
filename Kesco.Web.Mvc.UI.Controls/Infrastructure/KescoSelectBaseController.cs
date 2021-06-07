using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using BLToolkit.Reflection;
using Kesco.DataAccess;
using Kesco.Web.Mvc.UI.Controls.DataAccess;
using Newtonsoft.Json.Linq;

namespace Kesco.Web.Mvc.UI.Infrastructure
{
	/// <summary>
	/// Класс реализует базовый контроллер для элемента управления SELECT
	/// </summary>
	/// <typeparam name="A">Тип проводник данных.</typeparam>
	/// <typeparam name="T">Тип сущности.</typeparam>
	/// <typeparam name="S">Тип критериев поиска.</typeparam>
	/// <typeparam name="TID">Тип идентификатора.</typeparam>
	public abstract class KescoSelectBaseController<A, T, S, TID> : ControllerEx, IKescoSelectController<S, TID>
		where A : class, IKescoSelectAccessor, IEntityAccessor<T, S, TID>
        where T : Kesco.ObjectModel.IUniqueID<TID>
		where S : SearchParameters, new()
        where TID : struct
    {

        /// <summary>
        /// Создаёт проводник данных
        /// </summary>
		/// <returns>Проводник данных</returns>
        protected A CreateAccessor()
        {
            return TypeAccessor<A>.CreateInstance();
        }

		/// <summary>
		/// Возвращает отображаемое значение для сущности.
		/// </summary>
		/// <param name="entry">Экземпляр сущности</param>
		/// <returns>Отображаемое значение</returns>
		protected abstract string GetEntryLabel(T entry);

		/// <summary>
		/// Возвращает экземпляр сущности
		/// </summary>
		/// <param name="id">Идентификатор сущности.</param>
		/// <returns>Экземпляр сущности</returns>
		protected virtual T GetEntry(TID id)
		{
			A accessor = CreateAccessor();
			T entry = accessor.GetInstance(id);
			return entry;
		}

		/// <summary>
		/// Возвращает ссылку для расширенного поиска.
		/// </summary>
		/// <param name="parameters">Параметры фильтрации/поиска</param>
		/// <returns>
		/// Ссылку для расширенного поиска
		/// </returns>
		protected abstract string GetAdvancedSearchUrl(S parameters);

		/// <summary>
		/// Возвращает URL-адрес, для открытия окна с деталими.
		/// </summary>
		/// <returns>
		/// URL-адрес, для открытия окна с деталими
		/// </returns>
		protected abstract string GetDetailsUrl();

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
        public virtual ActionResult Dispatch(string command, string control, int mode, TID? id, S parameters)
        {
            try
            {
				var cmd = command.ToLower();
                switch (cmd)
                {
					case "getitem":
						return GetItem(control, mode, id);
					case "search":
						return Search(control, mode, parameters);
					case "setvalue":
						return SetValue(control, id);
                    case "advsearch":
						return AdvSearch(control, mode, parameters);
					case "details":
					case "view":
						return Details(control, mode);
                }
				throw new Exception(String.Format("Неизвестная команда: {0}/{2} - {1}", command, control, cmd));
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
		/// Устанавливает значение элемента управления
		/// </summary>
		/// <param name="control">Идентификатор контрола.</param>
		/// <param name="mode">Режим (1 - просмотр, иначе редактирование).</param>
		/// <param name="id">Идентификатор</param>
		/// <returns>
		/// Должен устанавливать значение, включая отображаемое, для элемента управления
		/// </returns>
		public virtual ActionResult GetItem(string control, int mode, TID? id)
		{
			string widget = mode == 1 ? "dynamicLink" : "selectBox";
			string value = "";
			string label = "";
			T entry = default(T);
			if (id.HasValue) {
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
				 Kesco.Web.Mvc.Json.Serialize(new {
					 value = value,
					 label = label,
					 data = entry
				 }),
				 widget
			);
			return JavaScript(script);
		}

		/// <summary>
		/// Устанавливает значение элемента управления
		/// </summary>
		/// <param name="control">Идентификатор контрола.</param>
		/// <param name="id">Идентификатор</param>
		/// <returns>Должен устанавливать значение, включая отображаемое, для элемента управления</returns>
		public virtual ActionResult SetValue(string control, TID? id)
		{
			string value = "";
			string label = "";
			T entry = default(T);
			if (id.HasValue)
			{
				value = id.Value.ToString();
				entry = GetEntry(id.Value);
				if (entry != null)
					label = GetEntryLabel(entry);
				else label = String.Format("#{0}", value);
			}
			string script = String.Format(@"(function() {{ // closure/замыкание
				    var $lookup = $('#{0}');
				    var item = {1};
					$lookup.selectBox('setValue', item );
				}})();
                ",
				 control,
				 Kesco.Web.Mvc.Json.Serialize(new { 
					 value = value,
					 label = label,
					 data = entry
				 })
			);
			return JavaScript(script);
		}

		/// <summary>
		/// Метод позволяет произвести корректировку критериев поиска.
		/// </summary>
		/// <param name="parameters">Объект с критериями поиска.</param>
		protected virtual void AdjustSearchParameters(S parameters) {}

		/// <summary>
		/// Выполняет поиск по указанным критериям.
		/// Критерии поиска определяют свойства объекта.
		/// </summary>
		/// <param name="control">Идентификатор контрола</param>
		/// <param name="mode">The mode.</param>
		/// <param name="parameters">The parameters.</param>
		/// <returns>
		/// Возвращает записи, совпадающие с критериями поиска
		/// </returns>
		public virtual ActionResult Search(string control, int mode, S parameters) 
		{
			try
			{
				var accessor = CreateAccessor();
				TryUpdateModel(parameters, "parameters");
				AdjustSearchParameters(parameters);
				var model = accessor.Search(parameters);

				string script = String.Format(@"
						(function() {{ // closure/замыкание
							var suggestions = {1};
							
							var $lookup = $('#{0}');
							$lookup.selectBox('suggest', suggestions);
						}})();
						",
						control,
						Kesco.Web.Mvc.Json.Serialize(new { model = model, status = "ok" }, true)
					);
				return JavaScript(script);

//				return JsonModel(model, JsonRequestBehavior.AllowGet);
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
		/// Выполняет команду расширенного поиска
		/// </summary>
		/// <param name="control">Идентификатор элемента управления.</param>
		/// <param name="parameters">The parameters.</param>
		/// <returns>
		/// Javascript-код открытия окна с расширенным поиском.
		/// </returns>
		public virtual ActionResult AdvSearch(string control, int mode, S parameters)
        {
			string script = String.Format(@"(function() {{ // closure/замыкание
				    var $lookup = $('#{0}');
				    var item = $lookup.selectBox('getValue');
				    var callbackUrl = encodeURIComponent('{1}');
				    
				    var url = '{2}';
				    url = $.validator.format(url, callbackUrl, encodeURIComponent(item.label));
                    KescoLookup_AdvSearch('#{0}', url);
                }})();
                ",
				 control, 
                 Url.FullPathAction("DialogResult"),
				 GetAdvancedSearchUrl(parameters)
            );

            return JavaScript(script);
        }

		/// <summary>
		/// Выполняет команду просмотра деталей.
		/// </summary>
		/// <param name="controlID">Идентификатор элемента управления.</param>
		/// <returns>
		/// Javascript-код открытия окна с деталями.
		/// </returns>
		public virtual ActionResult Details(string controlID, int mode)
		{
			string widget = mode == 1 ? "dynamicLink" : "selectBox";
			string script = String.Format(@"(function() {{ // closure/замыкание
				    var $control = $('#{0}');
				    var item = $control.{2}('getValue');
				    
					openPopupWindow('{1}?id='+item.value, null, null, 'wnd_Details_{0}', 800, 600, {{ close: false }});
				}})();
                ",
				 controlID,
				 GetDetailsUrl(),
				 widget
			);
            return JavaScript(script);
		}

    }
}
