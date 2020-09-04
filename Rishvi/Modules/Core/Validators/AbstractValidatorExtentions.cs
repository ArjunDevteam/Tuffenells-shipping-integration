using FluentValidation;
using System.Linq;

namespace Rishvi.Modules.Core.Validators
{
	public static class AbstractValidatorExtension
	{
		public static Result ValidateResult<TInstance>(this AbstractValidator<TInstance> validator, TInstance instance)
		{
			var results = validator.Validate(instance);

			if (results.IsValid) return new Result().SetSuccess();

			if (results.Errors.Count == 1 && results.Errors.First().PropertyName == "")
				return new Result().SetError(results.Errors.First().ErrorMessage);

			//var result = new Result { Success = false };
			var result = new Result().SetError("Marked red fields are mandatory.");

			foreach (var validationFailure in results.Errors)
				result.Errors.Add(validationFailure.PropertyName, validationFailure.ErrorMessage);

			return result;
		}
	}
}
