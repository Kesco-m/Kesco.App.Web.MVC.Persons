using System;
using BLToolkit.DataAccess;
using BLToolkit.Mapping;
using BLToolkit.Validation;
using Kesco.ObjectModel;

namespace Kesco.Persons.ObjectModel
{
    /// <summary>
    /// ФорматНомеровРегистрацииЛиц
    /// </summary>
    [TableName("ФорматНомеровРегистрацииЛиц")]
    [System.ComponentModel.DataAnnotations.MetadataType(typeof(Metadata.FormatRegistration))]
    public class FormatRegistration : Entity<FormatRegistration, int>
    {
        /// <summary>
        /// КодТерритории
        /// </summary>
        [MapField("КодТерритории")]
        [NonUpdatable(OnInsert = true), PrimaryKey]
        public override int ID { get; set; }

        /// <summary>
        /// НазваниеОГРН
        /// </summary>
        [MapField("НазваниеОГРН")]
        [MaxLength(50)]
        public string OGRNName { get; set; }

		/// <summary>
		/// НазваниеОГРН
		/// </summary>
		[MapField("НазваниеОГРНLocalizationKey")]
		[MaxLength(50)]
		public string OGRNName_LocalizationKey { get; set; }

		/// <summary>
		/// НазваниеИНН
		/// </summary>
		[MapField("НазваниеИННLocalizationKey")]
		[MaxLength(50)]
		public string INNName_LocalizationKey { get; set; }

		/// <summary>
		/// НазваниеОКПО
		/// </summary>
		[MapField("НазваниеОКПОLocalizationKey")]
		[MaxLength(50)]
		public string OKPOName_LocalizationKey { get; set; }


        /// <summary>
        /// ФорматОГРН
        /// </summary>
        [MapField("ФорматОГРН")]
        [MaxLength(50)]
        public string OGRNFormat { get; set; }

        /// <summary>
        /// НазваниеИНН
        /// </summary>
        [MapField("НазваниеИНН")]
        [MaxLength(50)]
        public string INNName { get; set; }

        /// <summary>
        /// ДлинаИНН1
        /// </summary>
        [MapField("ДлинаИНН1")]
        public int INNLength1 { get; set; }

        /// <summary>
        /// ДлинаИНН2
        /// </summary>
        [MapField("ДлинаИНН2")]
        public int INNLength2 { get; set; }

        /// <summary>
        /// НазваниеОКПО
        /// </summary>
        [MapField("НазваниеОКПО")]
        [MaxLength(50)]
        public string OKPOName { get; set; }

        /// <summary>
        /// ДлинаОКПО1
        /// </summary>
        [MapField("ДлинаОКПО1")]
        public int OKPOLength1 { get; set; }

        /// <summary>
        /// ДлинаОКПО2
        /// </summary>
        [MapField("ДлинаОКПО2")]
        public int OKPOLength2 { get; set; }


		/// <summary>
		/// Возвращает отображаемое имя.
		/// </summary>
		/// <returns>Отображаемое имя</returns>
		public override string GetInstanceFriendlyName()
		{
			return OGRNName ?? String.Format("#{0}", GetUniqueID());
		}
	}
}
