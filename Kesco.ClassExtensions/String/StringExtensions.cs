using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Kesco
{
	/// <summary>
	/// Класс-расширение, реализующий расширенные методы для строк
	/// </summary>
	public static class StringExtensions
	{
		public static readonly string Space = " ";
		/// <summary>
		/// Определяет паттерн для поиска слов в строке
		/// </summary>
		public static readonly Regex WordPattern = new Regex("((?<=\")[^\"]+(?=\"))|[0-9А-ЯA-Z_ёЁ]+", RegexOptions.Compiled | RegexOptions.IgnoreCase);

		public static readonly Regex SplitWordsPattern = new Regex(@"[-'""\(\{\.;\!\?\:\r\n\u00A0]+", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.ECMAScript | RegexOptions.CultureInvariant);

		public static readonly Regex RemoveSpacesPattern = new Regex(@"[ ]{2,}", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.ECMAScript | RegexOptions.CultureInvariant);

		private static readonly Regex RemoveHtmlSpacesPattern = new Regex(@"[\s]{2,}", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.ECMAScript | RegexOptions.CultureInvariant);

		private static readonly Regex RegexBetweenTags = new Regex(@">(?! )\s+", RegexOptions.Compiled);
		private static readonly Regex RegexLineBreaks = new Regex(@"([\n\t\s])+?(?<= {2,})<", RegexOptions.Compiled);

		public static string CompressHtml(this String html)
		{
			html = RegexBetweenTags.Replace(html ?? String.Empty, "> ");
			html = RegexLineBreaks.Replace(html, " <");

			return html.Trim();
		}

		/// <summary>
		/// Форматирует строку с указанными параметрами.
		/// </summary>
		/// <param name="format">Строка форматирования.</param>
		/// <param name="args">Параметры.</param>
		/// <returns>Отформатированная строка</returns>
		public static string FormatWith(this String format, params object[] args)
		{
			return String.Format(format, args);
		}

		/// <summary>
		/// Нормализует фразу, убираю и преобразуя ненужные символы на единичный пробел.
		/// Аналог: Инвентаризация.dbo.fn_SplitWords(@СтрокаПоиска)+\r\n\
		/// </summary>
		/// <param name="text">Текст.</param>
		/// <returns>Нормализованная фраза</returns>
		public static string SplitWords(this string text)
		{
			var normalized = SplitWordsPattern.Replace(text ?? String.Empty, Space);

			normalized = RemoveSpacesPattern.Replace(normalized, " ");

			return normalized.Trim();

		}

		/// <summary>
		/// Возвращает список слов, составляющих текст.
		/// </summary>
		/// <param name="text">Текст.</param>
		/// <returns>Массив слов</returns>
		public static string[] GetWords(this string text)
		{
			List<string> wordList = new List<string>();

			foreach (Match word in WordPattern.Matches(text))
				wordList.Add(word.Value);

			return wordList.ToArray();
		}

		/// <summary>
		/// Нормализует текст, возвращая текст, состоящий только из слов первоначального
		/// </summary>
		/// <param name="text">текст.</param>
		/// <returns>Текст, состоящий только из слов</returns>
		public static string NormalizeText(this string text) {
			return String.Join(Space, (text ?? "").GetWords());
		}

		/// <summary>
		/// To the number negative pattern.
		/// </summary>
		/// <param name="negativePattern">The negative pattern.</param>
		/// <returns></returns>
		public static int ToNumberNegativePattern(this string negativePattern)
		{
			int pattern = 1;
			switch (negativePattern) {
				case "(n)": pattern = 0; break;
				case "-n": pattern = 1; break;
				case "- n": pattern = 2; break;
				case "n-": pattern = 3; break;
				case "n -": pattern = 4; break;
			}
			return pattern;
		}

		/// <summary>
		/// To the currency positive pattern.
		/// </summary>
		/// <param name="negativePattern">The negative pattern.</param>
		/// <returns></returns>
		public static int ToCurrencyPositivePattern(this string negativePattern)
		{
			int pattern = -1;
			switch (negativePattern) {
				case "$n": pattern = 0; break;
				case "n$": pattern = 1; break;
				case "$ n": pattern = 2; break;
				case "n $": pattern = 3; break;
			}
			return pattern;
		}

		/// <summary>
		/// To the currency negative pattern.
		/// </summary>
		/// <param name="negativePattern">The negative pattern.</param>
		/// <returns></returns>
		public static int ToCurrencyNegativePattern(this string negativePattern)
		{
			int pattern = -1;
			switch (negativePattern) {
				case "($n)": pattern = 0; break;
				case "-$n": pattern = 1; break;
				case "$-n": pattern = 2; break;
				case "$n-": pattern = 3; break;
				case "(n$)": pattern = 4; break;
				case "-n$": pattern = 5; break;
				case "n-$": pattern = 6; break;
				case "n$-": pattern = 7; break;
				case "-n $": pattern = 8; break;
				case "-$ n": pattern = 9; break;
				case "n $-": pattern = 10; break;
				case "$ n-": pattern = 11; break;
				case "$ -n": pattern = 12; break;
				case "n- $": pattern = 13; break;
				case "($ n)": pattern = 14; break;
				case "(n $)": pattern = 15; break;
			}
			return pattern;
		}

		/// <summary>
		/// To the percent positive pattern.
		/// </summary>
		/// <param name="negativePattern">The negative pattern.</param>
		/// <returns></returns>
		public static int ToPercentPositivePattern(this string negativePattern)
		{
			int pattern = -1;
			switch (negativePattern) {
				case "n %": pattern = 0; break;
				case "n%": pattern = 1; break;
				case "%n": pattern = 2; break;
				case "% n": pattern = 3; break;
			}
			return pattern;
		}

		/// <summary>
		/// To the percent negative pattern.
		/// </summary>
		/// <param name="negativePattern">The negative pattern.</param>
		/// <returns></returns>
		public static int ToPercentNegativePattern(this string negativePattern)
		{
			int pattern = -1;
			switch (negativePattern) {
				case "-n %": pattern = 0; break;
				case "-n%": pattern = 1; break;
				case "-%n": pattern = 2; break;
				case "%-n": pattern = 3; break;
				case "%n-": pattern = 4; break;
				case "n-%": pattern = 5; break;
				case "n%-": pattern = 6; break;
				case "-% n": pattern = 7; break;
				case "n %-": pattern = 8; break;
				case "% n-": pattern = 9; break;
				case "% -n": pattern = 10; break;
				case "n- %": pattern = 11; break;
			}
			return pattern;
		}

		/// <summary>
		/// Преобразует список значений, представленный строкой
		/// в массив значений указанного типа
		/// </summary>
		/// <typeparam name="T">Тип значения</typeparam>
		/// <param name="list">Cписок значений</param>
		/// <param name="separator">Разделитель.</param>
		/// <param name="converter">Конвертер строки в значение указанного типа.</param>
		/// <returns>Массив значений указанного типа</returns>
		public static T[] ToArray<T>(this string list, char[] separator, Converter<string, T> converter)
			where T: struct
		{
			Guard.IsNotNull(separator, "separator");
			Guard.IsNotNull(converter, "converter");

			List<T> listT = new List<T>();
	
			if (!String.IsNullOrEmpty(list)) {
				Array.ForEach(list.Split(separator), s => { 
					listT.Add(converter(s)); 
				});
			}
			
			return listT.ToArray();
		}


		/// <summary>
		/// Возвращает массив строк с числами, соответствующими включенным разрядам переданного числа
		/// </summary>
		/// <param name="input">строка с числом, представляющим битовую маску</param>
		/// <returns>строковый массив чисел, соответствующих переданной маске</returns>
		public static string[] SplitToArrayOfBinaryDigits(this string input)
		{
			int n;
			if (!String.IsNullOrEmpty(input) && int.TryParse(input, out n))
				return n.SplitToArrayOfBinaryDigits();

			return new string[]{};
		}


		/// <summary>
		/// Возвращает массив строк с числами, соответствующими включенным разрядам переданного числа
		/// </summary>
		/// <param name="input">число, представляющее битовую маску</param>
		/// <returns>строковый массив чисел, соответствующих переданной маске</returns>
		public static string[] SplitToArrayOfBinaryDigits(this int? input)
		{
			return (input ?? 0).SplitToArrayOfBinaryDigits();
		}


		/// <summary>
		/// Возвращает массив строк с числами, соответствующими включенным разрядам переданного числа
		/// </summary>
		/// <param name="input">число, представляющее битовую маску</param>
		/// <returns>строковый массив чисел, соответствующих переданной маске</returns>
		public static string[] SplitToArrayOfBinaryDigits(this int input)
		{
			var values = new List<string>();
			for (int i = 1; i <= input; i <<= 1)
				if ((i & input) != 0)
					values.Add(i.ToString("D"));

			return values.ToArray();
		}

		/// <summary>
		/// Возвращает Uri для переданного адреса
		/// </summary>
		/// <param name="url">The URL.</param>
		/// <returns>Uri</returns>
		public static Uri GetUri(this string url)
		{
			return new UriBuilder(url).Uri;
		}

		/// <summary>
		/// Возвращает Uri для переданного адреса
		/// </summary>
		/// <param name="url">The URL.</param>
		/// <returns>Uri</returns>
		public static IDictionary<string, Uri> GetUriList(this string url)
		{

			var uries = new Dictionary<string, Uri>();
			
			if (!String.IsNullOrEmpty(url)) {
				
				var list = new Regex(
						@"([,;]|\s+)+", 
						RegexOptions.IgnoreCase | 
						RegexOptions.ECMAScript | 
						RegexOptions.CultureInvariant
					)
					.Replace(url, " ")
					.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

				Array.ForEach(list, (item) => {
					try {

						uries.Add(item, new UriBuilder(item).Uri);

					} catch { }
				});

			}

			return uries;

		}

		/// <summary>
		/// Преобразует номер телефона, заданный строкой,
		/// в международный набор номера.
		/// </summary>
		/// <param name="phone">Номер телефона, заданный строкой.</param>
		/// <returns>Международный набор номера</returns>
		public static string ToInternationalNumber(this string phone) {
			Regex regex = new Regex(@"[ +()]", RegexOptions.IgnoreCase | RegexOptions.ECMAScript | RegexOptions.CultureInvariant);
			string result = regex.Replace(phone, @"");
			result = new Regex(@"[\D]+", RegexOptions.IgnoreCase | RegexOptions.ECMAScript | RegexOptions.CultureInvariant)
				.Replace(result, @"");
			return result;
		}

		/// <summary>
		/// Возвращает первую найденную непустую строку
		/// </summary>
		/// <param name="values">Массив строк.</param>
		/// <returns>Первую найденную непустую строку или null.</returns>
		public static string Coalesco(params string[] values) {
			if (values == null) return null;
			return values.FirstOrDefault(val => !String.IsNullOrWhiteSpace(val));
		}

		/// <summary>
		/// Проверяет содержит ли строка только латинские буквы или нет.
		/// </summary>
		/// <param name="text">Значение текстового поля</param>
		/// <returns>
		/// [true], если строка содержит только латинские буквы.
		/// </returns>
		public static bool HasOnlyLatinChars(string text)
		{
			return Regex.IsMatch((text ?? String.Empty).Trim(), @"^[\p{IsBasicLatin}\s]+$");
		}

		public static string EscapeSqlQoutes(this string text)
		{
			return text.Replace("'", "''");
		}
	}
}
