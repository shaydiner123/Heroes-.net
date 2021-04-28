using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Md_exercise.Core.Models
{
    public class Result<T>
    {
        public bool IsSuccess { get; set; }
        public IEnumerable<string> ErrorMessages { get; set; }

        public T Content { get; set; }
    }
}



