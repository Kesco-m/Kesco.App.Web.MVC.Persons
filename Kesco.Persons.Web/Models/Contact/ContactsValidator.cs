using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;
using Kesco.Web.Mvc.Validation;
using Kesco.Persons.BusinessLogic;
using Kesco.Persons.ObjectModel;
using FluentValidation.Results;

namespace Kesco.Persons.Web.Models.Contact
{
	public class ContactsValidator : ObjectValidator<Contact>
	{
		protected List<string> invalidEmails = new List<string>();

		public ContactsValidator()
		{
			
			CascadeMode = CascadeMode.StopOnFirstFailure;

			RuleFor(r => r.ContactTypeID).NotNull()
				.WithMessage( Kesco.Persons.Web.Localization.Resources
					.Validation_Contact_SpecifyContactType);

			// Контакт типа 'Адрес' должен содержать хотя бы название страны
			When(c => c.ContactTypeID.HasValue && c.ContactTypeID.Value < 20, () => {
				RuleFor(r => r.CountryID).NotNull()
						.WithMessage( Kesco.Persons.Web.Localization.Resources
							.Validation_Contact_SpecifyCountryAtLeast);
				RuleFor(r => r.Zip)
					.Length(0, 6)
						.WithMessage( Kesco.Persons.Web.Localization.Resources
							.Validation_String_LengthExceeded);
				RuleFor(r => r.Region)
					.Length(0, 50)
						.WithMessage( Kesco.Persons.Web.Localization.Resources
							.Validation_String_LengthExceeded);
				RuleFor(r => r.CityName)
					.Length(0, 50)
						.WithMessage( Kesco.Persons.Web.Localization.Resources
							.Validation_String_LengthExceeded);
				RuleFor(r => r.Address)
					.Length(0, 300)
						.WithMessage( Kesco.Persons.Web.Localization.Resources
							.Validation_String_LengthExceeded);
			});

			// Для контакта типа 'Телефон' должны быть указаны код страны и местный номер
			When(c => c.ContactTypeID.HasValue && c.ContactTypeID.Value >= 20 && c.ContactTypeID.Value < 40, () => {

				RuleFor(r => r.CountryPhoneCode)
					.NotEmpty()
						.WithMessage( Kesco.Persons.Web.Localization.Resources
							.Validation_Contact_SpecifyCountryPhoneCode)
					.Length(0, 6)
						.WithMessage( Kesco.Persons.Web.Localization.Resources
							.Validation_String_LengthExceeded);

				RuleFor(r => r.CityPhoneCode)
					.Length(0, 6)
						.WithMessage( Kesco.Persons.Web.Localization.Resources
							.Validation_String_LengthExceeded);

				RuleFor(r => r.PhoneNumber)
					.NotEmpty()
						.WithMessage( Kesco.Persons.Web.Localization.Resources
							.Validation_Contact_SpecifyCountryPhoneCode)
					.Length(0, 40)
						.WithMessage( Kesco.Persons.Web.Localization.Resources
							.Validation_String_LengthExceeded);

				RuleFor(r => r.PhoneNumberAdd)
					.Length(0, 10)
						.WithMessage( Kesco.Persons.Web.Localization.Resources
							.Validation_String_LengthExceeded);

			});

            
            // Для общих контактов должнен быть указан текст контакта
            When(c => c.ContactTypeID.HasValue && c.ContactTypeID.Value >= 40 && c.ContactTypeID.Value < 54, () => {
				RuleFor(r => r.ContactText)
					.NotEmpty()
						.WithMessage(Kesco.Persons.Web.Localization.Resources.Validation_Specify_Field_Name)//,ReturnContactCaption)
					.Length(0,300)
						.WithMessage( Kesco.Persons.Web.Localization.Resources.Validation_String_LengthExceeded2//,ReturnContactCaption
							)
					;
			});

            // проверка формата электронного адреса
            When(c => c.ContactTypeID.HasValue && c.ContactTypeID.Value == 40, () => {
                RuleFor(r => r.ContactText).Must(EmailAddressMustBeValid).WithMessage(
                                    String.Format(
                                        Kesco.Persons.Web.Localization.Resources.Validation_EmailAddressInvalid,
                                        Kesco.Persons.Web.Localization.Resources.Views_Contact_ContactText_Email,
                                        String.Join("; ", lastInvalidEmails)
                                    ).Replace("[","").Replace("]","").Trim()
                    );
            });

			//	Custom(с => {
			//		var valid = EmailAddressMustBeValid(с, с.ContactText);
			//		return valid ? null
			//			: new ValidationFailure(
			//				Kesco.Persons.Web.Localization.Resources.Views_Contact_ContactText_Email,
			//				String.Format(
			//					Kesco.Persons.Web.Localization.Resources.Validation_EmailAddressInvalid,
			//					Kesco.Persons.Web.Localization.Resources.Views_Contact_ContactText_Email,
			//					String.Join("; ", lastInvalidEmails
			//				)
			//		));
			//   });
			//});



			//// проверка формата Url-адреса.
			When(c => c.ContactTypeID.HasValue && c.ContactTypeID.Value == 50, () => {
                RuleFor(r => r.ContactText).Must(UrlMustBeValid).WithMessage(

                                        String.Format(
                                            Kesco.Persons.Web.Localization.Resources.Validation_UrlAddressInvalid,
                                            Kesco.Persons.Web.Localization.Resources.Views_Contact_ContactText_Http
                                        )

                    );
            });
            //	Custom(с => {
            //		var valid = UrlMustBeValid(с, с.ContactText);
            //		return valid ? null
            //			: new ValidationFailure(
            //					Kesco.Persons.Web.Localization.Resources.Views_Contact_ContactText_Http,
            //					String.Format(
            //						Kesco.Persons.Web.Localization.Resources.Validation_UrlAddressInvalid,
            //						Kesco.Persons.Web.Localization.Resources.Views_Contact_ContactText_Http
            //					)
            //			);
            //	});
            //});

            // Для контакта - Другой контакт - должен быть указан текст другого контакта
            When(c => c.ContactTypeID.HasValue && c.ContactTypeID.Value == 54, () => {
				RuleFor(r => r.OtherContact).NotEmpty()
					.WithMessage( Kesco.Persons.Web.Localization.Resources
							.Validation_Contact_SpecifyOtherContact
						)
					.Length(0, 120)
						.WithMessage( Kesco.Persons.Web.Localization.Resources
							.Validation_String_LengthExceeded);
			});

			RuleFor(r => r.Comment)
				.Length(0, 300)
					.WithMessage( Kesco.Persons.Web.Localization.Resources
						.Validation_String_LengthExceeded);
		}

		protected bool IfEmailAddressThenMustBeValid(Contact instance, string contactText)
		{
			if (instance.ContactTypeID == 40) {
				bool result = EmailAddressMustBeValid(instance, contactText);
				if (!result) invalidEmails.AddRange(lastInvalidEmails);
				return result;
			}
			return true;
		}

		protected object ReturnContactCaption(Contact c) 
		{
			switch (c.ContactTypeID.Value) {
				case 40: return Kesco.Persons.Web.Localization.Resources.Views_Contact_ContactText_Email;
				case 41: return Kesco.Persons.Web.Localization.Resources.Views_Contact_ContactText_YahooIM;
				case 50: return Kesco.Persons.Web.Localization.Resources.Views_Contact_ContactText_Http;
				case 51: return Kesco.Persons.Web.Localization.Resources.Views_Contact_ContactText_Telex;
				case 52: return Kesco.Persons.Web.Localization.Resources.Views_Contact_ContactText_Teletype;
				case 53: return Kesco.Persons.Web.Localization.Resources.Views_Contact_ContactText_Pager;
				default: return "ContactText";
			}
		}
	}
}