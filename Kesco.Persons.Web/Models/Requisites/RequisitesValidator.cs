using System;
using System.Text.RegularExpressions;
using FluentValidation;
using Kesco.Persons.BusinessLogic;
using Kesco.Persons.ObjectModel;
using Kesco.Web.Mvc.Validation;

namespace Kesco.Persons.Web.Models.Requisites
{
	public class RequisitesValidator : ObjectValidator<Requisites>
	{
		public RequisitesValidator()
		{
			//RuleFor(r => r.From).Cascade(CascadeMode.StopOnFirstFailure)
			//    .Must(DatesAreValid)
			//        .WithMessage( Kesco.Persons.Web.Localization.Resources
			//            .Validation_Requisites_DateRange_must_be_valid);
			RuleFor(r => r.ShortNameRus).Cascade(CascadeMode.StopOnFirstFailure)
				.Must(OneOfTheShortNamesIsProvided)
					.WithMessage( Kesco.Persons.Web.Localization.Resources
						.Validation_Requisites_SomeShortNames_must_be_specified)
				.Must(ShortNameRusIsValid)
					.WithMessage( Kesco.Persons.Web.Localization.Resources
						.Validation_Requisites_ShortNameRus_must_be_correct)
				.Length(0, 200)
					.WithMessage( Kesco.Persons.Web.Localization.Resources
						.Models_JuridicalPersonCard_ShortNameRus_Name)
					.WithMessage( Kesco.Persons.Web.Localization.Resources
						.Validation_String_LengthExceeded);
	
			RuleFor(r => r.ShortNameLat).Cascade(CascadeMode.StopOnFirstFailure)
				.Length(0, 200)
					.WithMessage( Kesco.Persons.Web.Localization.Resources
						.Models_JuridicalPersonCard_ShortNameLat_Name)
					.WithMessage( Kesco.Persons.Web.Localization.Resources
						.Validation_String_LengthExceeded)
				.Must(StringHasOnlyLatinChars)
					.WithMessage( Kesco.Persons.Web.Localization.Resources
						.Models_JuridicalPersonCard_ShortNameLat_Name)
					.WithMessage( Kesco.Persons.Web.Localization.Resources
						.Validation_StringField_OnlyLatinChars)
				;

			RuleFor(r => r.FullName).Cascade(CascadeMode.StopOnFirstFailure)
				.Must(FullNameIsValid)
					.WithMessage( Kesco.Persons.Web.Localization.Resources
						.Validation_Requisites_FullName_is_invalid)
				.Length(0, 300)
					.WithMessage( Kesco.Persons.Web.Localization.Resources
						.Models_JuridicalPersonCard_FullName_Name)
					.WithMessage( Kesco.Persons.Web.Localization.Resources
						.Validation_String_LengthExceeded);

			RuleFor(r => r.OKONH).Cascade(CascadeMode.StopOnFirstFailure)
				.Length(0, 5)
					.WithMessage( Kesco.Persons.Web.Localization.Resources
						.Models_JuridicalPersonCard_OKONH_Name)
					.WithMessage( Kesco.Persons.Web.Localization.Resources
						.Validation_String_LengthExceeded);

			RuleFor(r => r.OKVED).Cascade(CascadeMode.StopOnFirstFailure)
				.Length(0, 8)
					.WithMessage( Kesco.Persons.Web.Localization.Resources
						.Models_JuridicalPersonCard_OKVED_Name)
					.WithMessage( Kesco.Persons.Web.Localization.Resources
						.Validation_String_LengthExceeded);

			RuleFor(r => r.KPP).Cascade(CascadeMode.StopOnFirstFailure)
				.Length(0, 20)
					.WithMessage( Kesco.Persons.Web.Localization.Resources
						.Models_JuridicalPersonCard_KPP_Name)
					.WithMessage( Kesco.Persons.Web.Localization.Resources
						.Validation_String_LengthExceeded);

			RuleFor(r => r.RwID).Cascade(CascadeMode.StopOnFirstFailure)
				.Length(0, 35)
					.WithMessage( Kesco.Persons.Web.Localization.Resources
						.Models_JuridicalPersonCard_RwID_Name)
					.WithMessage( Kesco.Persons.Web.Localization.Resources
						.Validation_String_LengthExceeded);

			RuleFor(r => r.AddressLegal).Cascade(CascadeMode.StopOnFirstFailure)
				.Length(0, 300)
					.WithMessage( Kesco.Persons.Web.Localization.Resources
						.Models_JuridicalPersonCard_AddressLegal_Name)
					.WithMessage( Kesco.Persons.Web.Localization.Resources
						.Validation_String_LengthExceeded)
				;

			RuleFor(r => r.AddressLegalLat).Cascade(CascadeMode.StopOnFirstFailure)
				.Length(0, 300)
					.WithMessage( Kesco.Persons.Web.Localization.Resources
						.Models_JuridicalPersonCard_AddressLegalLat_Name)
					.WithMessage( Kesco.Persons.Web.Localization.Resources
						.Validation_String_LengthExceeded)
				.Must(StringHasOnlyLatinChars)
					.WithMessage( Kesco.Persons.Web.Localization.Resources
						.Models_JuridicalPersonCard_AddressLegalLat_Name)
					.WithMessage( Kesco.Persons.Web.Localization.Resources
						.Validation_StringField_OnlyLatinChars)
				;

		}

		protected bool DatesAreValid(Requisites instance, DateTime? from)
		{
			if (!instance.From.HasValue) return true;
			if (!instance.To.HasValue) return true;
			return instance.From.Value <= instance.To.Value;
		}

		protected bool OneOfTheShortNamesIsProvided(Requisites instance, string shortName)
		{
			string rus = instance.ShortNameRus ?? String.Empty;
			string lat = instance.ShortNameLat ?? String.Empty;
			if (String.IsNullOrWhiteSpace(rus) && String.IsNullOrWhiteSpace(lat)) 
				return false;

			return true;
		}

		protected bool ShortNameRusIsValid(Requisites instance, string shortName)
		{
			if (String.IsNullOrWhiteSpace(shortName)) return true;
			return !shortName.Contains("...");
		}

		protected bool FullNameIsValid(Requisites instance, string fullName)
		{
			if (!instance.IncorporationFormID.HasValue) return true;

			if (String.IsNullOrWhiteSpace(fullName)) return false;
			//IncorporationForm form = Repository.IncorporationForms.GetInstance(
			//    instance.IncorporationFormID.Value);
			//if (form != null) {
			//    return fullName.StartsWith(form.Name);
			//}
			return true;
		}
	}
}