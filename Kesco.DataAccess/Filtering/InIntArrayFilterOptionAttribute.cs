using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Kesco.DataAccess.Filtering
{

	/// <summary>
	/// Определяет класс-атрибут для фильтрации по заданному полю
	/// c критерием, что значение поля должно входить в указанный список целых значений.
	/// </summary>
	public class InIntArrayFilterOptionAttribute : ArrayFilterOptionAttribute
	{
		public InIntArrayFilterOptionAttribute() : base() {
			Condition = ValueCondition.Required;
			ValueType = typeof(int);
		}
	}

	/// <summary>
	/// Определяет класс-атрибут для фильтрации по заданному полю
	/// c критерием, что значение поля не должно входить в указанный список целых значений.
	/// </summary>
	public class NotInIntArrayFilterOptionAttribute : ArrayFilterOptionAttribute
	{
		public NotInIntArrayFilterOptionAttribute() : base() {
			Condition = ValueCondition.Exclude;
			ValueType = typeof(int);
		}
	}

	/// <summary>
	/// Определяет класс-атрибут для фильтрации по заданному полю
	/// c критерием, что значение поля должно входить в указанный список целых значений.
	/// </summary>
	public class InStringArrayFilterOptionAttribute : ArrayFilterOptionAttribute
	{
		public InStringArrayFilterOptionAttribute()
			: base()
		{
			Condition = ValueCondition.Required;
			ValueType = typeof(string);
			QuoteValues = true;
		}
	}

	/// <summary>
	/// Определяет класс-атрибут для фильтрации по заданному полю
	/// c критерием, что значение поля не должно входить в указанный список целых значений.
	/// </summary>
	public class NotInStringArrayFilterOptionAttribute : ArrayFilterOptionAttribute
	{
		public NotInStringArrayFilterOptionAttribute()
			: base()
		{
			Condition = ValueCondition.Exclude;
			ValueType = typeof(string);
			QuoteValues = true;
		}
	}

}
