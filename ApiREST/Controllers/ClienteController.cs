using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiREST.Data;
using ApiREST.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiREST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            RPCliente cliente = new RPCliente();
            return Ok(cliente.ObtenerClientes());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            RPCliente cliente = new RPCliente();
            var cliRet = cliente.ObtenerCliente(id);

            if (cliRet == null)
            {
                var nf = NotFound("El Cliente " + id.ToString() + " no existe");
                return nf;
            }

            return Ok(cliRet);
        }

        [HttpPost("{agregar}")]
        public IActionResult AgregarCliente(Cliente nuevo)
        {
            RPCliente rp = new RPCliente();
            rp.Agregar(nuevo);
            return CreatedAtAction(nameof(AgregarCliente), nuevo);
        }
    }
}
