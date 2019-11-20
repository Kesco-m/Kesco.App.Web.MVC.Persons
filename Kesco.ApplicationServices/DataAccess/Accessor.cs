using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLToolkit.Aspects;
using BLToolkit.Data;
using BLToolkit.DataAccess;
using Kesco.DataAccess;
using Kesco.ApplicationServices.DataAccess;

namespace Kesco.ApplicationServices
{
	/// <summary>
	/// Проводник данных для на
	/// </summary>
	public abstract class Accessor : AccessorBase<DB, Accessor>
	{
		public class DB : Database { public DB() : base("DS_User") { } }

		[ClearCache]
		public abstract void ClearCache();

		[SqlQuery(@"
			SELECT 
				КодСотрудника, КодЛицаЗаказчика 
				, case @en when 1 then [Employee] else [Сотрудник] end as ФИО
			FROM dbo.Сотрудники 
			WHERE КодСотрудника = @id")]
		public abstract EmployeeInfo GetEmployee(int @id, int @en);

		[SqlQuery(@"
			SELECT 
				КодСотрудника, КодЛицаЗаказчика 
				, case @en when 1 then [Employee] else [Сотрудник] end as ФИО
			FROM dbo.Сотрудники 
			WHERE [SID] = SUSER_SID()")]
		public abstract EmployeeInfo GetCurrentEmployee(int @en);

		[SqlQuery(@"SELECT  CASE @en WHEN 1 THEN [FIO] ELSE [ФИО] END AS ФИО FROM [Инвентаризация]..Сотрудники WHERE КодСотрудника = @id")]
		[ScalarFieldName("ФИО")]
		public abstract string GetEmployeeLastNameWithInitials(int @id, int @en);

		[SqlQuery(@" 

			SET ROWCOUNT @limit;

			SELECT [КодСотрудника], КодЛицаЗаказчика
				, case @en when 1 then [Employee] else [Сотрудник] end as ФИО
			FROM [Инвентаризация]..[Сотрудники]
			WHERE @keyword is null 
				or (@keyword is not null and ([Сотрудник] LIKE @keyword or ([Employee] LIKE @keyword)))
			ORDER BY case @en when 1 then [Employee] else [Сотрудник] end;

			SET ROWCOUNT 0;
		")]
		public abstract EmployeeList SearchEmployee(string @keyword, int @limit, int @en);

		[SqlQuery(@"
			SELECT [Язык] FROM [Инвентаризация]..[Сотрудники] 
			WHERE [SID] = SUSER_SID()")]
		[ScalarFieldName("Язык")]
		public abstract string GetUserLanguage();

		[SqlQuery(@"
			UPDATE [Инвентаризация]..[Сотрудники] 
			SET [Язык] = @languageIsoCode
			WHERE [SID] = SUSER_SID()")]
		public abstract void SaveUserLanguage(string @languageIsoCode);

		/// <summary>
		/// Selects the user settings.
		/// </summary>
		/// <returns></returns>
		public List<UserSetting> SelectUserSettings()
		{
			return Query.SelectAll<UserSetting>();
		}

		/// <summary>
		/// Selects the client application user settings.
		/// </summary>
		/// <param name="clid">ID of the client application (CLID).</param>
		/// <returns>A list of the user settings of the client application</returns>
		[SqlQuery(@"
			SELECT * FROM [Инвентаризация]..[vwНастройки] 
			WHERE [КодНастройкиКлиента] = @clid
			ORDER BY [Параметр]")]
		public abstract List<UserSetting> SelectClientApplicationUserSettings(int @clid);

		/// <summary>
		/// Возвращает список установок клиента.
		/// </summary>
		/// <param name="clid">Идентификатор клиентского приложения.</param>
 		/// <returns>Возвращает список установок клиента.</returns>
		[SqlQuery(@"SELECT Параметр, Значение FROM [Инвентаризация]..[vwНастройки] WHERE [КодНастройкиКлиента] = @clid")]
		[Index("Параметр")]
		[ScalarFieldName("Значение")]
		public abstract Dictionary<string, string> SelectClientApplicationUserSettingDictionary(int @clid);

		/// <summary>
		/// Возвращает список установок клиента.
		/// </summary>
		/// <param name="clid">Идентификатор клиентского приложения.</param>
		/// <returns>Возвращает список установок клиента.</returns>
		[SqlQuery(@"
			SET @clid = COALESCE(@clid, 0)
			SET @Parameters = RTRIM(LTRIM(COALESCE(@Parameters, '')))

			DECLARE @Параметры TABLE(Параметр VARCHAR(20))
			DECLARE @I int, @S varchar(8000), @W varchar(20)

			SET @S = @Parameters
			WHILE LEN(@S) > 0 
			BEGIN
				SET @I = CHARINDEX(',', @S+',') 
				SET @W = LEFT(@S, @I-1)	
				SET @S = SUBSTRING(@S, @I + 1, LEN(@S) - LEN(@W))
				INSERT @Параметры SELECT @W
			END			

			SELECT vwНастройки.Параметр, vwНастройки.Значение 
			FROM vwНастройки
				INNER JOIN @Параметры P ON vwНастройки.Параметр = P.Параметр
			WHERE КодНастройкиКлиента = @clid")]
		[Index("Параметр")]
		[ScalarFieldName("Значение")]
		public abstract Dictionary<string, string> SelectClientApplicationUserSettingDictionary(int @clid, string @parameters);

		/// <summary>
		/// Gets the user setting.
		/// </summary>
		/// <param name="clid">The clid.</param>
		/// <param name="key">The key.</param>
		/// <returns></returns>
		[SqlQuery(@"
			SELECT * FROM [Инвентаризация]..[vwНастройки] 
			WHERE [КодНастройкиКлиента] = @clid AND [Параметр] = @key")]
		public abstract UserSetting GetUserSetting(int @clid, string @key);

		/// <summary>
		/// Gets the user setting value.
		/// </summary>
		/// <param name="clid">The clid.</param>
		/// <param name="key">The key.</param>
		/// <returns></returns>
		[SqlQuery(@"
			SELECT [Значение] FROM [Инвентаризация]..[vwНастройки] 
			WHERE [КодНастройкиКлиента] = @clid AND [Параметр] = @key")]
		[ScalarFieldName("Значение")]
		public abstract string GetUserSettingValue(int @clid, string @key);

		/// <summary>
		/// Saves the user setting.
		/// </summary>
		/// <param name="userSetting">The user setting.</param>
		public void SaveUserSetting(UserSetting userSetting)
		{
			Query.Insert(userSetting);
		}

		/// <summary>
		/// Saves the user setting.
		/// </summary>
		/// <param name="clid">The clid.</param>
		/// <param name="key">The key.</param>
		/// <param name="value">The value.</param>
		[SqlQuery(@"
			INSERT INTO [Инвентаризация].[dbo].[vwНастройки]([КодНастройкиКлиента], [Параметр], [Значение])
			VALUES(@clid, @key, @value)")]
		public abstract void SaveUserSetting(int @clid, string @key, string @value);

		/// <summary>
		/// Deletes the user setting.
		/// </summary>
		/// <param name="clientApplicationSettingID">The client application setting ID.</param>
		/// <param name="employeeID">The employee ID.</param>
		/// <param name="parameter">The parameter.</param>
		public void DeleteUserSetting(int clientApplicationSettingID, int employeeID, string parameter)
		{
			CacheAspect.ClearCache(this.GetType());
			Query.DeleteByKey<UserSetting>(clientApplicationSettingID, employeeID, parameter);
		}

	}
}
