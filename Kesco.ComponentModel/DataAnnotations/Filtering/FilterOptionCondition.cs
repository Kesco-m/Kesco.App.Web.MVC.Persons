using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kesco.ComponentModel.DataAnnotations.Filtering
{
	public enum FilterOptionCondition
	{
		None = 0,

		// для Option
		IsNull = 0x1, // 0001
		Equals = 0x2, // 0010
		Less = 0x4, // 0100
		More = 0x8, // 1000
		EqualsOrLess = Equals | Less,
		EqualsOrMore = Equals | More,
		NotEquals = Less | More,

		// дополнительно для IntOption
		/// <summary>
		/// искомый элемент является потомком (не обязательно прямым) указанного элемента
		/// </summary>
		ChildOf = 0x10, // 0000 0001 0000
		DirectChildOf = 0x20, // 0000 0010 0000
		ParentOf = 0x40, // 0000 0100 0000
		DirectParentOf = 0x80, // 0000 1000 0000
		SameAs = 0x100 // 0001 0000 0000

		// дополнительно для TextOption
	}
}
