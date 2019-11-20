using System;
using BLToolkit.Reflection;


namespace Kesco.Web.Mvc.Filtering
{
	/// <summary>
	/// Настройки группы фильтрации формы поиска
	/// </summary>
	public class FilterSetting<T>
		where T : FilterSetting<T>
	{
		/// <summary>
		/// Признак того, что параметры группы имеют редактируемое представление на форме
		/// </summary>
		public bool Editable { get; protected set; }

		/// <summary>
		/// Признак того, что группа с параметрами активна и участвует в поиске
		/// </summary>
		public bool Enable { get; protected set; }

		/// <summary>
		/// Значение по-умолчанию (для модели на клиенте), определяющее значения всех вложенных элементов группы
		/// </summary>
		protected object DefaultValue { get; set; }

		/// <summary>
		/// Оригинальное устанавливаемое значение параметра
		/// </summary>
		protected object OriginalValue { get; set; }

		/// <summary>
		/// Текущее значение группы (для модели на клиенте), определяющее значения всех вложенных элементов группы
		/// </summary>
		public object Value { get; protected set; }

		public FilterSetting() : this(true, false, null) { }

		public FilterSetting(bool editable, bool enable) : this(editable, enable, null) { }

		public FilterSetting(object defaultValue) : this(true, false, defaultValue) { }

		/// <summary>
		/// Конструктор настроек группы
		/// </summary>
		/// <param name="editable">группы имеют редактируемое представление на форме</param>
		/// <param name="enable">группа с параметрами активна и участвует в поиске</param>
		/// <param name="defaultValue">агрегатное значение (для клиента) по-умолчанию всех вложенных элементов группы</param>
		public FilterSetting(bool editable, bool enable, object defaultValue)
		{
			Editable = editable;
			Enable = enable;
			DefaultValue = defaultValue ?? String.Empty;
			Value = OriginalValue = String.Empty;
		}

		/// <summary>
		/// Установка значения группы
		/// </summary>
		/// <param name="value"></param>
		public virtual T SetValue(object value)
		{
			Value = value ?? DefaultValue;
			AdjustValueForClient();
			return this as T;
		}

		/// <summary>
		/// Включение/выключение условия фильтрации
		/// </summary>
		/// <param name="value">true - фильтр работает</param>
		public virtual T SetEnable(bool value)
		{
			Enable = value;
			return this as T;
		}

		/// <summary>
		/// Приведение значения по правилам, определенным для наследника
		/// Вызов базового метода обязателен при переопределении.
		/// </summary>
		protected virtual void AdjustValueForClient()
		{
			OriginalValue = Value;
		}

		/// <summary>
		/// Получение оригинального устанавливаемого значения параметра
		/// </summary>
		public object GetOriginalValue()
		{
			return OriginalValue;
		}

		/// <summary>
		/// Создаёт экземпляр настройки группы фильтрации
		/// </summary>
		/// <typeparam name="T">Тип настройки фильтрации</typeparam>
		/// <returns></returns>
		public static T CreateInstance()
		{
			return TypeAccessor<T>.CreateInstanceEx();
		}
	}

}
