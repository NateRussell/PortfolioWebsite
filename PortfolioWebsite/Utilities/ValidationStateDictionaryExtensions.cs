using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Mvc.ModelBinding.Validation
{
    public static class ValidationStateDictionaryExtensions
    {
        public static bool IsValid(this ValidationStateDictionary validationState)
        {
            return validationState.Count <= 0;
        }
    }
}
