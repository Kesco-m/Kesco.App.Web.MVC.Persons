using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLToolkit.Mapping;

namespace Kesco.Persons.BusinessLogic.Dossier
{
	/// <summary>
	/// Определяет уровень доступа к просмотру лица и возможностям его редактирования
	/// </summary>
	public enum PersonAccessLevel : int
	{
		/// <summary>
		/// Нет прав на работу с лицом (просмотр, редактирование)
		/// </summary>
		[MapValue(0)]
		None = 0,
		/// <summary>
		/// Доступ на просмотр лица
		/// </summary>
		[MapValue(1)]
		CanView = 1,
		/// <summary>
		/// Сотрудник является ответственным за лицо (или замещает такого сотрудника)
		/// </summary>
		[MapValue(2)]
		ResponsibleForPerson = 2,
		/// <summary>
		/// Секретарь
		/// </summary>
		[MapValue(3)]
		Secretary = 3,
		/// <summary>
		/// Администратор лиц или кадровик у данного лица
		/// </summary>
		[MapValue(4)]
		Administrator = 4
	}
}
