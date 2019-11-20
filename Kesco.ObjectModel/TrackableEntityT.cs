using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using BLToolkit.DataAccess;
using BLToolkit.Mapping;

namespace Kesco.ObjectModel
{

	/// <summary>
	/// Класс является базовым для объектов базы данных, 
	/// поддерживающих отслеживание последних изменений.
	/// </summary>
	/// <typeparam name="T">Тип объекта модели</typeparam>
	/// <typeparam name="TID">Тип уникального идентификатора</typeparam>
	public abstract class TrackableEntity<T, TID> : Entity<T, TID>, ITrackableEntity
		where T : TrackableEntity<T, TID>
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
		[MapField("Изменил"), NonUpdatable]
		[Display(
			ResourceType = typeof(Localization.Resources),
			Name = "TrackableEntityBase_ChangedBy_Display_Name",
			ShortName = "TrackableEntityBase_ChangedBy_Display_Name")]
		[ScaffoldColumn(false)]
		[ReadOnly(true)]
		[UIHint("ChangedBy")]
		public int? ChangedBy { get; set; }

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
		[MapField("Изменено"), NonUpdatable]
		[Display(
			ResourceType = typeof(Localization.Resources),
			Name = "TrackableEntityBase_ChangedDate_Display_Name",
			ShortName = "TrackableEntityBase_ChangedDate_Display_Name")]
		[ReadOnly(true)]
		[ScaffoldColumn(false)]
		[UIHint("ChangedDate")]
		public DateTime? ChangedDate { get; set; }

	}
}