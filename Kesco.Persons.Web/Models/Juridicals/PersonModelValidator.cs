using System.Collections.Generic;
using FluentValidation;
using Kesco.Persons.Web.Models.Requisites;

namespace Kesco.Persons.Web.Models.Juridicals
{
	public class PersonModelValidator : AbstractValidator<PersonModel>
	{
		public PersonModelValidator(bool checkRequisites = true) : base()
		{
			RuleFor(r => r.Card).SetValidator(new PersonCardValidator(checkRequisites));
			if (checkRequisites)
				RuleFor(r => r.Card.Requisites).SetValidator(new RequisitesValidator());
			if (checkRequisites)
				RuleFor(r => r.PersonTypes.PersonTypeIDs)
					.NotEmpty()
						.WithMessage( Kesco.Persons.Web.Localization.Resources
							.Validation_JuridicalPerson_MustSpecifyAtLeastOnePersonType);
            RuleFor(r => r.ResponsibleEmployees).Cascade(CascadeMode.StopOnFirstFailure)
                .Must(ResponsibleEmployeesSpecified)
                    .WithMessage( Localization.Resources.Views_NaturalPerson_Validation_Responsible);
		}

		protected bool MustSpecifyAtLeastOneType(List<int> list)
		{
			return list != null && list.Count > 0;
		}

        protected bool ResponsibleEmployeesSpecified(PersonModel instance, List<Naturals.PersonModel.SimplePersonModelClass> responsibleEmployees)
        {
            if (responsibleEmployees.Count == 0) return false;
            return true;
        }

	}
}