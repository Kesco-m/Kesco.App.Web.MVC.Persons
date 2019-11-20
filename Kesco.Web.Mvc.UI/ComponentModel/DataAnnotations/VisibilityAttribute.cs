using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Kesco.Web.Mvc.ComponentModel.DataAnnotations
{

	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public class VisibilityAttribute : Attribute, IMetadataAware
	{
		public bool ShowForEdit { get; set; }
		public bool ShowForDisplay { get; set; }

		public VisibilityAttribute(): base() {
			ShowForEdit = true;
			ShowForDisplay = true;
		}

		public void OnMetadataCreated(ModelMetadata metadata)
		{
			if (metadata == null) 
				throw new ArgumentNullException("metadata");
			
			metadata.ShowForDisplay = ShowForDisplay;
			metadata.ShowForEdit = ShowForEdit;
		}
	}
}
