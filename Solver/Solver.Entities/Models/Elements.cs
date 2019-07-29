
using Solver.Entities.Contracts;
//using Newtonsoft.Json;    
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Solver.Entities.Models
{
   public class Elements : IEntity
    {
        [Key]

        public int Id { get; set; }
        
        public int Quantity { get; set; }

        public DateTime CreateDate { get; set; }

        public virtual WorkingDays WorkingDays { get; set; }

        public virtual List<WeightLastElement> WeightLastElements{ get; set; }

    }
}