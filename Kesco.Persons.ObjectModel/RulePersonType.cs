using BLToolkit.DataAccess;
using BLToolkit.Mapping;
using Kesco.ObjectModel;
using System;

namespace Kesco.Persons.ObjectModel
{
    /// <summary>
    /// ПраваТипыЛиц
    /// </summary>
    [TableName("ПраваТипыЛиц")]
    public class RulePersonType : TrackableEntity<RulePersonType, int>
    {
        /// <summary>
        /// КодПраваТипыЛиц
        /// </summary>
        [MapField("КодПраваТипыЛиц")]
        [PrimaryKey, NonUpdatable]
        public override int ID { get; set; }

		/// <summary>
		/// КодСотрудника
		/// </summary>
		[MapField("КодСотрудника")]
		public int EmployeeID { get; set; }

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
        [MapField("КодТемыЛица")]
        public int PersonThemeID { get; set; }

		/// <summary>
		/// МожетДаватьПрава
		/// </summary>
		[MapField("МожетДаватьПрава")]
		public bool GrandAccessRule { get; set; }

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
