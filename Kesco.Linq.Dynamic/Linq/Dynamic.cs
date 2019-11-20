//Copyright (C) Microsoft Corporation.  All rights reserved.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading;

namespace Kesco.Linq
{
	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="T"></typeparam>
	internal static class IQueryableUtil<T>
	{
		public static int Count(IQueryable Source)
		{
			return Source.OfType<T>().AsQueryable<T>().Count<T>();
		}
		public static IQueryable Page(IQueryable Source, int PageSize, int PageIndex)
		{
			return Source.OfType<T>().AsQueryable<T>().Skip(PageSize * (PageIndex - 1)).Take(PageSize);
		}
	}

	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <typeparam name="PT">The type of the T.</typeparam>
	internal static class IQueryableUtil<T, PT>
	{
		public static IQueryable Sort(IQueryable source, string sortExpression, bool Ascending)
		{
			ParameterExpression parameterExpression = Expression.Parameter(typeof(T), "item");
			Expression<Func<T, PT>> keySelector = Expression.Lambda<Func<T, PT>>(Expression.Convert(Expression.Property(parameterExpression, sortExpression), typeof(PT)), new ParameterExpression[]
			{
				parameterExpression
			});
			if (Ascending) {
				return source.OfType<T>().AsQueryable<T>().OrderBy(keySelector);
			}
			return source.OfType<T>().AsQueryable<T>().OrderByDescending(keySelector);
		}
		public static IQueryable Contains(IQueryable Source, string PropertyName, string SearchClause)
		{
			ParameterExpression parameterExpression = Expression.Parameter(typeof(T), "item");
			MemberExpression memberExpression = Expression.Property(parameterExpression, PropertyName);
			Expression.Convert(memberExpression, typeof(object));
			ConstantExpression constantExpression = Expression.Constant(SearchClause, typeof(string));
			MethodCallExpression body = Expression.Call(memberExpression, "Contains", new Type[0], new Expression[]
			{
				constantExpression
			});
			Expression<Func<T, bool>> predicate = Expression.Lambda<Func<T, bool>>(body, new ParameterExpression[]
			{
				parameterExpression
			});
			return Source.OfType<T>().AsQueryable<T>().Where(predicate);
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public static class DynamicQueryable
    {
		public static IQueryable<T> Where<T>(this IQueryable<T> source, string predicate, params object[] values)
		{
			return (IQueryable<T>)source.Where(predicate, values);
		}
		public static IQueryable Where(this IQueryable source, string predicate, params object[] values)
		{
			if (source == null) {
				throw new ArgumentNullException("source");
			}
			if (predicate == null) {
				throw new ArgumentNullException("predicate");
			}
			LambdaExpression expression = DynamicExpression.ParseLambda(source.ElementType, typeof(bool), predicate, values);
			return source.Provider.CreateQuery(Expression.Call(typeof(Queryable), "Where", new Type[]
			{
				source.ElementType
			}, new Expression[]
			{
				source.Expression, 
				Expression.Quote(expression)
			}));
		}
		public static IQueryable Select(this IQueryable source, string selector, params object[] values)
		{
			if (source == null) {
				throw new ArgumentNullException("source");
			}
			if (selector == null) {
				throw new ArgumentNullException("selector");
			}
			LambdaExpression lambdaExpression = DynamicExpression.ParseLambda(source.ElementType, null, selector, values);
			return source.Provider.CreateQuery(Expression.Call(typeof(Queryable), "Select", new Type[]
			{
				source.ElementType, 
				lambdaExpression.Body.Type
			}, new Expression[]
			{
				source.Expression, 
				Expression.Quote(lambdaExpression)
			}));
		}
		public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string ordering, params object[] values)
		{
			return (IQueryable<T>)source.OrderBy(ordering, values);
		}
		public static IQueryable OrderBy(this IQueryable source, string ordering, params object[] values)
		{
			if (source == null) {
				throw new ArgumentNullException("source");
			}
			if (ordering == null) {
				throw new ArgumentNullException("ordering");
			}
			ParameterExpression[] parameters = new ParameterExpression[]
			{
				Expression.Parameter(source.ElementType, "")
			};
			ExpressionParser expressionParser = new ExpressionParser(parameters, ordering, values);
			IEnumerable<DynamicOrdering> enumerable = expressionParser.ParseOrdering();
			Expression expression = source.Expression;
			string text = "OrderBy";
			string text2 = "OrderByDescending";
			foreach (DynamicOrdering current in enumerable) {
				expression = Expression.Call(typeof(Queryable), current.Ascending ? text : text2, new Type[]
				{
					source.ElementType, 
					current.Selector.Type
				}, new Expression[]
				{
					expression, 
					Expression.Quote(Expression.Lambda(current.Selector, parameters))
				});
				text = "ThenBy";
				text2 = "ThenByDescending";
			}
			return source.Provider.CreateQuery(expression);
		}
		public static IQueryable Take(this IQueryable source, int count)
		{
			if (source == null) {
				throw new ArgumentNullException("source");
			}
			return source.Provider.CreateQuery(Expression.Call(typeof(Queryable), "Take", new Type[]
			{
				source.ElementType
			}, new Expression[]
			{
				source.Expression, 
				Expression.Constant(count)
			}));
		}
		public static IQueryable Skip(this IQueryable source, int count)
		{
			if (source == null) {
				throw new ArgumentNullException("source");
			}
			return source.Provider.CreateQuery(Expression.Call(typeof(Queryable), "Skip", new Type[]
			{
				source.ElementType
			}, new Expression[]
			{
				source.Expression, 
				Expression.Constant(count)
			}));
		}
		public static IQueryable GroupBy(this IQueryable source, string keySelector, string elementSelector, params object[] values)
		{
			if (source == null) {
				throw new ArgumentNullException("source");
			}
			if (keySelector == null) {
				throw new ArgumentNullException("keySelector");
			}
			if (elementSelector == null) {
				throw new ArgumentNullException("elementSelector");
			}
			LambdaExpression lambdaExpression = DynamicExpression.ParseLambda(source.ElementType, null, keySelector, values);
			LambdaExpression lambdaExpression2 = DynamicExpression.ParseLambda(source.ElementType, null, elementSelector, values);
			return source.Provider.CreateQuery(Expression.Call(typeof(Queryable), "GroupBy", new Type[]
			{
				source.ElementType, 
				lambdaExpression.Body.Type, 
				lambdaExpression2.Body.Type
			}, new Expression[]
			{
				source.Expression, 
				Expression.Quote(lambdaExpression), 
				Expression.Quote(lambdaExpression2)
			}));
		}
		public static bool Any(this IQueryable source)
		{
			if (source == null) {
				throw new ArgumentNullException("source");
			}
			return (bool)source.Provider.Execute(Expression.Call(typeof(Queryable), "Any", new Type[]
			{
				source.ElementType
			}, new Expression[]
			{
				source.Expression
			}));
		}
		public static int Count(this IQueryable source)
		{
			if (source == null) {
				throw new ArgumentNullException("source");
			}
			return (int)source.Provider.Execute(Expression.Call(typeof(Queryable), "Count", new Type[]
			{
				source.ElementType
			}, new Expression[]
			{
				source.Expression
			}));
		}
		public static int GetTotalRowCount(this IQueryable Source)
		{
			Type type = Source.GetType();
			Type dataItemType = Source.GetDataItemType();
			Type type2 = typeof(IQueryableUtil<>).MakeGenericType(new Type[]
			{
				dataItemType
			});
			return (int)type2.GetMethod("Count", new Type[]
			{
				type
			}).Invoke(null, new object[]
			{
				Source
			});
		}
		public static Type GetDataItemType(this IQueryable DataSource)
		{
			Type type = DataSource.GetType();
			Type result = typeof(object);
			if (type.HasElementType) {
				result = type.GetElementType();
			} else {
				if (type.IsGenericType) {
					result = type.GetGenericArguments()[0];
				} else {
					if (DataSource != null) {
						IEnumerator enumerator = DataSource.GetEnumerator();
						if (enumerator.MoveNext() && enumerator.Current != null) {
							result = enumerator.Current.GetType();
						}
					}
				}
			}
			return result;
		}
	}

	/// <summary>
	/// 
	/// </summary>
    public abstract class DynamicClass
    {
		public override string ToString()
		{
			PropertyInfo[] properties = base.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("{");
			for (int i = 0; i < properties.Length; i++) {
				if (i > 0) {
					stringBuilder.Append(", ");
				}
				stringBuilder.Append(properties[i].Name);
				stringBuilder.Append("=");
				stringBuilder.Append(properties[i].GetValue(this, null));
			}
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}
	}

	/// <summary>
	/// 
	/// </summary>
    public class DynamicProperty
    {
		private string name;
		private Type type;
		public string Name
		{
			get
			{
				return this.name;
			}
		}
		public Type Type
		{
			get
			{
				return this.type;
			}
		}
		public DynamicProperty(string name, Type type)
		{
			if (name == null) {
				throw new ArgumentNullException("name");
			}
			if (type == null) {
				throw new ArgumentNullException("type");
			}
			this.name = name;
			this.type = type;
		}
	}

	/// <summary>
	/// 
	/// </summary>
    public static class DynamicExpression
    {
		public static Expression Parse(Type resultType, string expression, params object[] values)
		{
			ExpressionParser expressionParser = new ExpressionParser(null, expression, values);
			return expressionParser.Parse(resultType);
		}
		public static LambdaExpression ParseLambda(Type itType, Type resultType, string expression, params object[] values)
		{
			return DynamicExpression.ParseLambda(new ParameterExpression[]
			{
				Expression.Parameter(itType, "")
			}, resultType, expression, values);
		}
		public static LambdaExpression ParseLambda(ParameterExpression[] parameters, Type resultType, string expression, params object[] values)
		{
			ExpressionParser expressionParser = new ExpressionParser(parameters, expression, values);
			return Expression.Lambda(expressionParser.Parse(resultType), parameters);
		}
		public static Expression<Func<T, S>> ParseLambda<T, S>(string expression, params object[] values)
		{
			return (Expression<Func<T, S>>)DynamicExpression.ParseLambda(typeof(T), typeof(S), expression, values);
		}
		public static Type CreateClass(params DynamicProperty[] properties)
		{
			return ClassFactory.Instance.GetDynamicClass(properties);
		}
		public static Type CreateClass(IEnumerable<DynamicProperty> properties)
		{
			return ClassFactory.Instance.GetDynamicClass(properties);
		}
	}

	/// <summary>
	/// 
	/// </summary>
    internal class DynamicOrdering
    {
        public Expression Selector;
        public bool Ascending;
    }

