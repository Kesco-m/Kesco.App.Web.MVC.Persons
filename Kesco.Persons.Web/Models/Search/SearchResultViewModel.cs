using System.Linq;
using Kesco.Persons.ObjectModel;
using Kesco.Web.Mvc;
using Kesco.Web.Mvc.UI.Grid;

namespace Kesco.Persons.Web.Models
{

	public class SearchResultViewModel : KescoGridModelBase<Person>
	{
		protected override void SetUpGrid()
		{
			base.SetUpGrid();
			JQGridColumn column = Grid.Columns.First((c) => {
				return c.DataField == "ID";
			});
			if (column != null) {
				column.Fixed = true;
				column.PrimaryKey = true;
				column.Formatter = null;
				column.TextAlign = TextAlign.Right;
			}

			Grid.Columns.All(c => {
				if (c.DataField == "Nickname" || c.DataField == "INN") {
					c.Visible = true;
				} else c.Visible = false;
				return true;
			});

			column = Grid.Columns.FirstOrDefault((c) => {
				return c.DataField == "Nickname";
			});

			if (column != null) {
				column.Formatter = new Kesco.Web.Mvc.UI.Grid.CustomFormatter {
					FormatFunction = "formatSearchResultGridNicknameColumn"
				};
			}

			column = Grid.Columns.FirstOrDefault((c) => {
				return c.DataField == "INN";
			});

			if (column != null) {
				column.Fixed = true;
				column.Width = 100;
			}

		}
	}

}
