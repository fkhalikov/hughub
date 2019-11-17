using System.Collections.Generic;
using System.Linq;

namespace HugHub.PriceEngine.Models
{
    public class ResponseResult<T>
    {
        public Dictionary<string, List<string>> Error { get; set; } = new Dictionary<string, List<string>>();

        public bool Success => !Error.Any();
        
        public T Value { get; set; }

        public bool HasValue => Value != null;
    }
}