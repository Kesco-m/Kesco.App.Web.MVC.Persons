using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Kesco.ComponentModel.DataAnnotations
{

	/// <summary>
	/// Определяет атрибут проверки, определяющий минимальное значение для свойства
	/// </summary>
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
	public class MinValueAttribute : ValidationAttribute
	{

		/// <summary>
		/// Возвращает минимальное значение для свойства/поля.
		/// </summary>
		public object MinValue { get; private set; }

		/// <summary>
		/// Возвращает тип минимального значения.
		/// </summary>
		/// <value>
		/// Тип операнда.
		/// </value>
		public Type OperandType { get; private set; }

		private Func<object, object> Conversion	{ get; set; }

		/// <summary>
		/// Инициализирует экземпляр класса <see cref="MinValueAttribute"/>.
		/// </summary>
		/// <param name="minValue">Минимальное значение для свойства.</param>
		public MinValueAttribute(object minValue)
		{
			MinValue = minValue;
		}

		/// <summary>
		/// Инициализирует экземпляр класса <see cref="MinValueAttribute"/>.
		/// </summary>
		/// <param name="minValue">Минимальное значение для свойства.</param>
		public MinValueAttribute(int minValue) : this()
		{
			this.MinValue = minValue;
			this.OperandType = typeof(int);
		}

		/// <summary>
		/// Инициализирует экземпляр класса <see cref="MinValueAttribute"/>.
		/// </summary>
		/// <param name="minValue">Минимальное значение для свойства.</param>
		public MinValueAttribute(decimal minValue)
			: this()
		{
			this.MinValue = minValue;
			this.OperandType = typeof(decimal);
		}

		/// <summary>
		/// Инициализирует экземпляр класса <see cref="MinValueAttribute"/>.
		/// </summary>
		/// <param name="minValue">Минимальное значение для свойства.</param>
		public MinValueAttribute(double minValue)
			: this()
		{
			this.MinValue = minValue;
			this.OperandType = typeof(double);
		}

		/// <summary>
		/// Инициализирует экземпляр класса  <see cref="MinValueAttribute"/>.
		/// </summary>
		/// <param name="type">Тип параметра</param>
		/// <param name="minValue">Минимальное значение.</param>
		public MinValueAttribute(Type type, string minValue)
			: this()
		{
			this.OperandType = type;
			this.MinValue = minValue;
		}

		/// <summary>
		/// Закрытый конструктор класса <see cref="MinValueAttribute"/>, для предотвращения создания экземпляра по умолчанию.
		/// </summary>
		private MinValueAttribute()
			: base(() => Resources.MinValueAttribute_ValidationError)
		{
		}

		/// <summary>
		/// Определяет, является ли заданное значение объекта допустимым.
		/// </summary>
		/// <param name="value">Значение объекта, который требуется проверить.</param>
		/// <returns>
		/// Значение true, если значение допустимо, в противном случае — значение false.
		/// </returns>
		public override bool IsValid(object value)
		{
			this.SetupConversion();
			if (value == null) {
				return true;
			}
			string text = value as string;
			if (text != null && string.IsNullOrEmpty(text)) {
				return true;
			}
			object obj = null;
			try {
				obj = this.Conversion(value);
			} catch (FormatException) {
				bool result = false;
				return result;
			} catch (InvalidCastException) {
				bool result = false;
				return result;
			} catch (NotSupportedException) {
				bool result = false;
				return result;
			}
			IComparable comparable = (IComparable) this.MinValue;
			return comparable.CompareTo(obj) <= 0;
		}

		/// <summary>
		/// Устанавливает внутренние соглашения для сравнения значений проверяемого свойства с указанныи минимальным значением.
		/// </summary>
		private void SetupConversion()
		{
			if (this.Conversion == null) {
				object minValue = this.MinValue;
				if (minValue == null) {
					throw new InvalidOperationException(Resources.MinValueAttribute_Must_Set_MinValue);
				}
				Type type2 = minValue.GetType();
				if (type2 == typeof(int)) {
					this.Initialize((int)minValue, (object v) => Convert.ToInt32(v, CultureInfo.CurrentCulture));
					return;
				}
				if (type2 == typeof(double)) {
					this.Initialize((double) minValue, (object v) => Convert.ToDouble(v, CultureInfo.CurrentCulture));
					return;
				}
				if (type2 == typeof(decimal)) {
					this.Initialize((decimal) minValue, (object v) => Convert.ToDecimal(v, CultureInfo.CurrentCulture));
					return;
				}
				Type type = this.OperandType;
				if (type == null) {
					throw new InvalidOperationException(Resources.MinValueAttribute_Must_Set_Operand_Type);
				}
				Type typeFromHandle = typeof(IComparable);
				if (!typeFromHandle.IsAssignableFrom(type)) {
					throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.MinValueAttribute_ArbitraryTypeNotIComparable, new object[]
					{
						type.FullName, 
						typeFromHandle.FullName
					}));
				}
				TypeConverter converter = TypeDescriptor.GetConverter(type);
				IComparable minValue2 = (IComparable) converter.ConvertFromString((string)minValue);
				Func<object, object> conversion = delegate(object value)
				{
					if (value == null || !(value.GetType() == type)) {
						return converter.ConvertFrom(value);
					}
					return value;
				};
				this.Initialize(minValue2, conversion);
			}
		}

		/// <summary>
		/// Применяет к сообщению об ошибке форматирование на основе поля данных, в котором произошла ошибка.
		/// </summary>
		/// <param name="name">Имя, которое должно быть включено в отформатированное сообщение.</param>
		/// <returns>
		/// Экземпляр форматированного сообщения об ошибке.
		/// </returns>
		public override string FormatErrorMessage(string name)
		{
			this.SetupConversion();
			return string.Format(CultureInfo.CurrentCulture, base.ErrorMessageString, new object[]
			{
				name, 
				this.MinValue
			});
		}

		/// <summary>
		/// Инициализирует минимальное значение и внутреннее соглашения для сравнения
		/// </summary>
		/// <param name="minimum">Минимальное значение.</param>
		/// <param name="conversion">Функция, задающая соглашение для сравнения минимального значения.</param>
		private void Initialize(IComparable minimum, Func<object, object> conversion)
		{
			this.MinValue = minimum;
			this.Conversion = conversion;
		}

	}
}
