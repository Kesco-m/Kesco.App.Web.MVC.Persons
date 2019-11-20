namespace Kesco.Web.Mvc.Filtering
{
	/// <summary>
	/// Настройки для группы поиска, настраиваемой через URL-параметры
	/// </summary>
	public class BooleanQSFilterSetting : IntQSFilterSetting<BooleanQSFilterSetting>
	{
		public BooleanQSFilterSetting() : base(0,1) { }

		/// <summary>
		/// Значение считается истинным, если параметр = 1
		/// </summary>
		protected override void AdjustValueForClient()
		{
			base.AdjustValueForClient();
			//if (Value)
			Value = (Value ?? "0").ToString() == "1";
		}
	}
}
