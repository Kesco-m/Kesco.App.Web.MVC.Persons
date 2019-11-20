using System;
using BLToolkit.DataAccess;
using BLToolkit.Mapping;
using BLToolkit.Validation;
using Kesco.ObjectModel;

namespace Kesco.Persons.ObjectModel
{
    /// <summary>
    /// vwКонтакты
    /// </summary>
    [TableName("vwКонтакты")]
    [System.ComponentModel.DataAnnotations.MetadataType(typeof(Metadata.Contact))]
    public class Contact : TrackableEntity<Contact, int>
    {
        /// <summary>
        /// КодКонтакта
        /// </summary>
        [MapField("КодКонтакта")]
        [PrimaryKey, NonUpdatable]
        public override int ID { get; set; }

        /// <summary>
        /// КодЛица
        /// </summary>
        [MapField("КодЛица")]
        public int? PersonID { get; set; }

        /// <summary>
        /// Связь Контакты -> Лица
        /// </summary>
        [Association(ThisKey = "PersonID", OtherKey = "ID", CanBeNull = false)]
        public Person Person { get; set; }

        /// <summary>
        /// КодСвязиЛиц
        /// </summary>
        [MapField("КодСвязиЛиц")]
        public int? PersonLinkID { get; set; }

        /// <summary>
        /// КодТипаКонтакта
        /// </summary>
        [MapField("КодТипаКонтакта")]
        public int ContactTypeID { get; set; }

        /// <summary>
        /// Связь Контакты -> ТипыКонтактов
        /// </summary>
        [Association(ThisKey = "ContactTypeID", OtherKey = "ID", CanBeNull = false)]
        public ContactType ContactType { get; set; }

        /// <summary>
        /// Контакт
        /// </summary>
        [MapField("Контакт")]
        [MaxLength(300)]
		[NonUpdatable]
        public string ContactText { get; set; }

        /// <summary>
        /// КонтактRL
        /// </summary>
        [MapField("КонтактRL")]
        [MaxLength(300)]
		[NonUpdatable]
		public string ContactTextRL { get; set; }

        /// <summary>
        /// КодСтраны
        /// </summary>
        [MapField("КодСтраны")]
        public int CountryID { get; set; }

        /// <summary>
        /// АдресИндекс
        /// </summary>
        [MapField("АдресИндекс")]
        [MaxLength(6)]
        public string Zip { get; set; }

        /// <summary>
        /// АдресОбласть
        /// </summary>
        [MapField("АдресОбласть")]
        [MaxLength(50)]
        public string Region { get; set; }

        /// <summary>
        /// АдресГород
        /// </summary>
        [MapField("АдресГород")]
        [MaxLength(50)]
        public string CityName { get; set; }

        /// <summary>
        /// АдресГородRus
        /// </summary>
        [MapField("АдресГородRus")]
        [MaxLength(50)]
        public string CityNameRus { get; set; }

        /// <summary>
        /// Адрес
        /// </summary>
        [MapField("Адрес")]
        [MaxLength(300)]
        public string Address { get; set; }

        /// <summary>
        /// ТелефонСтрана
        /// </summary>
        [MapField("ТелефонСтрана")]
        [MaxLength(6)]
        public string CountryPhoneCode { get; set; }

        /// <summary>
        /// ТелефонГород
        /// </summary>
        [MapField("ТелефонГород")]
        [MaxLength(6)]
        public string CityPhoneCode { get; set; }

        /// <summary>
        /// ТелефонНомер
        /// </summary>
        [MapField("ТелефонНомер")]
        [MaxLength(40)]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// ТелефонДоп
        /// </summary>
        [MapField("ТелефонДоп")]
        [MaxLength(10)]
        public string PhoneNumberAdd { get; set; }

        /// <summary>
        /// ТелефонКорпНомер
        /// </summary>
        [MapField("ТелефонКорпНомер")]
        [MaxLength(6)]
        public string PhoneNumberCorporative { get; set; }

        /// <summary>
        /// ДругойКонтакт
        /// </summary>
        [MapField("ДругойКонтакт")]
        [MaxLength(120)]
        public string OtherContact { get; set; }

        /// <summary>
        /// Примечание
        /// </summary>
        [MapField("Примечание")]
        [MaxLength(300)]
        public string Comment { get; set; }

		/// <summary>
		/// Возвращает отображаемое имя.
		/// </summary>
		/// <returns>Отображаемое имя.</returns>
		public override string GetInstanceFriendlyName()
		{
			return ContactText ?? String.Format("#{0}", GetUniqueID());
		}
	}
}