	/// <summary>
	/// 
	/// </summary>
    internal class Signature : IEquatable<Signature>
    {
		public DynamicProperty[] properties;
		public int hashCode;
		public Signature(IEnumerable<DynamicProperty> properties)
		{
			this.properties = properties.ToArray<DynamicProperty>();
			this.hashCode = 0;
			foreach (DynamicProperty current in properties) {
				this.hashCode ^= (current.Name.GetHashCode() ^ current.Type.GetHashCode());
			}
		}
		public override int GetHashCode()
		{
			return this.hashCode;
		}
		public override bool Equals(object obj)
		{
			return obj is Signature && this.Equals((Signature)obj);
		}
		public bool Equals(Signature other)
		{
			if (this.properties.Length != other.properties.Length) {
				return false;
			}
			for (int i = 0; i < this.properties.Length; i++) {
				if (this.properties[i].Name != other.properties[i].Name || this.properties[i].Type != other.properties[i].Type) {
					return false;
				}
			}
			return true;
		}
	}

	/// <summary>
	/// 
	/// </summary>
    internal class ClassFactory
    {
		public static readonly ClassFactory Instance;
		private ModuleBuilder module;
		private Dictionary<Signature, Type> classes;
		private int classCount;
		private ReaderWriterLock rwLock;

		static ClassFactory()
		{
			ClassFactory.Instance = new ClassFactory();
		}

		private ClassFactory()
		{
			AssemblyName name = new AssemblyName("DynamicClasses");
			AssemblyBuilder assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(name, AssemblyBuilderAccess.Run);
			this.module = assemblyBuilder.DefineDynamicModule("Module");
			this.classes = new Dictionary<Signature, Type>();
			this.rwLock = new ReaderWriterLock();
		}

		public Type GetDynamicClass(IEnumerable<DynamicProperty> properties)
		{
			this.rwLock.AcquireReaderLock(-1);
			Type result;
			try {
				Signature signature = new Signature(properties);
				Type type;
				if (!this.classes.TryGetValue(signature, out type)) {
					type = this.CreateDynamicClass(signature.properties);
					this.classes.Add(signature, type);
				}
				result = type;
			} finally {
				this.rwLock.ReleaseReaderLock();
			}
			return result;
		}

		private Type CreateDynamicClass(DynamicProperty[] properties)
		{
			LockCookie lockCookie = this.rwLock.UpgradeToWriterLock(-1);
			Type result;
			try {
				string name = "DynamicClass" + (this.classCount + 1);
				TypeBuilder typeBuilder = this.module.DefineType(name, TypeAttributes.Public, typeof(DynamicClass));
				FieldInfo[] fields = this.GenerateProperties(typeBuilder, properties);
				this.GenerateEquals(typeBuilder, fields);
				this.GenerateGetHashCode(typeBuilder, fields);
				Type type = typeBuilder.CreateType();
				this.classCount++;
				result = type;
			} finally {
				this.rwLock.DowngradeFromWriterLock(ref lockCookie);
			}
			return result;
		}

		private FieldInfo[] GenerateProperties(TypeBuilder tb, DynamicProperty[] properties)
		{
			FieldInfo[] array = new FieldBuilder[properties.Length];
			for (int i = 0; i < properties.Length; i++) {
				DynamicProperty dynamicProperty = properties[i];
				FieldBuilder fieldBuilder = tb.DefineField("_" + dynamicProperty.Name, dynamicProperty.Type, FieldAttributes.Private);
				PropertyBuilder propertyBuilder = tb.DefineProperty(dynamicProperty.Name, PropertyAttributes.HasDefault, dynamicProperty.Type, null);
				MethodBuilder methodBuilder = tb.DefineMethod("get_" + dynamicProperty.Name, MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.HideBySig | MethodAttributes.SpecialName, dynamicProperty.Type, Type.EmptyTypes);
				ILGenerator iLGenerator = methodBuilder.GetILGenerator();
				iLGenerator.Emit(OpCodes.Ldarg_0);
				iLGenerator.Emit(OpCodes.Ldfld, fieldBuilder);
				iLGenerator.Emit(OpCodes.Ret);
				MethodBuilder methodBuilder2 = tb.DefineMethod("set_" + dynamicProperty.Name, MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.HideBySig | MethodAttributes.SpecialName, null, new Type[]
				{
					dynamicProperty.Type
				});
				ILGenerator iLGenerator2 = methodBuilder2.GetILGenerator();
				iLGenerator2.Emit(OpCodes.Ldarg_0);
				iLGenerator2.Emit(OpCodes.Ldarg_1);
				iLGenerator2.Emit(OpCodes.Stfld, fieldBuilder);
				iLGenerator2.Emit(OpCodes.Ret);
				propertyBuilder.SetGetMethod(methodBuilder);
				propertyBuilder.SetSetMethod(methodBuilder2);
				array[i] = fieldBuilder;
			}
			return array;
		}

		private void GenerateEquals(TypeBuilder tb, FieldInfo[] fields)
		{
			MethodBuilder methodBuilder = tb.DefineMethod("Equals", MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Virtual | MethodAttributes.HideBySig, typeof(bool), new Type[]
			{
				typeof(object)
			});
			ILGenerator iLGenerator = methodBuilder.GetILGenerator();
			LocalBuilder local = iLGenerator.DeclareLocal(tb);
			Label label = iLGenerator.DefineLabel();
			iLGenerator.Emit(OpCodes.Ldarg_1);
			iLGenerator.Emit(OpCodes.Isinst, tb);
			iLGenerator.Emit(OpCodes.Stloc, local);
			iLGenerator.Emit(OpCodes.Ldloc, local);
			iLGenerator.Emit(OpCodes.Brtrue_S, label);
			iLGenerator.Emit(OpCodes.Ldc_I4_0);
			iLGenerator.Emit(OpCodes.Ret);
			iLGenerator.MarkLabel(label);
			for (int i = 0; i < fields.Length; i++) {
				FieldInfo fieldInfo = fields[i];
				Type fieldType = fieldInfo.FieldType;
				Type type = typeof(EqualityComparer<>).MakeGenericType(new Type[]
				{
					fieldType
				});
				label = iLGenerator.DefineLabel();
				iLGenerator.EmitCall(OpCodes.Call, type.GetMethod("get_Default"), null);
				iLGenerator.Emit(OpCodes.Ldarg_0);
				iLGenerator.Emit(OpCodes.Ldfld, fieldInfo);
				iLGenerator.Emit(OpCodes.Ldloc, local);
				iLGenerator.Emit(OpCodes.Ldfld, fieldInfo);
				iLGenerator.EmitCall(OpCodes.Callvirt, type.GetMethod("Equals", new Type[]
				{
					fieldType, 
					fieldType
				}), null);
				iLGenerator.Emit(OpCodes.Brtrue_S, label);
				iLGenerator.Emit(OpCodes.Ldc_I4_0);
				iLGenerator.Emit(OpCodes.Ret);
				iLGenerator.MarkLabel(label);
			}
			iLGenerator.Emit(OpCodes.Ldc_I4_1);
			iLGenerator.Emit(OpCodes.Ret);
		}

		private void GenerateGetHashCode(TypeBuilder tb, FieldInfo[] fields)
		{
			MethodBuilder methodBuilder = tb.DefineMethod("GetHashCode", MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Virtual | MethodAttributes.HideBySig, typeof(int), Type.EmptyTypes);
			ILGenerator iLGenerator = methodBuilder.GetILGenerator();
			iLGenerator.Emit(OpCodes.Ldc_I4_0);
			for (int i = 0; i < fields.Length; i++) {
				FieldInfo fieldInfo = fields[i];
				Type fieldType = fieldInfo.FieldType;
				Type type = typeof(EqualityComparer<>).MakeGenericType(new Type[]
				{
					fieldType
				});
				iLGenerator.EmitCall(OpCodes.Call, type.GetMethod("get_Default"), null);
				iLGenerator.Emit(OpCodes.Ldarg_0);
				iLGenerator.Emit(OpCodes.Ldfld, fieldInfo);
				iLGenerator.EmitCall(OpCodes.Callvirt, type.GetMethod("GetHashCode", new Type[]
				{
					fieldType
				}), null);
				iLGenerator.Emit(OpCodes.Xor);
			}
			iLGenerator.Emit(OpCodes.Ret);
		}
	}

	/// <summary>
	/// 
	/// </summary>
    public sealed class ParseException : Exception
    {
		private int position;
		public int Position
		{
			get
			{
				return this.position;
			}
		}

		public ParseException(string message, int position)
			: base(message)
		{
			this.position = position;
		}

		public override string ToString()
		{
			return string.Format("{0} (at index {1})", this.Message, this.position);
		}
	}

