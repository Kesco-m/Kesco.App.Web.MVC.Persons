using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.Script.Serialization;

namespace Kesco.Web.Mvc.UI
{
	public class KescoMenu : ControlBase
	{
	
		/// <summary>
		/// Создаёт экземпляр элемента управления <see cref="KescoButtonBar"/>.
		/// </summary>
		/// <param name="viewContext">Представляет контекст представления <see cref="ViewContext"/> для элемента управления.</param>
		public KescoMenu(ViewContext viewContext)
			: base(viewContext)
		{
			Menu = new List<KescoMenuItem>();
		}

		/// <summary>
		/// Возвращает или устанавливает список кнопок
		/// </summary>
		/// <value>
		/// Список кнопок
		/// </value>
		public List<KescoMenuItem> Menu { get; set; }

		/// <summary>
		/// Пишет HTML код, представляющий набор кнопок.
		/// </summary>
		/// <param name="writer">Экземпляр класса <see cref="HtmlTextWriter"/></param>
		protected override void WriteHtml(HtmlTextWriter writer)
		{
			KescoMenuHtmlBuilder builder = new KescoMenuHtmlBuilder(this);
			writer.Write(builder.DivTag());
			base.WriteHtml(writer);
		}

		/// <summary>
		/// Пишет скрипт инициализации элемента управления.
		/// </summary>
		/// <param name="writer">Экземпляр класса <see cref="TextWriter"/></param>
		public override void WriteInitializationScript(TextWriter writer)
		{
			base.WriteInitializationScript(writer);
/*
			List<object> buttons = new List<object>();


			var options = new
			{
				form = "",
				buttons = new List<dynamic>()
			};

			Buttons.ForEach((item) =>
			{
				options.buttons.Add(new { text = item.Caption
						, uri = item.Uri
						, targetForm = item.TargetForm
						, dialogWidth = item.DialogWidth
						, dialogHeight = item.DialogHeight
						, buttonType = item.ButtonType
						, callback = item.CallbackFunc
						, icons = new {
							primary = item.PrimaryIcon,
							secondary = item.SecondaryIcon
						}
				});
			});



			string jsonizedOptions = new JavaScriptSerializer().Serialize(options);

			writer.WriteLine(@"
					$(document).ready(function() {{
						$('#{0}').kescoButtonBar({1});
					}});
					"
				, ID, jsonizedOptions);
*/
		}

	}
}
