using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLToolkit.TypeBuilder;
using Kesco.ObjectModel;
using BLToolkit.Mapping;
using BLToolkit.Validation;

namespace Kesco.Persons.ObjectModel
{
	/// <summary>
	/// Карточка лица - базовый класс для физ. и юр. лиц
	/// </summary>
	[System.ComponentModel.DataAnnotations.MetadataType(typeof(Metadata.CardJ))]
	public abstract class PersonCard : TrackableEntity<PersonCard, int>
	{
		public static readonly DateTime MinFromDate = new DateTime(1980, 1, 1);
		public static readonly DateTime MaxToDate = new DateTime(2050, 1, 1);

		/// <summary>
		/// Код лица
		/// </summary>
		[MapField("КодЛица")]
		[Required]
		public int PersonID { get; set; }

		/// <summary>
		/// Связь КарточкиФизЛиц -> Лица
		/// </summary>
		[Association(ThisKey = "PersonID", OtherKey = "ID", CanBeNull = false)]
		public Person Person { get; set; }

		/// <summary>
		/// Дата начала периода действия карточки
		/// </summary>
		[MapField("От")]
		[Required]
		public DateTime? From { get; set; }

		/// <summary>
		/// Дата окончания периода действия карточки
		/// </summary>
		[MapField("До")]
		[Required]
		public DateTime? To { get; set; }

		/// <summary>
		/// Код организационно правовой формы
		/// </summary>
		[MapField("КодОргПравФормы"), Nullable]
		public int? IncorporationFormID { get; set; }

		/// <summary>
		/// Связь КарточкиФизЛиц -> ОргПравФормы
		/// </summary>
		[Association(ThisKey = "IncorporationFormID", OtherKey = "ID", CanBeNull = true)]
		public IncorporationForm IncorporationForm { get; set; }

		/// <summary>
		/// Общесоюзный классификатор Отрасли народного хозяйства
		/// </summary>
		[MapField("ОКОНХ")]
		[MaxLength(5)]
		[Required]
		public string OKONH { get; set; }

		/// <summary>
		/// Общероссийский классификатор видов экономической деятельности
		/// </summary>
		[MapField("ОКВЭД")]
		[MaxLength(8)]
		[Required]
		public string OKVED { get; set; }

		/// <summary>
		/// Код причины постановки на учёт
		/// </summary>
		[MapField("КПП")]
		[MaxLength(20), Parameter("")]
		[Required]
		public string KPP { get; set; }

		/// <summary>
		/// КодЖД
		/// </summary>
		[MapField("КодЖД")]
		[MaxLength(35)]
		[Required]
		public string RwID { get; set; }

		/// <summary>
		/// Адрес юридический
		/// </summary>
		[MapField("АдресЮридический")]
		[MaxLength(300), Parameter("")]
		[Required]
		public string AddressLegal { get; set; }

		/// <summary>
		/// Адрес юридический - англ. адрес (латинская транскрипция)
		/// </summary>
		[MapField("АдресЮридическийЛат")]
		[MaxLength(300), Parameter("")]
		[Required]
		public string AddressLegalLat { get; set; }

		/// <summary>
		/// Краткое название на русском языке
		/// </summary>
		[MapIgnore]
		public abstract string ShortName { get; }

		/// <summary>
		/// Карточка лица действительна на дату
		/// </summary>
		/// <param name="date">дата, на которую выполняется проверка</param>
		/// <returns>true - действует на дату</returns>
		public bool IsValidCard(DateTime date)
		{
			return From <= date && date <= To;
		}

		/// <summary>
		/// Возвращает отображаемое имя.
		/// </summary>
		/// <returns>Отображаемое имя.</returns>
		public override string GetInstanceFriendlyName()
		{
			return ShortName ?? String.Format("#{0}", GetUniqueID());
		}

		[MapIgnore]
		public abstract string NameRus { get; }

		[MapIgnore]
		public abstract string NameLat { get; }
	}
}
