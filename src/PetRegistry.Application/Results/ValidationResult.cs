using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetRegistry.Application.Results
{
    public class ValidationResult
    {
        public bool Valid { get; }
        public IEnumerable<string> Errors { get; }

        public ValidationResult(bool valid, IEnumerable<string>? errors = null)
        {
            Valid = valid;
            Errors = errors ?? Enumerable.Empty<string>();
        }
    }
}
