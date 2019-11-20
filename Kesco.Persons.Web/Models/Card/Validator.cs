using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;
using Kesco.Persons.BusinessLogic;
using Kesco.Persons.ObjectModel;

namespace Kesco.Persons.Web.Models.Card
{
	public class Validator : AbstractValidator<DataModel>
	{
		public Validator()
		{
			
			CascadeMode = CascadeMode.StopOnFirstFailure;

			RuleFor(r => r.From).Cascade(CascadeMode.StopOnFirstFailure)
				.Must(DatesAreValid)
					.WithLocalizedMessage(() => Kesco.Persons.Web.Localization.Resources
						.Validation_Card_DateRange_must_be_valid);

		}

		protected bool DatesAreValid(DataModel instance, DateTime? from)
		{
			if (!instance.From.HasValue) return true;
			if (!instance.To.HasValue) return true;
			return instance.From.Value <= instance.To.Value;
		}

	}
}