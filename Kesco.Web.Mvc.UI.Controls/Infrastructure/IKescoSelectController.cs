using System;
using System.Web.Mvc;
using Kesco.DataAccess;

namespace Kesco.Web.Mvc.UI
{
	public enum KescoSelectEnum : int
	{
		Edit	= 0,
		Display = 1
	}

	/// <summary>
	/// Определяет интерфейс элемента управления KescoSelect
	/// </summary>
	/// <typeparam name="S">Тип класса, описывающий критерии поиска сущности</typeparam>
	/// <typeparam name="TID">Тип идентификатора.</typeparam>
	interface IKescoSelectController<S, TID> : IController
		where S : SearchParameters, new()
		where TID : struct
	{
		ActionResult AdvSearch(string control, int mode, S parameters);
		ActionResult Details(string controlID, int mode);
		ActionResult Dispatch(string command, string control, int mode, TID? id, S parameters);
		ActionResult GetItem(string control, int mode, TID? id);
		ActionResult Search(string control, int mode, S parameters);
		ActionResult SetValue(string control, TID? id);
	}

}
