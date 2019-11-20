using System;


namespace Kesco.Web.Mvc.Filtering
{
	/// <summary>
	/// Настройки группы, имеющей только нередактируемое представление на форме
	/// </summary>
	public class ConstantFilterSetting : FilterSetting<ConstantFilterSetting>
	{
		/// <summary>
		/// Инициализирует экземпляр <see cref="ConstantFilterSetting"/>
		/// настройки группы, имеющей только нередактируемое представление на форме.
		/// </summary>
		public ConstantFilterSetting() : base(false, true, String.Empty) { }

		/// <summary>
		/// Инициализирует экземпляр <see cref="ConstantFilterSetting"/>
		/// настройки группы, имеющей только нередактируемое представление на форме.
		/// </summary>
		/// <param name="value">The value.</param>
		public ConstantFilterSetting(object value) : base(false, true, value) { }
	}

}
