using System;
using BLToolkit.DataAccess;
using BLToolkit.Mapping;
using BLToolkit.Validation;
using Kesco.ObjectModel;

namespace Kesco.Persons.ObjectModel
{
    /// <summary>
	/// Слова в дательном и родительном падежах.
    /// </summary>
    [TableName("Падежи")]
    [System.ComponentModel.DataAnnotations.MetadataType(typeof(Metadata.Case))]
    public class Case : TrackableEntity<Case, int>
    {
        /// <summary>
        /// Код
        /// </summary>
        [MapField("Код")]
        [PrimaryKey, NonUpdatable]
        public override int ID { get; set; }

        /// <summary>
        /// Именительный
        /// </summary>
        [MapField("Именительный")]
        [MaxLength(50)]
        public string Nominative { get; set; }

        /// <summary>
        /// Дательный
        /// </summary>
        [MapField("Дательный")]
        [MaxLength(50)]
        public string Dative { get; set; }

        /// <summary>
        /// Родительный
        /// </summary>
        [MapField("Родительный")]
        [MaxLength(50)]
        public string Genitive { get; set; }

		/// <summary>
		/// Возвращает отображаемое имя.
		/// </summary>
		/// <returns>Отображаемое имя</returns>
		public override string GetInstanceFriendlyName()
		{
			return Nominative ?? String.Format("#{0}", GetUniqueID());
		}

    }
}
