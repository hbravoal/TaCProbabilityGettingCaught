using Solver.DataAccessLayer.Contracts.Required;
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

        public bool isValid { get; set; }

        public DateTime CreateDate { get; set; }

    }
}