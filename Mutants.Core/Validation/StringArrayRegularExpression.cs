using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Mutants.Core.Validation
{
    public class StringArrayRegularExpression : RegularExpressionAttribute
    {
        string _pattern;
        public StringArrayRegularExpression(string pattern) : base(pattern)
        {
            _pattern = pattern;
        }
        public override bool IsValid(object value)  
        {  
            var array = (string[])(value);  
            return array.All(item => base.IsValid(item));
        }  
    }
}