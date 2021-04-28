using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Md_exercise.Core.Domain
{
    public class Ability:BaseEntity
    {
        
        [Required]
        public string Name { get; set; }

        public IList<Hero> Heroes { get; set; }

        public static readonly string Defender = "Defender";
        public static readonly string Attacker = "Attacker";

    }
}
