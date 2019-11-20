using System;
using BLToolkit.DataAccess;
using BLToolkit.Mapping;
using BLToolkit.Validation;
using Kesco.ObjectModel;

namespace Kesco.Persons.ObjectModel
{
    /// <summary>
    /// vwПодразделенияЛиц
    /// </summary>
    [TableName("vwПодразделенияЛиц")]
    [System.ComponentModel.DataAnnotations.MetadataType(typeof(Metadata.Department))]
    public class Department : TrackableTreeEntity<Department, int>
    {
        /// <summary>
        /// КодПодразделенияЛица
        /// </summary>
        [MapField("КодПодразделенияЛица")]
        [PrimaryKey, NonUpdatable]
        public override int ID { get; set; }

        /// <summary>
        /// КодЛица
        /// </summary>
        [MapField("КодЛица")]
        public int PersonID { get; set; }

        /// <summary>
        /// Связь ПодразделенияЛиц -> Лица
        /// </summary>
        [Association(ThisKey = "PersonID", OtherKey = "ID", CanBeNull = false)]
        public Person Person { get; set; }

        /// <summary>
        /// Подразделение
        /// </summary>
        [MapField("Подразделение")]
        [MaxLength(50)]
        public string DepartmentName { get; set; }

        /// <summary>
        /// ПодразделениеЛат
        /// </summary>
        [MapField("ПодразделениеЛат")]
        [MaxLength(50)]
        public string DepartmentNameLat { get; set; }

        /// <summary>
        /// Связь ПодразделенияЛиц -> ПодразделенияЛиц
        /// </summary>
        [Association(ThisKey = "Parent", OtherKey = "ID", CanBeNull = true)]
        public Department ParentDepartment { get; set; }

		/// <summary>
		/// Возвращает отображаемое имя
		/// </summary>
		/// <returns>Отображаемое имя</returns>
		public override string GetInstanceFriendlyName()
		{
			return DepartmentName ?? DepartmentNameLat ?? String.Format("#{0}", GetUniqueID());
		}

    }
}
