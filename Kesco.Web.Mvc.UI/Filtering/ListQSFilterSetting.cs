using System.Linq;
using System.Collections.Generic;
using Kesco.ObjectModel;


namespace Kesco.Web.Mvc.Filtering
{
	/// <summary>
	/// Настройки для группы поиска, настраиваемой через URL-параметры
	/// Значение для контролов группы хранится в соответствующих элементах массива строк.
	/// </summary>
	public class ListQSFilterSetting<T, TElement> : QSFilterSetting<T>
		where T :  ListQSFilterSetting<T, TElement>
		where TElement : IUnique
	{

		/// <summary>
		/// Возвращает список элементов заданного типа
		/// </summary>
		/// <value>
		/// Список элементов заданного типа
		/// </value>
		public List<TElement> List { get; protected set; }

		/// <summary>
		/// Определяет делегат для получения списка по заданному списку значений
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>Список элементов заданного типа</returns>
		public delegate List<TElement> ListAccessor(string value);

		/// <summary>
		/// Хранит получатель списка элементов заданного типа
		/// </summary>
		protected ListAccessor accessor;

		/// <summary>
		/// Initializes a new instance of the <see cref="ListQSFilterSetting&lt;T, TElement&gt;"/> class.
		/// </summary>
		public ListQSFilterSetting(){
			accessor = null;
			List = new List<TElement>();
		}

		/// <summary>
		/// Устанавливает получатель списка элементов заданного типа.
		/// </summary>
		/// <param name="accessor">The accessor.</param>
		/// <returns>Группу настроек фильтрации данных</returns>
		public T SetListAccessor(ListAccessor accessor)
		{
			this.accessor = accessor;
			return this as T;
		}

		/// <summary>
		/// Обновляет список элементов заданного типа
		/// в соотвествии с установленным значением.
		/// </summary>
		protected override void AdjustValueForClient()
		{
			base.AdjustValueForClient();
			if (accessor != null && Value != null)
				List = accessor(Value.ToString());
		}

		/// <summary>
		/// Возвращает значение фильтра в виде строки с идентификаторами, разделёнными запятой.
		/// </summary>
		public string GetValue()
		{
			//string result = null;
			//if (List != null)
			//{
			//    result = string.Join(",", List.Select(e => e.GetUniqueID()).ToArray());
			//}
			return Value as string;
		}

	}

}
