using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.Extensions.Logging;
using DemoFail.Shared;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using Microsoft.AspNetCore.Cors;
using System.Text;
using Syncfusion.Blazor.Gantt;

namespace DemoFail.Server.Controllers
{
    //[Authorize]
    //[ApiController]
    //[Route("OrgUnits")]
    public class OrgUnitsController : ODataController
    {
        public static List<OrgUnit> orgUnits = new List<OrgUnit>();
        public static List<Manager> managers = new List<Manager>();
        public static List<Location> locations = new List<Location>();
        public void GetData()
        {
            if (orgUnits.Count == 0)
            {
                var manager1 = new Manager() { ManagerId = 1, ManagerName = "John Smith" };
                var manager2 = new Manager() { ManagerId = 2, ManagerName = "Tommy Callahan" };

                var location1 = new Location() { LocationId = 1, LocationName = "East Campus" };
                var location2 = new Location() { LocationId = 2, LocationName = "West Campus" };

                orgUnits.Add(new OrgUnit() { Id = 1, HasChild = true});
                orgUnits.Add(new OrgUnit() { Id = 2, ParentId = 1, Summary = "Ou 2", ManagerId = 1, Manager = manager1, LocationId = 2, Location = location2 });
                orgUnits.Add(new OrgUnit() { Id = 3, ParentId = 1, Summary = "Ou 3", ManagerId = 2, Manager = manager2, LocationId = 1, Location = location1 });
                orgUnits.Add(new OrgUnit() { Id = 4, ParentId = 1, Summary = "Ou 4", ManagerId = 1, Manager = manager1, LocationId = 2, Location = location2 });
                orgUnits.Add(new OrgUnit() { Id = 5, ParentId = 1, Summary = "Ou 5", ManagerId = 2, Manager = manager2, LocationId = 1, Location = location1 });
                orgUnits.Add(new OrgUnit() { Id = 6, ParentId = 1, Summary = "Ou 6", ManagerId = 1, Manager = manager1, LocationId = 2, Location = location2 });
            }

            
        }



        //[HttpGet]
        [EnableQuery]
        public IActionResult Get(CancellationToken token)
        {
            GetData();
            IQueryable<OrgUnit> tOrgUnits = orgUnits.AsQueryable();
            return Ok(tOrgUnits);
        }

        [EnableQuery]
        [HttpGet]
        public IActionResult Get(int key)
        {
            var product = orgUnits.FirstOrDefault(p => p.Id == key);
            if (product == null)
            {
                return NotFound($"Not found product with id = {key}");
            }

            return Ok(product);
        }

        [HttpPost]
        // [EnableQuery]
        public IActionResult Post([FromBody] OrgUnit orgUnit, CancellationToken token)
        {
            orgUnits.Add(orgUnit);

            //_context.SaveChanges();

            return Created(orgUnit);
        }

        [HttpPut]
        public IActionResult Put(int key, [FromBody] Delta<OrgUnit> orgUnit)
        {
            var original = orgUnits.FirstOrDefault(p => p.Id == key);
            if (original == null)
            {
                return NotFound($"Not found product with id = {key}");
            }

            orgUnit.Put(original);
            //_context.SaveChanges();
            return Updated(original);
        }

        [HttpPatch]
        public IActionResult Patch(int key, Delta<OrgUnit> orgUnit)
        {
            var original = orgUnits.FirstOrDefault(p => p.Id == key);
            if (original == null)
            {
                return NotFound($"Not found product with id = {key}");
            }

            orgUnit.Patch(original);

            //_context.SaveChanges();

            return Updated(original);
        }

        [HttpDelete]
        public IActionResult Delete(int key)
        {
            var original = orgUnits.FirstOrDefault(p => p.Id == key);
            if (original == null)
            {
                return NotFound($"Not found product with id = {key}");
            }

            orgUnits.Remove(original);
            //_context.SaveChanges();
            return Ok();
        }
    }
}
