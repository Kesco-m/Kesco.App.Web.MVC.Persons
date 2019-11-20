using System;
using System.Globalization;
using System.ComponentModel.DataAnnotations;
using BLToolkit.DataAccess;
using BLToolkit.Mapping;
using Kesco.ObjectModel;

namespace Kesco.Territories.ObjectModel
{
	/// <summary>
	/// Территории
	/// </summary>
	[TableName("Территории")]
	[MetadataType(typeof(Metadata.Territory))]
	public class Territory : TrackableTreeEntity<Territory, int>
	{
		public const int Russia = 188;

		/// <summary>
		/// КодТерритории
		/// </summary>
		[MapField("КодТерритории")]
		[PrimaryKey, NonUpdatable]
		public override int ID { get; set; }

		/// <summary>
		/// Территория
		/// </summary>
		[MapField("Территория")]
		public string Name { get; set; }

		/// <summary>
		/// Территория
		/// </summary>
		[MapField("Caption")]
		public string Caption { get; set; }

		/// <summary>
		/// Возвращает отображаемое имя для территории.
		/// </summary>
		/// <returns>Отображаемое имя для территории</returns>
		public override string GetInstanceFriendlyName()
		{
			return Name ?? String.Format("#{0}", GetUniqueID());
		}
	}

}
