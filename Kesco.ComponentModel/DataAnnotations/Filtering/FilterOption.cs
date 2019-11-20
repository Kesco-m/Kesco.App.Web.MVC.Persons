using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kesco.ComponentModel.DataAnnotations;

namespace Kesco.ComponentModel.DataAnnotations.Filtering
{
	/// <summary>
	/// Базовый атрибут для указания опции фильтрации
	/// </summary>
	public class FilterOption : AnnotationAttribute
	{
		/// <summary>
		/// Маска флагов опции, которые может редактировать пользователь
		/// </summary>
		protected FilterOptionFlags flagsMask;

		/// <summary>
		/// Возвращает или устанавливает общие для опции флаги.
		/// </summary>
		/// <value>
		/// Общие для опции флаги.
		/// </value>
		public FilterOptionFlags Flags { get; set; }

		public FilterOptionCondition Condition { get; set; }

		/// <summary>
		/// Инициализация нового экземпляра <see cref="FilterOption" /> класса.
		/// </summary>
		public FilterOption()
			: base()
		{
			Flags = FilterOptionFlags.None;
		}
	}
}
