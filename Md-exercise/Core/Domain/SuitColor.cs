using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Md_exercise.Core.Domain
{
    public class SuitColor:BaseEntity
    {
        [Required]
        public string Name { get; set; }

        public IList<Hero> Heroes { get; set; }
    }
}
