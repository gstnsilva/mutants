
namespace Mutants.Core
{
    public class ApiStatusResult
    {
        public ApiStatusResult() {}
        
        public ApiStatusResult(string message)
        {
            Message = message;
        }

        public string Message { get; set; }

        public string Detail { get; set; }
    }
}
