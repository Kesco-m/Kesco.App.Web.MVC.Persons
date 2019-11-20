using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.ComponentModel;
using Kesco.ComponentModel.DataAnnotations;

namespace Kesco.Web.Mvc.UI.ComponentModel.DataAnnotations
{

    /// <summary>
    /// Атрибут, описывающий ссылку в выпадающем списке 
    /// для элемент управления SelectBox
    /// </summary>
    public class KescoSelectLinkAttribute : KescoSelectCommandAttribute
    {

		/// <summary>
		/// Возвращает или устанавливает условие показа ссылки
		/// </summary>
		/// <value>
		/// Условие показа ссылки
		/// </value>
		public KescoSelectLinkShowCondition ShowCondition { get; set; }

		/// <summary>
		/// Возвращает или устанавливает порядок ссылки для сортировки 
		/// </summary>
		/// <value>
		/// Порядок ссылки для сортировки
		/// </value>
		public int SortOrder { get; set; }

        /// <summary>
        /// Иницилизирует новый экземпляр <see cref="KescoSelectLinkAttribute" /> класса.
        /// </summary>
        /// <param name="command">Команда, которую посылает ссылка элементу управления.</param>
		/// <param name="linkText">Текст ссылки или имя свойства, если указан тип ресурса.</param>
        /// <param name="linkIcon">Иконка для ссылки</param>
        /// <param name="resourceType">Тип ресурса</param>
        public KescoSelectLinkAttribute(string command, string linkText, string linkIcon, Type resourceType)
            : this(command, linkText, linkIcon, resourceType, KescoSelectLinkShowCondition.Always) {}

		/// <summary>
		/// Иницилизирует новый экземпляр <see cref="KescoSelectLinkAttribute" /> класса.
		/// </summary>
		/// <param name="command">Команда, которую посылает ссылка элементу управления.</param>
		/// <param name="linkText">Текст ссылки или имя свойства, если указан тип ресурса.</param>
		/// <param name="linkIcon">Иконка для ссылки</param>
		/// <param name="resourceType">Тип ресурса</param>
		public KescoSelectLinkAttribute(string command, string linkText, string linkIcon, 
			Type resourceType, KescoSelectLinkShowCondition showCondition)
			: base(command, linkText, linkIcon, resourceType) {
				ShowCondition = showCondition;
		}

	}
}