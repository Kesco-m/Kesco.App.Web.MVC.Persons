using System;
using BLToolkit.DataAccess;
using BLToolkit.Mapping;
using BLToolkit.Validation;
using Kesco.ObjectModel;

namespace Kesco.Persons.ObjectModel
{
    /// <summary>
    /// vwСвидетельстваНП
    /// </summary>
    [TableName("vwСвидетельстваНП")]
    [System.ComponentModel.DataAnnotations.MetadataType(typeof(Metadata.DistributionCertificate))]
    public class DistributionCertificate : TrackableEntity<DistributionCertificate, int>
    {
        /// <summary>
        /// КодСвидетельстваНП
        /// </summary>
        [MapField("КодСвидетельстваНП")]
        [PrimaryKey, NonUpdatable]
        [Required("Значение поля КодСвидетельстваНП должно быть указано обязательно.")]
        public override int ID { get; set; }

        /// <summary>
        /// КодЛица
        /// </summary>
        [MapField("КодЛица")]
        public int PersonID { get; set; }

        /// <summary>
        /// Связь СвидетельстваНП -> Лица
        /// </summary>
        [Association(ThisKey = "PersonID", OtherKey = "ID", CanBeNull = false)]
        public Person Person { get; set; }

        /// <summary>
        /// От
        /// </summary>
        [MapField("От")]
        public DateTime From { get; set; }

        /// <summary>
        /// До
        /// </summary>
        [MapField("До")]
        public DateTime To { get; set; }

        /// <summary>
        /// Номер
        /// </summary>
        [MapField("Номер")]
        [MaxLength(100)]
        public string Number { get; set; }

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
			return Number ?? String.Format("#{0}", GetUniqueID());
		}

    }
}
