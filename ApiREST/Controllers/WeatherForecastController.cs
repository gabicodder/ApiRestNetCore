using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ApiREST.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(
                new object[] {
                    new { Id=1, Nombre="Cliente 1", Apellido="Apellido 1"},
                    new { Id=2, Nombre="Cliente 2", Apellido="Apellido 2"}

                });
        }
    }
}
