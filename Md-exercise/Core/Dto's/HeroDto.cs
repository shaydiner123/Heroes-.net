using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Md_exercise.Core.Dto_s
{
    public class HeroDto
    {

        public Guid? Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string AbilityName { get; set; }

        public Guid? HeroAbilityId { get; set; }

        public DateTime TrainingStartDate { get; set; }

        
        public IList<string> SuitColors { get; set; }

        public string TrainerId { get; set; }

        
        public decimal StartingPower { get; set; }

        
        public decimal? CurrentPower { get; set; } = 0;

        public uint? TrainingAmountPerformedToday { get; set; } = 0;
    }
}
