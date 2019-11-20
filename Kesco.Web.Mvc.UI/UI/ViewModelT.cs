using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kesco.Web.Mvc
{
	/// <summary>
	/// Базовый класс для модели представления. Модель представления будет
	/// имеет типизированное свойство Model. Это необходимо для унификации
	/// отсылки модели данных обслуживающему контроллеру.
	/// </summary>
	/// <typeparam name="TDataModel">Тип модели данных.</typeparam>
	public abstract class ViewModel<TDataModel> : DialogViewModel
		where TDataModel : class, new() 
	{
		/// <summary>
		/// Возвращает или устанавливает модель данных.
		/// </summary>
		/// <value>
		/// Модель данных.
		/// </value>
		public TDataModel Model { get; protected set; }

		/// <summary>
		/// Инициализирует новый экземпляр <see cref="ViewModel{TDataModel}" /> класса.
		/// </summary>
		public ViewModel()
		{
			Model = new TDataModel();
		}

		public virtual void SetDataModel(TDataModel dataModel)
		{
			Model = dataModel;
		}
	}
}
