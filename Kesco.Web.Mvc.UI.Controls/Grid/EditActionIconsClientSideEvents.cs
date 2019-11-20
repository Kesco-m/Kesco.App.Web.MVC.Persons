using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kesco.Web.Mvc.UI.Grid
{

	public class EditActionIconsClientSideEvents
	{
		// Properties
		public string OnEdit { get; set; }
		public string OnSuccess { get; set; }
		public string AfterSave { get; set; }
		public string AfterRestore { get; set; }
		public string OnError { get; set; }

		public EditActionIconsClientSideEvents()
        {
			this.OnEdit =
			this.OnSuccess =
			this.AfterSave =
			this.AfterRestore =
			this.OnError = String.Empty;
        }

	}

}
