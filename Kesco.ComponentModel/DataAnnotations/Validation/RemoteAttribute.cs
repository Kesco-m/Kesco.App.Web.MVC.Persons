using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kesco.ComponentModel.DataAnnotations
{
	/// <summary>
	/// Класс-атрибут для указания удалённой проверки значения свойства на стороне клиента.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public class RemoteAttribute : ValidationAttribute
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="RemoteAttribute" /> class.
		/// </summary>
		/// <param name="parameterName">Name of the parameter.</param>
		protected RemoteAttribute(string parameterName)
		{
			ParameterName = parameterName;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="RemoteAttribute" /> class.
		/// </summary>
		/// <param name="action">The action.</param>
		/// <param name="controller">The controller.</param>
		/// <param name="parameterName">Name of the parameter.</param>
		public RemoteAttribute(string action, string controller, string parameterName)
			: this(parameterName)
		{
			ControllerName = controller;
			ActionName = action;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="RemoteAttribute" /> class.
		/// </summary>
		/// <param name="routeName">Name of the route.</param>
		/// <param name="parameterName">Name of the parameter.</param>
		public RemoteAttribute(string routeName, string parameterName)
			: this(parameterName)
		{
			RouteName = routeName;
		}

		/// <summary>
		/// Возвращает имя параметра для проверки.
		/// </summary>
		/// <value>
		/// Имя параметра для проверки.
		/// </value>
		public string ParameterName { get; protected set; }

		/// <summary>
		/// Gets or sets the name of the controller.
		/// </summary>
		/// <value>
		/// The name of the controller.
		/// </value>
		public string ControllerName { get; protected set; }
		/// <summary>
		/// Gets or sets the name of the action.
		/// </summary>
		/// <value>
		/// The name of the action.
		/// </value>
		public string ActionName { get; protected set; }

		/// <summary>
		/// Gets or sets the name of the route.
		/// </summary>
		/// <value>
		/// The name of the route.
		/// </value>
		protected string RouteName { get; set; }

		/// <summary>
		/// Определяет, является ли заданное значение объекта допустимым.
		/// </summary>
		/// <param name="value">Значение объекта, который требуется проверить.</param>
		/// <returns>
		/// Значение true, если значение допустимо, в противном случае — значение false.
		/// </returns>
		public override bool IsValid(object value)
		{
			return true;
		}
	}
}
