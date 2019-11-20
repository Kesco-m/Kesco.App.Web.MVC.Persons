using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kesco.Persons.BusinessLogic.Persons
{

	/// <summary>
	/// Исключение, возникающее при наличии в базе похожих лиц
	/// </summary>
	public class SavePersonException : Exception
	{
		/// <summary>
		/// Возвращает список спорных вопросов, возникших при сохранении.
		/// </summary>
		/// <value>
		/// Список спорных вопросов.
		/// </value>
		public List<SaveIssue> Issues { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="SavePersonException" /> class.
		/// </summary>
		/// <param name="issues">The issues.</param>
		public SavePersonException(List<SaveIssue> issues)
			: base("duplicate")
		{
			Issues = issues;
		}
	}

}
