using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoFail.Shared
{
    public class Location
    {
        public Location() { }
        public int LocationId { get; set; }
        public string? LocationName { get; set; }

        [InverseProperty("Location")]
        public virtual ICollection<OrgUnit> OrgUnits { get; set; } = new List<OrgUnit>();
    }
}
