using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoFail.Shared
{
    public class Manager
    {
        public Manager() { }
        public int ManagerId { get; set; }
        public string? ManagerName { get; set; }

        [InverseProperty("Manager")]
        public virtual ICollection<OrgUnit> OrgUnits { get; set; } = new List<OrgUnit>();
    }
}
