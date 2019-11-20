using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLToolkit.DataAccess;
using System.Reflection;
using BLToolkit.Data;

namespace Kesco.DataAccess.Filtering
{
	
	/// <summary>
	/// Класс-атрибут для построения SQL-запроса с параметрами - критериями поиска
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public class FilterSqlBuilderAttribute : Attribute
	{

		Func<FilterSqlBuilderAttribute, string, string> sqlPreprocessor = (descriptor, input) => input
					.Replace("$(TableName)", descriptor.TableOrViewName)
					.Replace("$(KeyField)", descriptor.UniqueIdField)
					.Replace("$(FieldList)", descriptor.FieldList)
					.Replace("$(TableAlias)", "T0");

		/// <summary>
		/// Контекст для построения команды
		/// </summary>
		public class CommandContext
		{
			public DataAccessor Accessor { get; protected set; }
			public DbManager DbManager { get; protected set; }
			public object Parameters { get; protected set; }
			public StringBuilder Sql { get; protected set; }
			public List<string> Declarations { get; protected set; }
			public List<string> Predicates { get; protected set; }

			public bool HowSearchDefined { get; set; }
			public int RowStartIndex { get; set; }
			public int PageSize { get; set; }
			public List<string> OrderBy { get; set; }

			/// <summary>
			/// Initializes a new instance of the <see cref="CommandContext" /> class.
			/// </summary>
			/// <param name="accessor">The accessor.</param>
			/// <param name="dbManager">The db manager.</param>
			/// <param name="parameters">The parameters.</param>
			public CommandContext(DataAccessor accessor, DbManager dbManager, object parameters)
			{
				Accessor = accessor;
				DbManager = dbManager;
				Parameters = parameters;
				Declarations = new List<string>();
				Sql = new StringBuilder();
				Predicates = new List<string>();
			}

		}

		/// <summary>
		/// Gets or sets the type of the parameters.
		/// </summary>
		/// <value>
		/// The type of the parameters.
		/// </value>
		public Type ParametersType { get; set; }

		/// <summary>
		/// Gets or sets the name of the table or view.
		/// </summary>
		/// <value>
		/// The name of the table or view.
		/// </value>
		public string TableOrViewName { get; set; }

		/// <summary>
		/// Возвращает или устанавливает для таблицы/представления
		/// дополнительные объединения
		/// </summary>
		/// <value>
		/// Дополнительные объединения.
		/// </value>
		public string Joins { get; set; }

		/// <summary>
		/// Gets or sets the unique id field.
		/// </summary>
		/// <value>
		/// The unique id field.
		/// </value>
		public string UniqueIdField { get; set; }

		/// <summary>
		/// Gets or sets the field list.
		/// </summary>
		/// <value>
		/// The field list.
		/// </value>
		public string FieldList { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="FilterSqlBuilderAttribute" /> class.
		/// </summary>
		public FilterSqlBuilderAttribute() : base() { }

		/// <summary>
		/// Строит и подготавливает команду.
		/// </summary>
		/// <param name="accessor">The accessor.</param>
		/// <param name="dbManager">The db manager.</param>
		/// <param name="parameters">The parameters.</param>
		public virtual void BuildAndPrepareCommand(DataAccessor accessor, DbManager dbManager, object parameters)
		{
			CommandContext context = new CommandContext(accessor, dbManager, parameters);

			BuildInternal(context);

			dbManager.SetCommand(context.Sql.ToString());
		}

		/// <summary>
		/// Gets the order by clause.
		/// </summary>
		/// <returns></returns>
		protected virtual string GetOrderByClause()
		{
			return String.Format("ORDER BY 1");
		}

		/// <summary>
		/// Builds the internal.
		/// </summary>
		/// <param name="commandContext">The command context.</param>
		protected virtual void BuildInternal(CommandContext commandContext)
		{
			var properties = ParametersType
				.GetProperties(
						  BindingFlags.Instance
						| BindingFlags.Public 
						| BindingFlags.NonPublic
					)
				.Where(prop => prop.IsDefined(typeof(FilterOptionAttribute), false));

			string output = null;

			var howSearchProp = properties.FirstOrDefault(pr => pr is HowSearchFilterOptionAttribute);
			var pro = properties.FirstOrDefault(pr => pr is SearchStringFilterOptionAttribute);
			if (pro != null)
			{
				var context  =new FilterOptionAttributeContext {
					Context = commandContext,
					Property = pro,
					Value = pro.GetValue(commandContext.Parameters, null)
				};

				if (!string.IsNullOrEmpty((string)context.Value))
				{


					if (!commandContext.HowSearchDefined && howSearchProp != null)
					{
						commandContext.Declarations.Add(@"
							DECLARE @ГдеИскать INT SET @ГдеИскать = {0}
							".FormatWith(context.Value is int ? context.Value.ToString() : "0"));
						commandContext.HowSearchDefined = true;
					}
				}
			}
			
			foreach (var property in properties) {
				var option = property.GetCustomAttributes(typeof(FilterOptionAttribute), false).FirstOrDefault() as FilterOptionAttribute;
				var context = new FilterOptionAttributeContext {
					Context = commandContext,
					Property = property,
					Value = property.GetValue(commandContext.Parameters, null)
				};

				output = option.BuildPrecode(context);
				if (!String.IsNullOrEmpty(output))
					commandContext.Sql.AppendLine(sqlPreprocessor(this, output));

				output = option.BuildPredicate(context);
				if (!String.IsNullOrEmpty(output))
					commandContext.Predicates.Add(sqlPreprocessor(this, output));
			}

			if (commandContext.RowStartIndex > 0) {
				commandContext.Declarations.Add(@"
					;-- Определим таблицу для результатов поиска	
					DECLARE @РезультатПоиска TABLE(Код INT, Порядок BIGINT IDENTITY(0,1))
					");
			}

			if (commandContext.Declarations.Count > 0)
				commandContext.Sql.Insert(0, String.Join(";", commandContext.Declarations));

			int top = 0;

			if (commandContext.PageSize > 0) {
				top += commandContext.PageSize;
				if (commandContext.RowStartIndex > 0)
					top += commandContext.RowStartIndex;
			}

			if (commandContext.RowStartIndex > 0) {
				commandContext.Sql.AppendFormat(@"
					INSERT INTO @РезультатПоиска(Код)
					", this.UniqueIdField, commandContext.RowStartIndex, commandContext.PageSize);
			}

			commandContext.Sql.AppendFormat(@"
					SELECT {2} {1} FROM {0} T0 {3}
					", TableOrViewName,
					(commandContext.RowStartIndex > 0)
							? UniqueIdField
							: (String.IsNullOrEmpty(FieldList) ? " * " : FieldList),
					(top > 0) ? " TOP {0} ".FormatWith(top) : String.Empty,
					Joins ?? String.Empty
						);

			commandContext.Sql.AppendFormatIfTrue(commandContext.Predicates.Count > 0, 
					@"WHERE {0}
					", String.Join(@"
						AND	", commandContext.Predicates));

			commandContext.Sql.AppendFormatIfTrue(commandContext.OrderBy != null && commandContext.OrderBy.Count > 0,
					@"ORDER BY {0}
					", String.Join(@"
						, ", commandContext.OrderBy));

			if (commandContext.RowStartIndex > 0) {
				commandContext.Sql.AppendFormat(@"
					;-- Вернём результат
					SELECT {1}
					FROM @РезультатПоиска РП 
						INNER JOIN {0} T0 ON РП.Код = T0.{2}
					WHERE	РП.Порядок >= {3}{4}
					ORDER BY РП.Порядок
					", TableOrViewName
					 , String.IsNullOrEmpty(FieldList) ? " * " : FieldList
					 , UniqueIdField
					 , commandContext.RowStartIndex
					 , (commandContext.PageSize > 0) ? @" AND РП.Порядок < {0} ".FormatWith(top) : String.Empty);
			}

		}

	}

}
