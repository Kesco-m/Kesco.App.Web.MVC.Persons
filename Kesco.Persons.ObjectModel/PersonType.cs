using BLToolkit.DataAccess;
using BLToolkit.Mapping;
using Kesco.ObjectModel;
using System;

namespace Kesco.Persons.ObjectModel
{
    /// <summary>
    /// ТипыЛиц
    /// </summary>
    [TableName("ТипыЛиц")]
    [System.ComponentModel.DataAnnotations.MetadataType(typeof(Metadata.PersonType))]
    public class PersonType : TrackableEntity<PersonType, int>
    {
        /// <summary>
        /// КодТипаЛица
        /// </summary>
        [MapField("КодТипаЛица")]
        [PrimaryKey, NonUpdatable]
        public override int ID { get; set; }

        /// <summary>
        /// КодКаталога
        /// </summary>
        [MapField("КодКаталога")]
        public int CatalogID { get; set; }

        /// <summary>
        /// Связь ТипыЛиц -> Каталоги
        /// </summary>
        [Association(ThisKey = "CatalogID", OtherKey = "ID", CanBeNull = false)]
        public Catalog Catalog { get; set; }

        /// <summary>
        /// КодТемыЛица
        /// </summary>
        [Kesco.Persons.Controls.ComponentModel.PersonThemeSelect]
        [MapField("КодТемыЛица")]
        public int PersonThemeID { get; set; }

		/// <summary>
		/// Возвращает отображаемое имя для типа лица.
		/// </summary>
		/// <returns>Отображаемое имя для типа лица.</returns>
		public override string GetInstanceFriendlyName()
		{

			return String.Format("#{0}", GetUniqueID());
		}
	}
}