	/// <summary>
	/// 
	/// </summary>
    internal class ExpressionParser
    {
		private struct Token
		{
			public ExpressionParser.TokenId id;
			public string text;
			public int pos;
		}
		private enum TokenId
		{
			Unknown,
			End,
			Identifier,
			StringLiteral,
			IntegerLiteral,
			RealLiteral,
			Exclamation,
			Percent,
			Amphersand,
			OpenParen,
			CloseParen,
			Asterisk,
			Plus,
			Comma,
			Minus,
			Dot,
			Slash,
			Colon,
			LessThan,
			Equal,
			GreaterThan,
			Question,
			OpenBracket,
			CloseBracket,
			Bar,
			ExclamationEqual,
			DoubleAmphersand,
			LessThanEqual,
			LessGreater,
			DoubleEqual,
			GreaterThanEqual,
			DoubleBar
		}
		private interface ILogicalSignatures
		{
			void F(bool x, bool y);
			void F(bool? x, bool? y);
		}
		private interface IArithmeticSignatures
		{
			void F(int x, int y);
			void F(uint x, uint y);
			void F(long x, long y);
			void F(ulong x, ulong y);
			void F(float x, float y);
			void F(double x, double y);
			void F(decimal x, decimal y);
			void F(int? x, int? y);
			void F(uint? x, uint? y);
			void F(long? x, long? y);
			void F(ulong? x, ulong? y);
			void F(float? x, float? y);
			void F(double? x, double? y);
			void F(decimal? x, decimal? y);
		}
		private interface IRelationalSignatures : ExpressionParser.IArithmeticSignatures
		{
			void F(string x, string y);
			void F(char x, char y);
			void F(DateTime x, DateTime y);
			void F(TimeSpan x, TimeSpan y);
			void F(char? x, char? y);
			void F(DateTime? x, DateTime? y);
			void F(TimeSpan? x, TimeSpan? y);
		}
		private interface IEqualitySignatures : ExpressionParser.IRelationalSignatures, ExpressionParser.IArithmeticSignatures
		{
			void F(bool x, bool y);
			void F(bool? x, bool? y);
		}
		private interface IAddSignatures : ExpressionParser.IArithmeticSignatures
		{
			void F(DateTime x, TimeSpan y);
			void F(TimeSpan x, TimeSpan y);
			void F(DateTime? x, TimeSpan? y);
			void F(TimeSpan? x, TimeSpan? y);
		}
		private interface ISubtractSignatures : ExpressionParser.IAddSignatures, ExpressionParser.IArithmeticSignatures
		{
			void F(DateTime x, DateTime y);
			void F(DateTime? x, DateTime? y);
		}
		private interface INegationSignatures
		{
			void F(int x);
			void F(long x);
			void F(float x);
			void F(double x);
			void F(decimal x);
			void F(int? x);
			void F(long? x);
			void F(float? x);
			void F(double? x);
			void F(decimal? x);
		}
		private interface INotSignatures
		{
			void F(bool x);
			void F(bool? x);
		}
		private interface IEnumerableSignatures
		{
			void Where(bool predicate);
			void Any();
			void Any(bool predicate);
			void All(bool predicate);
			void Count();
			void Count(bool predicate);
			void Min(object selector);
			void Max(object selector);
			void Sum(int selector);
			void Sum(int? selector);
			void Sum(long selector);
			void Sum(long? selector);
			void Sum(float selector);
			void Sum(float? selector);
			void Sum(double selector);
			void Sum(double? selector);
			void Sum(decimal selector);
			void Sum(decimal? selector);
			void Average(int selector);
			void Average(int? selector);
			void Average(long selector);
			void Average(long? selector);
			void Average(float selector);
			void Average(float? selector);
			void Average(double selector);
			void Average(double? selector);
			void Average(decimal selector);
			void Average(decimal? selector);
		}
		private class MethodData
		{
			public MethodBase MethodBase;
			public ParameterInfo[] Parameters;
			public Expression[] Args;
		}
		private static readonly Type[] predefinedTypes = new Type[]
		{
			typeof(object), 
			typeof(bool), 
			typeof(char), 
			typeof(string), 
			typeof(sbyte), 
			typeof(byte), 
			typeof(short), 
			typeof(ushort), 
			typeof(int), 
			typeof(uint), 
			typeof(long), 
			typeof(ulong), 
			typeof(float), 
			typeof(double), 
			typeof(decimal), 
			typeof(DateTime), 
			typeof(TimeSpan), 
			typeof(Guid), 
			typeof(Math), 
			typeof(Convert)
		};
		private static readonly Expression trueLiteral = Expression.Constant(true);
		private static readonly Expression falseLiteral = Expression.Constant(false);
		private static readonly Expression nullLiteral = Expression.Constant(null);
		private static readonly string keywordIt = "it";
		private static readonly string keywordIif = "iif";
		private static readonly string keywordNew = "new";
		private static Dictionary<string, object> keywords;
		private Dictionary<string, object> symbols;
		private IDictionary<string, object> externals;
		private Dictionary<Expression, string> literals;
		private ParameterExpression it;
		private string text;
		private int textPos;
		private int textLen;
		private char ch;
		private ExpressionParser.Token token;
		public ExpressionParser(ParameterExpression[] parameters, string expression, object[] values)
		{
			if (expression == null) {
				throw new ArgumentNullException("expression");
			}
			if (ExpressionParser.keywords == null) {
				ExpressionParser.keywords = ExpressionParser.CreateKeywords();
			}
			this.symbols = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
			this.literals = new Dictionary<Expression, string>();
			if (parameters != null) {
				this.ProcessParameters(parameters);
			}
			if (values != null) {
				this.ProcessValues(values);
			}
			this.text = expression;
			this.textLen = this.text.Length;
			this.SetTextPos(0);
			this.NextToken();
		}
		private void ProcessParameters(ParameterExpression[] parameters)
		{
			for (int i = 0; i < parameters.Length; i++) {
				ParameterExpression parameterExpression = parameters[i];
				if (!string.IsNullOrEmpty(parameterExpression.Name)) {
					this.AddSymbol(parameterExpression.Name, parameterExpression);
				}
			}
			if (parameters.Length == 1 && string.IsNullOrEmpty(parameters[0].Name)) {
				this.it = parameters[0];
			}
		}
		private void ProcessValues(object[] values)
		{
			for (int i = 0; i < values.Length; i++) {
				object obj = values[i];
				if (i == values.Length - 1 && obj is IDictionary<string, object>) {
					this.externals = (IDictionary<string, object>)obj;
				} else {
					this.AddSymbol("@" + i.ToString(CultureInfo.InvariantCulture), obj);
				}
			}
		}
		private void AddSymbol(string name, object value)
		{
			if (this.symbols.ContainsKey(name)) {
				throw this.ParseError("The identifier '{0}' was defined more than once", new object[]
				{
					name
				});
			}
			this.symbols.Add(name, value);
		}
		public Expression Parse(Type resultType)
		{
			int pos = this.token.pos;
			Expression expression = this.ParseExpression();
			if (resultType != null && (expression = this.PromoteExpression(expression, resultType, true)) == null) {
				throw this.ParseError(pos, "Expression of type '{0}' expected", new object[]
				{
					ExpressionParser.GetTypeName(resultType)
				});
			}
			this.ValidateToken(ExpressionParser.TokenId.End, "Syntax error");
			return expression;
		}
		public IEnumerable<DynamicOrdering> ParseOrdering()
		{
			List<DynamicOrdering> list = new List<DynamicOrdering>();
			while (true) {
				Expression selector = this.ParseExpression();
				bool ascending = true;
				if (this.TokenIdentifierIs("asc") || this.TokenIdentifierIs("ascending")) {
					this.NextToken();
				} else {
					if (this.TokenIdentifierIs("desc") || this.TokenIdentifierIs("descending")) {
						this.NextToken();
						ascending = false;
					}
				}
				list.Add(new DynamicOrdering {
					Selector = selector,
					Ascending = ascending
				});
				if (this.token.id != ExpressionParser.TokenId.Comma) {
					break;
				}
				this.NextToken();
			}
			this.ValidateToken(ExpressionParser.TokenId.End, "Syntax error");
			return list;
		}
		private Expression ParseExpression()
		{
			int pos = this.token.pos;
			Expression expression = this.ParseLogicalOr();
			if (this.token.id == ExpressionParser.TokenId.Question) {
				this.NextToken();
				Expression expr = this.ParseExpression();
				this.ValidateToken(ExpressionParser.TokenId.Colon, "':' expected");
				this.NextToken();
				Expression expr2 = this.ParseExpression();
				expression = this.GenerateConditional(expression, expr, expr2, pos);
			}
			return expression;
		}
		private Expression ParseLogicalOr()
		{
			Expression expression = this.ParseLogicalAnd();
			while (this.token.id == ExpressionParser.TokenId.DoubleBar || this.TokenIdentifierIs("or")) {
				ExpressionParser.Token token = this.token;
				this.NextToken();
				Expression right = this.ParseLogicalAnd();
				this.CheckAndPromoteOperands(typeof(ExpressionParser.ILogicalSignatures), token.text, ref expression, ref right, token.pos);
				expression = Expression.OrElse(expression, right);
			}
			return expression;
		}
		private Expression ParseLogicalAnd()
		{
			Expression expression = this.ParseComparison();
			while (this.token.id == ExpressionParser.TokenId.DoubleAmphersand || this.TokenIdentifierIs("and")) {
				ExpressionParser.Token token = this.token;
				this.NextToken();
				Expression right = this.ParseComparison();
				this.CheckAndPromoteOperands(typeof(ExpressionParser.ILogicalSignatures), token.text, ref expression, ref right, token.pos);
				expression = Expression.AndAlso(expression, right);
			}
			return expression;
		}
		private Expression ParseComparison()
		{
			Expression expression = this.ParseAdditive();
			while (this.token.id == ExpressionParser.TokenId.Equal || this.token.id == ExpressionParser.TokenId.DoubleEqual || this.token.id == ExpressionParser.TokenId.ExclamationEqual || this.token.id == ExpressionParser.TokenId.LessGreater || this.token.id == ExpressionParser.TokenId.GreaterThan || this.token.id == ExpressionParser.TokenId.GreaterThanEqual || this.token.id == ExpressionParser.TokenId.LessThan || this.token.id == ExpressionParser.TokenId.LessThanEqual) {
				ExpressionParser.Token token = this.token;
				this.NextToken();
				Expression expression2 = this.ParseAdditive();
				bool flag = token.id == ExpressionParser.TokenId.Equal || token.id == ExpressionParser.TokenId.DoubleEqual || token.id == ExpressionParser.TokenId.ExclamationEqual || token.id == ExpressionParser.TokenId.LessGreater;
				if (flag && !expression.Type.IsValueType && !expression2.Type.IsValueType) {
					if (expression.Type != expression2.Type) {
						if (expression.Type.IsAssignableFrom(expression2.Type)) {
							expression2 = Expression.Convert(expression2, expression.Type);
						} else {
							if (!expression2.Type.IsAssignableFrom(expression.Type)) {
								throw this.IncompatibleOperandsError(token.text, expression, expression2, token.pos);
							}
							expression = Expression.Convert(expression, expression2.Type);
						}
					}
				} else {
					if (ExpressionParser.IsEnumType(expression.Type) || ExpressionParser.IsEnumType(expression2.Type)) {
						if (expression.Type != expression2.Type) {
							Expression expression3;
							if ((expression3 = this.PromoteExpression(expression2, expression.Type, true)) != null) {
								expression2 = expression3;
							} else {
								if ((expression3 = this.PromoteExpression(expression, expression2.Type, true)) == null) {
									throw this.IncompatibleOperandsError(token.text, expression, expression2, token.pos);
								}
								expression = expression3;
							}
						}
					} else {
						this.CheckAndPromoteOperands(flag ? typeof(ExpressionParser.IEqualitySignatures) : typeof(ExpressionParser.IRelationalSignatures), token.text, ref expression, ref expression2, token.pos);
					}
				}
				switch (token.id) {
					case ExpressionParser.TokenId.LessThan: {
							expression = this.GenerateLessThan(expression, expression2);
							break;
						}
					case ExpressionParser.TokenId.Equal:
					case ExpressionParser.TokenId.DoubleEqual: {
							expression = this.GenerateEqual(expression, expression2);
							break;
						}
					case ExpressionParser.TokenId.GreaterThan: {
							expression = this.GenerateGreaterThan(expression, expression2);
							break;
						}
					case ExpressionParser.TokenId.ExclamationEqual:
					case ExpressionParser.TokenId.LessGreater: {
							expression = this.GenerateNotEqual(expression, expression2);
							break;
						}
					case ExpressionParser.TokenId.LessThanEqual: {
							expression = this.GenerateLessThanEqual(expression, expression2);
							break;
						}
					case ExpressionParser.TokenId.GreaterThanEqual: {
							expression = this.GenerateGreaterThanEqual(expression, expression2);
							break;
						}
				}
			}
			return expression;
		}
		private Expression ParseAdditive()
		{
			Expression expression = this.ParseMultiplicative();
			while (this.token.id == ExpressionParser.TokenId.Plus || this.token.id == ExpressionParser.TokenId.Minus || this.token.id == ExpressionParser.TokenId.Amphersand) {
				ExpressionParser.Token token = this.token;
				this.NextToken();
				Expression expression2 = this.ParseMultiplicative();
				ExpressionParser.TokenId id = token.id;
				if (id != ExpressionParser.TokenId.Amphersand) {
					switch (id) {
						case ExpressionParser.TokenId.Plus: {
								if (expression.Type != typeof(string) && expression2.Type != typeof(string)) {
									this.CheckAndPromoteOperands(typeof(ExpressionParser.IAddSignatures), token.text, ref expression, ref expression2, token.pos);
									expression = this.GenerateAdd(expression, expression2);
									continue;
								}
								break;
							}
						case ExpressionParser.TokenId.Comma: {
								continue;
							}
						case ExpressionParser.TokenId.Minus: {
								this.CheckAndPromoteOperands(typeof(ExpressionParser.ISubtractSignatures), token.text, ref expression, ref expression2, token.pos);
								expression = this.GenerateSubtract(expression, expression2);
								continue;
							}
						default: {
								continue;
							}
					}
				}
				expression = this.GenerateStringConcat(expression, expression2);
			}
			return expression;
		}
		private Expression ParseMultiplicative()
		{
			Expression expression = this.ParseUnary();
			while (this.token.id == ExpressionParser.TokenId.Asterisk || this.token.id == ExpressionParser.TokenId.Slash || this.token.id == ExpressionParser.TokenId.Percent || this.TokenIdentifierIs("mod")) {
				ExpressionParser.Token token = this.token;
				this.NextToken();
				Expression right = this.ParseUnary();
				this.CheckAndPromoteOperands(typeof(ExpressionParser.IArithmeticSignatures), token.text, ref expression, ref right, token.pos);
				ExpressionParser.TokenId id = token.id;
				if (id <= ExpressionParser.TokenId.Percent) {
					if (id == ExpressionParser.TokenId.Identifier || id == ExpressionParser.TokenId.Percent) {
						expression = Expression.Modulo(expression, right);
					}
				} else {
					if (id != ExpressionParser.TokenId.Asterisk) {
						if (id == ExpressionParser.TokenId.Slash) {
							expression = Expression.Divide(expression, right);
						}
					} else {
						expression = Expression.Multiply(expression, right);
					}
				}
			}
			return expression;
		}
		private Expression ParseUnary()
		{
			if (this.token.id != ExpressionParser.TokenId.Minus && this.token.id != ExpressionParser.TokenId.Exclamation && !this.TokenIdentifierIs("not")) {
				return this.ParsePrimary();
			}
			ExpressionParser.Token token = this.token;
			this.NextToken();
			if (token.id == ExpressionParser.TokenId.Minus && (this.token.id == ExpressionParser.TokenId.IntegerLiteral || this.token.id == ExpressionParser.TokenId.RealLiteral)) {
				this.token.text = "-" + this.token.text;
				this.token.pos = token.pos;
				return this.ParsePrimary();
			}
			Expression expression = this.ParseUnary();
			if (token.id == ExpressionParser.TokenId.Minus) {
				this.CheckAndPromoteOperand(typeof(ExpressionParser.INegationSignatures), token.text, ref expression, token.pos);
				expression = Expression.Negate(expression);
			} else {
				this.CheckAndPromoteOperand(typeof(ExpressionParser.INotSignatures), token.text, ref expression, token.pos);
				expression = Expression.Not(expression);
			}
			return expression;
		}
		private Expression ParsePrimary()
		{
			Expression expression = this.ParsePrimaryStart();
			while (true) {
				if (this.token.id == ExpressionParser.TokenId.Dot) {
					this.NextToken();
					expression = this.ParseMemberAccess(null, expression);
				} else {
					if (this.token.id != ExpressionParser.TokenId.OpenBracket) {
						break;
					}
					expression = this.ParseElementAccess(expression);
				}
			}
			return expression;
		}
		private Expression ParsePrimaryStart()
		{
			switch (this.token.id) {
				case ExpressionParser.TokenId.Identifier: {
						return this.ParseIdentifier();
					}
				case ExpressionParser.TokenId.StringLiteral: {
						return this.ParseStringLiteral();
					}
				case ExpressionParser.TokenId.IntegerLiteral: {
						return this.ParseIntegerLiteral();
					}
				case ExpressionParser.TokenId.RealLiteral: {
						return this.ParseRealLiteral();
					}
				case ExpressionParser.TokenId.OpenParen: {
						return this.ParseParenExpression();
					}
			}
			throw this.ParseError("Expression expected", new object[0]);
		}
		private Expression ParseStringLiteral()
		{
			this.ValidateToken(ExpressionParser.TokenId.StringLiteral);
			char c = this.token.text[0];
			string text = this.token.text.Substring(1, this.token.text.Length - 2);
			int startIndex = 0;
			while (true) {
				int num = text.IndexOf(c, startIndex);
				if (num < 0) {
					break;
				}
				text = text.Remove(num, 1);
				startIndex = num + 1;
			}
			if (c != '\'') {
				this.NextToken();
				return this.CreateLiteral(text, text);
			}
			if (text.Length != 1) {
				throw this.ParseError("Character literal must contain exactly one character", new object[0]);
			}
			this.NextToken();
			return this.CreateLiteral(text[0], text);
		}
		private Expression ParseIntegerLiteral()
		{
			ValidateToken(TokenId.IntegerLiteral);
			string text = token.text;
			if (text[0] != '-') {
				ulong value;
				if (!UInt64.TryParse(text, out value))
					throw ParseError(Res.InvalidIntegerLiteral, text);
				NextToken();
				if (value <= (ulong)Int32.MaxValue) return CreateLiteral((int)value, text);
				if (value <= (ulong)UInt32.MaxValue) return CreateLiteral((uint)value, text);
				if (value <= (ulong)Int64.MaxValue) return CreateLiteral((long)value, text);
				return CreateLiteral(value, text);
			} else {
				long value;
				if (!Int64.TryParse(text, out value))
					throw ParseError(Res.InvalidIntegerLiteral, text);
				NextToken();
				if (value >= Int32.MinValue && value <= Int32.MaxValue)
					return CreateLiteral((int)value, text);
				return CreateLiteral(value, text);
			}
		}

