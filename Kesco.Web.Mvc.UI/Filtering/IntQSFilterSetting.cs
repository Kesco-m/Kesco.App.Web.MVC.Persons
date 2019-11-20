using System;


namespace Kesco.Web.Mvc.Filtering
{
	/// <summary>
	/// Настройки для группы поиска, настраиваемой через URL-параметры.
	/// Значение для всех контролов группы хранится в виде целого числа.
	/// </summary>
	public class IntQSFilterSetting<T> : QSFilterSetting<T>
		where T : IntQSFilterSetting<T>
	{
		private int minValue;
		private int maxValue;

		/// <summary>
		/// Инициализирует экземпляр <see cref="IntQSFilterSetting"/> для группы фильтрации,
		/// представленную целым числом.
		/// </summary>
		public IntQSFilterSetting() : this(int.MinValue, int.MaxValue) { }

		/// <summary>
		/// Инициализирует экземпляр <see cref="IntQSFilterSetting"/> для группы фильтрации,
		/// представленную целым числом.
		/// </summary>
		/// <param name="min">The min.</param>
		/// <param name="max">The max.</param>
		public IntQSFilterSetting(int min, int max): base()
		{
			minValue = min;
			maxValue = max;
		}

		/// <summary>
		/// Sets the range.
		/// </summary>
		/// <param name="min">The min.</param>
		/// <param name="max">The max.</param>
		/// <returns></returns>
		public T SetRange(int min, int max)
		{
			minValue = min;
			maxValue = max;
			AdjustValueForClient();
			return this as T;
		}

		/// <summary>
		/// Приведение значение к целому числу с учётом ограничений по MIN & MAX
		/// </summary>
		protected override void AdjustValueForClient()
		{
			base.AdjustValueForClient();
			int n = 0;
			if (Value != null)
				Int32.TryParse(Value.ToString(), out n);

			if (n < minValue) n = minValue;
			if (maxValue < n) n = maxValue;

			Value = n;
		}

		/// <summary>
		/// Возвращает значение фильтра в виде целого числа
		/// </summary>
		/// <returns>Число</returns>
		public int GetValue()
		{
			return (int) Value;
		}
	}
}
