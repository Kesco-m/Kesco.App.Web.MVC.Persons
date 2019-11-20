using Kesco.Web.Mvc.UI.ComponentModel.DataAnnotations;

namespace Kesco.BusinessProjects.Controls.ComponentModel
{
	public class BusinessProjectSelectSearchParameters : KescoSelectSearchParametersAttribute
	{

		/// <summary>
		/// TOP N - ограничивает, количество, возвращаемых процедурой записей
		/// </summary>
		public int Limit { get; set; }

		public BusinessProjectSelectSearchParameters()
			: base()
		{
		}
	}
}