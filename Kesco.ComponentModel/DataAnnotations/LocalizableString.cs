using System;
using System.ComponentModel.DataAnnotations.Resources;
using System.Globalization;
using System.Reflection;
using System.Runtime;

namespace Kesco.ComponentModel.DataAnnotations
{
    /// <summary>
    /// Класс представляет локализованную строку.
    /// Значение выбирается исходя из текущей культуры.
    /// Данный класс является точной копией класса
    /// System.ComponentModel.DataAnnotations.LocalizableString
    /// </summary>
	public sealed class LocalizableString
	{
		private string _propertyName;
		private string _propertyValue;
		private Type _resourceType;
		private Func<string> _cachedResult;

        /// <summary>
		/// Возвращает или устанавливает значение.
        /// </summary>
        /// <value>
		/// Значение.
        /// </value>
		public string Value
		{
			[TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
			get
			{
				return this._propertyValue;
			}
			set
			{
				if (this._propertyValue != value)
				{
					this.ClearCache();
					this._propertyValue = value;
				}
			}
		}

        /// <summary>
        /// Возвращает или устанавливает тип ресурса.
        /// </summary>
        /// <value>
        /// Тип ресурса
        /// </value>
		public Type ResourceType
		{
			[TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
			get
			{
				return this._resourceType;
			}
			set
			{
				if (this._resourceType != value)
				{
					this.ClearCache();
					this._resourceType = value;
				}
			}
		}

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="LocalizableString" /> класса.
        /// </summary>
        /// <param name="propertyName">Имя свойства из ресурса.</param>
		[TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
		public LocalizableString(string propertyName)
		{
			this._propertyName = propertyName;
		}

        /// <summary>
        /// Gets the localizable value.
        /// </summary>
        /// <returns></returns>
		public string GetLocalizableValue()
		{
			if (this._cachedResult == null)
			{
				if (this._propertyValue == null || this._resourceType == null)
				{
					this._cachedResult = (() => this._propertyValue);
				}
				else
				{
					PropertyInfo property = this._resourceType.GetProperty(this._propertyValue);
					bool flag = false;
					if (!this._resourceType.IsVisible || property == null || property.PropertyType != typeof(string))
					{
						flag = true;
					}
					else
					{
						MethodInfo getMethod = property.GetGetMethod();
						if (getMethod == null || !getMethod.IsPublic || !getMethod.IsStatic)
						{
							flag = true;
						}
					}
					if (flag)
					{
                        string exceptionMessage = string.Format(CultureInfo.CurrentCulture, Resources.LocalizableString_LocalizationFailed, new object[]
						{
							this._propertyName,
							this._resourceType.FullName,
							this._propertyValue
						});
						this._cachedResult = delegate
						{
							throw new InvalidOperationException(exceptionMessage);
						};
					}
					else
					{
						this._cachedResult = (() => (string)property.GetValue(null, null));
					}
				}
			}
			return this._cachedResult();
		}

        /// <summary>
        /// Clears the cache.
        /// </summary>
		private void ClearCache()
		{
			this._cachedResult = null;
		}
	}
}
