using BLToolkit.DataAccess;
using BLToolkit.Mapping;
using BLToolkit.Validation;
using Kesco.ObjectModel;
using System;

namespace Kesco.Persons.ObjectModel
{
    /// <summary>
    /// ТипыСвязейЛиц
    /// </summary>
    [TableName("ТипыСвязейЛиц")]
    [System.ComponentModel.DataAnnotations.MetadataType(typeof(Metadata.PersonLinkType))]
    public class PersonLinkType : Entity<PersonLinkType, int>
    {
        /// <summary>
        /// КодТипаСвязиЛиц
        /// </summary>
        [MapField("КодТипаСвязиЛиц")]
        [PrimaryKey, NonUpdatable]
        public override int ID { get; set; }

        /// <summary>
        /// ТипЛицаРодителя
        /// </summary>
        [MapField("ТипЛицаРодителя")]
        public byte ParentPersonType { get; set; }

        /// <summary>
        /// ТипЛицаПотомка
        /// </summary>
        [MapField("ТипЛицаПотомка")]
        public byte ChildPersonType { get; set; }

        /// <summary>
        /// НазваниеРодителяЕЧИмП
        /// </summary>
        [MapField("НазваниеРодителяЕЧИмП")]
        [MaxLength(50)]
        public string ParentNameSingularNominative { get; set; }

        /// <summary>
        /// НазваниеПотомкаЕЧИмП
        /// </summary>
        [MapField("НазваниеПотомкаЕЧИмП")]
        [MaxLength(50)]
        public string ChildNameSingularNominative { get; set; }

        /// <summary>
        /// НазваниеРодителяМЧИмП
        /// </summary>
        [MapField("НазваниеРодителяМЧИмП")]
        [MaxLength(50)]
        public string ParentNamePluralNominative { get; set; }

        /// <summary>
        /// НазваниеПотомкаМЧИмП
        /// </summary>
        [MapField("НазваниеПотомкаМЧИмП")]
        [MaxLength(50)]
        public string ChildNamePluralNominative { get; set; }

        /// <summary>
        /// НазваниеРодителяЕЧВинП
        /// </summary>
        [MapField("НазваниеРодителяЕЧВинП")]
        [MaxLength(50)]
        public string ParentNameSingularAccusative { get; set; }

        /// <summary>
        /// НазваниеПотомкаЕЧВинП
        /// </summary>
        [MapField("НазваниеПотомкаЕЧВинП")]
        [MaxLength(50)]
        public string ParentNamePluralAccusative { get; set; }

        /// <summary>
        /// НазваниеРодителяМЧЛат
        /// </summary>
        [MapField("НазваниеРодителяМЧЛат")]
        [MaxLength(50)]
        public string ParentNamePluralLat { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        [MapField("Описание")]
        [MaxLength(300)]
        public string Description { get; set; }

		/// <summary>
		/// Возвращает отображаемое имя.
		/// </summary>
		/// <returns>Отображаемое имя.</returns>
		public override string GetInstanceFriendlyName()
		{
			return String.Format(
					"#{0}: {1} <-> {2}", 
					GetUniqueID(), 
					ParentNameSingularNominative, 
					ChildNameSingularNominative
				);
		}

	}
}
