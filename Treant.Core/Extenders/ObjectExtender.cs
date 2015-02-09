namespace Treant.Core.Extenders
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public static class ObjectExtender
    {
        public static OperationResult Validate<T>(this T obj) where T : class
        {
            ICollection<ValidationResult> results = new List<ValidationResult>();

            Validator.TryValidateObject(obj, new ValidationContext(obj), results, true);

            return new OperationResult(results);
        }
    }
}
