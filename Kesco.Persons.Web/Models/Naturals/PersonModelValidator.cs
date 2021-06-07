using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using FluentValidation;


namespace Kesco.Persons.Web.Models.Naturals
{
	public class PersonModelValidator : AbstractValidator<PersonModel>
	{
		public PersonModelValidator(bool checkCard, bool checkNickname = true)
		{
			if (checkCard) {
				RuleFor(r => r.Card).SetValidator(new PersonCardValidator());
			}

            RuleFor(r => r.PersonTypes.PersonTypeIDs).Cascade(CascadeMode.StopOnFirstFailure)
                .Must(PersonTypesSpecified)
                    .WithMessage( Localization.Resources.Validation_JuridicalPerson_MustSpecifyAtLeastOnePersonType);

            RuleFor(r => r.ResponsibleEmployees).Cascade(CascadeMode.StopOnFirstFailure)
                .Must(ResponsibleEmployeesSpecified)
                    .WithMessage( Localization.Resources.Views_NaturalPerson_Validation_Responsible);

            RuleFor(r => r.Card.FirstNameRus).Cascade(CascadeMode.StopOnFirstFailure)
                .Must(AllNamesFieldsMustHaveTranslate)
                    .WithMessage( Kesco.Persons.Web.Localization.Resources
                        .Validation_NaturalPerson_NaturalFIOMustBeOnEnglish);
		}

		protected bool PersonTypesSpecified(PersonModel instance, string types)
		{
			if ((instance.Card.PersonID == null || instance.Card.PersonID.Value == 0) && String.IsNullOrEmpty(types) && instance.Card.IncorporationFormID.HasValue && instance.Card.IncorporationFormID.Value == 91)
				return false;

			return true;
		}

        protected bool ResponsibleEmployeesSpecified(PersonModel instance, List<PersonModel.SimplePersonModelClass> responsibleEmployees)
        {
            if (responsibleEmployees.Count == 0) return false;
            return true;
        }

        protected bool AllNamesFieldsMustHaveTranslate(PersonModel instance, string name)
        {
            if (instance.EmployerId == 0 || instance.EmployerId == null) return true;
            if (!String.IsNullOrWhiteSpace(instance.Card.FirstNameRus) && (!Regex.IsMatch(instance.Card.FirstNameRus, "^[a-zA-Z0-9]*$") && String.IsNullOrWhiteSpace(instance.Card.FirstNameLat)))
            {
                return false;
            }
            if (!String.IsNullOrWhiteSpace(instance.Card.LastNameRus) && (!Regex.IsMatch(instance.Card.LastNameRus, "^[a-zA-Z0-9]*$") && String.IsNullOrWhiteSpace(instance.Card.LastNameLat)))
            {
                return false;
            }
            if (!String.IsNullOrWhiteSpace(instance.Card.MiddleNameRus) && (!Regex.IsMatch(instance.Card.MiddleNameRus, "^[a-zA-Z0-9]*$") && String.IsNullOrWhiteSpace(instance.Card.MiddleNameLat)))
            {
                return false;
            }

            return true;
        }

	}
}