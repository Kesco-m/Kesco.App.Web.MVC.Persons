using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNet.SignalR;

namespace Kesco.SignalAccess
{
	/// <summary>
	/// Класс контекта сущности в разрезе работающего с ней пользователя пользоватателя
	/// </summary>
	public class EntityState
	{
		public int EntityID { get; set; }
		public EntityType EntityType { get; set; }
		public int UserID { get; set; }
		public EntityUserState NewState { get; set; }
	}


	/// <summary>
	/// 
	/// </summary>
	public class EntityStateFilterParameters
	{
		public List<int> EntityIDs { get; set; }
		public List<EntityType> EntityTypes { get; set; }
		public List<EntityUserState> EntityStates { get; set; }

		public EntityStateFilterParameters()
		{
			EntityIDs = new List<int>();
			EntityTypes = new List<EntityType>();
			EntityStates = new List<EntityUserState>();
		}
	}

	/// <summary>
	/// Базовый интерфейс хаба, умеющего регистрировать состояние изменяемой сущности и умеющий выполнять фильтрацию хранящихся сущностей 
	/// </summary>
	/// <typeparam name="T">тип объекта хаба</typeparam>
	public interface IEntityUserStateHub<THub, TState, TFilterParameters, TUserInfo>
		where THub : Hub
		where TState : EntityState
		where TFilterParameters : EntityStateFilterParameters
		where TUserInfo : EntityUserInfo
	{
		/// <summary>
		/// Обновляет состояние объекса в случае необходимости и уведомляется клиетскую сторону путем вызова callback метода 
		/// </summary>
		/// <param name="entityState">регистрируемое состояние</param>
		/// <param name="clientCallBackName">вызываемый callback метод</param>
		void SetEntityState(TState entityState, string clientCallBackName);

		/// <summary>
		/// Выполняет фильтрацию сущностей по переданным параметрам фильтрации
		/// </summary>
		/// <param name="parameters"></param>
		/// <returns></returns>
		IEnumerable<TUserInfo> GetEntityStates(TFilterParameters parameters);
	}
}
