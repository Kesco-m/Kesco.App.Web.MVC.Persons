using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Kesco.Web.Mvc
{
	public interface IDialogResultSupport
	{
		DialogResult DialogResult { get; }
	}

	public class DialogResultModel : IDialogResultSupport
	{
		[UIHint("DialogResult")]
		public DialogResult DialogResult { get; internal set; }

		public int CLID { get; set; }

		public DialogResultModel(string returnUri, string key, string value)
		{
			CLID = 0;
			DialogResult = new DialogResult(returnUri, key, value);
		}
	}
}
