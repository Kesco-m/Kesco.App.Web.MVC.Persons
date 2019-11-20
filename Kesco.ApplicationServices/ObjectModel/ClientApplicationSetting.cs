using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLToolkit.DataAccess;
using BLToolkit.Mapping;
using BLToolkit.Validation;
using Kesco.ObjectModel;

namespace Kesco.ApplicationServices
{
	///////////////////////////////////////////////////////////////////// 
	// ClientApplicationSetting class declaration
	//
	/// <summary>
	/// 
	/// </summary>
	[TableName("НастройкиКлиенты")]
	public abstract partial class ClientApplicationSetting : Entity<ClientApplicationSetting, int> {        
					
			[MapField("КодНастройкиКлиента")] 
			[PrimaryKey, NonUpdatable]
			[Required("Значение поля КодНастройкиКлиента должно быть указано обязательно.")]
			public override int  ID { get; set; } // int
        	    
        			
			[MapField("ЗначениеПоУмолчанию")] 
			[Required("Значение поля ЗначениеПоУмолчанию должно быть указано обязательно.")]
			[MaxLength(100)]
			public string  DefaultValue { get; set; } // varchar
        	    
        			
			[MapField("Сервер")] 
			[Required("Значение поля Сервер должно быть указано обязательно.")]
			[MaxLength(20)]
			public string  Server { get; set; } // varchar
        	    
        			
			[MapField("Клиент")] 
			[Required("Значение поля Клиент должно быть указано обязательно.")]
			[MaxLength(20)]
			public string  Client { get; set; } // varchar
        	    
        			
			[MapField("Описание")] 
			[Required("Значение поля Описание должно быть указано обязательно.")]
			[MaxLength(300)]
			public string  Description { get; set; } // varchar
        	    
        			
			[MapField("НачалоПараметра")] 
			[Required("Значение поля НачалоПараметра должно быть указано обязательно.")]
			[MaxLength(50)]
			public string  Prefix { get; set; } // varchar
        	    
            
	}

}
