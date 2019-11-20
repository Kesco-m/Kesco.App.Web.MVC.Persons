namespace Kesco.Web.Mvc.UI.Grid
{
	using System;
	using System.Collections.Generic;
	using System.Collections.Specialized;
	using System.ComponentModel;
	using System.Data;
	using System.IO;
	using System.Linq;
	using System.Runtime.CompilerServices;
	using System.Runtime.InteropServices;
	using System.Web;
	using System.Web.Mvc;
	using System.Web.UI;
	using System.Web.UI.WebControls;
	using Kesco.Linq;

	public class JQGrid
	{
		private EventHandlerList _events;
		private static readonly object EventDataResolved;
		public event JQGridDataResolvedEventHandler DataResolved
		{
			add
			{
				this.Events.AddHandler(JQGrid.EventDataResolved, value);
			}
			remove
			{
				this.Events.RemoveHandler(JQGrid.EventDataResolved, value);
			}
		}
		public bool AutoWidth
		{
			get;
			set;
		}
		public bool ShrinkToFit
		{
			get;
			set;
		}
		public List<JQGridColumn> Columns
		{
			get;
			set;
		}
		public List<JQGridHeaderGroup> HeaderGroups
		{
			get;
			set;
		}
		public EditDialogSettings EditDialogSettings
		{
			get;
			set;
		}
		public AddDialogSettings AddDialogSettings
		{
			get;
			set;
		}
		public DeleteDialogSettings DeleteDialogSettings
		{
			get;
			set;
		}
		public SearchDialogSettings SearchDialogSettings
		{
			get;
			set;
		}
		public SearchToolBarSettings SearchToolBarSettings
		{
			get;
			set;
		}
		public PagerSettings PagerSettings
		{
			get;
			set;
		}
		public ToolBarSettings ToolBarSettings
		{
			get;
			set;
		}
		public SortSettings SortSettings
		{
			get;
			set;
		}
		public AppearanceSettings AppearanceSettings
		{
			get;
			set;
		}
		public HierarchySettings HierarchySettings
		{
			get;
			set;
		}
		public GroupSettings GroupSettings
		{
			get;
			set;
		}
		public TreeGridSettings TreeGridSettings
		{
			get;
			set;
		}
		public ClientSideEvents ClientSideEvents
		{
			get;
			set;
		}
		public string ID
		{
			get;
			set;
		}
		public string DataUrl
		{
			get;
			set;
		}
		public object PostData { get; set; }
		public string EditUrl
		{
			get;
			set;
		}

		public bool LoadOnce { get; set; }

		public bool ColumnReordering
		{
			get;
			set;
		}
		public RenderingMode RenderingMode
		{
			get;
			set;
		}
		public bool MultiSelect
		{
			get;
			set;
		}
		public MultiSelectMode MultiSelectMode
		{
			get;
			set;
		}
		public MultiSelectKey MultiSelectKey
		{
			get;
			set;
		}
		public Unit Width
		{
			get;
			set;
		}
		public Unit Height
		{
			get;
			set;
		}
		public object DataSource
		{
			get;
			set;
		}
		internal bool ShowToolBar
		{
			get
			{
				return this.ToolBarSettings.ShowAddButton || this.ToolBarSettings.ShowDeleteButton || this.ToolBarSettings.ShowEditButton || this.ToolBarSettings.ShowRefreshButton || this.ToolBarSettings.ShowSearchButton || this.ToolBarSettings.ShowViewRowDetailsButton || this.ToolBarSettings.CustomButtons.Count > 0;
			}
		}
		public AjaxCallBackMode AjaxCallBackMode
		{
			get
			{
				string text = HttpContext.Current.Request.Form["oper"];
				string value = HttpContext.Current.Request.QueryString["editMode"];
				string value2 = HttpContext.Current.Request.QueryString["_search"];
				AjaxCallBackMode result = AjaxCallBackMode.RequestData;
				string a;
				if (!string.IsNullOrEmpty(text) && (a = text) != null) {
					if (a == "add") {
						result = AjaxCallBackMode.AddRow;
						return result;
					}
					if (a == "edit") {
						result = AjaxCallBackMode.EditRow;
						return result;
					}
					if (a == "del") {
						result = AjaxCallBackMode.DeleteRow;
						return result;
					}
				}
				if (!string.IsNullOrEmpty(value)) {
					result = AjaxCallBackMode.EditRow;
				}
				if (!string.IsNullOrEmpty(value2) && Convert.ToBoolean(value2)) {
					result = AjaxCallBackMode.Search;
				}
				return result;
			}
		}
		private EventHandlerList Events
		{
			get
			{
				if (this._events == null) {
					this._events = new EventHandlerList();
				}
				return this._events;
			}
		}
		static JQGrid()
		{
			JQGrid.EventDataResolved = new object();
		}
		public JQGrid()
		{
			this.AutoWidth = false;
			this.ShrinkToFit = true;
			this.EditDialogSettings = new EditDialogSettings();
			this.AddDialogSettings = new AddDialogSettings();
			this.DeleteDialogSettings = new DeleteDialogSettings();
			this.SearchDialogSettings = new SearchDialogSettings();
			this.SearchToolBarSettings = new SearchToolBarSettings();
			this.PagerSettings = new PagerSettings();
			this.ToolBarSettings = new ToolBarSettings();
			this.SortSettings = new SortSettings();
			this.AppearanceSettings = new AppearanceSettings();
			this.HierarchySettings = new HierarchySettings();
			this.GroupSettings = new GroupSettings();
			this.TreeGridSettings = new TreeGridSettings();
			this.ClientSideEvents = new ClientSideEvents();
			this.Columns = new List<JQGridColumn>();
			this.HeaderGroups = new List<JQGridHeaderGroup>();
			this.DataUrl = "";
			this.PostData = null;
			this.EditUrl = "";
			this.LoadOnce = false;
			this.ColumnReordering = false;
			this.RenderingMode = RenderingMode.Default;
			this.MultiSelect = false;
			this.MultiSelectMode = MultiSelectMode.SelectOnRowClick;
			this.MultiSelectKey = MultiSelectKey.None;
			this.Width = Unit.Empty;
			this.Height = Unit.Empty;
		}
		public JsonResult DataBind(object dataSource)
		{
			this.DataSource = dataSource;
			return this.DataBind();
		}
		public JsonResult DataBind()
		{
			if (this.AjaxCallBackMode != AjaxCallBackMode.RequestData) { }
			return this.GetJsonResponse();
		}
		public ActionResult ShowEditValidationMessage(string errorMessage)
		{
			HttpContext.Current.Response.Clear();
			HttpContext.Current.Response.StatusCode = 500;
			HttpContext.Current.Response.StatusDescription = errorMessage;
			return new ContentResult {
				Content = errorMessage
			};
		}
		private JsonResult FilterDataSource(object dataSource, NameValueCollection queryString, out IQueryable iqueryable)
		{
			iqueryable = (dataSource as IQueryable);
			Guard.IsNotNull(iqueryable, "DataSource", "should implement the IQueryable interface.");
			int num = Convert.ToInt32(queryString["page"]);
			int num2 = Convert.ToInt32(queryString["rows"]);
			string text = queryString["sidx"];
			string str = queryString["sord"];
			string arg_5E_0 = queryString["parentRowID"];
			string text2 = queryString["_search"];
			string text3 = queryString["filters"];
			string text4 = queryString["searchField"];
			string searchString = queryString["searchString"];
			string searchOper = queryString["searchOper"];
			this.PagerSettings.CurrentPage = num;
			this.PagerSettings.PageSize = num2;
			if (!string.IsNullOrEmpty(text2) && text2 != "false") {
				try {
					if (string.IsNullOrEmpty(text3) && !string.IsNullOrEmpty(text4)) {
						iqueryable = iqueryable.Where(Util.GetWhereClause(this, text4, searchString, searchOper), new object[0]);
					} else {
						if (!string.IsNullOrEmpty(text3)) {
							iqueryable = iqueryable.Where(Util.GetWhereClause(this, text3), new object[0]);
						} else {
							if (this.ToolBarSettings.ShowSearchToolBar || text2 == "true") {
								iqueryable = iqueryable.Where(Util.GetWhereClause(this, queryString), new object[0]);
							}
						}
					}
				} catch (DataTypeNotSetException ex) {
					throw ex;
				} catch (Exception) {
					return new JsonResult {
						Data = new object(),
						JsonRequestBehavior = JsonRequestBehavior.AllowGet
					};
				}
			}
			int num3 = iqueryable.Count();
			int totalPagesCount = (int)Math.Ceiling((double)((float)num3 / (float)num2));
			if (string.IsNullOrEmpty(text) && this.SortSettings.AutoSortByPrimaryKey) {
				if (this.Columns.Count == 0) {
					throw new Exception("JQGrid must have at least one column defined.");
				}
				text = Util.GetPrimaryKeyField(this);
				str = "asc";
			}
			if (!string.IsNullOrEmpty(text)) {
				string text5 = "";
				if (this.GroupSettings.GroupFields.Count > 0) {
					string str2 = text.Split(new char[]
					{
						' '
					})[0];
					string str3 = text.Split(new char[]
					{
						' '
					})[1].Split(new char[]
					{
						','
					})[0];
					text = text.Split(new char[]
					{
						','
					})[1];
					text5 = str2 + " " + str3;
				}
				if (text != null && text == " ") {
					text = "";
				}
				if (!string.IsNullOrEmpty(text)) {
					if (this.GroupSettings.GroupFields.Count > 0 && !text5.EndsWith(",")) {
						text5 += ",";
					}
					text5 = text5 + text + " " + str;
				}
				iqueryable = iqueryable.OrderBy(text5, new object[0]);
			}
			iqueryable = iqueryable.Skip((num - 1) * num2).Take(num2);
			DataTable dataTable = iqueryable.ToDataTable(this);
			this.OnDataResolved(new JQGridDataResolvedEventArgs(this, iqueryable, this.DataSource as IQueryable));
			if (this.TreeGridSettings.Enabled) {
				JsonTreeResponse response = new JsonTreeResponse(num, totalPagesCount, num3, num2, dataTable.Rows.Count, Util.GetFooterInfo(this));
				return Util.ConvertToTreeJson(response, this, dataTable);
			}
			JsonResponse response2 = new JsonResponse(num, totalPagesCount, num3, num2, dataTable.Rows.Count, Util.GetFooterInfo(this));
			return Util.ConvertToJson(response2, this, dataTable);
		}

		private JsonResult GetJsonResponse()
		{
			Guard.IsNotNull(this.DataSource, "DataSource");
			IQueryable queryable;
			var request = HttpContext.Current.Request;
			var namedValues = (request.RequestType == "POST")
					? request.Form
					: request.QueryString;
			return this.FilterDataSource(this.DataSource, namedValues, out queryable);
		}

		public JQGridEditData GetEditData()
		{
			NameValueCollection nameValueCollection = new NameValueCollection();
			foreach (string text in HttpContext.Current.Request.Form.Keys) {
				if (text != "oper") {
					nameValueCollection[text] = HttpContext.Current.Request.Form[text];
				}
			}
			string text2 = string.Empty;
			foreach (JQGridColumn current in this.Columns) {
				if (current.PrimaryKey) {
					text2 = current.DataField;
					break;
				}
			}
			if (!string.IsNullOrEmpty(text2) && !string.IsNullOrEmpty(nameValueCollection["id"])) {
				nameValueCollection[text2] = nameValueCollection["id"];
			}
			JQGridEditData jQGridEditData = new JQGridEditData();
			jQGridEditData.RowData = nameValueCollection;
			jQGridEditData.RowKey = nameValueCollection["id"];
			string text3 = HttpContext.Current.Request.QueryString["parentRowID"];
			if (!string.IsNullOrEmpty(text3)) {
				jQGridEditData.ParentRowKey = text3;
			}
			return jQGridEditData;
		}
		public JQGridTreeExpandData GetTreeExpandData()
		{

			JQGridTreeExpandData jQGridTreeExpandData = new JQGridTreeExpandData();
			if (HttpContext.Current.Request["nodeid"] != null) {
				jQGridTreeExpandData.ParentID = HttpContext.Current.Request["nodeid"];
			}
			if (HttpContext.Current.Request["n_level"] != null) {
				jQGridTreeExpandData.ParentLevel = Convert.ToInt32(HttpContext.Current.Request["n_level"]);
			}
			return jQGridTreeExpandData;
		}
		protected internal virtual void OnDataResolved(JQGridDataResolvedEventArgs e)
		{
			JQGridDataResolvedEventHandler jQGridDataResolvedEventHandler = (JQGridDataResolvedEventHandler)this.Events[JQGrid.EventDataResolved];
			if (jQGridDataResolvedEventHandler != null) {
				jQGridDataResolvedEventHandler(this, e);
			}
		}

		private void DoExportToExcel(object dataSource, string fileName)
		{
			GridView gridView = new GridView();
			gridView.AutoGenerateColumns = false;
			foreach (JQGridColumn current in this.Columns) {
				BoundField boundField = new BoundField();
				boundField.DataField = current.DataField;
				string headerText = string.IsNullOrEmpty(current.HeaderText) ? current.DataField : current.HeaderText;
				boundField.HeaderText = headerText;
				boundField.DataFormatString = current.DataFormatString;
				boundField.FooterText = current.FooterValue;
				gridView.Columns.Add(boundField);
			}
			gridView.DataSource = dataSource;
			gridView.DataBind();
			HttpContext.Current.Response.ClearContent();
			HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" + fileName);
			HttpContext.Current.Response.ContentType = "application/excel";
			StringWriter stringWriter = new StringWriter();
			HtmlTextWriter writer = new HtmlTextWriter(stringWriter);
			gridView.RenderControl(writer);
			HttpContext.Current.Response.Write(stringWriter.ToString());
			HttpContext.Current.Response.End();
		}

		private void DoExportToExcelWithState(object dataSource, string fileName, JQGridState gridState)
		{
			if (!gridState.CurrentPageOnly) {
				gridState.QueryString["page"] = "1";
				gridState.QueryString["rows"] = "1000000";
			}
			IQueryable dataSource2;
			this.FilterDataSource(dataSource, gridState.QueryString, out dataSource2);
			this.ExportToExcel(dataSource2, fileName);
		}

		public void ExportToExcel(object dataSource)
		{
			this.DoExportToExcel(dataSource, "GridExcelExport.xls");
		}

		public void ExportToExcel(object dataSource, string fileName)
		{
			this.DoExportToExcel(dataSource, fileName);
		}

		public void ExportToExcel(object dataSource, JQGridState gridState)
		{
			this.DoExportToExcelWithState(dataSource, "GridExcelExport.xls", gridState);
		}

		public void ExportToExcel(object dataSource, string fileName, JQGridState gridState)
		{
			this.DoExportToExcelWithState(dataSource, fileName, gridState);
		}

		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public JQGridState GetState(bool currentPageOnly)
		{
			NameValueCollection nameValueCollection = new NameValueCollection();
			foreach (string name in HttpContext.Current.Request.QueryString.Keys) {
				nameValueCollection.Add(name, HttpContext.Current.Request.QueryString[name]);
			}
			return new JQGridState {
				QueryString = nameValueCollection,
				CurrentPageOnly = currentPageOnly
			};
		}

		public JQGridState GetState()
		{
			NameValueCollection nameValueCollection = new NameValueCollection();
			foreach (string name in HttpContext.Current.Request.QueryString.Keys) {
				nameValueCollection.Add(name, HttpContext.Current.Request.QueryString[name]);
			}
			return new JQGridState {
				QueryString = nameValueCollection
			};
		}
	}
}

