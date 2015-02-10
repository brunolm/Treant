namespace Treant.Core.Extenders
{
    using Newtonsoft.Json;
    using System;
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

        /// <summary>
        /// Perform a deep copy of the object, using Json as a serialization method.
        /// </summary>
        /// <typeparam name="T">The type of object being clonned.</typeparam>
        /// <param name="source">The object instance to clone.</param>
        /// <returns>The cloned object.</returns>
        public static T Clone<T>(this T source)
        {
            if (Object.ReferenceEquals(source, null))
            {
                return default(T);
            }

            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(source));
        }
    }
}
