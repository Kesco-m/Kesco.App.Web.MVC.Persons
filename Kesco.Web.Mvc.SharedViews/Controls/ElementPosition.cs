using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kesco.Web.Mvc.SharedViews.Controls
{

	/// <summary>
	/// Перечисление, указывающее какая точка 
	/// информационной подсказки определяет позицию, 
	/// относительно целевого элемента
	/// </summary>
    [Obsolete("Более не используется. Позиция определяется автоматически")]
	public enum AlignPosition : int
	{
		/// <summary>
		/// Верхний левый угол. Указатель направлен вверх.
		/// </summary>
		TopLeft = 0,

		/// <summary>
		/// Верх по центру. Указатель направлен вверх.
		/// </summary>
		TopCenter = 1,

		/// <summary>
		/// Верхний правый угол. Указатель направлен вверх.
		/// </summary>
		TopRight = 2,

		/// <summary>
		/// Верхний правый угол. Указатель направлен вправо.
		/// </summary>
		RightTop = 3,

		/// <summary>
		/// Правая сторона по центру. Указатель направлен вправо.
		/// </summary>
		RightCenter = 4,

		/// <summary>
		/// Нижний правый угол. Указатель направлен вправо.
		/// </summary>
		RightBottom = 5,

		/// <summary>
		/// Нижний правый угол. Указатель направлен вниз.
		/// </summary>
		BottomRight = 6,

		/// <summary>
		/// Низ по центру. Указатель направлен вниз.
		/// </summary>
		BottomCenter = 7,

		/// <summary>
		/// Нижний левый угол. Указатель направлен вниз.
		/// </summary>
		BottomLeft = 8,

		/// <summary>
		/// Нижний левый угол. Указатель направлен влево.
		/// </summary>
		LeftBottom = 9,
		
		/// <summary>
		/// Левая сторона по центру. Указатель направлен влево.
		/// </summary>
		LeftCenter = 10,

		/// <summary>
		/// Верхний левый угол. Указатель направлен влево.
		/// </summary>
		LeftTop = 11
	}

}