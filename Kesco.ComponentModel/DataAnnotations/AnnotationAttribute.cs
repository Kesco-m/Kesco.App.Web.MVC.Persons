using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime;
using System.Globalization;

namespace Kesco.ComponentModel.DataAnnotations
{
	/// <summary>
	/// Атрибут определяет аннотацию (имя, краткое имя, описание, приглашение) 
	/// для свойства/поля модели. Если указан тип ресурса, то строки берутся из
	/// из соотвесвущих свойств строковых ресурсов.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
	public class AnnotationAttribute : Attribute
	{
		private Type _resourceType;
		private LocalizableString _shortName = new LocalizableString("ShortName");
		private LocalizableString _name = new LocalizableString("Name");
		private LocalizableString _description = new LocalizableString("Description");
		private LocalizableString _prompt = new LocalizableString("Prompt");
		private LocalizableString _groupName = new LocalizableString("GroupName");
		private int? _order;

		/// <summary>
		/// Возвращает или устанавливает краткое имя.
		/// </summary>
		/// <value>
		/// Краткое имя.
		/// </value>
		public string ShortName
		{
			get
			{
				return this._shortName.Value;
			}
			set
			{
				if (this._shortName.Value != value) {
					this._shortName.Value = value;
				}
			}
		}

		/// <summary>
		/// Возвращает или устанавливает отображаемое имя для свойства.
		/// </summary>
		/// <value>
		/// Имя.
		/// </value>
		public string Name
		{
			get
			{
				return this._name.Value;
			}
			set
			{
				if (this._name.Value != value) {
					this._name.Value = value;
				}
			}
		}

		/// <summary>
		/// Возвращает или устанавливает отображаемое описание для свойства.
		/// </summary>
		/// <value>
		/// Описание для свойства.
		/// </value>
		public string Description
		{
			get
			{
				return this._description.Value;
			}
			set
			{
				if (this._description.Value != value) {
					this._description.Value = value;
				}
			}
		}

		/// <summary>
		/// Возвращает или устанавливает отображаемое приглашение для свойства.
		/// </summary>
		/// <value>
		/// Отображаемое приглашение для свойства.
		/// </value>
		public string Prompt
		{
			get
			{
				return this._prompt.Value;
			}
			set
			{
				if (this._prompt.Value != value) {
					this._prompt.Value = value;
				}
			}
		}

		/// <summary>
		/// Возвращает или устанавливает имя группы для свойства.
		/// </summary>
		/// <value>
		/// Имя группы для свойства.
		/// </value>
		public string GroupName
		{
			get
			{
				return this._groupName.Value;
			}
			set
			{
				if (this._groupName.Value != value) {
					this._groupName.Value = value;
				}
			}
		}

		/// <summary>
		/// Возвращает или устанавливает тип ресурса для отображаемых свойств.
		/// </summary>
		/// <value>
		/// Тип ресурса для отображаемых свойств.
		/// </value>
		public Type ResourceType
		{
			[TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
			get { return this._resourceType; }
			set
			{
				if (this._resourceType != value) {
					this._resourceType = value;
					this._shortName.ResourceType = value;
					this._name.ResourceType = value;
					this._description.ResourceType = value;
					this._prompt.ResourceType = value;
					this._groupName.ResourceType = value;
				}
			}
		}

		public int Order
		{
			get
			{
				if (!this._order.HasValue) {
					throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.AnnotationAttribute_PropertyNotSet, new object[]
					{
						"Order",
						"GetOrder"
					}));
				}
				return this._order.Value;
			}
			set
			{
				this._order = new int?(value);
			}
		}

		/// <summary>
		/// Возвращает отображаемое краткое имя.
		/// </summary>
		/// <returns>Отображаемое краткое имя</returns>
		public string GetShortName()
		{
			return this._shortName.GetLocalizableValue() ?? this.GetName();
		}

		/// <summary>
		/// Возвращает отображаемое имя.
		/// </summary>
		/// <returns>Отображаемое имя</returns>
		public string GetName()
		{
			return this._name.GetLocalizableValue();
		}

		/// <summary>
		/// Возвращает отображаемое описание свойства.
		/// </summary>
		/// <returns>Отображаемое описание свойства.</returns>
		public string GetDescription()
		{
			return this._description.GetLocalizableValue();
		}

		/// <summary>
		/// Возвращает отображаемое приглашение для ввода значения свойства.
		/// </summary>
		/// <returns>Отображаемое приглашение для ввода значения свойства.</returns>
		public string GetPrompt()
		{
			return this._prompt.GetLocalizableValue();
		}

		/// <summary>
		/// Возвращает отображаемое имя группы для свойства.
		/// </summary>
		/// <returns>Отображаемое отображаемое имя группы для свойства.</returns>
		public string GetGroupName()
		{
			return this._groupName.GetLocalizableValue();
		}

		/// <summary>
		/// Gets the order.
		/// </summary>
		/// <returns></returns>
		public int? GetOrder()
		{
			return this._order;
		}
	}
}