		private Expression ParseRealLiteral()
		{
			ValidateToken(TokenId.RealLiteral);
			string text = token.text;
			object value = null;
			char last = text[text.Length - 1];
			if (last == 'F' || last == 'f') {
				float f;
				if (Single.TryParse(text.Substring(0, text.Length - 1), out f)) value = f;
			} else {
				double d;
				if (Double.TryParse(text, out d)) value = d;
			}
			if (value == null) throw ParseError(Res.InvalidRealLiteral, text);
			NextToken();
			return CreateLiteral(value, text);
		}

		private Expression CreateLiteral(object value, string text)
		{
			ConstantExpression constantExpression = Expression.Constant(value);
			this.literals.Add(constantExpression, text);
			return constantExpression;
		}
		private Expression ParseParenExpression()
		{
			this.ValidateToken(ExpressionParser.TokenId.OpenParen, "'(' expected");
			this.NextToken();
			Expression result = this.ParseExpression();
			this.ValidateToken(ExpressionParser.TokenId.CloseParen, "')' or operator expected");
			this.NextToken();
			return result;
		}
		private Expression ParseIdentifier()
		{
			this.ValidateToken(ExpressionParser.TokenId.Identifier);
			object obj;
			if (ExpressionParser.keywords.TryGetValue(this.token.text, out obj)) {
				if (obj is Type) {
					return this.ParseTypeAccess((Type)obj);
				}
				if (obj == ExpressionParser.keywordIt) {
					return this.ParseIt();
				}
				if (obj == ExpressionParser.keywordIif) {
					return this.ParseIif();
				}
				if (obj == ExpressionParser.keywordNew) {
					return this.ParseNew();
				}
				this.NextToken();
				return (Expression)obj;
			} else {
				if (this.symbols.TryGetValue(this.token.text, out obj) || (this.externals != null && this.externals.TryGetValue(this.token.text, out obj))) {
					Expression expression = obj as Expression;
					if (expression == null) {
						expression = Expression.Constant(obj);
					} else {
						LambdaExpression lambdaExpression = expression as LambdaExpression;
						if (lambdaExpression != null) {
							return this.ParseLambdaInvocation(lambdaExpression);
						}
					}
					this.NextToken();
					return expression;
				}
				if (this.ParseEnumType(out obj)) {
					Expression result = Expression.Constant(obj);
					this.NextToken();
					return result;
				}
				if (this.it != null) {
					return this.ParseMemberAccess(null, this.it);
				}
				throw this.ParseError("Unknown identifier '{0}'", new object[]
				{
					this.token.text
				});
			}
		}
		private bool ParseEnumType(out object value)
		{
			value = null;
			this.ValidateToken(ExpressionParser.TokenId.Identifier);
			Type type = null;
			int pos = this.token.pos;
			string text = this.token.text;
			while (type == null) {
				type = Type.GetType(text, false, true);
				if (type == null) {
					this.NextToken();
					if (this.token.id != ExpressionParser.TokenId.Dot) {
						break;
					}
					text += this.token.text;
					this.NextToken();
					if (this.token.id != ExpressionParser.TokenId.Identifier) {
						break;
					}
					text += this.token.text;
				}
			}
			if (type != null && ExpressionParser.IsEnumType(type)) {
				this.NextToken();
				this.ValidateToken(ExpressionParser.TokenId.Dot, "'.' expected");
				this.NextToken();
				this.ValidateToken(ExpressionParser.TokenId.Identifier, "Identifier expected");
				value = Enum.Parse(type, this.token.text, true);
				return true;
			}
			this.SetTextPos(pos);
			this.NextToken();
			return false;
		}
		private Expression ParseIt()
		{
			if (this.it == null) {
				throw this.ParseError("No 'it' is in scope", new object[0]);
			}
			this.NextToken();
			return this.it;
		}
		private Expression ParseIif()
		{
			int pos = this.token.pos;
			this.NextToken();
			Expression[] array = this.ParseArgumentList();
			if (array.Length != 3) {
				throw this.ParseError(pos, "The 'iif' function requires three arguments", new object[0]);
			}
			return this.GenerateConditional(array[0], array[1], array[2], pos);
		}
		private Expression GenerateConditional(Expression test, Expression expr1, Expression expr2, int errorPos)
		{
			if (test.Type != typeof(bool)) {
				throw this.ParseError(errorPos, "The first expression must be of type 'Boolean'", new object[0]);
			}
			if (expr1.Type != expr2.Type) {
				Expression expression = (expr2 != ExpressionParser.nullLiteral) ? this.PromoteExpression(expr1, expr2.Type, true) : null;
				Expression expression2 = (expr1 != ExpressionParser.nullLiteral) ? this.PromoteExpression(expr2, expr1.Type, true) : null;
				if (expression != null && expression2 == null) {
					expr1 = expression;
				} else {
					if (expression2 != null && expression == null) {
						expr2 = expression2;
					} else {
						string text = (expr1 != ExpressionParser.nullLiteral) ? expr1.Type.Name : "null";
						string text2 = (expr2 != ExpressionParser.nullLiteral) ? expr2.Type.Name : "null";
						if (expression != null && expression2 != null) {
							throw this.ParseError(errorPos, "Both of the types '{0}' and '{1}' convert to the other", new object[]
							{
								text, 
								text2
							});
						}
						throw this.ParseError(errorPos, "Neither of the types '{0}' and '{1}' converts to the other", new object[]
						{
							text, 
							text2
						});
					}
				}
			}
			return Expression.Condition(test, expr1, expr2);
		}
		private Expression ParseNew()
		{
			this.NextToken();
			this.ValidateToken(ExpressionParser.TokenId.OpenParen, "'(' expected");
			this.NextToken();
			List<DynamicProperty> list = new List<DynamicProperty>();
			List<Expression> list2 = new List<Expression>();
			int pos;
			while (true) {
				pos = this.token.pos;
				Expression expression = this.ParseExpression();
				string name;
				if (this.TokenIdentifierIs("as")) {
					this.NextToken();
					name = this.GetIdentifier();
					this.NextToken();
				} else {
					MemberExpression memberExpression = expression as MemberExpression;
					if (memberExpression == null) {
						break;
					}
					name = memberExpression.Member.Name;
				}
				list2.Add(expression);
				list.Add(new DynamicProperty(name, expression.Type));
				if (this.token.id != ExpressionParser.TokenId.Comma) {
					goto IL_BC;
				}
				this.NextToken();
			}
			throw this.ParseError(pos, "Expression is missing an 'as' clause", new object[0]);
		IL_BC:
			this.ValidateToken(ExpressionParser.TokenId.CloseParen, "')' or ',' expected");
			this.NextToken();
			Type type = DynamicExpression.CreateClass(list);
			MemberBinding[] array = new MemberBinding[list.Count];
			for (int i = 0; i < array.Length; i++) {
				array[i] = Expression.Bind(type.GetProperty(list[i].Name), list2[i]);
			}
			return Expression.MemberInit(Expression.New(type), array);
		}
		private Expression ParseLambdaInvocation(LambdaExpression lambda)
		{
			int pos = this.token.pos;
			this.NextToken();
			Expression[] array = this.ParseArgumentList();
			MethodBase methodBase;
			if (this.FindMethod(lambda.Type, "Invoke", false, array, out methodBase) != 1) {
				throw this.ParseError(pos, "Argument list incompatible with lambda expression", new object[0]);
			}
			return Expression.Invoke(lambda, array);
		}
		private Expression ParseTypeAccess(Type type)
		{
			int pos = this.token.pos;
			this.NextToken();
			if (this.token.id == ExpressionParser.TokenId.Question) {
				if (!type.IsValueType || ExpressionParser.IsNullableType(type)) {
					throw this.ParseError(pos, "Type '{0}' has no nullable form", new object[]
					{
						ExpressionParser.GetTypeName(type)
					});
				}
				type = typeof(Nullable<>).MakeGenericType(new Type[]
				{
					type
				});
				this.NextToken();
			}
			if (this.token.id != ExpressionParser.TokenId.OpenParen) {
				this.ValidateToken(ExpressionParser.TokenId.Dot, "'.' or '(' expected");
				this.NextToken();
				return this.ParseMemberAccess(type, null);
			}
			Expression[] array = this.ParseArgumentList();
			MethodBase methodBase;
			switch (this.FindBestMethod(type.GetConstructors(), array, out methodBase)) {
				case 0: {
						if (array.Length == 1) {
							return this.GenerateConversion(array[0], type, pos);
						}
						throw this.ParseError(pos, "No matching constructor in type '{0}'", new object[]
					{
						ExpressionParser.GetTypeName(type)
					});
					}
				case 1: {
						return Expression.New((ConstructorInfo)methodBase, array);
					}
				default: {
						throw this.ParseError(pos, "Ambiguous invocation of '{0}' constructor", new object[]
					{
						ExpressionParser.GetTypeName(type)
					});
					}
			}
		}
		private Expression GenerateConversion(Expression expr, Type type, int errorPos)
		{
			Type type2 = expr.Type;
			if (type2 == type) {
				return expr;
			}
			if (type2.IsValueType && type.IsValueType) {
				if ((ExpressionParser.IsNullableType(type2) || ExpressionParser.IsNullableType(type)) && ExpressionParser.GetNonNullableType(type2) == ExpressionParser.GetNonNullableType(type)) {
					return Expression.Convert(expr, type);
				}
				if (((ExpressionParser.IsNumericType(type2) || ExpressionParser.IsEnumType(type2)) && ExpressionParser.IsNumericType(type)) || ExpressionParser.IsEnumType(type)) {
					return Expression.ConvertChecked(expr, type);
				}
			}
			if (type2.IsAssignableFrom(type) || type.IsAssignableFrom(type2) || type2.IsInterface || type.IsInterface) {
				return Expression.Convert(expr, type);
			}
			throw this.ParseError(errorPos, "A value of type '{0}' cannot be converted to type '{1}'", new object[]
			{
				ExpressionParser.GetTypeName(type2), 
				ExpressionParser.GetTypeName(type)
			});
		}
		private Expression ParseMemberAccess(Type type, Expression instance)
		{
			if (instance != null) {
				type = instance.Type;
			}
			int pos = this.token.pos;
			string identifier = this.GetIdentifier();
			this.NextToken();
			if (this.token.id == ExpressionParser.TokenId.OpenParen) {
				if (instance != null && type != typeof(string)) {
					Type type2 = ExpressionParser.FindGenericType(typeof(IEnumerable<>), type);
					if (type2 != null) {
						Type elementType = type2.GetGenericArguments()[0];
						return this.ParseAggregate(instance, elementType, identifier, pos);
					}
				}
				Expression[] array = this.ParseArgumentList();
				MethodBase methodBase;
				switch (this.FindMethod(type, identifier, instance == null, array, out methodBase)) {
					case 0: {
							throw this.ParseError(pos, "No applicable method '{0}' exists in type '{1}'", new object[]
						{
							identifier, 
							ExpressionParser.GetTypeName(type)
						});
						}
					case 1: {
							MethodInfo methodInfo = (MethodInfo)methodBase;
							if (!ExpressionParser.IsPredefinedType(methodInfo.DeclaringType)) {
								throw this.ParseError(pos, "Methods on type '{0}' are not accessible", new object[]
							{
								ExpressionParser.GetTypeName(methodInfo.DeclaringType)
							});
							}
							if (methodInfo.ReturnType == typeof(void)) {
								throw this.ParseError(pos, "Method '{0}' in type '{1}' does not return a value", new object[]
							{
								identifier, 
								ExpressionParser.GetTypeName(methodInfo.DeclaringType)
							});
							}
							return Expression.Call(instance, methodInfo, array);
						}
					default: {
							throw this.ParseError(pos, "Ambiguous invocation of method '{0}' in type '{1}'", new object[]
						{
							identifier, 
							ExpressionParser.GetTypeName(type)
						});
						}
				}
			} else {
				MemberInfo memberInfo = this.FindPropertyOrField(type, identifier, instance == null);
				if (memberInfo == null) {
					throw this.ParseError(pos, "No property or field '{0}' exists in type '{1}'", new object[]
					{
						identifier, 
						ExpressionParser.GetTypeName(type)
					});
				}
				if (!(memberInfo is PropertyInfo)) {
					return Expression.Field(instance, (FieldInfo)memberInfo);
				}
				return Expression.Property(instance, (PropertyInfo)memberInfo);
			}
		}
		private static Type FindGenericType(Type generic, Type type)
		{
			while (type != null && type != typeof(object)) {
				if (type.IsGenericType && type.GetGenericTypeDefinition() == generic) {
					return type;
				}
				if (generic.IsInterface) {
					Type[] interfaces = type.GetInterfaces();
					for (int i = 0; i < interfaces.Length; i++) {
						Type type2 = interfaces[i];
						Type type3 = ExpressionParser.FindGenericType(generic, type2);
						if (type3 != null) {
							return type3;
						}
					}
				}
				type = type.BaseType;
			}
			return null;
		}
		private Expression ParseAggregate(Expression instance, Type elementType, string methodName, int errorPos)
		{
			ParameterExpression parameterExpression = this.it;
			ParameterExpression parameterExpression2 = Expression.Parameter(elementType, "");
			this.it = parameterExpression2;
			Expression[] array = this.ParseArgumentList();
			this.it = parameterExpression;
			MethodBase methodBase;
			if (this.FindMethod(typeof(ExpressionParser.IEnumerableSignatures), methodName, false, array, out methodBase) != 1) {
				throw this.ParseError(errorPos, "No applicable aggregate method '{0}' exists", new object[]
				{
					methodName
				});
			}
			Type[] typeArguments;
			if (methodBase.Name == "Min" || methodBase.Name == "Max") {
				typeArguments = new Type[]
				{
					elementType, 
					array[0].Type
				};
			} else {
				typeArguments = new Type[]
				{
					elementType
				};
			}
			if (array.Length == 0) {
				array = new Expression[]
				{
					instance
				};
			} else {
				array = new Expression[]
				{
					instance, 
					Expression.Lambda(array[0], new ParameterExpression[]
					{
						parameterExpression2
					})
				};
			}
			return Expression.Call(typeof(Enumerable), methodBase.Name, typeArguments, array);
		}
		private Expression[] ParseArgumentList()
		{
			this.ValidateToken(ExpressionParser.TokenId.OpenParen, "'(' expected");
			this.NextToken();
			Expression[] result = (this.token.id != ExpressionParser.TokenId.CloseParen) ? this.ParseArguments() : new Expression[0];
			this.ValidateToken(ExpressionParser.TokenId.CloseParen, "')' or ',' expected");
			this.NextToken();
			return result;
		}
		private Expression[] ParseArguments()
		{
			List<Expression> list = new List<Expression>();
			while (true) {
				list.Add(this.ParseExpression());
				if (this.token.id != ExpressionParser.TokenId.Comma) {
					break;
				}
				this.NextToken();
			}
			return list.ToArray();
		}
		private Expression ParseElementAccess(Expression expr)
		{
			int pos = this.token.pos;
			this.ValidateToken(ExpressionParser.TokenId.OpenBracket, "'(' expected");
			this.NextToken();
			Expression[] array = this.ParseArguments();
			this.ValidateToken(ExpressionParser.TokenId.CloseBracket, "']' or ',' expected");
			this.NextToken();
			if (expr.Type.IsArray) {
				if (expr.Type.GetArrayRank() != 1 || array.Length != 1) {
					throw this.ParseError(pos, "Indexing of multi-dimensional arrays is not supported", new object[0]);
				}
				Expression expression = this.PromoteExpression(array[0], typeof(int), true);
				if (expression == null) {
					throw this.ParseError(pos, "Array index must be an integer expression", new object[0]);
				}
				return Expression.ArrayIndex(expr, expression);
			} else {
				MethodBase methodBase;
				switch (this.FindIndexer(expr.Type, array, out methodBase)) {
					case 0: {
							throw this.ParseError(pos, "No applicable indexer exists in type '{0}'", new object[]
						{
							ExpressionParser.GetTypeName(expr.Type)
						});
						}
					case 1: {
							return Expression.Call(expr, (MethodInfo)methodBase, array);
						}
					default: {
							throw this.ParseError(pos, "Ambiguous invocation of indexer in type '{0}'", new object[]
						{
							ExpressionParser.GetTypeName(expr.Type)
						});
						}
				}
			}
		}
		private static bool IsPredefinedType(Type type)
		{
			Type[] array = ExpressionParser.predefinedTypes;
			for (int i = 0; i < array.Length; i++) {
				Type type2 = array[i];
				if (type2 == type) {
					return true;
				}
			}
			return false;
		}
		private static bool IsNullableType(Type type)
		{
			return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
		}
		private static Type GetNonNullableType(Type type)
		{
			if (!ExpressionParser.IsNullableType(type)) {
				return type;
			}
			return type.GetGenericArguments()[0];
		}
		private static string GetTypeName(Type type)
		{
			Type nonNullableType = ExpressionParser.GetNonNullableType(type);
			string text = nonNullableType.Name;
			if (type != nonNullableType) {
				text += '?';
			}
			return text;
		}
		private static bool IsNumericType(Type type)
		{
			return ExpressionParser.GetNumericTypeKind(type) != 0;
		}
		private static bool IsSignedIntegralType(Type type)
		{
			return ExpressionParser.GetNumericTypeKind(type) == 2;
		}
		private static bool IsUnsignedIntegralType(Type type)
		{
			return ExpressionParser.GetNumericTypeKind(type) == 3;
		}
		private static int GetNumericTypeKind(Type type)
		{
			type = ExpressionParser.GetNonNullableType(type);
			if (type.IsEnum) {
				return 0;
			}
			switch (Type.GetTypeCode(type)) {
				case TypeCode.Char:
				case TypeCode.Single:
				case TypeCode.Double:
				case TypeCode.Decimal: {
						return 1;
					}
				case TypeCode.SByte:
				case TypeCode.Int16:
				case TypeCode.Int32:
				case TypeCode.Int64: {
						return 2;
					}
				case TypeCode.Byte:
				case TypeCode.UInt16:
				case TypeCode.UInt32:
				case TypeCode.UInt64: {
						return 3;
					}
				default: {
						return 0;
					}
			}
		}
		private static bool IsEnumType(Type type)
		{
			return ExpressionParser.GetNonNullableType(type).IsEnum;
		}
		private void CheckAndPromoteOperand(Type signatures, string opName, ref Expression expr, int errorPos)
		{
			Expression[] array = new Expression[]
			{
				expr
			};
			MethodBase methodBase;
			if (this.FindMethod(signatures, "F", false, array, out methodBase) != 1) {
				throw this.ParseError(errorPos, "Operator '{0}' incompatible with operand type '{1}'", new object[]
				{
					opName, 
					ExpressionParser.GetTypeName(array[0].Type)
				});
			}
			expr = array[0];
		}
		private void CheckAndPromoteOperands(Type signatures, string opName, ref Expression left, ref Expression right, int errorPos)
		{
			Expression[] array = new Expression[]
			{
				left, 
				right
			};
			MethodBase methodBase;
			if (this.FindMethod(signatures, "F", false, array, out methodBase) != 1) {
				throw this.IncompatibleOperandsError(opName, left, right, errorPos);
			}
			left = array[0];
			right = array[1];
		}
		private Exception IncompatibleOperandsError(string opName, Expression left, Expression right, int pos)
		{
			return this.ParseError(pos, "Operator '{0}' incompatible with operand types '{1}' and '{2}'", new object[]
			{
				opName, 
				ExpressionParser.GetTypeName(left.Type), 
				ExpressionParser.GetTypeName(right.Type)
			});
		}
		private MemberInfo FindPropertyOrField(Type type, string memberName, bool staticAccess)
		{
			BindingFlags bindingAttr = BindingFlags.DeclaredOnly | BindingFlags.Public | (staticAccess ? BindingFlags.Static : BindingFlags.Instance);
			foreach (Type current in ExpressionParser.SelfAndBaseTypes(type)) {
				MemberInfo[] array = current.FindMembers(MemberTypes.Field | MemberTypes.Property, bindingAttr, Type.FilterNameIgnoreCase, memberName);
				if (array.Length != 0) {
					return array[0];
				}
			}
			return null;
		}
		private int FindMethod(Type type, string methodName, bool staticAccess, Expression[] args, out MethodBase method)
		{
			BindingFlags bindingAttr = BindingFlags.DeclaredOnly | BindingFlags.Public | (staticAccess ? BindingFlags.Static : BindingFlags.Instance);
			foreach (Type current in ExpressionParser.SelfAndBaseTypes(type)) {
				MemberInfo[] source = current.FindMembers(MemberTypes.Method, bindingAttr, Type.FilterNameIgnoreCase, methodName);
				int num = this.FindBestMethod(source.Cast<MethodBase>(), args, out method);
				if (num != 0) {
					return num;
				}
			}
			method = null;
			return 0;
		}
		private int FindIndexer(Type type, Expression[] args, out MethodBase method)
		{
			foreach (Type current in ExpressionParser.SelfAndBaseTypes(type)) {
				MemberInfo[] defaultMembers = current.GetDefaultMembers();
				if (defaultMembers.Length != 0) {
					IEnumerable<MethodBase> methods =
						from p in defaultMembers.OfType<PropertyInfo>()
						select p.GetGetMethod() into m
						where m != null
						select m;
					int num = this.FindBestMethod(methods, args, out method);
					if (num != 0) {
						return num;
					}
				}
			}
			method = null;
			return 0;
		}
		private static IEnumerable<Type> SelfAndBaseTypes(Type type)
		{
			if (type.IsInterface) {
				List<Type> list = new List<Type>();
				ExpressionParser.AddInterface(list, type);
				return list;
			}
			return ExpressionParser.SelfAndBaseClasses(type);
		}
		private static IEnumerable<Type> SelfAndBaseClasses(Type type)
		{
			while (type != null) {
				yield return type;
				type = type.BaseType;
			}
			yield break;
		}
		private static void AddInterface(List<Type> types, Type type)
		{
			if (!types.Contains(type)) {
				types.Add(type);
				Type[] interfaces = type.GetInterfaces();
				for (int i = 0; i < interfaces.Length; i++) {
					Type type2 = interfaces[i];
					ExpressionParser.AddInterface(types, type2);
				}
			}
		}
		private int FindBestMethod(IEnumerable<MethodBase> methods, Expression[] args, out MethodBase method)
		{
			ExpressionParser.MethodData[] applicable = (
				from m in methods
				select new ExpressionParser.MethodData {
					MethodBase = m,
					Parameters = m.GetParameters()
				} into m
				where this.IsApplicable(m, args)
				select m).ToArray<ExpressionParser.MethodData>();
			if (applicable.Length > 1) {
				applicable = (
					from m in applicable
					where applicable.All((ExpressionParser.MethodData n) => m == n || ExpressionParser.IsBetterThan(args, m, n))
					select m).ToArray<ExpressionParser.MethodData>();
			}
			if (applicable.Length == 1) {
				ExpressionParser.MethodData methodData = applicable[0];
				for (int i = 0; i < args.Length; i++) {
					args[i] = methodData.Args[i];
				}
				method = methodData.MethodBase;
			} else {
				method = null;
			}
			return applicable.Length;
		}
		private bool IsApplicable(ExpressionParser.MethodData method, Expression[] args)
		{
			if (method.Parameters.Length != args.Length) {
				return false;
			}
			Expression[] array = new Expression[args.Length];
			for (int i = 0; i < args.Length; i++) {
				ParameterInfo parameterInfo = method.Parameters[i];
				if (parameterInfo.IsOut) {
					return false;
				}
				Expression expression = this.PromoteExpression(args[i], parameterInfo.ParameterType, false);
				if (expression == null) {
					return false;
				}
				array[i] = expression;
			}
			method.Args = array;
			return true;
		}
		private Expression PromoteExpression(Expression expr, Type type, bool exact)
		{
			if (expr.Type == type) {
				return expr;
			}
			if (expr is ConstantExpression) {
				ConstantExpression constantExpression = (ConstantExpression)expr;
				if (constantExpression == ExpressionParser.nullLiteral) {
					if (!type.IsValueType || ExpressionParser.IsNullableType(type)) {
						return Expression.Constant(null, type);
					}
				} else {
					string name;
					if (this.literals.TryGetValue(constantExpression, out name)) {
						Type nonNullableType = ExpressionParser.GetNonNullableType(type);
						object obj = null;
						switch (Type.GetTypeCode(constantExpression.Type)) {
							case TypeCode.Int32:
							case TypeCode.UInt32:
							case TypeCode.Int64:
							case TypeCode.UInt64: {
									obj = ExpressionParser.ParseNumber(name, nonNullableType);
									break;
								}
							case TypeCode.Double: {
									if (nonNullableType == typeof(decimal)) {
										obj = ExpressionParser.ParseNumber(name, nonNullableType);
									}
									break;
								}
							case TypeCode.String: {
									obj = ExpressionParser.ParseEnum(name, nonNullableType);
									break;
								}
						}
						if (obj != null) {
							return Expression.Constant(obj, type);
						}
					}
				}
			}
			if (!ExpressionParser.IsCompatibleWith(expr.Type, type)) {
				return null;
			}
			if (type.IsValueType || exact) {
				return Expression.Convert(expr, type);
			}
			return expr;
		}
		private static object ParseNumber(string text, Type type)
		{
			switch (Type.GetTypeCode(ExpressionParser.GetNonNullableType(type))) {
				case TypeCode.SByte: {
						sbyte b;
						if (sbyte.TryParse(text, out b)) {
							return b;
						}
						break;
					}
				case TypeCode.Byte: {
						byte b2;
						if (byte.TryParse(text, out b2)) {
							return b2;
						}
						break;
					}
				case TypeCode.Int16: {
						short num;
						if (short.TryParse(text, out num)) {
							return num;
						}
						break;
					}
				case TypeCode.UInt16: {
						ushort num2;
						if (ushort.TryParse(text, out num2)) {
							return num2;
						}
						break;
					}
				case TypeCode.Int32: {
						int num3;
						if (int.TryParse(text, out num3)) {
							return num3;
						}
						break;
					}
				case TypeCode.UInt32: {
						uint num4;
						if (uint.TryParse(text, out num4)) {
							return num4;
						}
						break;
					}
				case TypeCode.Int64: {
						long num5;
						if (long.TryParse(text, out num5)) {
							return num5;
						}
						break;
					}
				case TypeCode.UInt64: {
						ulong num6;
						if (ulong.TryParse(text, out num6)) {
							return num6;
						}
						break;
					}
				case TypeCode.Single: {
						float num7;
						if (float.TryParse(text, out num7)) {
							return num7;
						}
						break;
					}
				case TypeCode.Double: {
						double num8;
						if (double.TryParse(text, out num8)) {
							return num8;
						}
						break;
					}
				case TypeCode.Decimal: {
						decimal num9;
						if (decimal.TryParse(text, out num9)) {
							return num9;
						}
						break;
					}
			}
			return null;
		}
		private static object ParseEnum(string name, Type type)
		{
			if (type.IsEnum) {
				MemberInfo[] array = type.FindMembers(MemberTypes.Field, BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.Public, Type.FilterNameIgnoreCase, name);
				if (array.Length != 0) {
					return ((FieldInfo)array[0]).GetValue(null);
				}
			}
			return null;
		}
		private static bool IsCompatibleWith(Type source, Type target)
		{
			if (source == target) {
				return true;
			}
			if (!target.IsValueType) {
				return target.IsAssignableFrom(source);
			}
			Type nonNullableType = ExpressionParser.GetNonNullableType(source);
			Type nonNullableType2 = ExpressionParser.GetNonNullableType(target);
			if (nonNullableType != source && nonNullableType2 == target) {
				return false;
			}
			TypeCode typeCode = nonNullableType.IsEnum ? TypeCode.Object : Type.GetTypeCode(nonNullableType);
			TypeCode typeCode2 = nonNullableType2.IsEnum ? TypeCode.Object : Type.GetTypeCode(nonNullableType2);
			switch (typeCode) {
				case TypeCode.SByte: {
						switch (typeCode2) {
							case TypeCode.SByte:
							case TypeCode.Int16:
							case TypeCode.Int32:
							case TypeCode.Int64:
							case TypeCode.Single:
							case TypeCode.Double:
							case TypeCode.Decimal: {
									return true;
								}
						}
						break;
					}
				case TypeCode.Byte: {
						switch (typeCode2) {
							case TypeCode.Byte:
							case TypeCode.Int16:
							case TypeCode.UInt16:
							case TypeCode.Int32:
							case TypeCode.UInt32:
							case TypeCode.Int64:
							case TypeCode.UInt64:
							case TypeCode.Single:
							case TypeCode.Double:
							case TypeCode.Decimal: {
									return true;
								}
						}
						break;
					}
				case TypeCode.Int16: {
						switch (typeCode2) {
							case TypeCode.Int16:
							case TypeCode.Int32:
							case TypeCode.Int64:
							case TypeCode.Single:
							case TypeCode.Double:
							case TypeCode.Decimal: {
									return true;
								}
						}
						break;
					}
				case TypeCode.UInt16: {
						switch (typeCode2) {
							case TypeCode.UInt16:
							case TypeCode.Int32:
							case TypeCode.UInt32:
							case TypeCode.Int64:
							case TypeCode.UInt64:
							case TypeCode.Single:
							case TypeCode.Double:
							case TypeCode.Decimal: {
									return true;
								}
						}
						break;
					}
				case TypeCode.Int32: {
						switch (typeCode2) {
							case TypeCode.Int32:
							case TypeCode.Int64:
							case TypeCode.Single:
							case TypeCode.Double:
							case TypeCode.Decimal: {
									return true;
								}
						}
						break;
					}
				case TypeCode.UInt32: {
						switch (typeCode2) {
							case TypeCode.UInt32:
							case TypeCode.Int64:
							case TypeCode.UInt64:
							case TypeCode.Single:
							case TypeCode.Double:
							case TypeCode.Decimal: {
									return true;
								}
						}
						break;
					}
				case TypeCode.Int64: {
						switch (typeCode2) {
							case TypeCode.Int64:
							case TypeCode.Single:
							case TypeCode.Double:
							case TypeCode.Decimal: {
									return true;
								}
						}
						break;
					}
				case TypeCode.UInt64: {
						switch (typeCode2) {
							case TypeCode.UInt64:
							case TypeCode.Single:
							case TypeCode.Double:
							case TypeCode.Decimal: {
									return true;
								}
						}
						break;
					}
				case TypeCode.Single: {
						switch (typeCode2) {
							case TypeCode.Single:
							case TypeCode.Double: {
									return true;
								}
						}
						break;
					}
				default: {
						if (nonNullableType == nonNullableType2) {
							return true;
						}
						break;
					}
			}
			return false;
		}
		private static bool IsBetterThan(Expression[] args, ExpressionParser.MethodData m1, ExpressionParser.MethodData m2)
		{
			bool result = false;
			for (int i = 0; i < args.Length; i++) {
				int num = ExpressionParser.CompareConversions(args[i].Type, m1.Parameters[i].ParameterType, m2.Parameters[i].ParameterType);
				if (num < 0) {
					return false;
				}
				if (num > 0) {
					result = true;
				}
			}
			return result;
		}
		private static int CompareConversions(Type s, Type t1, Type t2)
		{
			if (t1 == t2) {
				return 0;
			}
			if (s == t1) {
				return 1;
			}
			if (s == t2) {
				return -1;
			}
			bool flag = ExpressionParser.IsCompatibleWith(t1, t2);
			bool flag2 = ExpressionParser.IsCompatibleWith(t2, t1);
			if (flag && !flag2) {
				return 1;
			}
			if (flag2 && !flag) {
				return -1;
			}
			if (ExpressionParser.IsSignedIntegralType(t1) && ExpressionParser.IsUnsignedIntegralType(t2)) {
				return 1;
			}
			if (ExpressionParser.IsSignedIntegralType(t2) && ExpressionParser.IsUnsignedIntegralType(t1)) {
				return -1;
			}
			return 0;
		}
		private Expression GenerateEqual(Expression left, Expression right)
		{
			return Expression.Equal(left, right);
		}
		private Expression GenerateNotEqual(Expression left, Expression right)
		{
			return Expression.NotEqual(left, right);
		}
		private Expression GenerateGreaterThan(Expression left, Expression right)
		{
			if (left.Type == typeof(string)) {
				return Expression.GreaterThan(this.GenerateStaticMethodCall("Compare", left, right), Expression.Constant(0));
			}
			return Expression.GreaterThan(left, right);
		}
		private Expression GenerateGreaterThanEqual(Expression left, Expression right)
		{
			if (left.Type == typeof(string)) {
				return Expression.GreaterThanOrEqual(this.GenerateStaticMethodCall("Compare", left, right), Expression.Constant(0));
			}
			return Expression.GreaterThanOrEqual(left, right);
		}
		private Expression GenerateLessThan(Expression left, Expression right)
		{
			if (left.Type == typeof(string)) {
				return Expression.LessThan(this.GenerateStaticMethodCall("Compare", left, right), Expression.Constant(0));
			}
			return Expression.LessThan(left, right);
		}
		private Expression GenerateLessThanEqual(Expression left, Expression right)
		{
			if (left.Type == typeof(string)) {
				return Expression.LessThanOrEqual(this.GenerateStaticMethodCall("Compare", left, right), Expression.Constant(0));
			}
			return Expression.LessThanOrEqual(left, right);
		}
		private Expression GenerateAdd(Expression left, Expression right)
		{
			if (left.Type == typeof(string) && right.Type == typeof(string)) {
				return this.GenerateStaticMethodCall("Concat", left, right);
			}
			return Expression.Add(left, right);
		}
		private Expression GenerateSubtract(Expression left, Expression right)
		{
			return Expression.Subtract(left, right);
		}
		private Expression GenerateStringConcat(Expression left, Expression right)
		{
			return Expression.Call(null, typeof(string).GetMethod("Concat", new Type[]
			{
				typeof(object), 
				typeof(object)
			}), new Expression[]
			{
				left, 
				right
			});
		}
		private MethodInfo GetStaticMethod(string methodName, Expression left, Expression right)
		{
			return left.Type.GetMethod(methodName, new Type[]
			{
				left.Type, 
				right.Type
			});
		}
		private Expression GenerateStaticMethodCall(string methodName, Expression left, Expression right)
		{
			return Expression.Call(null, this.GetStaticMethod(methodName, left, right), new Expression[]
			{
				left, 
				right
			});
		}
		private void SetTextPos(int pos)
		{
			this.textPos = pos;
			this.ch = ((this.textPos < this.textLen) ? this.text[this.textPos] : '\0');
		}
		private void NextChar()
		{
			if (this.textPos < this.textLen) {
				this.textPos++;
			}
			this.ch = ((this.textPos < this.textLen) ? this.text[this.textPos] : '\0');
		}
		private void NextToken()
		{
			while (char.IsWhiteSpace(this.ch)) {
				this.NextChar();
			}
			int num = this.textPos;
			char c = this.ch;
			ExpressionParser.TokenId id;
			switch (c) {
				case '!': {
						this.NextChar();
						if (this.ch == '=') {
							this.NextChar();
							id = ExpressionParser.TokenId.ExclamationEqual;
							goto IL_41E;
						}
						id = ExpressionParser.TokenId.Exclamation;
						goto IL_41E;
					}
				case '"':
				case '\'': {
						char c2 = this.ch;
						while (true) {
							this.NextChar();
							while (this.textPos < this.textLen && this.ch != c2) {
								this.NextChar();
							}
							if (this.textPos == this.textLen) {
								break;
							}
							this.NextChar();
							if (this.ch != c2) {
								goto Block_14;
							}
						}
						throw this.ParseError(this.textPos, "Unterminated string literal", new object[0]);
					Block_14:
						id = ExpressionParser.TokenId.StringLiteral;
						goto IL_41E;
					}
				case '#':
				case '$':
				case '0':
				case '1':
				case '2':
				case '3':
				case '4':
				case '5':
				case '6':
				case '7':
				case '8':
				case '9':
				case ';': {
						break;
					}
				case '%': {
						this.NextChar();
						id = ExpressionParser.TokenId.Percent;
						goto IL_41E;
					}
				case '&': {
						this.NextChar();
						if (this.ch == '&') {
							this.NextChar();
							id = ExpressionParser.TokenId.DoubleAmphersand;
							goto IL_41E;
						}
						id = ExpressionParser.TokenId.Amphersand;
						goto IL_41E;
					}
				case '(': {
						this.NextChar();
						id = ExpressionParser.TokenId.OpenParen;
						goto IL_41E;
					}
				case ')': {
						this.NextChar();
						id = ExpressionParser.TokenId.CloseParen;
						goto IL_41E;
					}
				case '*': {
						this.NextChar();
						id = ExpressionParser.TokenId.Asterisk;
						goto IL_41E;
					}
				case '+': {
						this.NextChar();
						id = ExpressionParser.TokenId.Plus;
						goto IL_41E;
					}
				case ',': {
						this.NextChar();
						id = ExpressionParser.TokenId.Comma;
						goto IL_41E;
					}
				case '-': {
						this.NextChar();
						id = ExpressionParser.TokenId.Minus;
						goto IL_41E;
					}
				case '.': {
						this.NextChar();
						id = ExpressionParser.TokenId.Dot;
						goto IL_41E;
					}
				case '/': {
						this.NextChar();
						id = ExpressionParser.TokenId.Slash;
						goto IL_41E;
					}
				case ':': {
						this.NextChar();
						id = ExpressionParser.TokenId.Colon;
						goto IL_41E;
					}
				case '<': {
						this.NextChar();
						if (this.ch == '=') {
							this.NextChar();
							id = ExpressionParser.TokenId.LessThanEqual;
							goto IL_41E;
						}
						if (this.ch == '>') {
							this.NextChar();
							id = ExpressionParser.TokenId.LessGreater;
							goto IL_41E;
						}
						id = ExpressionParser.TokenId.LessThan;
						goto IL_41E;
					}
				case '=': {
						this.NextChar();
						if (this.ch == '=') {
							this.NextChar();
							id = ExpressionParser.TokenId.DoubleEqual;
							goto IL_41E;
						}
						id = ExpressionParser.TokenId.Equal;
						goto IL_41E;
					}
				case '>': {
						this.NextChar();
						if (this.ch == '=') {
							this.NextChar();
							id = ExpressionParser.TokenId.GreaterThanEqual;
							goto IL_41E;
						}
						id = ExpressionParser.TokenId.GreaterThan;
						goto IL_41E;
					}
				case '?': {
						this.NextChar();
						id = ExpressionParser.TokenId.Question;
						goto IL_41E;
					}
				default: {
						switch (c) {
							case '[': {
									this.NextChar();
									id = ExpressionParser.TokenId.OpenBracket;
									goto IL_41E;
								}
							case '\\': {
									break;
								}
							case ']': {
									this.NextChar();
									id = ExpressionParser.TokenId.CloseBracket;
									goto IL_41E;
								}
							default: {
									if (c == '|') {
										this.NextChar();
										if (this.ch == '|') {
											this.NextChar();
											id = ExpressionParser.TokenId.DoubleBar;
											goto IL_41E;
										}
										id = ExpressionParser.TokenId.Bar;
										goto IL_41E;
									}
									break;
								}
						}
						break;
					}
			}
			if (char.IsLetter(this.ch) || this.ch == '@' || this.ch == '_') {
				do {
					this.NextChar();
				}
				while (char.IsLetterOrDigit(this.ch) || this.ch == '_');
				id = ExpressionParser.TokenId.Identifier;
			} else {
				if (char.IsDigit(this.ch)) {
					id = ExpressionParser.TokenId.IntegerLiteral;
					do {
						this.NextChar();
					}
					while (char.IsDigit(this.ch));
					if (this.ch == '.') {
						id = ExpressionParser.TokenId.RealLiteral;
						this.NextChar();
						this.ValidateDigit();
						do {
							this.NextChar();
						}
						while (char.IsDigit(this.ch));
					}
					if (this.ch == 'E' || this.ch == 'e') {
						id = ExpressionParser.TokenId.RealLiteral;
						this.NextChar();
						if (this.ch == '+' || this.ch == '-') {
							this.NextChar();
						}
						this.ValidateDigit();
						do {
							this.NextChar();
						}
						while (char.IsDigit(this.ch));
					}
					if (this.ch == 'F' || this.ch == 'f') {
						this.NextChar();
					}
				} else {
					if (this.textPos != this.textLen) {
						throw this.ParseError(this.textPos, "Syntax error '{0}'", new object[]
						{
							this.ch
						});
					}
					id = ExpressionParser.TokenId.End;
				}
			}
		IL_41E:
			this.token.id = id;
			this.token.text = this.text.Substring(num, this.textPos - num);
			this.token.pos = num;
		}
		private bool TokenIdentifierIs(string id)
		{
			return this.token.id == ExpressionParser.TokenId.Identifier && string.Equals(id, this.token.text, StringComparison.OrdinalIgnoreCase);
		}
		private string GetIdentifier()
		{
			this.ValidateToken(ExpressionParser.TokenId.Identifier, "Identifier expected");
			string text = this.token.text;
			if (text.Length > 1 && text[0] == '@') {
				text = text.Substring(1);
			}
			return text;
		}
		private void ValidateDigit()
		{
			if (!char.IsDigit(this.ch)) {
				throw this.ParseError(this.textPos, "Digit expected", new object[0]);
			}
		}
		private void ValidateToken(ExpressionParser.TokenId t, string errorMessage)
		{
			if (this.token.id != t) {
				throw this.ParseError(errorMessage, new object[0]);
			}
		}
		private void ValidateToken(ExpressionParser.TokenId t)
		{
			if (this.token.id != t) {
				throw this.ParseError("Syntax error", new object[0]);
			}
		}
		private Exception ParseError(string format, params object[] args)
		{
			return this.ParseError(this.token.pos, format, args);
		}
		private Exception ParseError(int pos, string format, params object[] args)
		{
			return new ParseException(string.Format(CultureInfo.CurrentCulture, format, args), pos);
		}
		private static Dictionary<string, object> CreateKeywords()
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
			dictionary.Add("true", ExpressionParser.trueLiteral);
			dictionary.Add("false", ExpressionParser.falseLiteral);
			dictionary.Add("null", ExpressionParser.nullLiteral);
			dictionary.Add(ExpressionParser.keywordIt, ExpressionParser.keywordIt);
			dictionary.Add(ExpressionParser.keywordIif, ExpressionParser.keywordIif);
			dictionary.Add(ExpressionParser.keywordNew, ExpressionParser.keywordNew);
			Type[] array = ExpressionParser.predefinedTypes;
			for (int i = 0; i < array.Length; i++) {
				Type type = array[i];
				dictionary.Add(type.Name, type);
			}
			return dictionary;
		}
	}

