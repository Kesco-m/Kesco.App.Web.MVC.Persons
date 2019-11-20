using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kesco.DataAccess
{
	/// <summary>
	/// Интерфейс указывает, что сущность поддерживает поиск
	/// </summary>
	public interface IEntitySearchable
	{
		/// <summary>
		/// Возвращает тип критериев поиска для сущности.
		/// </summary>
		/// <returns></returns>
		Type GetSearchParametersType();
	}
}
