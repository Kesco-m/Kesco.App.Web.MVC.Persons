
namespace Kesco.Web.Mvc.UI.Grid
{
	public class JQAutoCompleteClientEvents
	{
		public string FormatItem { get; set; }
		public string OnItemFocus { get; set; }
		public string OnItemSelect { get; set; }
	}

	public class JQAutoCompleteEx
	{

		public JQAutoCompleteEx()
		{
			this.ClientEvents = new JQAutoCompleteClientEvents();
			this.AutoCompleteMode = AutoCompleteMode.BeginsWith;
			this.DataLabelField = "";
			this.DataSource = null;
			this.DataUrl = "";
			this.Delay = 300;
			this.DisplayMode = AutoCompleteDisplayMode.Standalone;
			this.Enabled = true;
			this.ID = "";
			this.MinLength = 1;
		}
		/*
		public JsonResult DataBind()
		{
			return this.GetJsonResponse();
		}

		public JsonResult DataBind(object dataSource)
		{
			this.DataSource = dataSource;
			return this.DataBind();
		}

		private JsonResult GetJsonResponse()
		{
			Guard.IsNotNull(this.DataSource, "DataSource");
			IQueryable queryable = this.DataSource as IQueryable;
			Guard.IsNotNull(queryable, "DataSource", "should implement the IQueryable interface.");
			Guard.IsNotNullOrEmpty(this.DataField, "DataField", "should be set to the datafield (column) of the datasource to search in.");
			SearchOperation searchOperation = SearchOperation.IsEqualTo;
			if (this.AutoCompleteMode == AutoCompleteMode.BeginsWith) {
				searchOperation = SearchOperation.BeginsWith;
			} else {
				searchOperation = SearchOperation.Contains;
			}
			string text = HttpContext.Current.Request.QueryString["term"];
			if (!string.IsNullOrEmpty(text)) {
				queryable = queryable.Where(Util.ConstructLinqFilterExpression(this, new Util.SearchArguments {
					SearchColumn = this.DataField,
					SearchOperation = searchOperation,
					SearchString = text
				}), new object[0]);
			}
			return new JsonResult {
				JsonRequestBehavior = JsonRequestBehavior.AllowGet,
				Data = queryable.ToListOfString(this)
			};
		}
		*/
		// Properties
		public AutoCompleteMode AutoCompleteMode { get; set; }
		/// <summary>
		/// Gets or sets the data field.
		/// </summary>
		/// <value>
		/// The data field.
		/// </value>
		public string DataLabelField { get; set; }
		public string DataValueField { get; set; }
		public JQAutoCompleteClientEvents ClientEvents { get; set; }
		/// <summary>
		/// Gets or sets the data source.
		/// </summary>
		/// <value>
		/// The data source.
		/// </value>
		public object DataSource { get; set; }
		/// <summary>
		/// Gets or sets the data URL.
		/// </summary>
		/// <value>
		/// The data URL.
		/// </value>
		public string DataUrl { get; set; }
		/// <summary>
		/// Gets or sets the delay.
		/// </summary>
		/// <value>
		/// The delay.
		/// </value>
		public int Delay { get; set; }
		/// <summary>
		/// Gets or sets the display mode.
		/// </summary>
		/// <value>
		/// The display mode.
		/// </value>
		public AutoCompleteDisplayMode DisplayMode { get; set; }
		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="JQAutoCompleteEx"/> is enabled.
		/// </summary>
		/// <value>
		///   <c>true</c> if enabled; otherwise, <c>false</c>.
		/// </value>
		public bool Enabled { get; set; }
		/// <summary>
		/// Gets or sets the ID.
		/// </summary>
		/// <value>
		/// The ID.
		/// </value>
		public string ID { get; set; }
		/// <summary>
		/// Gets or sets the length of the min.
		/// </summary>
		/// <value>
		/// The length of the min.
		/// </value>
		public int MinLength { get; set; }

	}
}
