using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Kesco.Web.Mvc.Infrastructure
{
	/// <summary>
	/// Интерфейс который обязана поддерживать модель-представление для поддержки валидации
	/// </summary>
	public interface IValidateResults
	{
		void UpdateValidationState(ModelStateDictionary modelState, string property);
		object GetValidationResults();
		object GetPartialUpdateValidationResults();
	}

	/// <summary>
	/// Класс расширяет базовый контроллер для поддержки валидации
	/// </summary>
	/// <typeparam name="TViewModel">Тип модели-представления.</typeparam>	
	public abstract class ValidationSupportedModelController<TDataModel> : ModelController<TDataModel>
		where TDataModel : class, new()
    {

    	protected abstract Kesco.Web.Mvc.ViewModel<TDataModel> GetViewModel(TDataModel dataModel);

		/// <summary>
		/// Выполняет диспетчеризацию команд.
		/// </summary>
		/// <param name="command">Команда</param>
		/// <param name="control">Идентификатор элемента управления на стороне клиента</param>
		/// <param name="model">Модель</param>
		/// <param name="result">Результат действия, если команда обработана, иначе null.</param>
		/// <returns>Возвращает истину, если команда обработана, иначе false</returns>
		protected override bool DoDispatch(string command, string control, TDataModel model, out ActionResult result)
		{
			 switch (command)
			 {
				 case "validate":
			 		result = Validate(control, model);
			 		return true;
			 }
			return base.DoDispatch(command, control, model, out result);
		}

		/// <summary>
		/// Выполняет валидацию используя модель-представление.
		/// </summary>
		/// <param name="control">Идентификатор элемента управления на стороне клиента</param>
		/// <param name="model">Модель</param>
		/// <returns>Возвращает обновленное совтояние модели-представления</returns>
		public virtual ActionResult Validate(string property, TDataModel model)
		{
			var vm = GetViewModel(model);
			if (!(vm is IValidateResults))
				throw new Exception("Модель представления не поддерживает валидацию");

			var validationResults = vm as IValidateResults;
			validationResults.UpdateValidationState(ModelState, property);

			string script = @"
						(function() {{
							ko.mapping.fromJS({0}, {{}}, ViewModel);						
						}})();";
			if (string.IsNullOrEmpty(property))
			{
				script = String.Format(script,
				(Kesco.Web.Mvc.Json.Serialize(validationResults.GetValidationResults(), true) ?? String.Empty));
			}
			else
			{
				script = String.Format(script,
				(Kesco.Web.Mvc.Json.Serialize(validationResults.GetPartialUpdateValidationResults(), true) ?? String.Empty));
			}
			return JavaScript(script);
    	}
    }
}
