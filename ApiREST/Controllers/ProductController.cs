using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiREST.Data;
using ApiREST.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiREST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly RpProducto _conexion;

        public ProductController(RpProducto conn)
        {
            this._conexion = conn ?? throw new ArgumentNullException(nameof(_conexion));
        }

        // GET: api/<ProductController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> Get()
        {
            return await _conexion.GetAll();
        }

        // GET api/<ProductController>/5
        [HttpGet("{name}")]
        public async Task<ActionResult<Producto>> Get(string name)
        {
            return await _conexion.GetById(name);
        }

        // POST api/<ProductController>
        [HttpPost]
        public async Task<string> Post([FromBody] Producto nuevo)
        {
            var rp = await _conexion.InsertProduct(nuevo);
            if (rp == 0) { return "Ohhh.. nooo!! revisa tus parámetros"; }

            return "You are a Champions..! Todo is Ok =)";
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
