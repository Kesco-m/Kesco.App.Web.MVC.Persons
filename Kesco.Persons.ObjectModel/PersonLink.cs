using System;
using BLToolkit.DataAccess;
using BLToolkit.Mapping;
using BLToolkit.Validation;
using Kesco.ObjectModel;

namespace Kesco.Persons.ObjectModel
{
    /// <summary>
    /// vwСвязиЛиц
    /// </summary>
    [TableName("vwСвязиЛиц")]
    [System.ComponentModel.DataAnnotations.MetadataType(typeof(Metadata.PersonLink))]
    public class PersonLink : TrackableEntity<PersonLink, int>
    {
        /// <summary>
        /// КодСвязиЛиц
        /// </summary>
        [MapField("КодСвязиЛиц")]
        [PrimaryKey, NonUpdatable]
        public override int ID { get; set; }

        /// <summary>
        /// КодТипаСвязиЛиц
        /// </summary>
        [MapField("КодТипаСвязиЛиц")]
        public int PersonLinkTypeID { get; set; }

        /// <summary>
        /// Связь СвязиЛиц -> ТипыСвязейЛиц
        /// </summary>
        [Association(ThisKey = "PersonLinkTypeID", OtherKey = "ID", CanBeNull = false)]
        public PersonLinkType PersonLinkType { get; set; }


        [MapField("От")]
        public DateTime From { get; set; }


        [MapField("До")]
        public DateTime To { get; set; }

        /// <summary>
        /// КодЛицаРодителя
        /// </summary>
        [MapField("КодЛицаРодителя")]
        public int ParentPersonID { get; set; }

        /// <summary>
        /// Связь КодЛицаРодителя -> Лица
        /// </summary>
        [Association(ThisKey = "ParentPersonID", OtherKey = "ID", CanBeNull = false)]
        public Person ParentPerson { get; set; }

        /// <summary>
        /// КодЛицаПотомка
        /// </summary>
        [MapField("КодЛицаПотомка")]
        public int ChildPersonID { get; set; }

        /// <summary>
        /// Связь КодЛицаПотомка -> Лица
        /// </summary>
        [Association(ThisKey = "ChildPersonID", OtherKey = "ID", CanBeNull = false)]
        public Person ChildPerson { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        [MapField("Описание")]
        [MaxLength(300)]
        public string Description { get; set; }

        /// <summary>
        /// Параметр
        /// </summary>
        [MapField("Параметр")]
        public int Parameter { get; set; }

		/// <summary>
		/// Возвращает отображаемое имя.
		/// </summary>
		/// <returns>Отображаемое имя</returns>
		public override string GetInstanceFriendlyName()
		{
			return Description ?? String.Format("#{0}", GetUniqueID());
		}
        

    }
}
