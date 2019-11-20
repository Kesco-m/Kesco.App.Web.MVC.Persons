using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kesco.DataAccess;
using BLToolkit.DataAccess;
using System.Data;
using BLToolkit.Data;
using BLToolkit.Mapping;
using Kesco.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Kesco.Web.Mvc.SharedViews.Models.SearchEntity
{
	/// <summary>
	/// 
	/// </summary>
	public class EntityDescription
	{
		[MapField("КодСущности")]
		public int EntityID { get; set; }

		[MapField("Сущность")]
		public string EntityName { get; set; }
	}

	/// <summary>
	/// 
	/// </summary>
	public class Entity : TrackableEntity<Entity, int>
	{
		[UIHint("UniqueID")]
		[MapField("КодСущности")]
		public override int ID { get; set; }

		[MapField("Сущность")]
		[UIHint("TextBox")]
		[Display(Name = "Сущность", Description="Имя сущности")]
		public string EntityName { get; set; }

		[MapField("Описание")]
		[UIHint("TwoLinesTextBox")]
		[StringLength(255)]
		[Display(Name = "Описание", Description = "Описание сущности")]
		public string Description { get; set; }

		[MapField("ТаблицаИлиПредставление")]
		[UIHint("TextBox")]
		[StringLength(64)]
		[Display(Name = "Имя таблицы", Description = "Имя таблицы или пердставления")]
		public string TableName { get; set; }

		[MapField("ПрефиксТаблицы")]
		[UIHint("TextBox")]
		[StringLength(64)]
		[Display(Name = "Префикс таблицы", Description = "Префикс таблицы для использования в запросах")]
		public string TablePrefix { get; set; }

		[MapField("КлючевоеПоле")]
		[UIHint("TextBox")]
		[StringLength(64)]
		[Display(Name = "Ключевое поле", Description = "Ключевое поле в таблице")]
		public string KeyField { get; set; }

		[MapField("СписокПолейВывода")]
		[UIHint("TwoLinesTextBox")]
		[StringLength(4000)]
		[Display(Name = "Список полей", Description = "Список полей для вывода")]
		public string OutputFieldList { get; set; }

		/// <summary>
		/// Gets the friendly name of the instance.
		/// </summary>
		/// <returns></returns>
		public override string GetInstanceFriendlyName()
		{
			return StringExtensions.Coalesco(EntityName, "#{0}".FormatWith(EntityName));
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public class DataModel
	{
		public int EntityID { get; set; }

		public Entity Entity { get; set; }

		public DataModel()
		{
			Entity = Entity.CreateInstance();
		}
			 
	}

	/// <summary>
	/// 
	/// </summary>
	public class ViewModel : ViewModel<DataModel>
	{
		public List<EntityDescription> Entities { get; set; }

		protected Repository repository { get; set; }

		public ViewModel()
		{
			repository = new Repository();
			Entities = repository.GetEntityList();
			//var accessor = SearchAccessor.CreateInstance
			Model.EntityID = 1;
			Model.Entity = repository.GetEntity(Model.EntityID);
		}

		protected override void CreateSettings()
		{
			;
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public class Repository
	{
		protected DbManager Database { get; private set; }

		public Repository()
		{
			Database = new DB();
		}

		public Entity GetEntity(int id)
		{
			return Database
				.SetCommand(@"
						SELECT * 
						FROM Сущности 
						WHERE КодСущности = @КодСущности",
						Database.Parameter("@КодСущности", id)
					)
				.ExecuteObject<Entity>(id);
		}

		public List<EntityDescription> GetEntityList()
		{
			var list = new List<EntityDescription>();
			Database
				.SetCommand("SELECT КодСущности, Сущность FROM Сущности ORDER BY Сущность")
				.ExecuteList<EntityDescription>(list);
			return list;
		}

	}

	/// <summary>
	/// 
	/// </summary>
	public class DB : Database
	{

		public DB() : base("DS_search") { }

		protected override IDbCommand OnInitCommand(IDbCommand command)
		{
			IDbCommand dbCommand = base.OnInitCommand(command);
			dbCommand.CommandTimeout = 30 * 3;
			return dbCommand;
		}

	}

}