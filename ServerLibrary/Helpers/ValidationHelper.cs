using System.ComponentModel.DataAnnotations;

namespace ServerLibrary.Helpers;
/// <summary>
/// Provides utility methods for validating data models using Data Annotations.
/// </summary>
public static class ValidationHelper
{
    ///<summary>
    ///Validates a model instance using data annotation attributes and return validation error messages
    ///</summary>
    ///<typeparam name="T">The Type of model to validate.</typeparam>
    ///<param name="model">The model instance to validate</param>
    ///<returns>
    ///boolen value of validation
    ///Return an true if model is valid or false if the model is invalid.
    ///</returns>
    public static bool ValidateModel<T>(T model) where T : class
    {
        var validationContext = new ValidationContext(model);
        var ValidationResults = new List<ValidationResult>();

        // validate all properties recursively (true anables full object validation)
        Validator.TryValidateObject(model, validationContext, ValidationResults, validateAllProperties: true);

        // Extract and retun only the error messages
        var errorMesseges = ValidationResults
            .Where(result => !string.IsNullOrWhiteSpace(result.ErrorMessage))
            .Select(result => result.ErrorMessage!)
            .ToList();

        return errorMesseges.Any() ? false : true;
    }

}
