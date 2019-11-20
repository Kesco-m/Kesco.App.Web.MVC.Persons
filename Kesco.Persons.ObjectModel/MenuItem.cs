using BLToolkit.DataAccess;
using BLToolkit.Mapping;
using BLToolkit.Validation;
using Kesco.ObjectModel;
using System;

namespace Kesco.Persons.ObjectModel
{
	/// <summary>
	/// ПунктыМенюЛиц
	/// </summary>
	[TableName("ПунктыМенюЛиц")]
	public partial class MenuItem : Entity<MenuItem, int>
	{
		/// <summary>
		/// КодПунктаМенюЛиц
		/// </summary>
		[MapField("КодПунктаМенюЛиц")]
		[PrimaryKey, NonUpdatable]
		public override int ID { get; set; }

		/// <summary>
		/// ТипЛица
		/// </summary>
		[MapField("ТипЛица")]
		public byte PersonType { get; set; }

		/// <summary>
		/// ПунктМеню
		/// </summary>
		[MapField("ПунктМеню")]
		[MaxLength(50)]
		public string MenuItemCaption { get; set; }

		/// <summary>
		/// Ключ для ресурсного файла с локализацией пунктов меню
		/// </summary>
		[MapField("ПунктМенюLocalizationKey")]
		[MaxLength(50)]
		public string MenuItemKey { get; set; }

		/// <summary>
		/// ПунктДосье
		/// </summary>
		[MapField("ПунктДосье")]
		[MaxLength(50)]
		public string DossierItemCaption { get; set; }

		/// <summary>
		/// Ключ для ресурсного файла с локализацией пунктов досье
		/// </summary>
		[MapField("ПунктДосьеLocalizationKey")]
		[MaxLength(50)]
		public string DossierItemKey { get; set; }

		/// <summary>
		/// Рисунок
		/// </summary>
		[MapField("Рисунок")]
		[MaxLength(30)]
		public string Picture { get; set; }

		/// <summary>
		/// MinДоступ_Проверено
		/// </summary>
		[MapField("MinДоступ_Проверено")]
		public byte MinAccess_Verified { get; set; }

		/// <summary>
		/// MinДоступ_НеПроверено
		/// </summary>
		[MapField("MinДоступ_НеПроверено")]
		public byte MinAccess_Nonverified { get; set; }

		/// <summary>
		/// Доступ_Роли
		/// </summary>
		[MapField("Доступ_Роли")]
		[MaxLength(50)]
		public string Access_Roles { get; set; }

		/// <summary>
		/// URLформы
		/// </summary>
		[MapField("URLформы")]
		[MaxLength(50)]
		public string URLForm { get; set; }

		/// <summary>
		/// URLпараметры
		/// </summary>
		[MapField("URLпараметры")]
		[MaxLength(100)]
		public string URLParameters { get; set; }

		/// <summary>
		/// РедактированиеСписком
		/// </summary>
		[MapField("РедактированиеСписком")]
		public byte EditByList { get; set; }

		/// <summary>
		/// ЗаголовокФормыВвода
		/// </summary>
		[MapField("ЗаголовокФормыВвода")]
		[MaxLength(100)]
		public string NewItemFormCaption { get; set; }

		/// <summary>
		/// ФормаВвода_aspx
		/// </summary>
		[MapField("ФормаВвода_aspx")]
		[MaxLength(100)]
		public string NewItemForm_aspx { get; set; }

		/// <summary>
		/// ФормаРедактирования_aspx
		/// </summary>
		[MapField("ФормаРедактирования_aspx")]
		[MaxLength(30)]
		public string EditForm_aspx { get; set; }

		/// <summary>
		/// КодТипаСвязейЛиц
		/// </summary>
		[MapField("КодТипаСвязейЛиц")]
		public byte? PersonLinksTypeID { get; set; }

		/// <summary>
		/// ТипURL
		/// </summary>
		[MapField("ТипURL")]
		public byte URLType { get; set; }

		/// <summary>
		/// ПараметрВывода
		/// </summary>
		[MapField("ПараметрВывода")]
		public byte? OutputParameter { get; set; }

		/// <summary>
		/// Разделитель
		/// </summary>
		[MapField("Разделитель")]
		public byte Separator { get; set; }

		/// <summary>
		/// Возвращает отображаемое имя.
		/// </summary>
		/// <returns>Отображаемое имя.</returns>
		public override string GetInstanceFriendlyName()
		{
			return MenuItemCaption ?? String.Format("#{0}", GetUniqueID());
		}
	}
}
