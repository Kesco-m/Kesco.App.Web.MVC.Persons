using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Kesco.ComponentModel.DataAnnotations
{
	/// <summary>
	/// Данный класс расширяет атрибут DisplayFormat, позволяя указывать 
	/// для свойства NullDisplayText строку ресурса 
	/// </summary>
	/// <example>
	/// <code>
	/// public class CashFlowItem : EntityBase&gt;CashFlowType&lt;
	/// {
	///		...
	///		
	///	[DisplayFormatEx(ResourceType = typeof(Resources), NullDisplayTextResourceName = "CashFlowItem_Parent_DisplayFormat_NullDisplayText")]
	///	public int? Parent { get; set; }
	/// 	
	///		...
	/// }
	/// </code>
	/// </example>
	/// <remarks></remarks>
	[Obsolete("Используйте атрибут Display")]
	public class DisplayFormatExAttribute : DisplayFormatAttribute
	{
		/// <summary>
		/// Создаёт экземпляр <see cref="DisplayFormatExAttribute"/> класса.
		/// </summary>
		public DisplayFormatExAttribute() : base() { }

		/// <summary>
		/// Возвращает или задает текст, отображаемый в поле, значение которого равно null.
		/// </summary>
		/// <returns>Текст, отображаемый в поле, значение которого равно null.Значение по умолчанию — пустая строка (""), указывающая, что это свойство не задано.</returns>
		public new string NullDisplayText
		{
			get
			{
				if (ResourceType != null && !String.IsNullOrWhiteSpace(NullDisplayTextResourceName))
				{
					DisplayAttribute nullDisplayText = new DisplayAttribute { ResourceType = ResourceType, Name = NullDisplayTextResourceName };
					return nullDisplayText.GetName();
				}
				return base.NullDisplayText;
			}
			set
			{
				base.NullDisplayText = value;
			}
		}

		/// <summary>
		/// Возвращает/устанавливает имя строки ресурса, 
		/// хранящий строку для свойства NullDisplayText.
		/// </summary>
		/// <value>
		/// Имя ресурса для свойства NullDisplayText.
		/// </value>
		public string NullDisplayTextResourceName { get; set; }

		/// <summary>
		/// Возвращает/устанавливает тип ресурса, хранящий 
		/// строку для свойства NullDisplayText.
		/// </summary>
		/// <value>
		/// Тип ресурса
		/// </value>
		public Type ResourceType { get; set; }

	}
}
