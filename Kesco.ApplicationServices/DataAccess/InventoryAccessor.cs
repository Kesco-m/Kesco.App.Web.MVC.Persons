using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLToolkit.Aspects;
using BLToolkit.Data;
using BLToolkit.DataAccess;
using Kesco.DataAccess;

namespace Kesco.ApplicationServices
{
	// Проверка ССНЕТ 2
	public abstract class InventoryAccessor : AccessorBase<InventoryAccessor.DB, InventoryAccessor>
	{
		public class DB : Database { public DB() : base("DS_User") { } }

		[SqlQuery(@"SELECT CASE @en WHEN 1 THEN [FIO] ELSE [ФИО] END AS ФИО FROM Сотрудники WHERE КодСотрудника = @id")]
		[ScalarFieldName("ФИО")]
		public abstract string GetEmployeeLastNameWithInitials(int @id, int @en);

		[SqlQuery(@" 
			/* -- FOR TEST
			DECLARE @keyword varchar(255)
			DECLARE @limit int
			DECLARE @en int

			SET @keyword = '%ФИЛ%'
			SET @limit = 8
			SET @en = 0*/

			SET ROWCOUNT @limit;

			SELECT [КодСотрудника]
				, case @en when 1 then [Employee] else [Сотрудник] end as FIO
			FROM [Инвентаризация]..[Сотрудники]
			WHERE @keyword is null 
				or (@keyword is not null and ([Сотрудник] LIKE @keyword or ([Employee] LIKE @keyword)))
			ORDER BY case @en when 1 then [Employee] else [Сотрудник] end;

			SET ROWCOUNT 0;
		")]
		public abstract List<EmployeeInfo> SearchEmployee(string @keyword, int @limit, int @en);

	}
}
