using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Kesco.Web.Mvc.UI
{
	using Kesco.Web.Mvc.UI.Controls.Localization;

	/// <summary>
	/// Класс-помощник реализует HTML построение элемента управления
	/// <see cref="KescoDateRange"/>
	/// </summary>
	public class KescoDateRangeHtmlBuilder
	{
		/// <summary>
		/// Gets the control.
		/// </summary>
		public KescoDateRange Control { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="KescoDateRangeHtmlBuilder"/> class.
		/// </summary>
		/// <param name="control">The control.</param>
		public KescoDateRangeHtmlBuilder(KescoDateRange control)
		{
			Control = control; 
		}

		/// <summary>
		/// Создаёт div HTML-тег, контайнер для кнопок.
		/// </summary>
		/// <returns>Строка, содержащая HTML-тег.</returns>
		public string InputTag()
		{
			TagBuilder tag = new TagBuilder("INPUT");
			tag.Attributes.Add("type", "text");
			tag.Attributes.Add("id", Control.ID);
			tag.Attributes.Add("name", Control.Name);
			tag.Attributes.Add("size", "10");
			tag.Attributes.Add("maxlength", "10");
			if (Control.Value.HasValue)
				tag.Attributes.Add("value", Control.Value.Value.ToLocalTime().ToShortDateString());
			return tag.ToString(TagRenderMode.SelfClosing);
		}

		/// <summary>
		/// Создаёт div HTML-тег, контайнер для кнопок.
		/// </summary>
		/// <returns>Строка, содержащая HTML-тег.</returns>
		public string TableTag()
		{
			StringBuilder sb = new StringBuilder();

			sb.Append("\t<tr>\n");
			sb.Append("\t\t<td>\n");
			sb.Append(SelectMenuTag());
			sb.Append("\t\t</td>\n");

			sb.Append("\t\t<td>\n");
			sb.AppendFormat("\t\t<a id='{0}___PrevButton' href='javascript: void(0);' title='{1}'><span class='ui-icon ui-icon-circle-triangle-w'>&nbsp;</span></a>"
				, Control.ID, Resources.KescoDateRangeControl_PrevButton);
			sb.Append("\t\t</td>\n");

			sb.AppendFormat("\t\t<td id='{0}___CellLabelFrom'>{1}</td>\n"
				, Control.ID, Resources.KescoDateRangeControl_Label_From);

			if (String.IsNullOrEmpty(Control.DatePickerFrom.Name))
				Control.DatePickerFrom.Name = String.Format("{0}.From", Control.Name);
			sb.Append("\t\t<td>\n");
			sb.Append(Control.DatePickerFrom.ToHtmlString());
			sb.Append("\t\t</td>\n");

			sb.AppendFormat("\t\t<td id='{0}___CellLabelTo'>{1}</td>\n"
				, Control.ID, Resources.KescoDateRangeControl_Label_To);

			if (String.IsNullOrEmpty(Control.DatePickerTo.Name))
				Control.DatePickerTo.Name = String.Format("{0}.To", Control.Name);
			sb.Append("\t\t<td>\n");
			sb.Append(Control.DatePickerTo.ToHtmlString());
			sb.Append("\t\t</td>\n");

			sb.Append("\t\t<td>\n");
			sb.AppendFormat("\t\t<a id='{0}___NextButton' href='javascript: void(0);' title='{1}'><span class='ui-icon ui-icon-circle-triangle-e'>&nbsp;</span></a>"
				, Control.ID, Resources.KescoDateRangeControl_NextButton);
			sb.Append("\t\t</td>\n");

			sb.Append("\t</tr>\n");

			TagBuilder builder = new TagBuilder("table");
			if (!String.IsNullOrEmpty(Control.LayoutCssClass))
				builder.AddCssClass(Control.LayoutCssClass);

			if (!String.IsNullOrEmpty(Control.LayoutCssStyle))
				builder.Attributes["style"] = (Control.LayoutCssStyle);

			builder.MergeAttributes(Control.HtmlAttributes, false);
			builder.InnerHtml = sb.ToString();

			return builder.ToString(TagRenderMode.Normal);

		}

		protected virtual string SelectMenuTag() {
			TagBuilder builder = new TagBuilder("select");
			builder.Attributes.Add("name", Control.Name + ".RangeSelector");
			builder.Attributes.Add("id", Control.ID + "_RangeSelector");
			builder.InnerHtml = String.Format(@"\n
						<option value='day'>{0}</option>
						<option value='week'>{1}</option>
						<option value='month'>{2}</option>
						<option value='quarter'>{3}</option>
						<option value='year'>{4}</option>
						<option value='any'>{5}</option>"
				, Resources.KescoDateRangeControl_SelectMenu_Day
				, Resources.KescoDateRangeControl_SelectMenu_Week
				, Resources.KescoDateRangeControl_SelectMenu_Month
				, Resources.KescoDateRangeControl_SelectMenu_Quarter
				, Resources.KescoDateRangeControl_SelectMenu_Year
				, Resources.KescoDateRangeControl_SelectMenu_Arbitrarily
				);
			return builder.ToString(TagRenderMode.Normal);
		}

	}
}
