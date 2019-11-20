using BLToolkit.DataAccess;
using BLToolkit.Mapping;

namespace Kesco.Persons.ObjectModel
{
    /// <summary>
    /// КаталогиЛиц
    /// </summary>
    [TableName("КаталогиЛиц")]
    [System.ComponentModel.DataAnnotations.MetadataType(typeof(Metadata.PersonCatalog))]
    public class PersonCatalog
    {
        /// <summary>
        /// КодЛица
        /// </summary>
        [MapField("КодЛица")]
        [NonUpdatable]
        public int PersonID { get; set; }

        /// <summary>
        /// Связь КаталогиЛиц -> Лица
        /// </summary>
        [Association(ThisKey = "PersonID", OtherKey = "ID", CanBeNull = false)]
        public Person Person { get; set; }

        /// <summary>
        /// КодКаталога
        /// </summary>
        [MapField("КодКаталога")]
        [NonUpdatable]
        public int CatalogID { get; set; }

        /// <summary>
        /// Связь КаталогиЛиц -> Каталоги
        /// </summary>
        [Association(ThisKey = "CatalogID", OtherKey = "ID", CanBeNull = false)]
        public Catalog Catalog { get; set; }

    }
}
