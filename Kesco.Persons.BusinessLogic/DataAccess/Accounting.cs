using System.Collections.Generic;
using System.Linq;
using BLToolkit.Aspects;
using BLToolkit.Data;
using BLToolkit.Reflection;

namespace Kesco.Persons.BusinessLogic.DataAccess
{
	public static class Accounting
	{
		public class CheckAbacusStatusEntry {
			public string Org { get; set; }
			public string Бухгалтерия { get; set; }
			public int Num { get; set; }
		}

		public class Check1SStatusEntry
		{
			public int КодТипа1с { get; set; }
			public int КодЛица { get; set; }
			public string Вид { get; set; }
			public string Бухгалтерия { get; set; }
			public int Тип { get; set; }
			public int Cnt { get; set; }
		}

		public abstract class DataAccessor : BLToolkit.DataAccess.DataAccessor
		{
			#region Overrides

			/// <summary>
			/// Создаёт экземпляр менеджера базы данных.
			/// </summary>
			/// <returns>Менеджер базы данных, производный от <see cref="BLToolkit.Data.DbManager">DbManager</see></returns>
			[NoInterception]
			protected override DbManager CreateDbManager()
			{
				return new AbacusDB();
			}

			#endregion

			[BLToolkit.DataAccess.SprocName("sp_СписокБухгалтерийОрганизации")]
			public abstract List<CheckAbacusStatusEntry> GetPersonAbacusStatus(int @КодЛица);

			[BLToolkit.DataAccess.SprocName("sp_1s_СписокЭкспортировано")]
			public abstract List<Check1SStatusEntry> GetPerson1SStatus(int? @КодСправочникаОУ, int @Код, int? @КодСправочника1с = null);

		}

		internal static DataAccessor Accessor { get { return TypeAccessor<DataAccessor>.CreateInstance(); } }

		public static bool CheckPersonAbacusStatus(int personID) {
			bool ret = false;
			List<CheckAbacusStatusEntry> list = Accessor.GetPersonAbacusStatus(personID);
			ret = list.Any(status => status.Num == 1);
			return ret;
		}

		public static bool CheckPerson1SStatus(int personID)
		{
			bool ret = false;
			List<Check1SStatusEntry> list = Accessor.GetPerson1SStatus(1, personID);
			ret = list.Any(status => status.Cnt == 1);
			return ret;

		}
	}
}
