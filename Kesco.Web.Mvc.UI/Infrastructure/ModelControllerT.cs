using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Kesco.Web.Mvc
{
	/// <summary>
	/// Класс расширяет базовый контроллер для поддержки диспетчеризации
	/// команд/событий для элементов управления
	/// </summary>
	/// <typeparam name="TDataModel">Тип модели данных.</typeparam>
	public abstract class ModelController<TDataModel> : ControllerEx
	{
		/// <summary>
		/// Выполняет диспетчеризацию команд.
		/// </summary>
		/// <param name="command">Команда</param>
		/// <param name="control">Идентификатор элемента управления на стороне клиента</param>
		/// <returns></returns>
		/// <exception cref="System.Exception">Неизвестная команда</exception>
		public virtual ActionResult Dispatch(string command, string control, [Bind(Prefix="Model")] TDataModel model)
		{
			ActionResult result = null;
			//try
			//{
				var cmd = (command ?? "").ToLower();
				if (DoDispatch(cmd, control, model, out result))
					return result;
				throw new Exception(String.Format("Неизвестная команда: {0}/{2} - {1}", command, control, cmd));
			//}
			//catch (Exception ex)
			//{
			//    Kesco.Logging.Logger.WriteEx(ex);
			//    return JavaScriptAlert(
			//            Kesco.Localization.Resources.Ajax_Alert_Title_ApplicationError,
			//            ex.Message
			//        );
			//}
		}

		/// <summary>
		/// Выполняет диспетчеризацию команд.
		/// </summary>
		/// <param name="command">Команда</param>
		/// <param name="control">Идентификатор элемента управления на стороне клиента</param>
		/// <param name="model">Модель</param>
		/// <param name="result">Результат действия, если команда обработана, иначе null.</param>
		/// <returns>Возвращает истину, если команда обработана, иначе false</returns>
		protected virtual bool DoDispatch(string command, string control, TDataModel model, out ActionResult result)
		{
			result = null;
			return false;
		}


	}
}
