using SharedLibrary.Helpers.ApiResponse;
using SharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Helpers.Validators
{
    public static class IsObjectValid<T> where T : class
    {
        public static Result<List<ValidationResult>> CheckValidation(T entity)
        {
            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(entity, serviceProvider: null, items: null);
            bool isValid = Validator.TryValidateObject(entity, context, validationResults, true);
            if (!isValid)
            {
                return Result.Failure(validationResults, new Error("errore validazione"));
            }

            return Result.Success(validationResults);
        }

    }

    public static class ValidationErrorsHelpers
    {

        public static string GetValidationMessage(this List<ValidationResult> errors)
        {
            return string.Join(",", errors.Select(er => er.ErrorMessage));
        }
    }

}
