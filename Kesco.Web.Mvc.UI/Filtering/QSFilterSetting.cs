using System.Web;


namespace Kesco.Web.Mvc.Filtering
{
	/// <summary>
	/// Настройки для группы поиска, настраиваемой через URL-параметры
	/// </summary>
	public class QSFilterSetting<T> : FilterSetting<T>
		where T : QSFilterSetting<T>
	{
		/// <summary>
		/// Инициализирует экземпляр из строки запроса в URL from query string.
		/// </summary>
		/// <param name="qsParamName">Имя параметра из строки запроса (QueryString)</param>
		/// <param name="defaultValue">The default value.</param>
		/// <returns>Настройки группы фильтрации</returns>
		public virtual T InitFromQueryString(string qsParamName, object defaultValue)
		{
            Editable = HttpContext.Current.Request[qsParamName] == null || HttpContext.Current.Request[qsParamName] == "";
            Enable = HttpContext.Current.Request[qsParamName] != null && HttpContext.Current.Request[qsParamName] != "";
            DefaultValue = defaultValue ?? HttpContext.Current.Request["_" + qsParamName];
            
			return SetValue(HttpContext.Current.Request[qsParamName]);
		}
	}
}
