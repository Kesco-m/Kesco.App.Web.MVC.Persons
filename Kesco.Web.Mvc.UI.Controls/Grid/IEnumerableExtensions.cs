namespace Kesco.Web.Mvc.UI.Grid
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Data;
	using System.Data.Common;
	using System.Reflection;

	internal static class IEnumerableExtensions
	{
		public static List<string> ToListOfString(this IEnumerable en, JQAutoComplete autoComplete)
		{
			DataTable dataTable = IEnumerableExtensions.ToDataTable(en, autoComplete);
			List<string> list = new List<string>();
			IEnumerator enumerator = dataTable.Rows.GetEnumerator();
			try {
				while (enumerator.MoveNext()) {
					DataRow row = (DataRow)enumerator.Current;
					if (string.IsNullOrEmpty(list.Find((Predicate<string>)(s => s == row[autoComplete.DataField].ToString()))))
						list.Add(row[autoComplete.DataField].ToString());
				}
			} finally {
				IDisposable disposable = enumerator as IDisposable;
				if (disposable != null)
					disposable.Dispose();
			}
			return list;
		}

		public static DataTable ToDataTable(this IEnumerable en, JQAutoComplete autoComplete)
		{
			return IEnumerableExtensions.ToDataTable(en, 
					new JQGrid() {
						Columns = {
							new JQGridColumn() {
								DataField = autoComplete.DataField
							}
						}
					}
				);
		}

		public static DataTable ToDataTable(this IEnumerable en, JQGrid grid)
		{
			DataTable dataTable = new DataTable();
			DataView dataView = en as DataView;
			if (dataView != null)
				dataTable = dataView.ToTable();
			else if (en != null)
				dataTable = IEnumerableExtensions.ObtainDataTableFromIEnumerable(en, grid);
			return dataTable;
		}

		private static DataTable ObtainDataTableFromIEnumerable(IEnumerable ien, JQGrid grid)
		{
			DataTable dataTable = new DataTable();
			foreach (object obj1 in ien) {
				if (obj1 is DbDataRecord) {
					DbDataRecord dbDataRecord = obj1 as DbDataRecord;
					if (dataTable.Columns.Count == 0) {
						foreach (JQGridColumn jqGridColumn in grid.Columns)
							dataTable.Columns.Add(jqGridColumn.DataField);
					}
					DataRow row = dataTable.NewRow();
					foreach (JQGridColumn jqGridColumn in grid.Columns)
						row[jqGridColumn.DataField] = dbDataRecord[jqGridColumn.DataField];
					dataTable.Rows.Add(row);
				} else if (obj1 is DataRow) {
					DataRow dataRow = obj1 as DataRow;
					if (dataTable.Columns.Count == 0) {
						foreach (JQGridColumn jqGridColumn in grid.Columns)
							dataTable.Columns.Add(jqGridColumn.DataField);
					}
					DataRow row = dataTable.NewRow();
					foreach (JQGridColumn jqGridColumn in grid.Columns)
						row[jqGridColumn.DataField] = dataRow[jqGridColumn.DataField];
					dataTable.Rows.Add(row);
				} else {
					PropertyInfo[] properties = obj1.GetType().GetProperties();
					if (dataTable.Columns.Count == 0) {
						foreach (PropertyInfo propertyInfo in properties) {
							Type type = propertyInfo.PropertyType;
							if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
								type = Nullable.GetUnderlyingType(type);
							dataTable.Columns.Add(propertyInfo.Name, type);
						}
					}
					DataRow row = dataTable.NewRow();
					foreach (PropertyInfo propertyInfo in properties) {
						object obj2 = propertyInfo.GetValue(obj1, (object[])null);
						row[propertyInfo.Name] = obj2 == null ? (object)DBNull.Value : obj2;
					}
					dataTable.Rows.Add(row);
				}
			}
			return dataTable;
		}

		/*public static string ToJson(this IEnumerable en)
		{
			JavaScriptSerializer serializer = new JavaScriptSerializer();
			List<dynamic> list = new List<dynamic>();
			IEnumerator enumerator = dataTable.Rows.GetEnumerator();
			try {
				DataRow row;
				while (enumerator.MoveNext()) {
					row = (DataRow) enumerator.Current;
					if (string.IsNullOrEmpty(list.Find((string s) => s == row[autoComplete.DataField].ToString()))) {
						list.Add(row[autoComplete.DataField].ToString());
					}
				}
			} finally {
				IDisposable disposable = enumerator as IDisposable;
				if (disposable != null) {
					disposable.Dispose();
				}
			}
			return serializer.Serialize();
		}*/
	}
}
