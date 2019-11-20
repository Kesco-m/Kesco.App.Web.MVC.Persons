using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Kesco.Web.Mvc
{
	/// <summary>
	/// Реализует расширенного поставщика модели метаданных для приложения MVC ASP.NET.
	/// Реализована поддержка для аттрибута <see cref="System.ComponentModel.Description"/>
	/// </summary>
	public class KescoDataAnnotationsModelMetadataProvider : DataAnnotationsModelMetadataProvider
	{
		/// <summary>
		/// Создаёт мета-данные для модели данных.
		/// </summary>
		/// <param name="attributes">The attributes.</param>
		/// <param name="containerType">Type of the container.</param>
		/// <param name="modelAccessor">The model accessor.</param>
		/// <param name="modelType">Type of the model.</param>
		/// <param name="propertyName">Name of the property.</param>
		/// <returns>Мета-данные для модели данных</returns>
		protected override ModelMetadata CreateMetadata(IEnumerable<System.Attribute> attributes, System.Type containerType, System.Func<object> modelAccessor, System.Type modelType, string propertyName)
		{
			var baseModelMetadata = base.CreateMetadata(attributes, containerType, modelAccessor, modelType, propertyName);
			/*var result = new CustomMetadata(modelMetadataProvider, containerType, modelAccessor, modelType, propertyName, attributes.OfType<DisplayColumnAttribute>().FirstOrDefault(), attributes) {
				TemplateHint = !string.IsNullOrEmpty(templateName) ? templateName : baseModelMetaData.TemplateHint,
				HideSurroundingHtml = baseModelMetaData.HideSurroundingHtml,
				DataTypeName = baseModelMetaData.DataTypeName,
				IsReadOnly = baseModelMetaData.IsReadOnly,
				NullDisplayText = baseModelMetaData.NullDisplayText,
				DisplayFormatString = baseModelMetaData.DisplayFormatString,
				ConvertEmptyStringToNull = baseModelMetaData.ConvertEmptyStringToNull,
				EditFormatString = baseModelMetaData.EditFormatString,
				ShowForDisplay = baseModelMetaData.ShowForDisplay,
				ShowForEdit = baseModelMetaData.ShowForEdit,
				DisplayName = baseModelMetaData.DisplayName
			};*/
			var descriptionAttribute = attributes.OfType<DescriptionAttribute>().SingleOrDefault();
			if (descriptionAttribute != null)
				baseModelMetadata.Description = descriptionAttribute.Description;
			return baseModelMetadata;
		}
	}

	/// <summary>
	/// Класс расширяет стандартные мета-данные для модели данных с поддержкой атрибута <see cref="System.ComponentModel.Description"/>.
	/// </summary>
	public class CustomMetadata : DataAnnotationsModelMetadata
	{
		private string _description;

		/// <summary>
		/// Initializes a new instance of the <see cref="CustomMetadata"/> class.
		/// </summary>
		/// <param name="provider">The provider.</param>
		/// <param name="containerType">Type of the container.</param>
		/// <param name="modelAccessor">The model accessor.</param>
		/// <param name="modelType">Type of the model.</param>
		/// <param name="propertyName">Name of the property.</param>
		/// <param name="displayColumnAttribute">The display column attribute.</param>
		/// <param name="attributes">The attributes.</param>
		public CustomMetadata(DataAnnotationsModelMetadataProvider provider, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName, DisplayColumnAttribute displayColumnAttribute, IEnumerable<Attribute> attributes)
			: base(provider, containerType, modelAccessor, modelType, propertyName, displayColumnAttribute)
		{
			var descAttr = attributes.OfType<DescriptionAttribute>().SingleOrDefault();
			_description = descAttr != null ? descAttr.Description : "";
		}

		/// <summary>
		/// Получает или задает описание модели.
		/// </summary>
		/// <returns>Описание модели. По умолчанию установлено значение null.</returns>
		public override string Description
		{
			get
			{
				return _description;
			}
			set
			{
				_description = value;
			}
		}
	}
}
