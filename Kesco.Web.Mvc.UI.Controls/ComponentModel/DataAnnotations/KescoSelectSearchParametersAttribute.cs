using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kesco.Web.Mvc.UI.ComponentModel.DataAnnotations
{
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
	public class KescoSelectSearchParametersAttribute : Attribute
	{
	
		/// <summary>
		/// Идентификатор клиента
		/// </summary>
		public int CLID { get; set; }

		/// <summary>
		/// Строка поиска
		/// </summary>
		public string Search { get; set; }

		public KescoSelectSearchParametersAttribute() : base() { }
	}
}
