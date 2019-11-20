using System;

namespace Kesco.ObjectModel
{
	/// <summary>
	/// Интерфейс для получения отображаемого 
	/// имени сущности.
	/// </summary>
	public interface IFriendlyNamed  
	{
		/// <summary>
		/// Возвращает отображаемое (дружелюбное)
		/// имя сущности.
		/// </summary>
		/// <returns>
		/// Отображаемое (дружелюбное) имя сущности.
		/// </returns>
		string GetInstanceFriendlyName();
		//
		//
	}
}
