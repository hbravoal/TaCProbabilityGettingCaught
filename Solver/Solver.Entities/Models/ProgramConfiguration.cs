using Solver.Entities.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Solver.Entities.Models
{
    public class ProgramConfiguration : IEntity
    {
        [Key]

        public int Id { get; set; }

        public string Description { get; set; }

        public string ManagmentProvider{ get; set; }
        public string AnalyzeFileProvider { get; set; }
        public DateTime CreateDate { get; set; }

    }
}