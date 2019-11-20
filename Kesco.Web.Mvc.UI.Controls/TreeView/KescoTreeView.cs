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
	using Controls.Localization;

	/// <summary>
	/// Данный класс реализует установки для модуля 'Json' элемента управления TreeView
	/// </summary>
	public class KescoTreeViewJsonDataPluginSettings
	{
		/// <summary>
		/// Возвращает или устанавливает URL-адрес для модуля Json элемента управления TreeView.
		/// Данные возвращаются в формате Json.
		/// </summary>
		/// <value>
		/// URL адрес.
		/// </value>
		public string Uri { get; set; }

		/// <summary>
		/// Возвращает или устанавливает значение, указывающее кодировать ли данные для передачи традиционным путём.
		/// </summary>
		/// <value>
		///   <c>true</c> кодирование данных для передачи осуществляется традиционным путём; иначе, <c>false</c>.
		/// </value>
		public bool Traditional { get; set; }

		/// <summary>
		/// Возвращает или устанавливает данные в формате Json для элемента TreeView. 
		/// Данное поле может быть javascript функцией, которая возвращает параметры 
		/// для Ajax-запроса
		/// </summary>
		/// <value>
		/// The data.
		/// </value>
		public string Data { get; set; }

		/// <summary>
		/// Указывает является свойство Data javascript функцией или нет].
		/// </summary>
		/// <value>
		/// 	<c>true</c> если поле Data содержит javascript функцию, иначе поле Data содержит данные в формате Json, <c>false</c>.
		/// </value>
		public bool DataIsJavaScriptFunction { get; set; }


		/// <summary>
		/// Указывает Javascript функцию в случае успешного запроса.
		/// </summary>
		/// <value>
		/// Имя клиентской функции
		/// </value>
		public string SuccessFunc { get; set; }

		/// <summary>
		/// Указывает Javascript функцию в случае 
		/// возникновения ошибки во время выполнения запроса.
		/// </summary>
		/// <value>
		/// Имя клиентской функции
		/// </value>
		public string ErrorFunc { get; set; }
	}

	/// <summary>
	/// Указывает CSS классы для элементов дерева
	/// </summary>
	public class KescoTreeViewCssClasses
	{
		/*
			"opened"	: "ui-icon-triangle-1-se",
			"closed"	: "ui-icon-triangle-1-e",
			"item"		: "ui-state-default",
			"item_h"	: "ui-state-hover",
			"item_a"	: "ui-state-active",
			"item_open"	: "ui-icon-folder-open",
			"item_clsd"	: "ui-icon-folder-collapsed",
			"item_leaf"	: "ui-icon-document"
		*/

		/// <summary>
		/// Возвращает или устанавливает CSS стиль иконки для окрытого узла дерева.
		/// </summary>
		/// <value>
		/// CSS стиль иконки
		/// </value>
		public string NodeOpened { get; set; }

		/// <summary>
		/// Возвращает или устанавливает CSS стиль иконки для закрытого узла дерева.
		/// </summary>
		/// <value>
		/// CSS стиль иконки
		/// </value>
		public string NodeClosed { get; set; }

		/// <summary>
		/// Возвращает или устанавливает CSS стиль по умолчанию для элемента с названием узла.
		/// </summary>
		/// <value>
		/// CSS стиль элемента с названием узла
		/// </value>
		public string Item { get; set; }

		/// <summary>
		/// Возвращает или устанавливает CSS стиль 
		/// для элемента узла в состоянии hover.
		/// </summary>
		/// <value>
		/// CSS стиль элемента с названием узла
		/// </value>
		public string ItemHover { get; set; }

		/// <summary>
		/// Возвращает или устанавливает CSS стиль 
		/// для активного элемента узла.
		/// </summary>
		/// <value>
		/// CSS стиль элемента с названием узла
		/// </value>
		public string ItemActive { get; set; }

		/// <summary>
		/// Возвращает или устанавливает CSS стиль иконки
		/// для открытого элемента узла.
		/// </summary>
		/// <value>
		/// CSS стиль иконки для открытого элемента узла
		/// </value>
		public string ItemOpenedIcon { get; set; }

		/// <summary>
		/// Возвращает или устанавливает CSS стиль иконки
		/// для закрытого элемента узла.
		/// </summary>
		/// <value>
		/// CSS стиль иконки для закрытого элемента узла
		/// </value>
		public string ItemClosedIcon { get; set; }

		/// <summary>
		/// Возвращает или устанавливает CSS стиль иконки
		/// для элемента узла, не содержащего дочерних элементов.
		/// </summary>
		/// <value>
		/// CSS стиль элемента с названием узла
		/// </value>
		public string ItemLeafIcon { get; set; }

		public KescoTreeViewCssClasses()
		{
			NodeOpened	= "ui-icon-triangle-1-se";
			NodeClosed	= "ui-icon-triangle-1-e";
			Item = "ui-state-default";
			ItemHover = "ui-state-hover";
			ItemActive = "ui-state-active";
			ItemOpenedIcon = "ui-icon-folder-open";
			ItemClosedIcon = "ui-icon-folder-collapsed";
			ItemLeafIcon = "ui-icon-document";
		}

	}

	/// <summary>
	/// Данный класс определяет обработчики
	/// клиентских событий
	/// </summary>
	public class KescoTreeViewClientEvents
	{
		public string NodeClick { get; set; }

		/// <summary>
		/// Возвращает или устанавливает обработчик клика 
		/// на иконку узла на стороне клиента
		/// </summary>
		/// <value>
		/// Имя Javascript функции
		/// </value>
		public string NodeIconClick { get; set; }

		/// <summary>
		/// Возвращает или устанавливает обработчик 
		/// двойного клика мышью на узел на стороне клиента
		/// </summary>
		/// <value>
		/// Имя Javascript функции
		/// </value>
		public string NodeDoubleClick { get; set; }
	}

	public class KescoTreeView : ControlBase
	{
		public bool AllowEdit { get; set; }
		public bool AllowDragAndDrop { get; set; }
		public bool AllowMultiple { get; set; }

		public string Cookie { get; set; }
		public string MoveNodeUri { get; set; }
		public string RenameNodeUri { get; set; }

		public string LoadingUri { get; set; }
		public string LoadingBefore { get; set; }
		public string LoadingMessage { get; set; }
		public string LoadingErrorMessage { get; set; }

		public string OnInitUri { get; set; }
		public string OnNodeDoubleClickClientAction { get; set; }
		public KescoTreeViewClientEvents ClientEvents { get; internal set; }

		public KescoTreeViewCssClasses CssClasses { get; internal set; }

		public KescoTreeViewJsonDataPluginSettings JsonDataSettings { get; internal set; }

		public string AjaxRequestContext { get; set; }
		public string Root { get; set; }

		/// <summary>
		/// Создаёт экземпляр элемента управления <see cref="KescoButtonBar"/>.
		/// </summary>
		/// <param name="viewContext">Представляет контекст представления <see cref="ViewContext"/> для элемента управления.</param>
		public KescoTreeView(ViewContext viewContext)
			: base(viewContext)
		{
			AjaxRequestContext = null;
			Root = null;
			ClientEvents = new KescoTreeViewClientEvents();
			JsonDataSettings = new KescoTreeViewJsonDataPluginSettings { Traditional = true, Data = null, DataIsJavaScriptFunction = false };
			CssClasses = new KescoTreeViewCssClasses();
			AllowEdit = false;
			AllowDragAndDrop = false;
			AllowMultiple = false;
		}

		/// <summary>
		/// Пишет HTML код, представляющий набор кнопок.
		/// </summary>
		/// <param name="writer">Экземпляр класса <see cref="HtmlTextWriter"/></param>
		protected override void WriteHtml(HtmlTextWriter writer)
		{
			base.WriteHtml(writer);
		}

		protected string PrepareJsonDataPluginSettings()
		{
			string functionAjax_Data =
						@"function(node) {
							var params = {}; 
							if (node) {
								params.parent = node.attr? node.attr('id') : null;
							}
							return params;
						}";

			object data = null;
			object success = "$$$functionAjax_Success$$$";
			object error = "$$$functionAjax_Error$$$";

			if (this.JsonDataSettings.DataIsJavaScriptFunction) {
				data = "$$$functionAjax_Data$$$";
				if (String.IsNullOrEmpty(this.JsonDataSettings.Data))
					this.JsonDataSettings.Data = functionAjax_Data;
			} else data = this.JsonDataSettings.Data;

			var jsonData = new {
				ajax = new {
					url = JsonDataSettings.Uri,
					data = data,
					success = success,
					error = error,
				}
			};

			string jsonizedOptions = new JavaScriptSerializer().Serialize(jsonData);
			
			if (this.JsonDataSettings.DataIsJavaScriptFunction)
				jsonizedOptions = jsonizedOptions.Replace("\"$$$functionAjax_Data$$$\"", this.JsonDataSettings.Data.ToString());

			if (String.IsNullOrEmpty(this.JsonDataSettings.SuccessFunc))
				jsonizedOptions = jsonizedOptions.Replace("\"$$$functionAjax_Success$$$\"", "function (data) { return data; }");
			else
				jsonizedOptions = jsonizedOptions.Replace("\"$$$functionAjax_Success$$$\"", this.JsonDataSettings.SuccessFunc);

			if (String.IsNullOrEmpty(this.JsonDataSettings.ErrorFunc))
				jsonizedOptions = jsonizedOptions.Replace("\"$$$functionAjax_Error$$$\"", "function (x, t, e) { alert('Error') }");
			else
				jsonizedOptions = jsonizedOptions.Replace("\"$$$functionAjax_Error$$$\"", this.JsonDataSettings.ErrorFunc);

			return jsonizedOptions;
				 
		}

		/// <summary>
		/// Пишет скрипт инициализации элемента управления.
		/// </summary>
		/// <param name="writer">Экземпляр класса <see cref="TextWriter"/></param>
		public override void WriteInitializationScript(TextWriter writer)
		{
			base.WriteInitializationScript(writer);
			//List<object> buttons = new List<object>();


			var plugins = new List<string> { "json_data", "ui", "cookies", "search", "hotkeys"};
			if (AllowEdit) plugins.Add("crrm");
			if (AllowDragAndDrop) plugins.Add("dnd");
			if (AllowMultiple) plugins.Add("checkbox");

			plugins.Add("themeroller");

			var options = new {
				plugins = plugins.ToArray(),
				core = new { 
					html_titles = true,
					animation = 0 
				}, 
				json_data = "$$$JsonData$$$",
				ui = new {
						select_limit = 1,
						disable_selecting_children = true
					},
				themes = new {
					theme = "classic"
				},
				cookies = new {
					save_opened = Cookie
				},
				dnd = new {
					drop_target = false,
					drag_target = false,
					copy_modifier = false
				}, 
				themeroller = new {
					opened = CssClasses.NodeOpened,
					closed = CssClasses.NodeClosed,
					item = CssClasses.Item,
					item_h = CssClasses.ItemHover,
					item_a = CssClasses.ItemActive,
					item_open = CssClasses.ItemOpenedIcon,
					item_clsd = CssClasses.ItemClosedIcon,
					item_leaf = CssClasses.ItemLeafIcon,
					item_icon_click = "$$$ItemIconClick$$$"
				}
			};

			string jsonizedOptions = new JavaScriptSerializer().Serialize(options);

			string jsonDataPluginSettings = PrepareJsonDataPluginSettings();

			string itemIconClickHandler = String.IsNullOrEmpty(ClientEvents.NodeIconClick) ? "null" : ClientEvents.NodeIconClick;

			jsonizedOptions = jsonizedOptions
				.Replace("\"$$$ItemIconClick$$$\"", itemIconClickHandler)
				.Replace("\"$$$JsonData$$$\"", jsonDataPluginSettings);

			string treeViewRenameNodeHandlerScript = "";
			string treeViewMoveNodeHandlerScript = "";

			if (AllowEdit) {
				treeViewRenameNodeHandlerScript = String.Format(@"
							.bind('rename.jstree', function (e, data) {{
								if (data.rslt.new_name == data.rslt.old_name) return;
								data.inst.save_cookie();
								confirmAction(
									'{2}'
									, '{3}'
									, '{4}'
									, function() {{
										var meta = data.rslt.obj.data();
								
										$.ajax({{
											url: '{1}', 
											type: 'POST',
											traditional: true,
											data: {{ 
												context: treeView_Instance_{0}.ajaxRequestContext,
												id : meta.ID,
												newName : data.rslt.new_name
											}}, 
											success: function (response) {{
												try {{
													if(response.status == 'ok') {{
														treeView_Instance_{0}.load(null, {8});
													}} else {{
														$.jstree.rollback(data.rlbk);
														alertMessage('{6}', $.validator.format('{7}', response.error), '{4}');
													}}
												}} catch(e) {{ alert(e.description); }}
											}},
											error: function (xhr, status, errorThrown) {{
												try {{
													$.jstree.rollback(data.rlbk);
													alertMessage('{6}', $.validator.format('{7}', xhr.responseText), '{4}');
												}} catch(e) {{ alert(e.description); }}
											}}
										}});
									}}
									, '{5}'
									, function (response) {{
										$.jstree.rollback(data.rlbk);
									}}
								);
							}})"
						, ID // 0
						, RenameNodeUri // 1
						, Resources.KescoTreeView_RenameNode_ConfirmDialog_Title // 2
						, Resources.KescoTreeView_RenameNode_ConfirmDialog_Message // 3
						, Resources.KescoTreeView_DialogButton_OK // 4
						, Resources.KescoTreeView_DialogButton_Cancel // 5
						, Resources.KescoTreeView_RenameNode_ErrorAlert_Title // 6
						, Resources.KescoTreeView_RenameNode_ErrorAlert_Message // 7
						, JsonDataSettings.DataIsJavaScriptFunction ? (JsonDataSettings.Data + "()") : "{}" // 8
					);
			}

			if (AllowDragAndDrop) {
				treeViewMoveNodeHandlerScript = String.Format(@"
					.bind('move_node.jstree', function (e, data) {{
						/*
						.o - the node being moved
						.r - the reference node in the move
						.ot - the origin tree instance
						.rt - the reference tree instance
						.p - the position to move to (may be a string - 'last', 'first', etc)
						.cp - the calculated position to move to (always a number)
						.np - the new parent
						.oc - the original node (if there was a copy)
						.cy - boolen indicating if the move was a copy
						.cr - same as np, but if a root node is created this is -1
						.op - the former parent
						.or - the node that was previously in the position of the moved node
						*/
						var ids = [];
						var parentNode = data.rslt.np.data(); // metadata
						var referenceNode = data.rslt.r.data(); // metadata
						var L = 0;
						switch(data.rslt.p) {{
							case 'after': 
							case 'last': 
								L = referenceNode.R+1;
								break;
							case 'inside': 
							case 'first': 
							case 'before': 
								L = referenceNode.L;
								break;
							default:
								$.jstree.rollback(data.rlbk);
								alert('Неизвестная позиция:'+data.rslt.p);
								return;
								break;
						}}
						data.rslt.o.each(function (index, node) {{
							ids.push($(this).data().ID);
						}})
						confirmAction(
							'{6}'
							, '{7}'
							, '{8}'
							, function() {{
							
								///*
								$.ajax({{
									async : false,
									type: 'POST',
									url: '{1}',
									traditional: true,
									data : {{ 
										context: treeView_Instance_{0}.ajaxRequestContext,
										ids: ids, 
										parent: (parentNode && parentNode.ID)? parentNode.ID : null,
										L: L
									}},
									success : function (response) {{
										if(response.status == 'ok') {{
											//if (parentNode)
												treeView_Instance_{0}.load(null, {10}); //data.inst.refresh(-1);
										}} else {{
											$.jstree.rollback(data.rlbk);
											alertMessage('{4}', response.error+'<br><br>'+response.error_details, '{3}');
										}}
									}},
									error: function(xhr, status, errorThrown) {{
										alertMessage('{4}', $.validator.format('{5}', xhr.responseText), '{3}');
										$.jstree.rollback(data.rlbk);
									}}
								}});//*/
							}}
							, '{9}'
							, function () {{
								$.jstree.rollback(data.rlbk);
							}}
						);
					}})"
					, ID // 0
					, MoveNodeUri //1
					, Resources.KescoTreeView_Alert_DialogTitle_Error // 2
					, Resources.KescoTreeView_DialogButton_OK // 3
					, Resources.KescoTreeView_MoveNode_ErrorAlert_Title // 4
					, Resources.KescoTreeView_MoveNode_ErrorAlert_Message // 5
					, Resources.KescoTreeView_MoveNode_ConfirmDialog_Title // 6
					, Resources.KescoTreeView_MoveNode_ConfirmDialog_Message // 7
					, Resources.KescoTreeView_DialogButton_OK // 8
					, Resources.KescoTreeView_DialogButton_Cancel // 9
					, JsonDataSettings.DataIsJavaScriptFunction?(JsonDataSettings.Data+"()"):"{}" // 10
					);
			}

			writer.WriteLine(@"
			!(function(scope) {{
				var treeView = scope.treeView_Instance_{0} = {{
					selector: '#{0}',
					init: function(treeData) {{
						var plugins = [ 'themes', 'json_data', 'ui', 'cookies', 'search', 'hotkeys', 'returnLink'];
						var options = {1};
						if (treeData) {{
							options.json_data.data = treeData;
						}}
						$(this.selector).jstree(options)
						{2}
						{3}
						.bind('dblclick.jstree', function (e, data) {{
									var node = $(e.target).closest('li');
									$(this).jstree('select_node', node, true, e);
									// Do my action
									if ($.isFunction({7})) {{
										{7}(node);
									}};
						}})
						.bind('before.jstree', function(e, data) {{
							if(data.func == 'rename_node') {{ 
								// do stuff here 
								//alert('hi');
							}}
						}})
						.bind('create_node.jstree', function(e, data) {{
							if(data.func == 'rename_node') {{ 
								// do stuff here 
								//alert('hi');
							}}
						}})
						.bind('close_node.jstree', function(e, data) {{
							var meta = data.rslt.obj.data();
							if (meta && !meta.Reloaded) {{ 
								meta.Reloaded = false;
								data.inst.rename_node(data.rslt.obj, meta.Name);
								if (data.args.length)
									$(data.args[0]).children('ul').remove();
							}}
						}});
					}},
					load: function(url, params) {{
						var self = this;
						var $container = $(this.selector);
						try {{
							$container.jstree('destroy');
							$container.html('{5}');
							url = url?url:'{4}';
							params = $.extend({{
									context: treeView_Instance_{0}.ajaxRequestContext,
									root: treeView_Instance_{0}.root
								}}, params);

    						$.ajax({{
    							url: url,
    							cache: false,
								type: 'POST',
								data: params,
    							dataType: 'json'
								, error: function (xhr, status, errorThrown) {{
									var errorLoadingMsg = '{6}';
 									$container.html(errorLoadingMsg + xhr.status + ' ' + xhr.statusText);
   								}}
								, success: function (response) {{
									var errorLoadingMsg = '{6}';
									try {{
										if (response.status) {{
											if (response.status == 'ok') {{
												self.init(response.model.data);
												if (response.model.message) {{
													alertMessage(response.model.messageTitle, response.model.message, 'OK');
												}}
											}}
											if (response.status == 'error') {{
												$container.html(errorLoadingMsg);
												alertMessage('{9}', response.error+'<br><br>'+response.error_details, 'OK');
											}}
										}} else {{
											self.init(response);
										}}
									}} catch(e) {{}}
								}}
    						}});

						}} catch(e) {{}}
					}},
					getSelectedItems: function () {{
						var self = this;
						var selectedItems = [];
						var selection = $(self.selector).jstree('get_selected');

						selection.each(function(index, item) {{
							selectedItems[index] = {{
								id: $(item).attr('id'),
								label: $(self.selector).jstree('get_path', item).reverse().join('.').replace(' ', '_').replace(/<[^>]+>/ig,''),
								item: item,
								parent: $(self.selector).jstree('_get_parent', $(item)),
								metadata: $(item).data()
							}}
						}});
						return selectedItems;
					}},

					ajaxRequestContext: '{10}',
					root: '{11}'

				}};

				$('#{0}').keypress(function (event) {{
					if (event.keyCode == '13') {{
						event.preventDefault();
					}}
				}});

			}})(window);

				"
				, ID // 0
				, jsonizedOptions // 1
				, treeViewRenameNodeHandlerScript // 2
				, treeViewMoveNodeHandlerScript //3
				, LoadingUri // 4
				, LoadingMessage // 5
				, LoadingErrorMessage // 6
				, String.IsNullOrWhiteSpace(ClientEvents.NodeDoubleClick) ? "$.noop" : ClientEvents.NodeDoubleClick // 7
				, OnInitUri // 8
				, Resources.KescoTreeView_Alert_DialogTitle_Error // 9
				, AjaxRequestContext // 10
				, Root // 11
				);

		}


	}

}
