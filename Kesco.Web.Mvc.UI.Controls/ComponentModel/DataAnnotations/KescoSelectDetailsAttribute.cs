using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kesco.Web.Mvc.UI.ComponentModel.DataAnnotations
{
	/// <summary>
	/// Данный класс-атрибут реализует настройки стандартной команды 
	/// просмотра деталей для элемента управления KescoSelect
	/// </summary>
	public class KescoSelectDetailsAttribute : KescoSelectCommandAttribute
	{
		/// <summary>
		/// Иницилизирует новый экземпляр  <see cref="KescoSelectDetailsAttribute" /> атрибута.
		/// </summary>
		public KescoSelectDetailsAttribute() : base("view", "KescoSelect_Details", "ui-icon-document", typeof(Controls.Localization.Resources)) { }
	}
}
