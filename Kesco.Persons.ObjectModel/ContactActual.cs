using System;
using BLToolkit.DataAccess;
using BLToolkit.Mapping;
using Kesco.ObjectModel;

namespace Kesco.Persons.ObjectModel
{
    /// <summary>
    /// КонтактыАктуальность
    /// </summary>
    [TableName("КонтактыАктуальность")]
    [System.ComponentModel.DataAnnotations.MetadataType(typeof(Metadata.ContactActual))]
    public class ContactActual : TrackableEntity<ContactActual, int>
    {

		/// <summary>
		/// Возвращает или устанавливает идентификатор лица,
		/// подтвердивший актуальность.
		/// </summary>
		/// <value>
		/// Идентификатор лица
		/// </value>
        [MapField("КодЛица")]
        [PrimaryKey, NonUpdatable(OnInsert = true)]
        public override int ID { get; set; }

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
