using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kesco.Web.Mvc.SharedViews.Models
{
	public class Alert
	{
		public string Title { get; set; }
		public string Message { get; set; }
	}


	public class ErrorObjectNotFound : ViewModel<Alert>
	{
		public ErrorObjectNotFound() : base()
		{ 
		}

		public ErrorObjectNotFound(string title, string message) : this()
		{
			Model.Title = title;
			Model.Message = message;
		}

		protected override void CreateSettings()
		{
			settings = new object();
		}

	}
}