using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLToolkit.DataAccess;
using Kesco.ObjectModel;
using BLToolkit.Mapping;
using BLToolkit.Validation;

namespace Kesco.Persons.ObjectModel
{
	[TableName("vwТемыЛиц")]
	[System.ComponentModel.DataAnnotations.MetadataType(typeof(Metadata.PersonTheme))]
	public class PersonTheme : TrackableTreeEntity<PersonTheme, int>
	{
		/// <summary>
		/// КодТемыЛица
		/// </summary>
		[MapField("КодТемыЛица")]
		[PrimaryKey, NonUpdatable]
		public override int ID { get; set; }

		/// <summary>
		/// ТемаЛица
		/// </summary>
		[MapField("ТемаЛица")]
		[MaxLength(100)]
		public string Name { get; set; }

		/// <summary>
		/// Возвращает отображаемое имя.
		/// </summary>
		/// <returns>Отображаемое имя</returns>
		public override string GetInstanceFriendlyName()
		{
			return Name ?? String.Format("#{0}", GetUniqueID());
		}

	}
}
