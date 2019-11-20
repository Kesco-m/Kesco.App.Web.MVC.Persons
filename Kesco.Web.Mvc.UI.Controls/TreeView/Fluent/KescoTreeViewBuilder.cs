using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kesco.Web.Mvc.UI.Fluent
{

	/// <summary>
	/// Класс, реализующий построитель для элемента управления <see cref="KescoTreeView"/>.
	/// </summary>
	public class KescoTreeViewBuilder : ControlBuilderBase<KescoTreeView, KescoTreeViewBuilder>
	{

		public KescoTreeViewBuilder(KescoTreeView control) : base(control) { }



		/// <summary>
		/// Allows the edit.
		/// </summary>
		/// <param name="allowed">if set to <c>true</c> [allowed].</param>
		/// <returns></returns>
		public KescoTreeViewBuilder AllowEdit(bool allowed)
		{
			this.control.AllowEdit = allowed;
			return this;
		}

		public KescoTreeViewBuilder AllowDragAndDrop(bool allowed)
		{
			this.control.AllowDragAndDrop = allowed;
			return this;
		}

		public KescoTreeViewBuilder AllowMultiple(bool allowed)
		{
			this.control.AllowMultiple = allowed;
			return this;
		}

		public KescoTreeViewBuilder Cookie(string cookie)
		{
			this.control.Cookie = cookie;
			return this;
		}

		public KescoTreeViewBuilder RenameNodeUri(string uri)
		{
			this.control.RenameNodeUri = uri;
			return this;
		}

		public KescoTreeViewBuilder MoveNodeUri(string uri)
		{
			this.control.MoveNodeUri = uri;
			return this;
		}

		public KescoTreeViewBuilder Loading(string uri, string loadingMessage, string errorMessage )
		{
			this.control.LoadingUri = uri;
			this.control.LoadingMessage = loadingMessage;
			this.control.LoadingErrorMessage = errorMessage;
			return this;
		}

		public KescoTreeViewBuilder CssClasses(Action<KescoTreeViewCssClasses> customize)
		{
			if (customize != null) {
				customize(this.control.CssClasses);
			}
			return this;
		}

		public KescoTreeViewBuilder ClientEvents(Action<KescoTreeViewClientEvents> customize)
		{
			if (customize != null) {
				customize(this.control.ClientEvents);
			}
			return this;
		}

		public KescoTreeViewBuilder JsonData(Action<KescoTreeViewJsonDataPluginSettings> customize)
		{
			if (customize != null) {
				customize(this.control.JsonDataSettings);
			}
			return this;
		}

		public KescoTreeViewBuilder OnInitUri(string uri)
		{
			if (!String.IsNullOrEmpty(uri)) {
				control.OnInitUri = uri;
			}
			return this;
		}

		/*
		public KescoTreeViewBuilder OnNodeDoubleClickClientAction(string clientAction)
		{
			if (!String.IsNullOrEmpty(clientAction)) {
				control.OnNodeDoubleClickClientAction = clientAction;
			}
			return this;
		}*/

		public KescoTreeViewBuilder AjaxRequestContext(string context)
		{
			control.AjaxRequestContext = context;
			return this;
		}

		public KescoTreeViewBuilder Root(string root)
		{
			control.Root = root;
			return this;
		}

	}
}
