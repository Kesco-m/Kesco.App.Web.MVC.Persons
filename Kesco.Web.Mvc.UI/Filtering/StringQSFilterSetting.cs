using System;

namespace Kesco.Web.Mvc.Filtering
{
	/// <summary>
	/// Настройки для группы поиска, настраиваемой через URL-параметры.
	/// Значение для всех контролов группы хранится в виде строки.
	/// </summary>
	public class StringQSFilterSetting : QSFilterSetting<StringQSFilterSetting>
	{
		/// <summary>
		/// Adjusts the value for client.
		/// </summary>
		protected override void AdjustValueForClient()
		{
			base.AdjustValueForClient();
			Value = (Value ?? String.Empty).ToString();
		}

		/// <summary>
		/// Возвращает значение фильтра в виде строки.
		/// </summary>
		/// <returns>Число</returns>
		public string GetValue()
		{
			return (string) Value;
		}
	}
}
