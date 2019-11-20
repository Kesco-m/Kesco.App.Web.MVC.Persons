using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;

namespace Kesco.Web.Mvc.SharedViews.Models.Dump
{
	public class DataModel
	{
		public List<Assembly> Assemblies { get; set; }
	}

	public class ViewModel : ViewModel<DataModel>
	{
		public ViewModel()
			: base()
		{
			Model.Assemblies = AppDomain.CurrentDomain
				.GetAssemblies()
				.OrderBy(asm => asm.FullName)
				.ToList();
		}

		protected override void CreateSettings()
		{
			this.settings = new object { };
		}
	}
}