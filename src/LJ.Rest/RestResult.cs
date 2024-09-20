using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LJ.Rest
{
    public class RestResult<T>(bool isSuccess, T? value, Dictionary<string, IEnumerable<string>> header, string? exceptionMessage)
    {
        public bool IsSuccess { get; } = isSuccess;
        public T? Value { get; } = value;
        public Dictionary<string, IEnumerable<string>> Header { get; } = header;

        public string? ExceptionMessage { get; } = exceptionMessage;
    }
}
