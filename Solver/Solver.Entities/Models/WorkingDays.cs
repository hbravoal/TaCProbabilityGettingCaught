using Solver.Entities.Contracts;
//using Newtonsoft.Json;    
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Solver.Entities.Models
{
    public class WorkingDays : IEntity
    {
        [Key]

        public int Id { get; set; }

        public DateTime CreateDate { get; set; }
        public int DayToWork { get; set; }

        public virtual List<Elements>Elements{ get; set; }
    }
}