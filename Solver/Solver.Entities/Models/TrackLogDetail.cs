using Solver.DataAccessLayer.Contracts.Required;
//using Newtonsoft.Json;    
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Solver.Entities.Models
{
    public class TrackLogDetail : IEntity
    {
        [Key]

        public int Id { get; set; }

        public string Message { get; set; }

        public int RowLine{ get; set; }

        public DateTime CreateDate { get; set; }



    }
}