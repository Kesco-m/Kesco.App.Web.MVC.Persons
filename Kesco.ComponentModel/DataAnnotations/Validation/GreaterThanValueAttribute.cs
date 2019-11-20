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
	/// Класс-аттрибут для указания, что значение свойства 
	/// должно быть больше или равно заданного значения
	/// </summary>
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
	public class GreaterThanAttribute : ValidationAttribute
	{

		public object MinValue { get; private set; }
		public Type OperandType { get; private set; }
		private Func<object, object> Conversion	{ get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="GreaterThanAttribute" /> class.
		/// </summary>
		/// <param name="minValue">The min value.</param>
		public GreaterThanAttribute(object minValue)
		{
			MinValue = minValue;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="GreaterThanAttribute" /> class.
		/// </summary>
		/// <param name="minValue">The min value.</param>
		public GreaterThanAttribute(int minValue) : this()
		{
			this.MinValue = minValue;
			this.OperandType = typeof(int);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="GreaterThanAttribute" /> class.
		/// </summary>
		/// <param name="minValue">The min value.</param>
		public GreaterThanAttribute(double minValue) : this()
		{
			this.MinValue = minValue;
			this.OperandType = typeof(double);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="GreaterThanAttribute" /> class.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="minValue">The min value.</param>
		public GreaterThanAttribute(Type type, string minValue)
			: this()
		{
			this.OperandType = type;
			this.MinValue = minValue;
		}

		/// <summary>
		/// Prevents a default instance of the <see cref="GreaterThanAttribute" /> class from being created.
		/// </summary>
		private GreaterThanAttribute()
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
			return comparable.CompareTo(obj) < 0;
		}

		/// <summary>
		/// Устанавливает внутренние соглашения для сравнения значений проверяемого свойства с указанныи минимальным значением.
		/// </summary>
		/// <exception cref="System.InvalidOperationException"></exception>
		private void SetupConversion()
		{
			if (this.Conversion == null) {
				object maximum = this.MinValue;
				if (maximum == null) {
					throw new InvalidOperationException(Resources.MinValueAttribute_Must_Set_MinValue);
				}
				Type type2 = maximum.GetType();
				if (type2 == typeof(int)) {
					this.Initialize((int)maximum, (object v) => Convert.ToInt32(v, CultureInfo.InvariantCulture));
					return;
				}
				if (type2 == typeof(double)) {
					this.Initialize((double)maximum, (object v) => Convert.ToDouble(v, CultureInfo.InvariantCulture));
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
				IComparable maximum2 = (IComparable)converter.ConvertFromString((string)maximum);
				Func<object, object> conversion = delegate(object value)
				{
					if (value == null || !(value.GetType() == type)) {
						return converter.ConvertFrom(value);
					}
					return value;
				};
				this.Initialize(maximum2, conversion);
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
		/// Initializes the specified maximum.
		/// </summary>
		/// <param name="maximum">The maximum.</param>
		/// <param name="conversion">The conversion.</param>
		private void Initialize(IComparable maximum, Func<object, object> conversion)
		{
			this.MinValue = maximum;
			this.Conversion = conversion;
		}

	}
}
