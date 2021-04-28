using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Md_exercise.Core.Domain
{
    public class BaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
