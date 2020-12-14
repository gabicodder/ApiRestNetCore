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
    public class ValuesController : ControllerBase
    {
        private readonly RPValue _conexion;

        public ValuesController(RPValue conexion)
        {
            this._conexion = conexion??throw new ArgumentNullException(nameof(conexion)); 
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Value>>> Get()
        {
            return await _conexion.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Value>> Get(int id)
        {
            var response = await _conexion.GetById(id);
            if (response == null) { return NotFound(); }
            return response;
        }

        [HttpPost("{post}")]
        public async Task<string> Post([FromBody] Value value)
        {
          var rep = await _conexion.Insert(value);
            
            if (rep != "OK") { return rep; }

            return "OK..!";

        }

        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] string value)
        {

        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _conexion.DeleteById(id);
        }
    }
}
