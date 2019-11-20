using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kesco.ComponentModel.DataAnnotations
{
	/// <summary>
	/// Класс-атрибут, указывающий, что тип является
	/// классом с метаданными (buddy) для указанного типа (friend).
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public class MetadataTypeForAttribute : Attribute
	{
		/// <summary>
		/// Возвращает или устанавливает ассоциированный тип, 
		/// для которого предоставляются метаданные.
		/// </summary>
		/// <value>
		/// Ассоциированный тип.
		/// </value>
		public Type AssociatedType { get; set; }

		/// <summary>
		/// Инициализирует новый экземпляр <see cref="MetadataTypeForAttribute" /> класса.
		/// </summary>
		/// <param name="associatedType">Ассоциированный тип.</param>
		public MetadataTypeForAttribute(Type associatedType)
		{
			AssociatedType = associatedType;
		}
	}
}
