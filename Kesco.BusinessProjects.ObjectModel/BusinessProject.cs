using BLToolkit.DataAccess;
using BLToolkit.Mapping;
using Kesco.ObjectModel;

namespace Kesco.BusinessProjects.ObjectModel
{
	/// <summary>
	/// vwБизнесПроекты
	/// </summary>
	[TableName("vwБизнесПроекты")]
	[System.ComponentModel.DataAnnotations.MetadataType(typeof(Metadata.BusinessProject))]
	public class BusinessProject : Entity<BusinessProject, int>
	{
		/// <summary>
		/// КодБизнесПроекта
		/// </summary>
		[MapField("КодБизнесПроекта")]
		[PrimaryKey, NonUpdatable]
		public override int ID { get; set; }

		/// <summary>
		/// БизнесПроект
		/// </summary>
		[MapField("БизнесПроект")]
		public string Name { get; set; }

		/// <summary>
		/// Возвращает отображаемое имя для бизнес-проекта.
		/// </summary>
		/// <returns>Отображаемое имя для бизнес-проекта</returns>
		public override string GetInstanceFriendlyName()
		{
			return Name ?? ("#"+GetUniqueID());
		}
	}
}