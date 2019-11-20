using System;
using FluentValidation;
using Kesco.Persons.BusinessLogic;
using Kesco.Web.Mvc.Validation;
using System.Text.RegularExpressions;
using System.Collections.Generic;


namespace Kesco.Persons.Web.Models.Naturals
{
	public class PersonCardValidator : ObjectValidator<PersonCard>
	{
		public PersonCardValidator() : base()
		{
			//RuleFor(r => r.From).Cascade(CascadeMode.StopOnFirstFailure)
			//    .Must(DatesAreValid)
			//        .WithLocalizedMessage(() => Kesco.Persons.Web.Localization.Resources
			//            .Validation_Requisites_DateRange_must_be_valid);

            //RuleFor(r => r.OGRN).Cascade(CascadeMode.StopOnFirstFailure)
            //    .Must(ValidateOGRNLenght)
            //        .WithLocalizedMessage(() => Kesco.Persons.Web.Localization.Resources
            //            .Validation_NaturalPerson_SexMustBeSpecified);

			RuleFor(r => r.Sex).Cascade(CascadeMode.StopOnFirstFailure)
				.Must(SexIsSpecified)
					.WithLocalizedMessage(() => Kesco.Persons.Web.Localization.Resources
						.Validation_NaturalPerson_SexMustBeSpecified);

			RuleFor(r => r.FirstNameRus).Cascade(CascadeMode.StopOnFirstFailure)
				.Must(OneOfTheNamesMustBeProvided)
					.WithLocalizedMessage(() => Kesco.Persons.Web.Localization.Resources
						.Validation_NaturalPerson_AtLeastTheNameMustBeSpecified);

			RuleFor(r => r.FirstNameLat).Cascade(CascadeMode.StopOnFirstFailure)
				.Must(StringHasOnlyLatinChars)
					.WithLocalizedMessage(
							() => Kesco.Persons.Web.Localization.Resources.Validation_NaturalPerson_NameHasNoCyrillicChars,
							Kesco.Persons.ObjectModel.Localization.Resources.Kesco_Persons_MDL_269
						);

			RuleFor(r => r.LastNameLat).Cascade(CascadeMode.StopOnFirstFailure)
				.Must(StringHasOnlyLatinChars)
					.WithLocalizedMessage(
							() => Kesco.Persons.Web.Localization.Resources.Validation_NaturalPerson_NameHasNoCyrillicChars,
							Kesco.Persons.ObjectModel.Localization.Resources.Kesco_Persons_MDL_265
						);

			RuleFor(r => r.Nickname).Cascade(CascadeMode.StopOnFirstFailure)
					.Must(PersonWithTheSameNicknameDoesNotExist)
						.WithLocalizedMessage(() => Kesco.Persons.Web.Localization.Resources
							.Validation_NaturalPerson_PersonWithTheSameNicknameDoesNotExist);
		}

		protected bool PersonWithTheSameNicknameDoesNotExist(PersonCard instance, string name)
		{
			return String.IsNullOrWhiteSpace(name) 
				|| !Repository.Persons.HasPersonWithTheSameNickname(name, instance.PersonID ?? 0);
		}

		protected bool DatesAreValid(PersonCard instance, DateTime? from)
		{
			if (!instance.From.HasValue) return true;
			if (!instance.To.HasValue) return true;
			return instance.From.Value < instance.To.Value;
		}

		protected bool SexIsSpecified(PersonCard instance, char? sex)
		{
			return sex.HasValue;
		}

        //protected bool ValidateOGRNLenght(PersonCard instance, string name)
        //{
        //    if (instance.OGRN.Length == 0) return true;
        //    List<Kesco.Persons.ObjectModel.FormatRegistration> FormatRegistrations = Repository.FormatRegistrations.GetAllFormats();
        //    if (FormatRegistrations.Exists(f => f.ID == instance.TerritoryID))
        //    {
        //        int lenght = 0;
        //        if(instance.IncorporationFormID == 91)
        //        {
        //            lenght = FormatRegistrations.Find(m => m.ID == instance.TerritoryID).OGRNLength2;
        //        }
        //        else
        //        {
        //            lenght = FormatRegistrations.Find(m => m.ID == instance.TerritoryID).OGRNLength1;
        //        }
        //        if (instance.OGRN.Length != lenght) return false;
        //    }
           
        //    return true;
        //}

	    protected bool OneOfTheNamesMustBeProvided(PersonCard instance, string name)
		{
			if (String.IsNullOrWhiteSpace(instance.LastNameRus)
				&& String.IsNullOrWhiteSpace(instance.FirstNameRus)
				&& String.IsNullOrWhiteSpace(instance.LastNameLat)
				&& String.IsNullOrWhiteSpace(instance.FirstNameLat)
				)
				return false;

			return true;
		}

	}
}