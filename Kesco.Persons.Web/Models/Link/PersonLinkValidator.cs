using System;
using FluentValidation;
using FluentValidation.Results;

namespace Kesco.Persons.Web.Models.PersonLink
{
	public class PersonLinkValidator : AbstractValidator<PersonLink>
	{
		public PersonLinkValidator( int? PersonLinkTypeID )
		{
			string parentLabel = "";
			string childLabel = "";
			if (PersonLinkTypeID.HasValue)
				switch (PersonLinkTypeID.Value)
				{
					case 1:
						parentLabel = global::Resources.Resources.Persons_Link_Parent_Type1;
						childLabel = global::Resources.Resources.Persons_Link_Child_Type1;
						break;
					case 2:
						parentLabel = global::Resources.Resources.Persons_Link_Parent_Type2;
						childLabel = global::Resources.Resources.Persons_Link_Child_Type2;
						break;
					case 3:
						parentLabel = global::Resources.Resources.Persons_Link_Parent_Type3;
						childLabel = global::Resources.Resources.Persons_Link_Child_Type3;
						break;
					case 4:
						parentLabel = global::Resources.Resources.Persons_Link_Parent_Type4;
						childLabel = global::Resources.Resources.Persons_Link_Child_Type4;
						break;
				}

			CascadeMode = CascadeMode.StopOnFirstFailure;

			RuleFor(r => r.From)
				.Must(DatesAreValid)
					.WithMessage( Kesco.Persons.Web.Localization.Resources.Validation_Requisites_DateRange_must_be_valid);

			RuleFor(r => r.ParentPersonID).NotNull()
					.WithMessage( Kesco.Persons.Web.Localization.Resources.Validation_Specify_Field_Name + " " + parentLabel);

			RuleFor(r => r.ChildPersonID).NotNull()
					.WithMessage( Kesco.Persons.Web.Localization.Resources.Validation_Specify_Field_Name  + " " +  childLabel);

			RuleFor(r => r.Description)
				.Length(0, 300)
					.WithMessage( Kesco.Persons.Web.Localization.Resources.Validation_String_LengthExceeded);
		}

		protected bool DatesAreValid(PersonLink instance, DateTime? from)
		{
			if (!instance.From.HasValue) return true;
			if (!instance.To.HasValue) return true;
			return instance.From.Value < instance.To.Value;
		}

	}
}