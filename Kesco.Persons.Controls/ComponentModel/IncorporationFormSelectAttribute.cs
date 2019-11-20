using System.Web.Mvc;
using Kesco.Persons.BusinessLogic;

namespace Kesco.Persons.Controls.ComponentModel
{
	public class IncorporationFormSelectAttribute : DropDownAttribute
	{

		public int PersonKind { get; set; }

		public IncorporationFormSelectAttribute() : base("IncorporationFormSelect") {
			PersonKind = 0;
		}

		public override object GetOptions()
		{
			return Repository.IncorporationForms.Search(new BusinessLogic.DataAccess.IncorporationFormAccessor.SearchParameters {
				PersonKind = PersonKind
			});
		}

		#region Члены IMetadataAware

		public override void OnMetadataCreated(ModelMetadata metadata)
		{
			base.OnMetadataCreated(metadata);
		}

		#endregion

	}
}