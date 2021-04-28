using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Md_exercise.Core.Models
{
    public class Response
    {
        public bool IsSuccess { get; set; }
        public ResponseError Error { get; set; }
        public Object Content { get; set; }


    }


    public class ResponseError
    {
        //Reason=stacktrace
        public string Reason { get; set; }
        public IEnumerable<string> Messages { get; set; }
    }
}
