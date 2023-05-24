using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeData.Models
{
    public class ZoneDetails
    {
        public int ZoneId { get; set; }
        public string ZoneName { get; set; }
        public List<BranchDetails> Branches { get; set; }
    }
}