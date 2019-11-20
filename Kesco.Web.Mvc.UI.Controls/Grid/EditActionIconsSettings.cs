namespace Kesco.Web.Mvc.UI.Grid
{
    using System;

    public class EditActionIconsSettings
    {

        public EditActionIconsSettings()
        {
            this.ShowEditIcon = true;
            this.ShowDeleteIcon = true;
            this.SaveOnEnterKeyPress = false;
			this.ReloadAfterDelete = true;
			this.Events = new EditActionIconsClientSideEvents();
        }

		// Properties
		public bool SaveOnEnterKeyPress { get; set; }
		public bool ShowDeleteIcon { get; set; }
		public bool ShowEditIcon { get; set; }

		public bool ReloadAfterDelete { get; set; }

		public EditActionIconsClientSideEvents Events { get; set; }

	}

}
