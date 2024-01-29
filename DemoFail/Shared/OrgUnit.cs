using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoFail.Shared
{
    public class OrgUnit
    {

        public OrgUnit() { }
        public int Id { get; set; }

        public int? ParentId { get; set; }

        public bool HasChild { get; set; }

        public string? Summary { get; set; }
        public int ManagerId { get; set; }
        public int LocationId { get; set; }

        [ForeignKey("ManagerId")]
        [InverseProperty("OrgUnits")]
        public virtual Manager Manager { get; set; }

        [ForeignKey("LocationId")]
        [InverseProperty("OrgUnits")]
        public virtual Location Location { get; set; }

        //public byte[] SysTS { get; set; }

    }
}