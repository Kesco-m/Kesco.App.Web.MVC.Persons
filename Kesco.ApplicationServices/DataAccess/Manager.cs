using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BLToolkit.Aspects;
using BLToolkit.Data;
using System.Collections.Specialized;
using BLToolkit.Mapping;

namespace Kesco.ApplicationServices
{

	public static class Manager
	{

		public static void ClearCache() {
			Accessor.ClearCache();
		}

		public static EmployeeInfo GetEmployee(int id, bool en)
		{
			return Accessor.GetEmployee(id, en ? 1 : 0);
		}

		public static EmployeeInfo GetCurrentEmployee()
		{
			return Accessor.GetCurrentEmployee(0);
		}

		public static EmployeeInfo GetCurrentEmployee(bool en)
		{
			return Accessor.GetCurrentEmployee(en ? 1 : 0);
		}

		/// <summary>
		/// Gets the employee last name with initials.
		/// </summary>
		/// <param name="id">The id.</param>
		/// <param name="en">if set to <c>true</c> [en].</param>
		/// <returns></returns>
		public static string GetEmployeeLastNameWithInitials(int id, bool en)
		{
			return Accessor.GetEmployeeLastNameWithInitials(id, en?1:0);
		}

		/// <summary>
		/// Осуществляет поиск сотрудников, соответсвующих ключевому слову.
		/// </summary>
		/// <param name="keyword">Ключевое слово.</param>
		/// <param name="limit">Задаёт максимальное количество элементов в возвращающемся списке. Если установлено в 0, то возвращается полный список.</param>
		/// <param name="en">Если установлено в <c>true</c> возвращается английское имя для сотрудника, иначе русское.</param>
		/// <returns>Возвращает список сотрудников, чьё имя соответсвует ключевому слову.</returns>
		public static EmployeeList SearchEmployee(string keyword, int limit, bool en)
		{
			EmployeeList results = null;
			if (!String.IsNullOrWhiteSpace(keyword)) {
				results = Accessor.SearchEmployee("%" + keyword + "%", limit, en ? 1 : 0);
			} else {
				results = Accessor.SearchEmployee(null, limit, en ? 1 : 0);
			}

			return results; 
		}

		/// <summary>
		/// Возвращает язык для текущего пользователя.
		/// </summary>
		/// <returns></returns>
		public static string GetUserLanguage()
		{
			return Accessor.GetUserLanguage();
		}

		/// <summary>
		/// Saves the user language.
		/// </summary>
		/// <param name="languageIsoCode">The language iso code.</param>
		public static void SaveUserLanguage(string languageIsoCode)
		{
			Accessor.SaveUserLanguage(languageIsoCode);
		}

		/// <summary>
		/// Gets the user setting.
		/// </summary>
		/// <param name="clid">The clid.</param>
		/// <param name="key">The key.</param>
		/// <returns></returns>
		public static UserSetting GetUserSetting(int clid, string key)
		{
			return Accessor.GetUserSetting(clid, key);
		}

		/// <summary>
		/// Gets the user setting value.
		/// </summary>
		/// <param name="clid">The clid.</param>
		/// <param name="key">The key.</param>
		/// <returns></returns>
		public static string GetUserSettingValue(int clid, string key)
		{
			return Accessor.GetUserSettingValue(clid, key);
		}

		/// <summary>
		/// Selects the user settings.
		/// </summary>
		/// <returns></returns>
		public static List<UserSetting> SelectUserSettings()
		{
			return Accessor.SelectUserSettings();
		}

		/// <summary>
		/// Selects the client application user settings.
		/// </summary>
		/// <param name="clid">The clid.</param>
		/// <returns></returns>
		public static List<UserSetting> SelectClientApplicationUserSettings(int clid)
		{
			return Accessor.SelectClientApplicationUserSettings(clid);
		}

		public static Dictionary<string, string> SelectClientApplicationUserSettingDictionary(int @clid)
		{
			return Accessor.SelectClientApplicationUserSettingDictionary(clid);
		}

		/// <summary>
		/// Устанавливает пользовательские настройки для экземпляра класса параметров
		/// </summary>
		/// <param name="clid">Код клиентского приложения</param>
		/// <param name="parameters">Экземпляр класса с типизированными параметрами</param>
		public static void SetClientApplicationUserSettings(int clid, object parameters)
		{
			Dictionary<string, string> stored = Accessor.SelectClientApplicationUserSettingDictionary(clid);
			Map.DictionaryToObject(stored, parameters);
		}

		public static void LoadClientApplicationUserSettings(int clid, Dictionary<string, string> parameters)
		{
			Dictionary<string, string> stored = Accessor.SelectClientApplicationUserSettingDictionary(clid, String.Join(",", parameters.Keys.ToArray()));
			if (stored != null) {
				stored.All(p => {
					if (parameters.ContainsKey(p.Key)) {
						parameters[p.Key] = p.Value;
					} else {
						parameters.Add(p.Key, p.Value);
					}
					return true;
				});
			}
		}

		/// <summary>
		/// Saves the user setting.
		/// </summary>
		/// <param name="userSetting">The user setting.</param>
		public static void SaveUserSetting(UserSetting userSetting)
		{
			Accessor.SaveUserSetting(userSetting);
		}

		/// <summary>
		/// Saves the user setting.
		/// </summary>
		/// <param name="clid">The clid.</param>
		/// <param name="key">The key.</param>
		/// <param name="value">The value.</param>
		public static void SaveUserSetting(int clid, string key, string value)
		{
			Accessor.SaveUserSetting(clid, key, value);
		}

		#region Accessor

		static Accessor Accessor
		{
			[System.Diagnostics.DebuggerStepThrough]
			get { return Accessor.CreateInstance(); }
		}

		#endregion
	}
}
