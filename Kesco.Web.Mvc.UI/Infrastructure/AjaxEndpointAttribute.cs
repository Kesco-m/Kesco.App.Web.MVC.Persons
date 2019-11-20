using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kesco.Web.Mvc
{
	/// <summary>
	/// 
	/// </summary>
	public sealed class AjaxEndpointAttribute : Attribute
	{
		public AjaxEndpointAttribute() { }

		public AjaxEndpointAttribute(params string[] requestParameters) { }
	}
}
