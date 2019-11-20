using BLToolkit.DataAccess;
using BLToolkit.Mapping;
using BLToolkit.Validation;
using Kesco.ObjectModel;
using System;

namespace Kesco.Persons.ObjectModel
{
    /// <summary>
    /// ТипыКонтактов
    /// </summary>
    [TableName("ТипыКонтактов")]
    [System.ComponentModel.DataAnnotations.MetadataType(typeof(Metadata.ContactType))]
    public class ContactType : Entity<ContactType, int>
    {
        /// <summary>
        /// КодТипаКонтакта
        /// </summary>
        [MapField("КодТипаКонтакта")]
        [PrimaryKey, NonUpdatable]
        public override int ID { get; set; }

        /// <summary>
        /// ТипКонтакта
        /// </summary>
        [MapField("ТипКонтакта")]
        [MaxLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// ТипКонтактаЛат
        /// </summary>
        [MapField("ТипКонтактаЛат")]
        [MaxLength(50)]
        public string NameLat { get; set; }

        /// <summary>
        /// Категория
        /// </summary>
        [MapField("Категория")]
        public int Category { get; set; }

        /// <summary>
        /// icon
        /// </summary>
        [MapField("icon")]
        [MaxLength(50)]
        public string Icon { get; set; }
		
		/// <summary>
		/// Возвращает отображаемое имя.
		/// </summary>
		/// <returns>Отображаемое имя.</returns>
		public override string GetInstanceFriendlyName()
		{
			// TODO: Текущий язык + Транслитерация
			return Name ?? NameLat ?? String.Format("#{0}", GetUniqueID());
		}
	}
}
