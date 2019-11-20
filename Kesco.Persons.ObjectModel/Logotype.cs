using System;
using BLToolkit.DataAccess;
using BLToolkit.Mapping;
using Kesco.ObjectModel;

namespace Kesco.Persons.ObjectModel
{
    /// <summary>
    /// ЛоготипыЛиц
    /// </summary>
    [TableName("ЛоготипыЛиц")]
    [System.ComponentModel.DataAnnotations.MetadataType(typeof(Metadata.Logotype))]
    public class Logotype : TrackableEntity<Logotype, int>
    {
        /// <summary>
        /// КодЛоготипаЛица
        /// </summary>
        [MapField("КодЛоготипаЛица")]
        [PrimaryKey, NonUpdatable]
        public override int ID { get; set; }

        /// <summary>
        /// КодЛица
        /// </summary>
        [MapField("КодЛица")]
        public int PersonID { get; set; }

        /// <summary>
        /// Связь ЛоготипыЛиц -> Лица
        /// </summary>
        [Association(ThisKey = "PersonID", OtherKey = "ID", CanBeNull = false)]
        public Person Person { get; set; }

        /// <summary>
        /// ДатаСохранения
        /// </summary>
        [MapField("ДатаСохранения")]
        public DateTime SaveDate { get; set; }

        /// <summary>
        /// Логотип
        /// </summary>
        [MapField("Логотип")]
        public byte[] Логотип { get; set; }

        /// <summary>
        /// ВерхнийКолонтитул
        /// </summary>
        [MapField("ВерхнийКолонтитул")]
        public byte[] Header { get; set; }

        /// <summary>
        /// НижнийКолонтитул
        /// </summary>
        [MapField("НижнийКолонтитул")]
        public byte[] Footer { get; set; }

		/// <summary>
		/// Возвращает отображаемое имя.
		/// </summary>
		/// <returns>Отображаемое имя.</returns>
		public override string GetInstanceFriendlyName()
		{
			return String.Format("#{0}", GetUniqueID());
		}

    }
}
