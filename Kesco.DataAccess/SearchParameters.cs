using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kesco.ComponentModel.DataAnnotations.Filtering;
using BLToolkit.Mapping;

namespace Kesco.DataAccess
{
	// TODO: Сделать свойством класса SearchParameters
	[MapValue(0, HowSearch.Contains)]
	[MapValue(1, HowSearch.StartsWith)]
	public enum HowSearch : int
	{
		Contains = 0,
		StartsWith = 1
	}

	/// <summary>
	/// Базовый класс для критериев поиска сущности
	/// </summary>
	public class SearchParameters
	{
		public int CLID { get; set; }

		/// <summary>
		/// Возвращает или устанавливает строку поиска.
		/// </summary>
		/// <value>
		/// Строку поиска.
		/// </value>
		[FilterOption(Name="Строка поиска")]
		public string Search { get; set; }
	}
}
