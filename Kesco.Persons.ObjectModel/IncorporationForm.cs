using BLToolkit.DataAccess;
using BLToolkit.Mapping;
using BLToolkit.Validation;
using Kesco.ObjectModel;
using System;

namespace Kesco.Persons.ObjectModel
{
	/// <summary>
	/// ОргПравФормы
	/// </summary>
	/// <remarks></remarks>
	[TableName("ОргПравФормы")]
	[System.ComponentModel.DataAnnotations.MetadataType(typeof(Metadata.IncorporationForm))]
	public class IncorporationForm : Entity<IncorporationForm, int>
	{
		/// <summary>
		/// КодОргПравФормы
		/// </summary>
		[MapField("КодОргПравФормы")]
		[PrimaryKey, NonUpdatable]
		public override int ID { get; set; }

		/// <summary>
		/// ОргПравФорма
		/// </summary>
		[MapField("ОргПравФорма")]
		[MaxLength(100)]
		public string Name { get; set; }

		/// <summary>
		/// КраткоеНазвание
		/// </summary>
		[MapField("КраткоеНазвание")]
		[MaxLength(10)]
		public string ShortName { get; set; }

		/// <summary>
		/// ТипЛица
		/// </summary>
		[MapField("ТипЛица")]
		public byte PersonType { get; set; }

		/// <summary>
		/// Возвращает отображаемое имя.
		/// </summary>
		/// <returns>Отображаемое имя</returns>
		public override string GetInstanceFriendlyName()
		{
			return ShortName ?? Name ?? String.Format("#{0}", GetUniqueID());
		}
	}
}
