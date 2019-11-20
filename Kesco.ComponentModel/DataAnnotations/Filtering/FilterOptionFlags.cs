using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kesco.ComponentModel.DataAnnotations.Filtering
{
	/// <summary>
	/// Флаги опции фильтрации
	/// </summary>
	[Flags]
	public enum FilterOptionFlags : int
	{
		None = 0,
		Enabled = 0x1,			//	0001
		Fixed = 0x2,			//	0010
		Inverse = 0x4,			//	0100
		MatchAnyItem = 0x8,		// по умолчанию все 

		//флаги по текстовому поиску
		TextBeginsWith = 0x10,	//текст начинается с ...
		TextContains = 0x20,	//текст содержит .. (не удаляются символы)	
		WordsBeginWith = 0x30,	//DEFAULT слова содержат текст (слова берутся из текста по маске)
		WordsContain = 0x40,	//слова начинаются с текста
		TextEquals = 0x80,		//текст точно совпадает

		// TODO: Сделать через ExtendedFlags
		//Флаги по сущностям
		PersonByName = 0x100,
		PersonByDetails = 0x200,
		PersonByContacts = 0x300,
		PersonByAll = PersonByName | PersonByDetails | PersonByContacts

	}
}
