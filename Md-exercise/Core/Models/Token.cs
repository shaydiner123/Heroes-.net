using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Md_exercise.Core.Models
{
    public class Token
    {
        public String TokenValue { get; set; }
        public DateTime ExpiresIn { get; set; }
    }
}
