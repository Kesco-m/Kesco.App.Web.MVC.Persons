using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLToolkit.Reflection;
using System.Collections;
using BLToolkit.TypeBuilder;
using BLToolkit.Mapping;

namespace Kesco.Web.Mvc.SharedViews.Models
{
	/// <summary>
	/// 
	/// </summary>
	public class IndexViewModel : ViewModel<DummyEntity>
	{
		public class ClientParameters {
			[MapField("Width"), Parameter(600)]
			public int Width { get; set; }

			[MapField("Height"), Parameter(600)]
			public int Height { get; set; }

			/*[MapField("CallDlgWidth"), Parameter(550)]
			public int CallDlgWidth { get; set; }

			[MapField("CallDlgHeight"), Parameter(230)]
			public int CallDlgHeight { get; set; }*/
		}

		public ClientParameters Params { get { return settings as ClientParameters; } }


		/// <summary>
		/// Initializes a new instance of the <see cref="IndexViewModel"/> class.
		/// </summary>
		public IndexViewModel() : base()
		{
			Model = HttpContext.Current.Session["Dummy"] as DummyEntity;
			if (Model == null)
			{
				Model = TypeAccessor<DummyEntity>.CreateInstanceEx();
				Model.ID = 1;
				Model.DocumentDate = DateTime.Now.AddYears(-20);
				Model.ChangedBy = 2877;
				Model.ChangedDate = DateTime.Now.AddYears(-1);
			}
			HttpContext.Current.Session["Dummy"] = Model; 
		}

		protected override void CreateSettings()
		{
			settings = TypeAccessor<ClientParameters>.CreateInstanceEx();
		}
	}
}