using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Kesco.Web.Mvc.Compression.Extensions
{
	/// <summary>
	/// Класс определяет методы-расширения для строки
	/// </summary>
	public static class StringExtensions
	{
		/// <summary>
		/// Метод-расширение преобразует строку в поток.
		/// </summary>
		/// <param name="source">The source.</param>
		/// <returns>Поток</returns>
		public static Stream ToStream(this string source)
		{
			MemoryStream stream = new MemoryStream();
			StreamWriter writer = new StreamWriter(stream);
			writer.Write(source ?? String.Empty);
			writer.Flush();
			return stream;
		}
	}
}
