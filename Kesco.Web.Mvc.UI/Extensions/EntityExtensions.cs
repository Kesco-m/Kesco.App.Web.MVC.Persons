using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLToolkit.Reflection;
using System.Web.Mvc;
using BLToolkit.Common;


namespace Kesco.Web.Mvc.Extensions
{
	public static class EntityExtensions
	{
		public static ModelMetadata GetModelMetadata<T>(this T entity)
			where T : EntityBase<T>
		{
			T model = TypeAccessor<T>.CreateInstance();
			return ModelMetadataProviders.Current.GetMetadataForType(() => model, model.GetType());
		}
	}
}
