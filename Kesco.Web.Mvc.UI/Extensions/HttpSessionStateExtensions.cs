using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.SessionState;

namespace Kesco.Web.Mvc
{
	/// <summary>
	/// Класс-расширение, определяющий методы, возвращающий пользовательский контекст для экземпляров 
	/// класса <see cref="System.Web.HttpSessionState"/> и <see cref="System.Web.HttpSessionStateBase"/>
	/// </summary>
	public static class HttpSessionStateExtensions
	{

		/// <summary>
		/// Gets the user context.
		/// </summary>
		/// <param name="sessionState">State of the session.</param>
		/// <returns>Пользовательский контекст.</returns>
		public static UserContext GetUserContext(this HttpSessionStateBase sessionState)
		{
			lock (sessionState) {
				UserContext ctx = sessionState["User.Context"] as UserContext;
				if (ctx == null) {
					ctx = new UserContext();
					sessionState["User.Context"] = ctx;
				}
				return ctx;
			}
		}

		/// <summary>
		/// Gets the user context.
		/// </summary>
		/// <param name="sessionState">State of the session.</param>
		/// <returns>Пользовательский контекст.</returns>
		public static UserContext GetUserContext(this HttpSessionState sessionState)
		{
			lock (sessionState) {
				UserContext ctx = sessionState["User.Context"] as UserContext;
				if (ctx == null) {
					ctx = new UserContext();
					sessionState["User.Context"] = ctx;
				}
				return ctx;
			}
		}
	}
}
