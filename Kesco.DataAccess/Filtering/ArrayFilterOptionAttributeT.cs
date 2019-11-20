using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Kesco.DataAccess.Filtering
{

	/// <summary>
	/// Определяет базовый класс-атрибут для критерия фильтрации, 
	/// представленным массивом значений по заданному полю.
	/// Свойство Condition определяет признак либо значение поля 
	///		- должно быть в списке значений
	///		- не должно быть в списке значений
	/// </summary>
	public class ArrayFilterOptionAttribute : FilterOptionAttribute
		//where T : IComparable
	{
		/// <summary>
		/// Признаки вхождения значение поля в список заданных значений
		/// </summary>
		public enum ValueCondition : int
		{
			Required = 1,
			Exclude = 2
		}

		/// <summary>
		/// Служит для хранения приведённого списка значений
		/// </summary>
		class State
		{
			public IEnumerable<IComparable> AdjustedValue { get; set; }
		}

		/// <summary>
		/// Gets or sets the type of the value.
		/// </summary>
		/// <value>
		/// The type of the value.
		/// </value>
		public Type ValueType { get; set; }

		/// <summary>
		/// Возвращает или устанавливает условие вхождения 
		/// значения поля в список заданных значений 
		/// </summary>
		/// <value>
		/// Условие вхождения значения поля
		/// </value>
		public ValueCondition Condition { get; set; }

		/// <summary>
		/// Возвращает или устанавливает признак экранирования 
		/// значений из заданного списка в SQL-строки.
		/// </summary>
		/// <value>
		///   <c>true</c> если требуется экранировать в SQL-строки; если нет, то <c>false</c>.
		/// </value>
		public bool QuoteValues { get; set; }

		/// <summary>
		/// Возвращает или устанавливает признак, что допускается пустой список.
		/// В этом случае, значение поля в зависимости от значения свойства <see cref="Condition"/>.
		///		- <c>ValueCondition.Required</c> - должно содержать значение NULL.
		///		- <c>ValueCondition.Exclude</c> - не должно содержать значение NULL.
		/// </summary>
		/// <value>
		///   <c>true</c> если допускается пустой список; иначе <c>false</c>.
		/// </value>
		public bool AllowEmpty { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="ArrayFilterOptionAttribute" /> class.
		/// </summary>
		public ArrayFilterOptionAttribute()
			: base()
		{
			Condition = ValueCondition.Required;
			QuoteValues = false;
		}

		/// <summary>
		/// Строит подготовительный SQL-код для критерия фильтрации.
		/// </summary>
		/// <param name="context">контекст для атрибута критерия фильтрации.</param>
		/// <returns>
		/// SQL-код для критерия фильтрации, который должен быть выполнен перед основным запросом
		/// </returns>
		public override string BuildPrecode(FilterOptionAttributeContext context)
		{
			var state = new State();
			context.State = state;

			if (context.Value != null && context.Value is IEnumerable) {
				var defValue = this.ValueType.IsValueType ? Activator.CreateInstance(ValueType) : null;
				var list = new List<IComparable>();
				var values = context.Value as IEnumerable;
				foreach(var value in values) {
					if (value is IComparable && defValue != value) {
						list.Add((IComparable) (QuoteValues ? GetQuotedValue(value) : value));
					}
				}
				state.AdjustedValue = list.ToArray();
			}
			return null;
		}

		/// <summary>
		/// Строит предикат - SQL-код условия фильтрации в конструкции WHERE.
		/// </summary>
		/// <param name="context">контекст для атрибута критерия фильтрации.</param>
		/// <returns>
		/// SQL-код условия фильтрации в конструкции WHERE
		/// </returns>
		public override string BuildPredicate(FilterOptionAttributeContext context)
		{
			var state = context.State as State;
			if (state == null || state.AdjustedValue == null)
				return null;
			
			int count = state.AdjustedValue.Count();
			if (!AllowEmpty && count == 0) 
				return null;

			string expression = String.Empty;
			if (count == 0) {
				return String.Format(
						"(T0.{0} IS {1}NULL)", 
						FieldName,
						Condition == ValueCondition.Exclude ? "NOT " : ""
					);
			}

			if (count == 1) {
				expression = String.Format("{1}= {0}", state.AdjustedValue.First(), Condition == ValueCondition.Exclude?"!":"");
			} else {
				expression = String.Format("{1}IN ({0})", String.Join(", ", state.AdjustedValue), Condition == ValueCondition.Exclude ? "NOT " : "");
			}
			return String.Format("(T0.{0} {1})", FieldName, expression);
		}

		/// <summary>
		/// Возвращает значение в виде экранированной SQL-строки.
		/// </summary>
		/// <param name="value">Значение.</param>
		/// <returns>Значение в виде экранированной SQL-строки</returns>
		protected virtual object GetQuotedValue(object value)
		{
			return "'"+value.ToString().EscapeSqlQoutes()+"'";
		}
	}

}
