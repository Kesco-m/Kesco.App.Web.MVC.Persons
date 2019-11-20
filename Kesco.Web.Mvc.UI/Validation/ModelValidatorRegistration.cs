using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Kesco.ComponentModel.DataAnnotations;

namespace Kesco.Web.Mvc.Validation
{
	/// <summary>
	/// Класс добавляет <see cref="System.Web.Mvc.DataAnnotationsModelValidatorProvider">DataAnnotationsModelValidatorProvider</see>
	/// новыми адаптерами для проверки на стороне клиента.
	/// </summary>
	public static class ModelValidatorRegistration
	{
		/// <summary>
		/// Регистрирует дополнительные адаптеры для проверки на стороне клиента.
		/// </summary>
		public static void RegisterAdapters()
		{
			DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(MinValueAttribute), typeof(MinValueAttributeAdapter));
			DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(MaxValueAttribute), typeof(MaxValueAttributeAdapter));
		}
	}
}
