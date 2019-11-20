using BLToolkit.DataAccess;
using BLToolkit.Mapping;
using BLToolkit.Validation;
using Kesco.ObjectModel;

namespace Kesco.Persons.ObjectModel
{
	/// <summary>
	/// Карточка юридического лица
	/// </summary>
	[TableName("vwКарточкиЮрЛиц")]
	[System.ComponentModel.DataAnnotations.MetadataType(typeof(Metadata.CardJ))]
	public class PersonCardJuridical : PersonCard
	{
		/// <summary>
		/// Код карточки юридического лица
		/// </summary>
		[MapField("КодКарточкиЮрЛица"), PrimaryKey, NonUpdatable]
		public override int ID { get; set; }

		[MapIgnore]
		public override string ShortName
		{
			get { return (Person != null) ? Person.Nickname : ShortNameRus; }
		}

		/// <summary>
		/// Краткое название
		/// </summary>
		[MapField("КраткоеНазваниеРус")]
		[MaxLength(200)]
		[Required]
		public string ShortNameRus { get; set; }

		/// <summary>
		/// Краткое название в родительном падеже
		/// </summary>
		[MapField("КраткоеНазваниеРусРП")]
		[MaxLength(200)]
		[Required]
		public string ShortNameRusGen { get; set; }

		/// <summary>
		/// Краткоен название - английское (латинская транскрипция)
		/// </summary>
		[MapField("КраткоеНазваниеЛат")]
		[MaxLength(200)]
		[Required]
		public string ShortNameLat { get; set; }

		/// <summary>
		/// Полное название
		/// </summary>
		[MapField("ПолноеНазвание")]
		[MaxLength(300)]
		[Required]
		public string FullName { get; set; }


		[MapIgnore]
		public override string NameRus
		{
			get { return ShortNameRus; }
		}

		[MapIgnore]
		public override string NameLat
		{
			get { return ShortNameLat; }
		}
	}
}
