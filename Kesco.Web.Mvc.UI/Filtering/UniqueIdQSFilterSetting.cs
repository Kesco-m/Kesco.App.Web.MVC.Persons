using System;


namespace Kesco.Web.Mvc.Filtering
{
	/// <summary>
	/// Целочисленный параметр, являющийся уникальным ключом (кодом сущности)
	/// </summary>
	public class UniqueIdQSFilterSetting : IntQSFilterSetting<UniqueIdQSFilterSetting>
	{
		public UniqueIdQSFilterSetting() : base(0, Int32.MaxValue) {}
	}
}