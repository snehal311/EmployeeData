using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeData.Models
{
    public class RegionDetails
    {
        public int RegionId { get; set; }
        public string RegionName { get; set; }
        public List<ZoneDetails> Zones { get; set; }
    }
}