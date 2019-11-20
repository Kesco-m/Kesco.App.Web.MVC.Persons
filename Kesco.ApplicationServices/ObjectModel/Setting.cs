using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kesco.ObjectModel;
using BLToolkit.DataAccess;
using BLToolkit.Mapping;
using BLToolkit.Validation;

namespace Kesco.ApplicationServices
{
	[TableName("НастройкиПараметры")]
	public partial class Setting : Entity<Setting, string>
	{
		[MapField("Параметр")]
		[PrimaryKey, NonUpdatable]
		[Required("Значение поля Параметр должно быть указано обязательно.")]
		[MaxLength(20)]
		public override string ID { get; set; } // varchar


		[MapField("ТипПараметра")]
		[Required("Значение поля ТипПараметра должно быть указано обязательно.")]
		public int ParameterType { get; set; } // tinyint


		[MapField("ВозможныеЗначения")]
		[Required("Значение поля ВозможныеЗначения должно быть указано обязательно.")]
		[MaxLength(50)]
		public string ValidValues { get; set; } // varchar


		[MapField("ЗначениеПоУмолчанию")]
		[Required("Значение поля ЗначениеПоУмолчанию должно быть указано обязательно.")]
		[MaxLength(50)]
		public string DefaultValue { get; set; } // varchar


		[MapField("Описание")]
		[Required("Значение поля Описание должно быть указано обязательно.")]
		[MaxLength(300)]
		public string Description { get; set; } // varchar

	}
}
