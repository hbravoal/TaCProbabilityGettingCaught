using System.Collections.Generic;

namespace Solver.Common.Models
{
    public class ProcessInformationResponse
    {
        public int Case { get; set; }

        public List<ProcessDetailInformationResponse> Detail { get; set; }
    }
    public class ProcessDetailInformationResponse
    {

        public int TopItemWeight { get; set; }
        public int Quantity { get; set; }
    }
}
