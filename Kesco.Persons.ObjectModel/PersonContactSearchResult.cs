using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLToolkit.DataAccess;
using BLToolkit.Mapping;
using BLToolkit.Validation;
using Kesco.ObjectModel;

namespace Kesco.Persons.ObjectModel
{
    /// <summary>
    /// Класс для представления контактов лица
	/// Справочники.dbo.sp_Лица_Контакты_Поиск 
    /// </summary>
	public class PersonContactSearchResult : Entity<PersonContactSearchResult, int>
    {
        /// <summary>
        /// КодКонтакта
        /// </summary>
        [MapField("КодКонтакта")]
        [PrimaryKey, NonUpdatable]
        public override int ID { get; set; }

		/// <summary>
		/// Контакт
		/// </summary>
		[MapField("icon")]
		public string Icon { get; set; }
		
		/// <summary>
        /// КодТипаКонтакта
        /// </summary>
        [MapField("КодТипаКонтакта")]
        public int ContactTypeID { get; set; }

		/// <summary>
		/// КодТипаКонтакта
		/// </summary>
		[MapField("ТипКонтакта")]
		public string ContactTypeDesc { get; set; }

        /// <summary>
        /// Контакт
        /// </summary>
        [MapField("Контакт")]
        public string ContactText { get; set; }

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
			return ContactText ?? String.Format("#{0}", GetUniqueID());
		}

	}

	/// <summary>
	/// Описание результатов запроса по контактам лица + связанным контактам лица
	/// ContactAccessor.GetContactsForPersonLinks(...)
	/// </summary>
	public class PersonLinkedContact : TrackableEntity<PersonLinkedContact, int>
	{
		/// <summary>
		/// КодКонтакта
		/// </summary>
		[MapField("КодКонтакта")]
		public override int ID { get; set; }

		/// <summary>
		/// Код Лица
		/// </summary>
		[MapField("КодЛица")]
		public int PersonID { get; set; }

		[MapField("КодСвязиЛиц")]
		public int PersonLinkID { get; set; }

		[MapField("КодЛицаСвязи")]
		public int LinkedPersonParentID { get; set; }
		
        [MapField("НадписьЛица")]
		public string LinkedPersonText { get; set; }
		
        [MapField("КодЛицаСвязанный")]
		public int LinkedPersonChildID { get; set; }
		
        [MapField("ПоКлиенту")]
		public int ByClient { get; set; }

        /// <summary>
		/// Иконка
		/// </summary>
		[MapField("Icon")]
        public string Icon { get; set; }

        /// <summary>
        /// КодТипаКонтакта
        /// </summary>
        [MapField("КодТипаКонтакта")]
		public int ContactTypeID { get; set; }

		/// <summary>
		/// КодТипаКонтакта
		/// </summary>
		[MapField("ТипКонтакта")]
		public string ContactTypeDesc { get; set; }

		/// <summary>
		/// Контакт
		/// </summary>
		[MapField("Контакт")]
		public string ContactText { get; set; }

		/// <summary>
		/// ТелефонНомер
		/// </summary>
		[MapField("НомерМеждународный")]
		public string PhoneNumber { get; set; }

		/// <summary>
		/// Примечание
		/// </summary>
		[MapField("Примечание")]
		[MaxLength(300)]
		public string Comment { get; set; }

		[MapField("ИзменилФИО")]
		public string ChangedByFIO { get; set; }

		public override string GetInstanceFriendlyName()
		{
			return ContactText ?? String.Format("#{0}", GetUniqueID());
		}
	}


}
