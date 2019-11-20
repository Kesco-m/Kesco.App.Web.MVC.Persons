using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kesco.ObjectModel
{
	/// <summary>
	/// Определяет интерфейс, возвращающий текстовое представление уникального идентификатора
	/// </summary>
	public interface IUnique
	{
		/// <summary>
		/// Получить текстовое представление уникального кода
		/// </summary>
		string GetUniqueID();
	}
}
