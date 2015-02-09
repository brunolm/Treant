using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Treant.Core
{
    public sealed class OperationResult
    {
        public int AffectedResults { get; set; }

        public bool Valid
        {
            get
            {
                return ValidationResults.Count == 0;
            }
        }

        public ICollection<ValidationResult> ValidationResults { get; private set; }

        public OperationResult(ICollection<ValidationResult> validationResults)
        {
            ValidationResults = validationResults;

            if (ValidationResults == null)
            {
                ValidationResults = new List<ValidationResult>(0);
            }
        }
    }
}
