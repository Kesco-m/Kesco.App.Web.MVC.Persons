using System;
using System.ComponentModel.DataAnnotations;
using Kesco.BusinessProjects.Controls.ComponentModel;
using Kesco.Web.Mvc.Filtering;


namespace Kesco.Persons.Web.Models.Search.Filtering
{
	[MetadataType(typeof(BusinessProjectFilterSetting.Metadata))]
	public class BusinessProjectFilterSetting : IntQSFilterSetting<BusinessProjectFilterSetting>
	{
		internal class Metadata
		{
			[BusinessProjectSelect]
			[BusinessProjectSelectSearchParameters(CLID = 20, Limit = 9)]
			public object Value { get; set; }
		}

		public bool SubBProject { get { return (bool)subBusinessProject.Value; } }

		public bool AllBProject { get { return (bool)allBusinessProject.Value; } }

		private BooleanQSFilterSetting subBusinessProject;

		private BooleanQSFilterSetting allBusinessProject;

		public BusinessProjectFilterSetting() : base(0, Int32.MaxValue)
		{
			subBusinessProject = BooleanQSFilterSetting.CreateInstance();
			allBusinessProject = BooleanQSFilterSetting.CreateInstance();
		}

		public BusinessProjectFilterSetting InitFromQS(string qsParamName, object defaultValue,
				string qsSubBProjectParamName, object subBProjectDefaultValue,
				string qsAllBProjectParamName, object allBProjectDefaultValue)
		{
			InitFromQueryString(qsParamName, defaultValue);
			subBusinessProject.InitFromQueryString(qsSubBProjectParamName, subBProjectDefaultValue);
			allBusinessProject.InitFromQueryString(qsAllBProjectParamName, allBProjectDefaultValue);
			return this;
		}

	}
}