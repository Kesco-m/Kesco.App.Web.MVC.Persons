using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLToolkit.Common;
using BLToolkit.DataAccess;
using BLToolkit.Mapping;
using BLToolkit.Validation;
using Kesco.ObjectModel;

namespace Kesco.ApplicationServices
{
	
	[TableName("vwНастройки")]
	public abstract partial class UserSetting : EntityBase<UserSetting> {        
					
			[MapField("КодНастройкиКлиента")]
			[PrimaryKey]
			[Required("Значение поля КодНастройкиКлиента должно быть указано обязательно.")]
			public int ClientApplicationID { get; set; } // int
        			
			[MapField("Параметр")]
			[PrimaryKey]
			[Required("Значение поля Параметр должно быть указано обязательно.")]
			[MaxLength(20)]
			public string Parameter { get; set; } // varchar
        			
			[MapField("Значение")] 
			[Required("Значение поля Значение должно быть указано обязательно.")]
			[MaxLength(1000)]
			public string Value { get; set; } // varchar
            
	}

}
