using Solver.Entities.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Solver.Entities.Models
{
    public class WeightLastElement : IEntity
    {
        [Key]

        public int Id { get; set; }

        public DateTime CreateDate { get; set; }
        public int Weight { get; set; }

        public virtual Elements Element { get; set; }
    }
}