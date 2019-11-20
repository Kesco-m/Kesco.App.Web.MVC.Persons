using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kesco.Web.Mvc.UI.ComponentModel.DataAnnotations
{
	/// <summary>
	/// Данный класс-атрибут реализует настройки стандартной команды 
	/// расширенного поиска для элемента управления KescoSelect
	/// </summary>
	public class KescoSelectAdvancedSearchAttribute : KescoSelectLinkAttribute
	{
		/// <summary>
		/// Иницилизирует новый экземпляр  <see cref="KescoSelectAdvancedSearchAttribute" /> атрибута.
		/// </summary>
		public KescoSelectAdvancedSearchAttribute() : base("advSearch", "KescoSelect_AdvancedSearch", "ui-icon-search", typeof(Controls.Localization.Resources)) {
			SortOrder = 0;
		}
	}
}
