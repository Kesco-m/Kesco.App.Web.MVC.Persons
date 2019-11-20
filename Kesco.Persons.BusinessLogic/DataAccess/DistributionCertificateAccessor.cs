using Kesco.Persons.ObjectModel;
using Kesco.DataAccess;

namespace Kesco.Persons.BusinessLogic.DataAccess
{
    public abstract class DistributionCertificateAccessor : EntityAccessor<DistributionCertificateAccessor, DB, 
		DistributionCertificate, DistributionCertificateAccessor.SearchParameters, int>
    {
		public class SearchParameters : Kesco.DataAccess.SearchParameters { }
    }
}
