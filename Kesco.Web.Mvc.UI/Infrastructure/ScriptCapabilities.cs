using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kesco.Web.Mvc
{
	/// <summary>
	/// Описывает возможности скрипта для веб-страницы.
	/// Указывает какие скрипты должны быть загружены.
	/// </summary>
	public class ScriptCapabilities
	{
		/// <summary>
		/// Возвращает или устанавливает признак загрузки скрипта грида.
		/// </summary>
		/// <value>
		///   <c>true</c> если скрипт грида должен быть загружен; иначе, <c>false</c>.
		/// </value>
		public bool LoadGridScript { get; set; }

		public bool LoadHubScript { get; set; }

		/// <summary>
		/// Возвращает или устанавливает признак загрузки скрипта дерева.
		/// </summary>
		/// <value>
		///   <c>true</c> если скрипт дерева должен быть загружен; иначе, <c>false</c>.
		/// </value>
		public bool LoadTreeScript { get; set; }

		public ScriptCapabilities DisableGridScript()
		{
			LoadGridScript = false;
			return this;
		}

		public ScriptCapabilities DisableTreeScript()
		{
			LoadTreeScript = false;
			return this;
		}

		public ScriptCapabilities DisableHubScript()
		{
			LoadHubScript = false;
			return this;
		}
	}
}
