using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.Script.Serialization;
using System.Globalization;

namespace Kesco.Web.Mvc.UI
{
	public class KescoTabs : ControlBase
	{
	
		/// <summary>
		/// Создаёт экземпляр элемента управления <see cref="KescoButtonBar"/>.
		/// </summary>
		/// <param name="viewContext">Представляет контекст представления <see cref="ViewContext"/> для элемента управления.</param>
		public KescoTabs(ViewContext viewContext)
			: base(viewContext)
		{
			Culture = CultureInfo.CurrentUICulture;
			Tabs = new List<KescoTabItem>();
		}


		/// <summary>
		/// Возвращает или устанавливает локализацию для элемента управления
		/// </summary>
		/// <value>
		/// Локализация элемента управления
		/// </value>
		public CultureInfo Culture { get; set; }

		/// <summary>
		/// Возвращает или устанавливает список закладок
		/// </summary>
		/// <value>
		/// Список закладок
		/// </value>
		public List<KescoTabItem> Tabs { get; protected set; }

		public bool AutoResize { get; set; }

		/// <summary>
		/// Пишет HTML код, представляющий набор кнопок.
		/// </summary>
		/// <param name="writer">Экземпляр класса <see cref="HtmlTextWriter"/></param>
		protected override void WriteHtml(HtmlTextWriter writer)
		{
			KescoTabsHtmlBuilder builder = new KescoTabsHtmlBuilder(this);
			writer.Write(builder.ContainerTag());
			base.WriteHtml(writer);
		}

		/// <summary>
		/// Пишет скрипт инициализации элемента управления.
		/// </summary>
		/// <param name="writer">Экземпляр класса <see cref="TextWriter"/></param>
		public override void WriteInitializationScript(TextWriter writer)
		{
			base.WriteInitializationScript(writer);
			List<object> tabs = new List<object>();

			var options = new
			{
			};


			string jsonizedOptions = new JavaScriptSerializer().Serialize(options);

			string autoResizeScript = @"
					$(function() {{
							var resizeTabContainer = function() {{
								var $container = $('#{0}');
								$container.height($container.parent().height()-20);
								$container.width($container.parent().width()-15);
							}};
							$(window).resize(resizeTabContainer);
							var resizeTabs = function() {{
								var $container = $('#{0}');
								var $tabs = $container.children('div');
								var $links = $container.children('ul');
								var height = $container.height()-$links.height();
								var width = $container.width();

								$tabs.height(height-40);
								$tabs.width(width-30);
							}};
							$(window).resize(resizeTabs);
					}});
			";
			writer.WriteLine(@"
					$(document).ready(function() {{
						var options = {1};
						$.extend(options, {{}} );
						$('#{0}').tabs(options);
					}});
{3}

					"
				, ID, jsonizedOptions, Culture.IetfLanguageTag.Substring(0,2), autoResizeScript);

		}

	}
}
