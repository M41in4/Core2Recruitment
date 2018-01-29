using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SIENN.Services.DTO;
using SIENN.Services.Managers;

namespace SIENN.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class UnitsController : Controller
    {
        private readonly IUnitsManager _unitsManager;

        public UnitsController(IUnitsManager unitsManager)
        {
            _unitsManager = unitsManager;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UnitDTO>), 200)]
        public IActionResult GetUnits()
        {
            var units = _unitsManager.GetUnits();
            return new ObjectResult(units);
        }

        [HttpGet("{id}", Name = "GetUnits")][ProducesResponseType(typeof(UnitDTO), 200)]
        public IActionResult GetUnit(int id)
        {
            var unit = _unitsManager.GetUnitById(id);
            return unit != null ? (IActionResult) new ObjectResult(unit) : NotFound();
        }

        [HttpPost]
        public IActionResult Save([FromBody] UnitDTO unit)
        {
            var id = _unitsManager.Save(unit);
            return new ObjectResult(id);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUnit(int id)
        {
            _unitsManager.DeleteUnitById(id);
            return Ok(id);
        }
    }
}