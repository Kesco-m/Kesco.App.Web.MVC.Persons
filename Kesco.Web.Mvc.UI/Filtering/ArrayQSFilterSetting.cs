using System;


namespace Kesco.Web.Mvc.Filtering
{
	/// <summary>
	/// Настройки для группы поиска, настраиваемой через URL-параметры
	/// Значение для контролов группы хранится в соответствующих элементах массива строк.
	/// </summary>
	public class ArrayQSFilterSetting : QSFilterSetting<ArrayQSFilterSetting>
	{
		/// <summary>
		/// Конвертирует значение группы настройки фильтрации к форме, воспринимаемым клиентом.
		/// Значение представляется массивом целых чисел, представлющих битовую маску.
		/// </summary>
		protected override void AdjustValueForClient()
		{
			base.AdjustValueForClient();
			Value = (Value ?? String.Empty).ToString().SplitToArrayOfBinaryDigits();
		}

		/// <summary>
		/// Возвращает значение фильтра в виде целого числа.
		/// </summary>
		/// <returns>Число</returns>
		public int? GetValue()
		{
			int? v = null;
			if (OriginalValue != null)
			{
				int value = 0;
				Int32.TryParse(OriginalValue.ToString(), out value);
				v = value;
			}
			return v;
		}

	}
}
