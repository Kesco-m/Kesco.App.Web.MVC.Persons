using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace Kesco.SignalAccess
{

	/// <summary>
	/// Состояние сущности в контексте работающего с ней пользователя 
	/// </summary>
	public enum EntityUserState
	{
		ALL,
		VIEW,
		EDIT,
		CHANGE,
		SIGNS,
		DELETE		
	}

	/// <summary>
	/// Тип сущности по которой регистрируется активность
	/// </summary>
	public enum EntityType
	{
		ALL,
		DOCUMENT,
		EMPLOYEE,
		PERSON
	}

	public class EntityUserInfo
	{
		public int UserID { get; set; }

		public string UserName { get; set; }

		public int EntityID { get; set; }

		public EntityType EntityType { get; set; }

		public EntityUserState EntityUserState { get; set; }		

		public DateTime LastUserStateInfoDate { get; set; }

	}
}