using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kesco.ObjectModel
{
	/// <summary>
	/// Данный интерфейс описывает сущность,
	/// имеющую древовидную структуру
	/// </summary>
	public interface ITreeEntity
	{
		/// <summary>
		/// Возвращает значение кода родительского элемента
		/// </summary>
		/// <value>
		/// Код родительского элемента
		/// </value>
		int? Parent { get; set; }

		/// <summary>
		/// Возвращает значение L положение элемента в дереве
		/// </summary>
		/// <value>
		/// Значение L положение элемента в дереве
		/// </value>
		int L { get; set; }

		/// <summary>
		/// Возвращает значение L положение элемента в дереве
		/// </summary>
		/// <value>
		/// Значение L положение элемента в дереве
		/// </value>
		int R { get; set; }
	}
}
