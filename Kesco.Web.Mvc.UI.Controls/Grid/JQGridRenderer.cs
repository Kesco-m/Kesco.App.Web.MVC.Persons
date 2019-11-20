namespace Kesco.Web.Mvc.UI.Grid
{
    using System;
    using System.Collections;
    using System.Text;
    using System.Web.Script.Serialization;
	using System.Collections.Generic;

    internal class JQGridRenderer
    {
		public string RenderHtml(JQGrid grid, bool includeJS = true)
		{
			string text = "<table id='{0}'></table>";
			if (grid.ToolBarSettings.ToolBarPosition != ToolBarPosition.Hidden) {
				text += "<div id='{0}_pager'></div>";
			}
			if (string.IsNullOrEmpty(grid.ID)) {
				throw new Exception("You need to set ID for this grid.");
			}
			text = string.Format(text, grid.ID);
			if (includeJS) {
				if (grid.HierarchySettings.HierarchyMode == HierarchyMode.Child || grid.HierarchySettings.HierarchyMode == HierarchyMode.ParentAndChild) {
					text += this.GetChildSubGridJavaScript(grid);
				} else {
					text += this.GetStartupJavascript(grid, false);
				}
			}
			return text;
		}

		public string GetJavascriptCode(JQGrid grid)
		{
			if (grid.HierarchySettings.HierarchyMode == HierarchyMode.Child || grid.HierarchySettings.HierarchyMode == HierarchyMode.ParentAndChild) {
				return this.GetChildSubGridJavaScript(grid, true);
			} else {
				return this.GetStartupJavascript(grid, false, true);
			}
		}

		private string GetStartupJavascript(JQGrid grid, bool subgrid, bool doNotPutInScriptTag = false)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (!doNotPutInScriptTag) {
				stringBuilder.Append("<script type='text/javascript'>\n");
			}
			stringBuilder.Append("jQuery(document).ready(function() {");
			stringBuilder.Append(this.GetStartupOptions(grid, subgrid));
			stringBuilder.Append("});");
			if (!doNotPutInScriptTag) {
				stringBuilder.Append("</script>");
			}
			return stringBuilder.ToString();
		}
		private string GetChildSubGridJavaScript(JQGrid grid, bool doNotPutInScriptTag = false)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (!doNotPutInScriptTag) {
				stringBuilder.Append("<script type='text/javascript'>\n");
			}
			stringBuilder.AppendFormat("function showSubGrid_{0}(subgrid_id, row_id, message, suffix) {{", grid.ID);
			stringBuilder.Append("var subgrid_table_id, pager_id;\r\n\t\t                subgrid_table_id = subgrid_id+'_t';\r\n\t\t                pager_id = 'p_'+ subgrid_table_id;\r\n                        if (suffix) { subgrid_table_id += suffix; pager_id += suffix;  }\r\n                        if (message) jQuery('#'+subgrid_id).append(message);                        \r\n\t\t                jQuery('#'+subgrid_id).append('<table id=' + subgrid_table_id + ' class=scroll></table><div id=' + pager_id + ' class=scroll></div>');\r\n                ");
			stringBuilder.Append(this.GetStartupOptions(grid, true));
			stringBuilder.Append("}");
			if (!doNotPutInScriptTag) {
				stringBuilder.Append("</script>");
			}
			return stringBuilder.ToString();
		}
		private string GetStartupOptions(JQGrid grid, bool subGrid)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string arg = subGrid ? "jQuery('#' + subgrid_table_id)" : string.Format("jQuery('#{0}')", grid.ID);
			string arg2 = subGrid ? "jQuery('#' + pager_id)" : string.Format("jQuery('#{0}')", grid.ID + "_pager");
			string pagerSelectorID = subGrid ? "'#' + pager_id" : string.Format("'#{0}'", grid.ID + "_pager");
			string text = subGrid ? "&parentRowID=' + row_id + '" : string.Empty;
			string text2 = (grid.DataUrl.IndexOf("?") > 0) ? "&" : "?";
			string text3 = (grid.EditUrl.IndexOf("?") > 0) ? "&" : "?";
			string arg3 = string.Format("{0}{1}jqGridID={2}{3}", new object[]
			{
				grid.DataUrl, 
				text2, 
				grid.ID, 
				text
			});
			string arg4 = string.Format("{0}{1}jqGridID={2}&editMode=1{3}", new object[]
			{
				grid.EditUrl, 
				text3, 
				grid.ID, 
				text
			});
			stringBuilder.AppendFormat("{0}.jqGrid({{", arg);
			stringBuilder.AppendFormat("url: '{0}'", arg3);
			if (grid.PostData != null) {
				stringBuilder.AppendFormat(",postData: {0}", Json.Serialize(grid.PostData, true));
			}
			stringBuilder.AppendFormat(",editurl: '{0}'", arg4);
			stringBuilder.AppendFormat(",mtype: 'POST'", new object[0]);
			stringBuilder.AppendFormat(",datatype: 'json'", new object[0]);
			stringBuilder.AppendFormat(",page: {0}", grid.PagerSettings.CurrentPage);
			stringBuilder.AppendFormat(",colNames: {0}", this.GetColNames(grid));
			stringBuilder.AppendFormat(",colModel: {0}", this.GetColModel(grid));
			stringBuilder.AppendFormat(",viewrecords: true", new object[0]);
			stringBuilder.AppendFormat(",scrollrows: false", new object[0]);
			stringBuilder.AppendFormat(",prmNames: {{ id: \"{0}\" }}", Util.GetPrimaryKeyField(grid));
			if (grid.LoadOnce) {
				stringBuilder.AppendLine(",loadonce: true");
			}
			if (grid.AppearanceSettings.ShowFooter) {
				stringBuilder.AppendLine(",footerrow: true");
				stringBuilder.AppendLine(",userDataOnFooter: true");
			}
			if (!grid.AppearanceSettings.ShrinkToFit) {
				stringBuilder.AppendLine(",shrinkToFit: false");
			}
			if (grid.ColumnReordering) {
				stringBuilder.AppendLine(",sortable: true");
			}
			if (grid.AppearanceSettings.ScrollBarOffset != 18) {
				stringBuilder.AppendFormat(",scrollOffset: {0}", grid.AppearanceSettings.ScrollBarOffset);
			}
			if (grid.AppearanceSettings.RightToLeft) {
				stringBuilder.AppendLine(",direction: 'rtl'");
			}
			if (grid.AutoWidth) {
				stringBuilder.AppendLine(",autowidth: true");
			}
			if (!grid.ShrinkToFit) {
				stringBuilder.AppendLine(",shrinkToFit: false");
			}
			if (grid.ToolBarSettings.ToolBarPosition == ToolBarPosition.Bottom || grid.ToolBarSettings.ToolBarPosition == ToolBarPosition.TopAndBottom) {
				stringBuilder.AppendFormat(",pager: {0}", arg2);
			}
			if (grid.ToolBarSettings.ToolBarPosition == ToolBarPosition.Top || grid.ToolBarSettings.ToolBarPosition == ToolBarPosition.TopAndBottom) {
				stringBuilder.AppendLine(",toppager: true");
			}
			if (grid.RenderingMode == RenderingMode.Optimized) {
				if (grid.HierarchySettings.HierarchyMode != HierarchyMode.None) {
					throw new Exception("Optimized rendering is not compatible with hierarchy.");
				}
				stringBuilder.AppendLine(",gridview: true");
			}
			if (grid.HierarchySettings.HierarchyMode == HierarchyMode.Parent || grid.HierarchySettings.HierarchyMode == HierarchyMode.ParentAndChild) {
				stringBuilder.AppendLine(",subGrid: true");
				stringBuilder.AppendFormat(",subGridOptions: {0}", grid.HierarchySettings.ToJSON());
			}
			if (!string.IsNullOrEmpty(grid.ClientSideEvents.SubGridRowExpanded)) {
				stringBuilder.AppendFormat(",subGridRowExpanded: {0}", grid.ClientSideEvents.SubGridRowExpanded);
			}
			if (!string.IsNullOrEmpty(grid.ClientSideEvents.ServerError)) {
				stringBuilder.AppendFormat(",errorCell: {0}", grid.ClientSideEvents.ServerError);
			}
			if (!string.IsNullOrEmpty(grid.ClientSideEvents.RowSelect)) {
				stringBuilder.AppendFormat(",onSelectRow: {0}", grid.ClientSideEvents.RowSelect);
			}
			if (!string.IsNullOrEmpty(grid.ClientSideEvents.ColumnSort)) {
				stringBuilder.AppendFormat(",onSortCol: {0}", grid.ClientSideEvents.ColumnSort);
			}
			if (!string.IsNullOrEmpty(grid.ClientSideEvents.RowDoubleClick)) {
				stringBuilder.AppendFormat(",ondblClickRow: {0}", grid.ClientSideEvents.RowDoubleClick);
			}
			if (!string.IsNullOrEmpty(grid.ClientSideEvents.RowRightClick)) {
				stringBuilder.AppendFormat(",onRightClickRow: {0}", grid.ClientSideEvents.RowRightClick);
			}
			if (!string.IsNullOrEmpty(grid.ClientSideEvents.LoadDataError)) {
				stringBuilder.AppendFormat(",loadError: {0}", grid.ClientSideEvents.LoadDataError);
			} else {
				stringBuilder.AppendFormat(",loadError: {0}", "jqGrid_aspnet_loadErrorHandler");
			}
			if (!string.IsNullOrEmpty(grid.ClientSideEvents.GridInitialized)) {
				stringBuilder.AppendFormat(",gridComplete: {0}", grid.ClientSideEvents.GridInitialized);
			}
			if (!string.IsNullOrEmpty(grid.ClientSideEvents.BeforeAjaxRequest)) {
				stringBuilder.AppendFormat(",beforeRequest: {0}", grid.ClientSideEvents.BeforeAjaxRequest);
			}
			if (!string.IsNullOrEmpty(grid.ClientSideEvents.AfterAjaxRequest)) {
				stringBuilder.AppendFormat(",loadComplete: {0}", grid.ClientSideEvents.AfterAjaxRequest);
			}
			if (grid.TreeGridSettings.Enabled) {
				stringBuilder.AppendFormat(",treeGrid: true", new object[0]);
				stringBuilder.AppendFormat(",treedatatype: 'json'", new object[0]);
				stringBuilder.AppendFormat(",treeGridModel: 'adjacency'", new object[0]);
				string arg5 = "{ level_field: 'tree_level', parent_id_field: 'tree_parent', leaf_field: 'tree_leaf', expanded_field: 'tree_expanded', loaded: 'tree_loaded', icon_field: 'tree_icon' }";
				stringBuilder.AppendFormat(",treeReader: {0}", arg5);
				stringBuilder.AppendFormat(",ExpandColumn: '{0}'", this.GetFirstVisibleDataField(grid));
				Hashtable hashtable = new Hashtable();
				if (!string.IsNullOrEmpty(grid.TreeGridSettings.CollapsedIcon)) {
					hashtable.Add("plus", grid.TreeGridSettings.CollapsedIcon);
				}
				if (!string.IsNullOrEmpty(grid.TreeGridSettings.ExpandedIcon)) {
					hashtable.Add("minus", grid.TreeGridSettings.ExpandedIcon);
				}
				if (!string.IsNullOrEmpty(grid.TreeGridSettings.LeafIcon)) {
					hashtable.Add("leaf", grid.TreeGridSettings.LeafIcon);
				}
				if (hashtable.Count > 0) {
					stringBuilder.AppendFormat(",treeIcons: {0}", new JavaScriptSerializer().Serialize(hashtable));
				}
			}
			if (!grid.AppearanceSettings.HighlightRowsOnHover) {
				stringBuilder.AppendLine(",hoverrows: false");
			}
			if (grid.AppearanceSettings.AlternateRowBackground) {
				stringBuilder.AppendLine(",altRows: true");
			}
			if (!String.IsNullOrEmpty(grid.AppearanceSettings.AlternateRowCssClass)) {
				stringBuilder.AppendFormat(",altclass: '{0}'", grid.AppearanceSettings.AlternateRowCssClass);
			}
			if (grid.AppearanceSettings.ShowRowNumbers) {
				stringBuilder.AppendLine(",rownumbers: true");
			}
			if (grid.AppearanceSettings.RowNumbersColumnWidth != 25) {
				stringBuilder.AppendFormat(",rownumWidth: {0}", grid.AppearanceSettings.RowNumbersColumnWidth.ToString());
			}

			if (!grid.PagerSettings.ShowPagerButtons) { // !!! FDV
				stringBuilder.AppendFormat(",pgbuttons: false", new object[0]);
			}
			if (!grid.PagerSettings.ShowPageInputBox) { // !!! FDV
				stringBuilder.AppendFormat(",pginput: false", new object[0]);
			}
			if (grid.PagerSettings.ScrollBarPaging) {
				stringBuilder.AppendFormat(",scroll: 1", new object[0]);
			}
			stringBuilder.AppendFormat(",rowNum: {0}", grid.PagerSettings.PageSize.ToString());
			stringBuilder.AppendFormat(",rowList: {0}", grid.PagerSettings.PageSizeOptions.ToString());
			if (!string.IsNullOrEmpty(grid.PagerSettings.NoRowsMessage)) {
				stringBuilder.AppendFormat(",emptyrecords: '{0}'", grid.PagerSettings.NoRowsMessage.ToString());
			}
			stringBuilder.AppendFormat(",editDialogOptions: {0}", this.GetEditOptions(grid));
			stringBuilder.AppendFormat(",addDialogOptions: {0}", this.GetAddOptions(grid));
			stringBuilder.AppendFormat(",delDialogOptions: {0}", this.GetDelOptions(grid));
			stringBuilder.AppendFormat(",searchDialogOptions: {0}", this.GetSearchOptions(grid));
			string format;
			if (grid.TreeGridSettings.Enabled) {
				format = ",jsonReader: {{ id: \"{0}\", repeatitems:false,subgrid:{{repeatitems:false}} }}";
			} else {
				format = ",jsonReader: {{ id: \"{0}\" }}";
			}
			stringBuilder.AppendFormat(format, grid.Columns[Util.GetPrimaryKeyIndex(grid)].DataField);
			if (!string.IsNullOrEmpty(grid.SortSettings.InitialSortColumn)) {
				stringBuilder.AppendFormat(",sortname: '{0}'", grid.SortSettings.InitialSortColumn);
			}
			stringBuilder.AppendFormat(",sortorder: '{0}'", grid.SortSettings.InitialSortDirection.ToString().ToLower());
			if (grid.MultiSelect) {
				stringBuilder.AppendLine(",multiselect: true");
				if (grid.MultiSelectMode == MultiSelectMode.SelectOnCheckBoxClickOnly) {
					stringBuilder.AppendFormat(",multiboxonly: true", grid.MultiSelect.ToString().ToLower());
				}
				if (grid.MultiSelectKey != MultiSelectKey.None) {
					stringBuilder.AppendFormat(",multikey: '{0}'", this.GetMultiKeyString(grid.MultiSelectKey));
				}
			}
			if (!string.IsNullOrEmpty(grid.AppearanceSettings.Caption)) {
				stringBuilder.AppendFormat(",caption: '{0}'", grid.AppearanceSettings.Caption);
			}
			if (!grid.Width.IsEmpty) {
				stringBuilder.AppendFormat(",width: '{0}'", grid.Width.ToString().Replace("px", ""));
			}
			if (!grid.Height.IsEmpty) {
				stringBuilder.AppendFormat(",height: '{0}'", grid.Height.ToString().Replace("px", ""));
			}
			if (grid.GroupSettings.GroupFields.Count > 0) {
				stringBuilder.AppendLine(",grouping:true");
				stringBuilder.AppendLine(",groupingView: {");
				stringBuilder.AppendFormat("groupField: ['{0}']", grid.GroupSettings.GroupFields[0].DataField);
				stringBuilder.AppendFormat(",groupColumnShow: [{0}]", grid.GroupSettings.GroupFields[0].ShowGroupColumn.ToString().ToLower());
				stringBuilder.AppendFormat(",groupText: ['{0}']", grid.GroupSettings.GroupFields[0].HeaderText);
				stringBuilder.AppendFormat(",groupOrder: ['{0}']", grid.GroupSettings.GroupFields[0].GroupSortDirection.ToString().ToLower());
				stringBuilder.AppendFormat(",groupSummary: [{0}]", grid.GroupSettings.GroupFields[0].ShowGroupSummary.ToString().ToLower());
				stringBuilder.AppendFormat(",groupCollapse: {0}", grid.GroupSettings.CollapseGroups.ToString().ToLower());
				stringBuilder.AppendFormat(",groupDataSorted: true", new object[0]);
				stringBuilder.Append("}");
			}
			stringBuilder.AppendFormat(",viewsortcols: [{0},'{1}',{2}]", "false", grid.SortSettings.SortIconsPosition.ToString().ToLower(), (grid.SortSettings.SortAction == SortAction.ClickOnHeader) ? "true" : "false");
			stringBuilder.AppendFormat("}})\r", new object[0]);
			stringBuilder.Append(this.GetToolBarOptions(grid, subGrid, pagerSelectorID));
			if (!grid.PagerSettings.ScrollBarPaging) {
				stringBuilder.AppendFormat(".bindKeys()", new object[0]);
			}
			stringBuilder.Append(";");
			stringBuilder.Append(this.GetLoadErrorHandler());
			stringBuilder.Append(";");
			if (grid.HeaderGroups.Count > 0) {
				List<Hashtable> list = new List<Hashtable>();
				foreach (JQGridHeaderGroup current in grid.HeaderGroups) {
					list.Add(current.ToHashtable());
				}
				stringBuilder.AppendFormat("{0}.setGroupHeaders( {{ useColSpanStyle:true,groupHeaders:{1} }});", arg, new JavaScriptSerializer().Serialize(list));
			}
			if (grid.ToolBarSettings.ShowSearchToolBar) {
				stringBuilder.AppendFormat("{0}.filterToolbar({1});", arg, new JsonSearchToolBar(grid).Process());
			}
			return stringBuilder.ToString();
		}
		private string GetEditOptions(JQGrid grid)
		{
			JsonEditDialog jsonEditDialog = new JsonEditDialog(grid);
			return jsonEditDialog.Process();
		}
		private string GetAddOptions(JQGrid grid)
		{
			JsonAddDialog jsonAddDialog = new JsonAddDialog(grid);
			return jsonAddDialog.Process();
		}
		private string GetDelOptions(JQGrid grid)
		{
			JsonDelDialog jsonDelDialog = new JsonDelDialog(grid);
			return jsonDelDialog.Process();
		}
		private string GetSearchOptions(JQGrid grid)
		{
			JsonSearchDialog jsonSearchDialog = new JsonSearchDialog(grid);
			return jsonSearchDialog.Process();
		}
		private string GetColNames(JQGrid grid)
		{
			string[] array = new string[grid.Columns.Count];
			for (int i = 0; i < grid.Columns.Count; i++) {
				JQGridColumn jQGridColumn = grid.Columns[i];
				array[i] = (string.IsNullOrEmpty(jQGridColumn.HeaderText) ? jQGridColumn.DataField : jQGridColumn.HeaderText);
			}
			return new JavaScriptSerializer().Serialize(array);
		}
		private string GetColModel(JQGrid grid)
		{
			Hashtable[] array = new Hashtable[grid.Columns.Count];
			for (int i = 0; i < grid.Columns.Count; i++) {
				JsonColModel jsonColModel = new JsonColModel(grid.Columns[i], grid);
				array[i] = jsonColModel.JsonValues;
			}
			string input = new JavaScriptSerializer().Serialize(array);
			return JsonColModel.RemoveQuotesForJavaScriptMethods(input, grid);
		}
		private string GetMultiKeyString(MultiSelectKey key)
		{
			switch (key) {
				case MultiSelectKey.Shift: {
						return "shiftKey";
					}
				case MultiSelectKey.Ctrl: {
						return "ctrlKey";
					}
				case MultiSelectKey.Alt: {
						return "altKey";
					}
				default: {
						throw new Exception("Should not be here.");
					}
			}
		}
		private string GetToolBarOptions(JQGrid grid, bool subGrid, string pagerSelectorID)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (grid.ShowToolBar) {
				JsonToolBar obj = new JsonToolBar(grid.ToolBarSettings);
				if (!subGrid) {
					stringBuilder.AppendFormat(".navGrid('#{0}',{1},{2},{3},{4},{5} )", new object[]
					{
						grid.ID + "_pager", 
						new JavaScriptSerializer().Serialize(obj), 
						string.Format("jQuery('#{0}').getGridParam('editDialogOptions')", grid.ID), 
						string.Format("jQuery('#{0}').getGridParam('addDialogOptions')", grid.ID), 
						string.Format("jQuery('#{0}').getGridParam('delDialogOptions')", grid.ID), 
						string.Format("jQuery('#{0}').getGridParam('searchDialogOptions')", grid.ID)
					});
				} else {
					stringBuilder.AppendFormat(".navGrid('#' + pager_id,{0},{1},{2},{3},{4} )", new object[]
					{
						new JavaScriptSerializer().Serialize(obj), 
						"jQuery('#' + subgrid_table_id).getGridParam('editDialogOptions')", 
						"jQuery('#' + subgrid_table_id).getGridParam('addDialogOptions')", 
						"jQuery('#' + subgrid_table_id).getGridParam('delDialogOptions')", 
						"jQuery('#' + subgrid_table_id).getGridParam('searchDialogOptions')"
					});
				}
				foreach (JQGridToolBarButton current in grid.ToolBarSettings.CustomButtons) {
					if (grid.ToolBarSettings.ToolBarPosition == ToolBarPosition.Bottom || grid.ToolBarSettings.ToolBarPosition == ToolBarPosition.TopAndBottom) {
						JsonCustomButton jsonCustomButton = new JsonCustomButton(current);
						stringBuilder.AppendFormat(".navButtonAdd({0},{1})", pagerSelectorID, jsonCustomButton.Process());
					}
					if (grid.ToolBarSettings.ToolBarPosition == ToolBarPosition.TopAndBottom || grid.ToolBarSettings.ToolBarPosition == ToolBarPosition.Top) {
						JsonCustomButton jsonCustomButton2 = new JsonCustomButton(current);
						stringBuilder.AppendFormat(".navButtonAdd({0},{1})", pagerSelectorID.Replace("_pager", "_toppager"), jsonCustomButton2.Process());
					}
				}
				return stringBuilder.ToString();
			}
			return string.Empty;
		}
		private string GetLoadErrorHandler()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("\n");
			stringBuilder.Append("function jqGrid_aspnet_loadErrorHandler(xht, st, handler) {");
			stringBuilder.Append("jQuery(document.body).css('font-size','100%'); jQuery(document.body).html(xht.responseText);");
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}
		private string GetJQuerySubmit(JQGrid grid)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat(@"
                        var _theForm = document.getElementsByTagName('FORM')[0];
						jQuery(_theForm).submit( function() {{
                            jQuery('#{0}').attr('value', jQuery('#{1}').getGridParam('selrow'));
                        }});
                       ", grid.ID + "_SelectedRow", grid.ID, grid.ID + "_CurrentPage");
			return stringBuilder.ToString();
		}
		private string GetFirstVisibleDataField(JQGrid grid)
		{
			foreach (JQGridColumn current in grid.Columns) {
				if (current.Visible) {
					return current.DataField;
				}
			}
			return grid.Columns[0].DataField;
		}
	}
}
