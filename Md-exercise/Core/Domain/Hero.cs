using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Md_exercise.Core.Domain
{
    public class Hero:BaseEntity
    {

        [Required]
        public string Name { get; set; }

        [Required]
        public Ability HeroAbility { get; set; }

        [ForeignKey("HeroAbility")]
        public Guid HeroAbilityId { get; set; }

        public DateTime TrainingStartDate { get; set; }

       
        public IList<SuitColor> SuitColors { get; set; } 


        public ApplicationUser Trainer { get; set; }


        [ForeignKey("Trainer")]
        public string TrainerId { get; set; }

    
        [Column(TypeName = "decimal(8,2)")]
        public decimal StartingPower { get; set; }

    
        [Column(TypeName = "decimal(8,2)")]
        public decimal CurrentPower { get; set; }

        public uint TrainingAmountPerformedToday { get; set; }
    }
}
