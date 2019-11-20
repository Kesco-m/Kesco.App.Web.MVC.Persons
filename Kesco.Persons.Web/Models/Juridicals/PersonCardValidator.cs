using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;
using Kesco.Persons.BusinessLogic;

namespace Kesco.Persons.Web.Models.Juridicals
{
	/// <summary>
	/// Валидатор для юр. лица
	/// </summary>
	public class PersonCardValidator : AbstractValidator<PersonCard>
	{
		public PersonCardValidator(bool checkRequisites = true) : base()
		{

			RuleFor(r => r.Nickname).Cascade(CascadeMode.StopOnFirstFailure)
				.Must(PersonWithTheSameNicknameDoesNotExist)
					.WithLocalizedMessage(() => Kesco.Persons.Web.Localization.Resources
						.Validation_NaturalPerson_PersonWithTheSameNicknameDoesNotExist);
			//RuleFor(r => r.TerritoryID).Cascade(CascadeMode.StopOnFirstFailure)
			//    .NotNull()
			//        .WithLocalizedMessage(() => Kesco.Persons.Web.Localization.Resources
			//            .Validation_JuridicalPerson_RegistrationCountryMustBeSpecified);
			if (checkRequisites)
				RuleFor(r => r.Requisites.ShortNameRus).Cascade(CascadeMode.StopOnFirstFailure)
					.Must(RequisitesMustBeSpecified)
						.WithLocalizedMessage(() => Kesco.Persons.Web.Localization.Resources
							.Validation_JuridicalPerson_RequisitesMustBeSpecified);
		}

		protected bool PersonWithTheSameNicknameDoesNotExist(PersonCard instance, string name)
		{
			return String.IsNullOrWhiteSpace(name)
				|| !Repository.Persons.HasPersonWithTheSameNickname(name, instance.PersonID ?? 0);
		}

		protected bool RequisitesMustBeSpecified(PersonCard instance, string name)
		{
			string rus = instance.Requisites.ShortNameRus ?? String.Empty;
			string lat = instance.Requisites.ShortNameLat ?? String.Empty;
			if (String.IsNullOrWhiteSpace(rus) && String.IsNullOrWhiteSpace(lat))
				return false;

			return true;
		}


	}
}