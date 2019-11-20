using BLToolkit.Reflection;
using Kesco.Persons.BusinessLogic;
using Kesco.Persons.ObjectModel;

namespace Kesco.Persons.Web.Models
{
    public class IndividualViewModel : PersonCardViewModel
    {
        public PersonCardNatural Card { get; internal set; }

        public IndividualViewModel()
        {
			Card = TypeAccessor<PersonCardNatural>.CreateInstanceEx();
            Card.Person = TypeAccessor<Person>.CreateInstanceEx();
            IncorporationForms = Repository.IncorporationForms.GetAllByPersonKind(2);
            Card.Person.TerritoryID = null;

            HelpTopic = "CreateIndividual";
        }

    }

}