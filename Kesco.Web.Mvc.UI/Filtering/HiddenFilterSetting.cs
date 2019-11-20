
namespace Kesco.Web.Mvc.Filtering
{
	/// <summary>
	/// Настройки группы, неявно присутствующей на форме:
	/// участвует только в поиске, значение устанавливается через URL-параметры, на форме не отображается
	/// </summary>
	public class HiddenFilterSetting : FilterSetting<HiddenFilterSetting>
	{
		public HiddenFilterSetting() : base(false, true) { }
	}

}
