using BLToolkit.DataAccess;
using BLToolkit.Mapping;
using BLToolkit.Validation;
using Kesco.ObjectModel;
using System;

namespace Kesco.Persons.ObjectModel
{
    /// <summary>
    /// Класс для представления контактов лица
	/// Справочники.dbo.sp_Лица_Контакты 
    /// </summary>
	[TableName("vwКонтакты")]
	[ActionSprocName("Search", "sp_КонтактыЛица_Поиск")]
	[Trimmable(false)]
	public class PersonContact : Entity<PersonContact, int>
    {
        /// <summary>
        /// КодКонтакта
        /// </summary>
        [MapField("КодКонтакта")]
        [PrimaryKey, NonUpdatable]
        public override int ID { get; set; }

		/// <summary>
        /// КодТипаКонтакта
        /// </summary>
        [MapField("КодТипаКонтакта")]
        public int ContactTypeID { get; set; }

		/// <summary>
		/// Код Лица
		/// </summary>
		[MapField("КодЛица")]
		public int PersonID { get; set; }

		/// <summary>
		/// КодТипаКонтакта
		/// </summary>
		[MapField("ТипКонтакта")]
		public string ContactTypeDesc { get; set; }

        /// <summary>
        /// Контакт
        /// </summary>
        [MapField("Контакт")]
        public string ContactText { get; set; }

        /// <summary>
        /// ТелефонНомер
        /// </summary>
        [MapField("НомерМеждународный")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Примечание
        /// </summary>
        [MapField("Примечание")]
        [MaxLength(300)]
        public string Comment { get; set; }

		/// <summary>
		/// Gets or sets the order.
		/// </summary>
		/// <value>
		/// The order.
		/// </value>
		[MapField("Порядок")]
		public int Order { get; set; }

		/// <summary>
		/// Возвращает отображаемое имя.
		/// </summary>
		/// <returns>Отображаемое имя</returns>
		public override string GetInstanceFriendlyName()
		{
			return ContactText ?? String.Format("#{0}", GetUniqueID());
		}
	}
}
