using Solver.Entities.Contracts;
//using Newtonsoft.Json;    
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Solver.Entities.Models
{
    public class TrackLog : IEntity
    {
        [Key]

        public int Id { get; set; }

        public string  FileName{ get; set; }
        public string Identification{ get; set; }

        public bool IsValid { get; set; }

        public DateTime CreateDate { get; set; }

        public virtual List<TrackLogDetail> TrackLogDetails{ get; set; }
    }
}