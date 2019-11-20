using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kesco.Persons.BusinessLogic.Persons
{
	/// <summary>
	/// Исключение, возникающее при наличии в базе лица с указанным псевдонимом
	/// </summary>
	public class DuplicateNicknameException : Exception
	{
		public List<int> Ids { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="DuplicateNicknameException" /> class.
		/// </summary>
		/// <param name="ids">Список идентификаторов лиц с указанным псевдонимом</param>
		public DuplicateNicknameException(List<int> ids)
			: base("duplicateNicknames")
		{
			Ids = ids;
		}
	}

}
