using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using BLToolkit.DataAccess;
using BLToolkit.Mapping;

namespace Kesco.ObjectModel
{
	/// <summary>
	/// Данный класс описывает базовую структуру сущности,
	/// имеющую древовидную структуру c поддержкой трекинга изменений.
	/// </summary>
	/// <typeparam name="T">Тип сущности, имеющую древовидную структуру</typeparam>
	/// <typeparam name="TID">Тип идентификатора</typeparam>
	public abstract class TrackableTreeEntity<T, TID> : TrackableEntity<T, TID>, ITreeEntity
		where T : TrackableTreeEntity<T, TID>
	{
		/// <summary>
		/// Возвращает значение кода родительского элемента
		/// </summary>
		/// <value>
		/// Код родительского элемента
		/// </value>
		[MapField("Parent")]
		public int? Parent { get; set; }

		/// <summary>
		/// Возвращает значение L положение элемента в дереве
		/// </summary>
		/// <value>
		/// Значение L положение элемента в дереве
		/// </value>
		[MapField("L"), NonUpdatable]
		public int L { get; set; }

		/// <summary>
		/// Возвращает значение L положение элемента в дереве
		/// </summary>
		/// <value>
		/// Значение L положение элемента в дереве
		/// </value>
		[MapField("R"), NonUpdatable]
		public int R { get; set; }
	}
}
