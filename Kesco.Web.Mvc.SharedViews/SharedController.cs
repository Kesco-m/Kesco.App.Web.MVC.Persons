using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kesco.Web.Mvc.Infrastructure;
using System.Data.SqlClient;
using Kesco.Lib.Log;
using Kesco.Web.Mvc.SharedViews.Localization;

namespace Kesco.Web.Mvc.SharedViews
{
	/// <summary>
	/// Класс расширяет базовый контроллер 
	/// для поддержки настроек культуры
	/// </summary>
	public abstract class SharedController : ControllerEx
	{
		/// <summary>
		/// Возвращает настройки культуры для приложения.
		/// </summary>
		/// <returns>Настройки культуры для приложения</returns>
		protected override CultureSettings GetCorporateCultureSettings()
		{
			return Configuration.AppSettings.Culture;
		}
	}

	/// <summary>
	/// Класс расширяет базовый контроллер 
	/// для поддержки настроек культуры
	/// </summary>
	public abstract class SharedModelController<TDataModel> : ModelController<TDataModel>
	{
		/// <summary>
		/// Возвращает настройки культуры для приложения.
		/// </summary>
		/// <returns>Настройки культуры для приложения</returns>
		protected override CultureSettings GetCorporateCultureSettings()
		{
			return Configuration.AppSettings.Culture;
		}

	}

	/// <summary>
	/// Класс расширяет базовый контроллер 
	/// для поддержки настроек культуры c возможностью валидации
	/// </summary>
	public abstract class SharedValidationSupportedController<TDataModel> : ValidationSupportedModelController<TDataModel>
	where TDataModel : class, new()
	{
		/// <summary>
		/// Возвращает настройки культуры для приложения.
		/// </summary>
		/// <returns>Настройки культуры для приложения</returns>
		protected override CultureSettings GetCorporateCultureSettings()
		{
			return Configuration.AppSettings.Culture;
		}
	}
}