	/// <summary>
	/// 
	/// </summary>
    static class Res
    {
		public const string DuplicateIdentifier = "The identifier '{0}' was defined more than once";
		public const string ExpressionTypeMismatch = "Expression of type '{0}' expected";
		public const string ExpressionExpected = "Expression expected";
		public const string InvalidCharacterLiteral = "Character literal must contain exactly one character";
		public const string InvalidIntegerLiteral = "Invalid integer literal '{0}'";
		public const string InvalidRealLiteral = "Invalid real literal '{0}'";
		public const string UnknownIdentifier = "Unknown identifier '{0}'";
		public const string NoItInScope = "No 'it' is in scope";
		public const string IifRequiresThreeArgs = "The 'iif' function requires three arguments";
		/// <summary>
		/// 
		/// </summary>
		public const string FirstExprMustBeBool = "The first expression must be of type 'Boolean'";
		public const string BothTypesConvertToOther = "Both of the types '{0}' and '{1}' convert to the other";
		public const string NeitherTypeConvertsToOther = "Neither of the types '{0}' and '{1}' converts to the other";
		public const string MissingAsClause = "Expression is missing an 'as' clause";
		public const string ArgsIncompatibleWithLambda = "Argument list incompatible with lambda expression";
		public const string TypeHasNoNullableForm = "Type '{0}' has no nullable form";
		public const string NoMatchingConstructor = "No matching constructor in type '{0}'";
		public const string AmbiguousConstructorInvocation = "Ambiguous invocation of '{0}' constructor";
		public const string CannotConvertValue = "A value of type '{0}' cannot be converted to type '{1}'";
		public const string NoApplicableMethod = "No applicable method '{0}' exists in type '{1}'";
		public const string MethodsAreInaccessible = "Methods on type '{0}' are not accessible";
		public const string MethodIsVoid = "Method '{0}' in type '{1}' does not return a value";
		public const string AmbiguousMethodInvocation = "Ambiguous invocation of method '{0}' in type '{1}'";
		public const string UnknownPropertyOrField = "No property or field '{0}' exists in type '{1}'";
		public const string NoApplicableAggregate = "No applicable aggregate method '{0}' exists";
		public const string CannotIndexMultiDimArray = "Indexing of multi-dimensional arrays is not supported";
		public const string InvalidIndex = "Array index must be an integer expression";
		public const string NoApplicableIndexer = "No applicable indexer exists in type '{0}'";
		public const string AmbiguousIndexerInvocation = "Ambiguous invocation of indexer in type '{0}'";
		public const string IncompatibleOperand = "Operator '{0}' incompatible with operand type '{1}'";
		public const string IncompatibleOperands = "Operator '{0}' incompatible with operand types '{1}' and '{2}'";
		public const string UnterminatedStringLiteral = "Unterminated string literal";
		public const string InvalidCharacter = "Syntax error '{0}'";
		public const string DigitExpected = "Digit expected";
		public const string SyntaxError = "Syntax error";
		public const string TokenExpected = "{0} expected";
		public const string ParseExceptionFormat = "{0} (at index {1})";
		public const string ColonExpected = "':' expected";
		public const string OpenParenExpected = "'(' expected";
		public const string CloseParenOrOperatorExpected = "')' or operator expected";
		public const string CloseParenOrCommaExpected = "')' or ',' expected";
		public const string DotOrOpenParenExpected = "'.' or '(' expected";
		public const string OpenBracketExpected = "'[' expected";
		public const string CloseBracketOrCommaExpected = "']' or ',' expected";
		public const string IdentifierExpected = "Identifier expected";
		public const string DotExpected = "'.' expected";
	}
}
