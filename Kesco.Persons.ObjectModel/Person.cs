using System;
using System.Collections.Generic;
using BLToolkit.DataAccess;
using BLToolkit.Mapping;
using BLToolkit.TypeBuilder;
using BLToolkit.Validation;
using Kesco.ObjectModel;

namespace Kesco.Persons.ObjectModel
{

	public enum PersonCardType : byte
	{
		Juridical = 1,
		Natural = 2
	}

    /// <summary>
    /// vwЛица
    /// </summary>
    [TableName("vwЛица")]
	[ActionSprocName("Search", "sp_Лица_Поиск")]
	[System.ComponentModel.DataAnnotations.MetadataType(typeof(Metadata.Person))]
    public class Person : TrackableEntity<Person, int>
    {
        /// <summary>
        /// КодЛица
        /// </summary>
        [MapField("КодЛица")]
        [PrimaryKey, NonUpdatable]
        public override int ID { get; set; }

        /// <summary>
        /// ТипЛица
        /// </summary>
        [MapField("ТипЛица")]
		[MapValue(PersonCardType.Juridical, 1)]
		[MapValue(PersonCardType.Natural, 2)]
		[Required]
		public PersonCardType PersonType { get; set; }

    	/// <summary>
    	/// Связь Лица-КарточкиЮрЛиц
    	/// </summary>
    	[Association(ThisKey = "КодЛица", OtherKey = "КодЛица", CanBeNull = false)]
		public List<PersonCardJuridical> PersonCardJuridicals { get; set; }

		/// <summary>
		/// Связь Лица-КарточкиФизЛиц
		/// </summary>
		[Association(ThisKey = "КодЛица", OtherKey = "КодЛица", CanBeNull = false)]
		public List<PersonCardNatural> PersonCardNaturals { get; set; }

			/// <summary>
        /// КодБизнесПроекта
        /// </summary>
        [MapField("КодБизнесПроекта")]
        public int? BusinessProjectID { get; set; }

        /// <summary>
        /// Проверено
        /// </summary>
        [MapField("Проверено")]
		[Required]
        public bool Verified { get; set; }

        /// <summary>
        /// Кличка
        /// </summary>
        [MapField("Кличка")]
        [MaxLength(50)]
        [Required]
        public string Nickname { get; set; }

        /// <summary>
        /// КличкаRL
        /// </summary>
        [MapField("КличкаRL")]
        [MaxLength(50)]
        public string NicknameRL { get; set; }

        /// <summary>
        /// НазваниеRL
        /// </summary>
        [MapField("НазваниеRL")]
        [MaxLength(400)]
        public string NameRL { get; set; }

        /// <summary>
        /// КодТерритории
        /// </summary>
        [MapField("КодТерритории")]
        public int? TerritoryID { get; set; }

        /// <summary>
        /// ГосОрганизация
        /// </summary>
        [MapField("ГосОрганизация")]
		[MapValue(true, 1)]
		[MapValue(false, 0)]
		[Required]
		public bool IsStateOrganization { get; set; }

        /// <summary>
        /// ИНН
        /// </summary>
        [MapField("ИНН")]
        [MaxLength(20)]
		[Required]
        public string INN { get; set; }

        /// <summary>
        /// ОГРН
        /// </summary>
        [MapField("ОГРН")]
        [MaxLength(20)]
        public string OGRN { get; set; }

        /// <summary>
        /// ОКПО
        /// </summary>
        [MapField("ОКПО")]
        [MaxLength(20)]
        public string OKPO { get; set; }

        /// <summary>
        /// БИК
        /// </summary>
        [MapField("БИК")]
        [MaxLength(15)]
        public string BIK { get; set; }

        /// <summary>
        /// КорСчет
        /// </summary>
        [MapField("КорСчет")]
        [MaxLength(20)]
        public string LoroConto { get; set; }

        /// <summary>
        /// БИКРКЦ
        /// </summary>
        [MapField("БИКРКЦ")]
        [MaxLength(9)]
        public string BIKRKC { get; set; }

        /// <summary>
        /// SWIFT
        /// </summary>
        [MapField("SWIFT")]
        [MaxLength(15)]
        public string SWIFT { get; set; }

		/// <summary>
		/// ДатаРождения
		/// </summary>
		[MapField("ДатаРождения")]
		public DateTime? Birthday { get; set; }

		/// <summary>
		/// Примечание
		/// </summary>
		[MapField("Примечание")]
		[MaxLength(300)]
		public string Comment { get; set; }

		/// <summary>
		/// Возвращает отображаемое имя.
		/// </summary>
		/// <returns>Отображаемое имя</returns>
		public override string GetInstanceFriendlyName()
		{
			return Nickname ?? String.Format("#{0}", GetUniqueID());
		}

    }

	[TableName("vwЛица")]
	public class PersonSimple : Entity<PersonSimple, int>
	{
		/// <summary>
		/// КодЛица
		/// </summary>
		[MapField("КодЛица")]
		[PrimaryKey, NonUpdatable]
		public override int ID { get; set; }

		/// <summary>
		/// Кличка
		/// </summary>
		[MapField("Кличка")]
		[MaxLength(50)]
		[Required]
		public string Nickname { get; set; }

		/// <summary>
		/// ИНН
		/// </summary>
		[MapField("ИНН")]
		[MaxLength(20)]
		[Required]
		public string INN { get; set; }

		/// <summary>
		/// Возвращает отображаемое имя.
		/// </summary>
		/// <returns>Отображаемое имя</returns>
		public override string GetInstanceFriendlyName()
		{
			return Nickname ?? String.Format("#{0}", GetUniqueID());
		}

	}

}