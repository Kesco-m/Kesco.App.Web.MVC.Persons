using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kesco.Web.Mvc.UI.Fluent
{

	/// <summary>
	/// Класс, реализующий построитель для элемента управления KescoSelectTextBox.
	/// </summary>
	public class KescoButtonBarBuilder : ControlBuilderBase<KescoButtonBar, KescoButtonBarBuilder>
	{

		public KescoButtonBarBuilder(KescoButtonBar control) : base(control) { }

		/// <summary>
		/// Adds the button which opens dialog window.
		/// </summary>
		/// <param name="uri">The URI.</param>
		/// <param name="caption">The caption.</param>
		/// <returns></returns>
		public KescoButtonBarBuilder AddOpenButton(string caption, string uri)
		{
			return AddOpenButtonIf(true, caption, uri);
		}

		/// <summary>
		/// Adds the button which opens dialog window.
		/// </summary>
		/// <param name="uri">The URI.</param>
		/// <param name="caption">The caption.</param>
		/// <returns></returns>
		public KescoButtonBarBuilder AddOpenButtonIf(bool condition, string caption, string uri)
		{
			if (condition)
				this.control.Buttons.Add(new KescoButtonBarItem { Caption = caption, Uri = uri, ButtonType = "open" });
			return this;

		}

		/// <summary>
		/// Adds the button which opens dialog window.
		/// </summary>
		/// <param name="uri">The URI.</param>
		/// <param name="caption">The caption.</param>
		/// <param name="dialogWidth">Width of the dialog.</param>
		/// <param name="dialogHeight">Height of the dialog.</param>
		/// <returns></returns>
		public KescoButtonBarBuilder AddOpenButton(string caption, string uri, int? dialogWidth, int? dialogHeight)
		{
			return AddOpenButtonIf(true, caption, uri, dialogWidth, dialogHeight);

		}

		/// <summary>
		/// Adds the button which opens dialog window.
		/// </summary>
		/// <param name="uri">The URI.</param>
		/// <param name="caption">The caption.</param>
		/// <param name="dialogWidth">Width of the dialog.</param>
		/// <param name="dialogHeight">Height of the dialog.</param>
		/// <returns></returns>
		public KescoButtonBarBuilder AddOpenButtonIf(bool condition, string caption, string uri, int? dialogWidth, int? dialogHeight)
		{
			if (condition) 
				this.control.Buttons.Add(new KescoButtonBarItem { Caption = caption, Uri = uri, DialogWidth = dialogWidth, DialogHeight = dialogHeight, ButtonType = "open" });
			return this;

		}

		/// <summary>
		/// Adds the button with callback function defined on client-side.
		/// </summary>
		/// <param name="uri">The URI.</param>
		/// <param name="caption">The caption.</param>
		/// <param name="callback">The name of client-side callback function.</param>
		/// <returns></returns>
		public KescoButtonBarBuilder AddCallbackButton(string caption, string callback)
		{
			return AddCallbackButtonIf(true, caption, callback);
		}

		/// <summary>
		/// Adds the button with callback function defined on client-side.
		/// </summary>
		/// <param name="uri">The URI.</param>
		/// <param name="caption">The caption.</param>
		/// <param name="callback">The name of client-side callback function.</param>
		/// <returns></returns>
		public KescoButtonBarBuilder AddCallbackButtonIf(bool condition, string caption, string callback)
		{
			if (condition)
				this.control.Buttons.Add(new KescoButtonBarItem { Caption = caption, ButtonType = "callback", CallbackFunc = callback });
			return this;

		}

		/// <summary>
		/// Adds the button customized by action.
		/// </summary>
		/// <param name="condition">Условие</param>
		/// <param name="caption">The caption.</param>
		/// <param name="callback">The name of client-side callback function.</param>
		/// <returns></returns>
		public KescoButtonBarBuilder AddButton(string caption, Action<KescoButtonBarItem> customization, bool condition)
		{
			if (condition) {
				KescoButtonBarItem button = new KescoButtonBarItem { Caption = caption, ButtonType = "callback" };
				this.control.Buttons.Add(button);
				if (customization != null) customization(button);
			}
			return this;

		}

		public KescoButtonBarBuilder AddButton(string caption, Action<KescoButtonBarItem> customization)
		{
			return AddButton(caption, customization, true);

		}

		/// <summary>
		/// Adds the button which performs submitting of the form with specified action
		/// </summary>
		/// <param name="uri">The URI.</param>
		/// <param name="caption">The caption.</param>
		/// <returns></returns>
		public KescoButtonBarBuilder AddSubmitButton(string caption, string targetForm, string targetFormAction)
		{
			return AddSubmitButtonIf(true, caption, targetForm, targetFormAction);

		}

		/// <summary>
		/// Adds the button which performs submitting of the form with specified action
		/// </summary>
		/// <param name="uri">The URI.</param>
		/// <param name="caption">The caption.</param>
		/// <returns></returns>
		public KescoButtonBarBuilder AddSubmitButtonIf(bool toAdd, string caption, string targetForm, string targetFormAction)
		{
			if (toAdd)
				this.control.Buttons.Add(new KescoButtonBarItem { Caption = caption, TargetForm = targetForm, ButtonType = "submit", Uri = targetFormAction });
			return this;

		}

		/// <summary>
		/// Adds the button which performs close action.
		/// </summary>
		/// <param name="uri">The URI.</param>
		/// <param name="caption">The caption.</param>
		/// <returns></returns>
		public KescoButtonBarBuilder AddCloseButton(string caption)
		{
			return AddCloseButtonIf(true, caption);

		}

		/// <summary>
		/// Adds conditionally the button which performs close action.
		/// </summary>
		/// <param name="uri">The URI.</param>
		/// <param name="caption">The caption.</param>
		/// <returns></returns>
		public KescoButtonBarBuilder AddCloseButtonIf(bool condition, string caption)
		{
			if (condition)
				this.control.Buttons.Add(new KescoButtonBarItem { Caption = caption, ButtonType = "close" });
			return this;

		}


	}
}
