namespace Kesco.Web.Mvc.UI.Grid
{
    using System;
    using System.Runtime.CompilerServices;

    public class ClientSideEvents
    {

		public string BeforeAddDialogShown { get; set; }

		public string AfterAddDialogShown { get; set; }

		public string AfterAddDialogRowInserted { get; set; }

		public string BeforeEditDialogShown { get; set; }

		public string AfterEditDialogShown { get; set; }

		public string AfterEditDialogRowInserted { get; set; }

		public string BeforeDeleteDialogShown { get; set; }

		public string AfterDeleteDialogShown { get; set; }

		public string AfterDeleteDialogRowDeleted { get; set; }

		public string RowSelect { get; set; }

		public string RowDoubleClick { get; set; }

		public string RowRightClick { get; set; }

		public string GridInitialized { get; set; }

		public string BeforeAjaxRequest { get; set; }

		public string AfterAjaxRequest { get; set; }

		public string ServerError { get; set; }

		public string LoadDataError { get; set; }

		public string SubGridRowExpanded { get; set; }

		public string ColumnSort { get; set; }

		public ClientSideEvents()
		{
			this.BeforeAddDialogShown = "";
			this.AfterAddDialogShown = "";
			this.AfterAddDialogRowInserted = "";
			this.BeforeEditDialogShown = "";
			this.AfterEditDialogShown = "";
			this.AfterEditDialogRowInserted = "";
			this.BeforeDeleteDialogShown = "";
			this.AfterDeleteDialogShown = "";
			this.AfterDeleteDialogRowDeleted = "";
			this.RowSelect = "";
			this.RowDoubleClick = "";
			this.RowRightClick = "";
			this.GridInitialized = "";
			this.BeforeAjaxRequest = "";
			this.AfterAjaxRequest = "";
			this.ServerError = "";
			this.LoadDataError = "";
			this.SubGridRowExpanded = "";
			this.ColumnSort = "";
		}

		/*
        public ClientSideEvents()
        {
            this.BeforeAjaxRequest = 
            this.GridInitialized = 
            this.ColumnSort = 
            this.AddDialogAfterShow = this.AddDialogBeforeShow = this.AddDialogAfterRowInserted = this.AddDialogAfterSubmit = 
            this.EditDialogAfterShow = this.EditDialogBeforeShow = this.EditDialogAfterRowInserted = 
            this.RowRightClick = this.RowDoubleClick = this.RowSelect = this.LoadDataError = 
            this.SubGridRowExpanded = this.ServerError = String.Empty;
        }

		// Properties
		public string AddDialogAfterRowInserted {  get;  set; }
		public string AddDialogBeforeShow { get; set; }
		public string AddDialogAfterShow { get; set; }
		public string AddDialogAfterSubmit { get; set; }

		public string EditDialogBeforeShow { get; set; }
		public string EditDialogAfterShow { get; set; }
		public string EditDialogAfterRowInserted { get; set; }

		public string BeforeAjaxRequest { get; set; }
		public string ColumnSort {  get;  set; }
		public string GridInitialized {  get;  set; }
		public string LoadDataError {  get;  set; }

		public string RowDoubleClick {  get;  set; }
		public string RowRightClick {  get;  set; }
		public string RowSelect {  get;  set; }

		public string ServerError {  get;  set; }
		public string SubGridRowExpanded {  get;  set; }
		 * 
		 */

	}
}
