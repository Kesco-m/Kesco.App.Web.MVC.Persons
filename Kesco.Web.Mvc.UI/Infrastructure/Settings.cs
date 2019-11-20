using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLToolkit.Mapping;

namespace Kesco.Web.Mvc
{
	/// <summary>
	/// Представляет абстрактный класс для установок веб приложения.
	/// </summary>
	/// <example>
	/// public abstract class CacheSettings : Settings
	/// {
	/// 	public abstract bool Enabled { get; protected internal set; }
	/// 	public abstract int Expiration { get; protected internal set; }
	/// 
	/// }
	/// 
	/// public abstract class AppSettings : Settings
	/// {
	/// 	public abstract string AppName { get; protected internal set; }
	/// 	public abstract string CompanyName { get; protected internal set; }
	/// 	public abstract CacheSettings Cache { get; protected internal set; }
	/// }
	/// public class SiteApplication : HttpApplication
	/// {
	/// 	private AppSettings _settings = null;
	/// 
	/// 	public AppSettings AppSettings { get { return _settings; } }
	/// 
	/// 	public override void Init()
	/// 	{
	/// 		InitSettings(System.Web.Configuration.WebConfigurationManager.AppSettings);
	/// 		base.Init();
	/// 	}
	/// 
	/// 	protected virtual void InitSettings(NameValueCollection settings)
	/// 	{
	/// 		_settings = TypeAccessor&lt;AppSettings&gt;.CreateInstanceEx();
	/// 
	/// 		Hashtable ht = new Hashtable();
	/// 
	/// 		foreach (string key in settings.AllKeys)
	/// 		{
	/// 			ht.Add(key, settings[key]);
	/// 		}
	/// 
	/// 		Map.DictionaryToObject(ht, _settings);
	/// 
	/// 	}
	/// }
	/// </example>
	public abstract class Settings : ICloneable
    {


		#region ICloneable Members

		/// <summary>
		/// Создает новый объект, который является копией текущего экземпляра.
		/// </summary>
		/// <returns>
		/// Новый объект, являющийся копией этого экземпляра.
		/// </returns>
        public object Clone()
        {
            return Map.ObjectToObject(this, this.GetType());
        }

        #endregion
    }


}
