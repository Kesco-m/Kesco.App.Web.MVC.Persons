using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kesco.Web.Mvc
{
	/// <summary>
	/// Реализует класс модели данных для представления,
	/// которое является диалогом и возвращает результат
	/// </summary>
	public class DialogResult : DialogViewModel
	{
		/// <summary>
		/// Возвращает ключ, по которому диалог будет возвращать значение
		/// </summary>
		public string Key { get; internal set; }

		/// <summary>
		/// Возвращает значение, которое вернёт диалог
		/// </summary>
		public string Value { get; internal set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="DialogResult"/> class.
		/// </summary>
		/// <param name="returnUri">The return URI.</param>
		/// <param name="key">The key.</param>
		/// <param name="value">The value.</param>
		public DialogResult(string returnUri, string key, string value)
			: base(0, returnUri, true)
		{
			Key = key;
			Value = value;
		}

		/// <summary>
		/// Создаёт объект с настройками пользователя.
		/// </summary>
		protected override void CreateSettings() {
			settings = null;
		}

		/// <summary>
		/// Возвращает признак, указывающий, что установлен ключ со значением.
		/// </summary>
		/// <value>
		///   <c>true</c> если установлен ключ со значением; иначе, <c>false</c>.
		/// </value>
		public bool HasKey
		{
			get { return !String.IsNullOrEmpty(Key); }
		}

	}

}
