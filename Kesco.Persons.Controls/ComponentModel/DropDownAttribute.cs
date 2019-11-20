using System;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using Kesco.DataAccess;
using BLToolkit.Reflection;

namespace Kesco.Persons.Controls.ComponentModel
{
	public class DropDownAttribute : UIHintAttribute, IMetadataAware
	{
		public const string AdditionalValuesKey = "DropDown";

		/// <summary>
		/// Возвращает или устанавливает проводник данных.
		/// </summary>
		/// <value>
		/// Проводник данных.
		/// </value>
		public Type EntityAccessorType { get; set; }

		public DropDownAttribute(string uiHint) : base(uiHint) { }

		public DropDownAttribute() : base("DropDown") { }

		public IAccessor CreateAccessor()
		{
			return BLToolkit.Reflection.TypeAccessor.CreateInstanceEx(EntityAccessorType) as Kesco.DataAccess.IAccessor;
		}

		public virtual object GetOptions() { return null; }

		#region Члены IMetadataAware

		public virtual void OnMetadataCreated(ModelMetadata metadata)
		{
			var copy = TypeAccessor.Copy(this);
			metadata.AdditionalValues.Add(AdditionalValuesKey, copy);
		}

		#endregion

	}
}