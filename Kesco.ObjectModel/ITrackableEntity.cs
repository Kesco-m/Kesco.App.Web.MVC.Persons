﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kesco.ObjectModel
{
	/// <summary>
	/// Декларирует интерфейс для объектной модели, 
	/// поддерживающих последние изменения
	/// </summary>
	/// <remarks>
	/// Многие таблицы базы данных поддерживают трекинг последних изменений:
	/// Кто последний и когда изменил данную запись.
	/// </remarks>
	public interface ITrackableEntity
	{
		/// <summary>
		/// Возвращает значение идентификатора пользователя,
		/// кто последний раз изменил запись
		/// </summary>
		/// <value>
		/// Идентификатор пользователя кто последний раз изменил объект базы данных
		/// </value>
		/// <remarks>
		/// Значение свойства является только для чтения. 
		/// Устанавливается программно логикой, определённой в базе данных.
		/// </remarks>
		int? ChangedBy { get; }

		/// <summary>
		/// Возвращает значение даты/времени,
		/// когда последний раз была изменена запись
		/// </summary>
		/// <value>
		/// Значение даты/времени,
		/// когда последний раз была изменена запись
		/// </value>
		/// <remarks>
		/// Значение свойства является только для чтения. 
		/// Устанавливается программно логикой, определённой в базе данных.
		/// </remarks>
		DateTime? ChangedDate { get; }
	}

}