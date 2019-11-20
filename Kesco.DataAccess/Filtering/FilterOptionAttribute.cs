using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Kesco.DataAccess.Filtering
{

	/// <summary>
	/// Определяет контекст для атрибута критерия фильтрации
	/// </summary>
	public class FilterOptionAttributeContext
	{
		/// <summary>
		/// Gets or sets the context.
		/// </summary>
		/// <value>
		/// The context.
		/// </value>
		public FilterSqlBuilderAttribute.CommandContext Context { get; set; }

		/// <summary>
		/// Gets or sets the property.
		/// </summary>
		/// <value>
		/// The property.
		/// </value>
		public PropertyInfo Property { get; set; }

		/// <summary>
		/// Gets or sets the value.
		/// </summary>
		/// <value>
		/// The value.
		/// </value>
		public object Value  { get; set; }

		/// <summary>
		/// Gets or sets the state.
		/// </summary>
		/// <value>
		/// The state.
		/// </value>
		public object State { get; set; }

	}

	/// <summary>
	/// Определяет базовый класс-атрибут для критерия фильтрации 
	/// по заданному полю.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Class, AllowMultiple = false)]
	public abstract class FilterOptionAttribute : Attribute
	{
		/// <summary>
		/// Возвращает или устанавливает имя поля.
		/// </summary>
		/// <value>
		/// Имя поля.
		/// </value>
		public string FieldName { get; set; }

		/// <summary>
		/// Возвращает или устанавливает имя свойства объекта, 
		/// чьё значение будет использоваться при поиске.
		/// </summary>
		/// <value>
		/// Имя свойства объекта.
		/// </value>
		public string PropertyName { get; set; }

		/// <summary>
		/// Строит подготовительный SQL-код для критерия фильтрации.
		/// </summary>
		/// <param name="context">контекст для атрибута критерия фильтрации.</param>
		/// <returns>
		///		SQL-код для критерия фильтрации, который должен быть выполнен перед основным запросом
		/// </returns>
		public virtual string BuildPrecode(FilterOptionAttributeContext context)
		{
			return null;
		}

		/// <summary>
		/// Строит предикат - SQL-код условия фильтрации в конструкции WHERE.
		/// </summary>
		/// <param name="context">контекст для атрибута критерия фильтрации.</param>
		/// <returns>
		///		SQL-код условия фильтрации в конструкции WHERE
		/// </returns>
		public virtual string BuildPredicate(FilterOptionAttributeContext context)
		{
			return null;
		}
	}
}
