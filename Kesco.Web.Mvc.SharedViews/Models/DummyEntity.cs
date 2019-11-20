using System;
using System.ComponentModel.DataAnnotations;
using Kesco.ComponentModel.DataAnnotations;
using Kesco.ObjectModel;
using Kesco.Web.Mvc.SharedViews.ComponentModel;
using Kesco.Web.Mvc.UI;

namespace Kesco.Web.Mvc.SharedViews.Models
{
	public class DummyEntity : TrackableEntity<DummyEntity, int>
	{
		[UIHint("UniqueID")]
		public override int ID { get; set; }

		[UIHint("DateField"), DisplayFormatEx(NullDisplayText = "- не указана -")]
		[Required]
		[Display(Name = "Дата документа")]
		public DateTime? DocumentDate { get; set; }

        [Display(
                Name = "Отправить копию по факсу", 
                Description="Указывает, что копия документа должна быть отправлена по факсу",
                ShortName = "Отправить по факсу"
        )]
        public bool SendCopyByFax { get; set; }


        /*
        [Display(Name = "Телефон")]
        [UIHint("Phone"), DisplayFormatEx(NullDisplayText = "- не указано -")]
        [AdditionalMetadata("PhoneIcon", "PhoneStandard.gif")]
        public string Phone { get; set; }

        [Display(Name = "Мобильный")]
        [UIHint("Phone"), DisplayFormatEx(NullDisplayText = "- не указан -")]
        [AdditionalMetadata("PhoneIcon", "PhoneGSM.gif")]
        public string Mobile { get; set; }

        [Display(Name = "Эл.адрес")]
        [UIHint("Email"), DisplayFormatEx(NullDisplayText = "- не указан -")]
        public string Email { get; set; }

        [Display(Name = "MSN Мессенжер")]
        [UIHint("MSN"), DisplayFormatEx(NullDisplayText = "- не указан -")]
        public string MsnMessenger { get; set; }
        */

		public override string GetInstanceFriendlyName()
		{
			return String.Format("#{0} - {1}", GetUniqueID(), DocumentDate);
		}
	}
}