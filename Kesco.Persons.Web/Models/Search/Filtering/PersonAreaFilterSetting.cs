using System;
using System.ComponentModel.DataAnnotations;
using Kesco.Territories.Controls.ComponentModel;
using Kesco.Web.Mvc.Filtering;

namespace Kesco.Persons.Web.Models.Search.Filtering
{
	/// <summary>
	/// 
	/// </summary>
	[MetadataType(typeof(PersonAreaFilterSetting.Metadata))]
	public class PersonAreaFilterSetting : IntQSFilterSetting<PersonAreaFilterSetting>
	{
		/// <summary>
		/// Класс с метаданными для представления значения фильтра
		/// </summary>
		internal class Metadata
		{
			/// <summary>
			/// Возвращает или устанавливает значения фильтра для страна регистрации.
			/// </summary>
			/// <value>
			/// Значения фильтра страна регистрации
			/// </value>
			[TerritorySelect]
			[TerritorySelectSearchParameters(CLID=66, TAreaID=2, Limit=9)]
			public object Value { get; set; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="PersonAreaFilterSetting" /> class.
		/// </summary>
		public PersonAreaFilterSetting() : base(0, Int32.MaxValue) { }
	}
}