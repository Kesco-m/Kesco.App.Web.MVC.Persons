using Kesco.Web.Mvc.UI.ComponentModel.DataAnnotations;

namespace Kesco.Territories.Controls.ComponentModel
{
	public class TerritorySelectSearchParameters : KescoSelectSearchParametersAttribute
	{
		/// <summary>
		/// Ограничивает поиск по территории с указанным кодом
		/// </summary>
		public int TAreaID { get; set; }

		/// <summary>
		/// TOP N - ограничивает, количество, возвращаемых процедурой записей
		/// </summary>
		public int Limit { get; set; }

		public TerritorySelectSearchParameters()
			: base()
		{
		}
	}
}