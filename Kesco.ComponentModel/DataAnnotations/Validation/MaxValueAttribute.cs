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
	/// Определяет атрибут проверки, определяющий максимальное значение для свойства
	/// </summary>
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
	public class MaxValueAttribute : ValidationAttribute
	{

		/// <summary>
		/// Возвращает максимальное значение для свойства/поля.
		/// </summary>
		public object MaxValue { get; private set; }

		/// <summary>
		/// Возвращает тип максимального значения.
		/// </summary>
		/// <value>
		/// Тип максимального значения.
		/// </value>
		public Type OperandType { get; private set; }

		private Func<object, object> Conversion	{ get; set; }

		/// <summary>
		/// Инициализирует экземпляр класса <see cref="MaxValueAttribute"/>.
		/// </summary>
		/// <param name="maxValue">Максимальное значение для свойства.</param>
		public MaxValueAttribute(object maxValue)
		{
			MaxValue = maxValue;
		}

		/// <summary>
		/// Инициализирует экземпляр класса <see cref="MaxValueAttribute"/>.
		/// </summary>
		/// <param name="maxValue">Максимальное значение для свойства.</param>
		public MaxValueAttribute(int maxValue)
			: this()
		{
			this.MaxValue = maxValue;
			this.OperandType = typeof(int);
		}

		/// <summary>
		/// Инициализирует экземпляр класса <see cref="MaxValueAttribute"/>.
		/// </summary>
		/// <param name="maxValue">Максимальное значение для свойства.</param>
		public MaxValueAttribute(double maxValue)
			: this()
		{
			this.MaxValue = maxValue;
			this.OperandType = typeof(double);
		}

		/// <summary>
		/// Инициализирует экземпляр класса <see cref="MaxValueAttribute"/>.
		/// </summary>
		/// <param name="type">Тип значения</param>
		/// <param name="maxValue">Максимальное значение для свойства.</param>
		public MaxValueAttribute(Type type, string maxValue)
			: this()
		{
			this.OperandType = type;
			this.MaxValue = maxValue;
		}

		/// <summary>
		/// Закрытый конструктор класса <see cref="MaxValueAttribute"/>, для предотвращения создания экземпляра по умолчанию.
		/// </summary>
		private MaxValueAttribute()
			: base(() => Resources.MaxValueAttribute_ValidationError)
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
			IComparable comparable = (IComparable) this.MaxValue;
			return comparable.CompareTo(obj) >= 0;
		}

		/// <summary>
		/// Устанавливает внутренние соглашения для сравнения значений проверяемого свойства с указанныи минимальным значением.
		/// </summary>
		private void SetupConversion()
		{
			if (this.Conversion == null) {
				object maximum = this.MaxValue;
				if (maximum == null) {
					throw new InvalidOperationException(Resources.MaxValueAttribute_Must_Set_MaxValue);
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
					throw new InvalidOperationException(Resources.MaxValueAttribute_Must_Set_Operand_Type);
				}
				Type typeFromHandle = typeof(IComparable);
				if (!typeFromHandle.IsAssignableFrom(type)) {
					throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.MaxValueAttribute_ArbitraryTypeNotIComparable, new object[]
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
				this.MaxValue
			});
		}

		/// <summary>
		/// Инициализирует максимальное значение и внутреннее соглашения для сравнения
		/// </summary>
		/// <param name="maximum">Максимальное значение.</param>
		/// <param name="conversion">Функция, задающая соглашение для сравнения минимального значения.</param>
		private void Initialize(IComparable maximum, Func<object, object> conversion)
		{
			this.MaxValue = maximum;
			this.Conversion = conversion;
		}

	}
}
