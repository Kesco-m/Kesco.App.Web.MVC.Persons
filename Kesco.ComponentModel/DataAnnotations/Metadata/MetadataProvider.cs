using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Kesco.ComponentModel.DataAnnotations
{
	/// <summary>
	/// Статический класс для регистрации метаданных для ассоциированных типов.
	/// </summary>
	public static class MetadataProvider
	{
		/// <summary>
		/// Регистрирует типы-метаданные для всех ассоциированных типов.
		/// </summary>
		public static void RegisterAllProviders()
		{
			var assemblies = AppDomain.CurrentDomain.GetAssemblies().
				Where(a =>
					a.GetName().FullName.StartsWith("Kesco", StringComparison.CurrentCultureIgnoreCase)
				);
			var allAssemblyTypes = assemblies.SelectMany(a => a.GetTypes()).ToList();
			var buddyAssociations =
				from t in allAssemblyTypes
				let mdt = t.GetCustomAttributes(typeof(MetadataTypeForAttribute), false)
						.FirstOrDefault() as MetadataTypeForAttribute
				where mdt != null
				select new { Friend = mdt.AssociatedType, Buddy = t };

			foreach (var association in buddyAssociations) {
				var descriptionProvider = new AssociatedMetadataTypeTypeDescriptionProvider( association.Friend, association.Buddy);
				TypeDescriptor.AddProviderTransparent(descriptionProvider, association.Friend);
			}
		}

	}

}

