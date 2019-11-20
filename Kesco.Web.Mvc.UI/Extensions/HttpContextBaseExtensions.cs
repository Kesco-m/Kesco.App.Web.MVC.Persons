using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Collections.Specialized;
using System.Web.Mvc;

namespace Kesco.Web.Mvc 
{

	/// <summary>
	/// Расширения для Http контекста
	/// </summary>
	public static class HttpContextBaseExtensions
	{
		/// <summary>
		/// Регистрирует скрипт с определённым общим ключом.
		/// </summary>
		/// <param name="context">The context.</param>
		/// <param name="key">The key.</param>
		/// <param name="script">The script.</param>
		public static void RegisterCommonScript(this HttpContextBase context, string key, string script)
		{
			var scripts = context.Items["CommonScripts"] as OrderedDictionary ?? new OrderedDictionary();

			if (!scripts.Contains(key)) scripts.Add(key, script);

			context.Items["CommonScripts"] = scripts;

		}

		/// <summary>
		/// Регистрирует скрипт с определённым общим ключом.
		/// </summary>
		/// <param name="context">The context.</param>
		/// <param name="key">The key.</param>
		/// <param name="scriptWriter">The script writer.</param>
		public static void RegisterCommonScript(this HttpContextBase context, string key, Func<string> scriptWriter)
		{
			var scripts = context.Items["CommonScripts"] as OrderedDictionary ?? new OrderedDictionary();

			if (!scripts.Contains(key)) scripts.Add(key, scriptWriter());

			context.Items["CommonScripts"] = scripts;

		}

	}
}
