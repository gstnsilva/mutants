using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Mutants.Core.Validation
{
    public class StringArrayLengthMustMatch : ValidationAttribute
    {
        public override bool IsValid(object value)  
        {  
            var array = (string[])(value);  
            return array.All(item => item.Length == array.Length);
        }  
    }
}