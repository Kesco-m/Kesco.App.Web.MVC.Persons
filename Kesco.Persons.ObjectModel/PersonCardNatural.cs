using BLToolkit.DataAccess;
using BLToolkit.Mapping;
using BLToolkit.TypeBuilder;
using BLToolkit.Validation;
using System.Text.RegularExpressions;
using System.Threading;

namespace Kesco.Persons.ObjectModel
{
	/// <summary>
	/// Карточка физического лица
	/// </summary>
	[TableName("vwКарточкиФизЛиц")]
	[System.ComponentModel.DataAnnotations.MetadataType(typeof(Metadata.CardN))]
	public class PersonCardNatural : PersonCard
	{
		static Regex spaceRemover = new Regex("^[ ]+|[ ]+$");

		/// <summary>
		/// Код карточки физического лица
		/// </summary>
		[MapField("КодКарточкиФизЛица"), PrimaryKey, NonUpdatable]
		public override int ID { get; set; }

		[MapIgnore]
		public override string ShortName
		{
			get { return (Person != null) ? Person.Nickname : FIORus; }
		}

		/// <summary>
		/// Фамилия
		/// </summary>
		[MapField("ФамилияРус")]
		[MaxLength(50), Parameter("")]
		[Required]
		public string LastNameRus { get; set; }

		/// <summary>
		/// Имя
		/// </summary>
		[MapField("ИмяРус")]
		[MaxLength(50), Parameter("")]
		[Required]
		public string FirstNameRus { get; set; }

		/// <summary>
		/// Отчество
		/// </summary>
		[MapField("ОтчествоРус")]
		[MaxLength(50), Parameter("")]
		[Required]
		public string MiddleNameRus { get; set; }

		/// <summary>
		/// ФИО
		/// </summary>
		[MapField("ФИОРус")]
		[MaxLength(55), Parameter("")]
		public string FIORus { get; set; }

		/// <summary>
		/// ИОФ
		/// </summary>
		[MapField("ИОФРус")]
		[MaxLength(56), Parameter("")]
		public string IOFRus { get; set; }

		/// <summary>
		/// Фамилия - английская (латинская транскрипция)
		/// </summary>
		[MapField("ФамилияЛат")]
		[MaxLength(50), Parameter("")]
		[Required]
		public string LastNameLat { get; set; }

		/// <summary>
		/// Имя- английское (латинская транскрипция)
		/// </summary>
		[MapField("ИмяЛат")]
		[MaxLength(50), Parameter("")]
		[Required]
		public string FirstNameLat { get; set; }

		/// <summary>
		/// Отчество - английское (латинская транскрипция)
		/// </summary>
		[MapField("ОтчествоЛат")]
		[MaxLength(50), Parameter("")]
		[Required]
		public string MiddleNameLat { get; set; }

		/// <summary>
		/// Пол
		/// </summary>
		[MapField("Пол")]
		[MaxLength(1)]
		[Required]
		public char Sex { get; set; }

		/// <summary>
		/// Получение полного наименования лица
		/// Выводится ИП для физ. лиц, при такой ОП форме
		/// </summary>
		[MapIgnore]
		public override string NameRus
		{
			get
			{
				if (LastNameRus.Length + FirstNameRus.Length + MiddleNameRus.Length == 0) return "";

				string _form = (IncorporationFormID == 91 && IncorporationForm != null) ? IncorporationForm.Name + " " : "";
				return _form + spaceRemover.Replace(LastNameRus + " " + FirstNameRus + " " + MiddleNameRus, "");
			}
		}

		[MapIgnore]
		public override string NameLat { get { return spaceRemover.Replace(LastNameLat + " " + FirstNameLat + " " + MiddleNameLat, ""); } }
	}
}
