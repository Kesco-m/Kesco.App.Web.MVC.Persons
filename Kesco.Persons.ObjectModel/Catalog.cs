using System;
using BLToolkit.DataAccess;
using BLToolkit.Mapping;
using BLToolkit.Validation;
using Kesco.ObjectModel;

namespace Kesco.Persons.ObjectModel
{
    /// <summary>
    /// vwКаталоги
    /// </summary>
    [TableName("vwКаталоги")]
    [System.ComponentModel.DataAnnotations.MetadataType(typeof(Metadata.Catalog))]
    public class Catalog : Entity<Catalog, int>
    {
        /// <summary>
        /// КодКаталога
        /// </summary>
        [MapField("КодКаталога")]
        [PrimaryKey, NonUpdatable]
        public override int ID { get; set; }

        /// <summary>
        /// Каталог
        /// </summary>
        [MapField("Каталог")]
        [MaxLength(100)]
        public string CatalogName { get; set; }

		/// <summary>
		/// Возвращает отображаемое имя.
		/// </summary>
		/// <returns>Отображаемое имя.</returns>
		public override string GetInstanceFriendlyName()
		{
			return CatalogName ?? String.Format("#{0}", GetUniqueID());
		}
	}
}
