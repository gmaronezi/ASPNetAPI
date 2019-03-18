using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProAgil.API.Data;
using ProAgil.API.Models;

namespace ProAgil.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly DataContext _contexto;

        public ValuesController(DataContext contexto)
        {
            _contexto = contexto;
        }
        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                //async serve pra esperar ir no banco de dados e retornar os eventos
                //cada chamada do controller será criada uma instancia e uma nova thread será aberta. Cada chamada aberta terá uma espera, não travando o recurso
                var results = await _contexto.Eventos.ToListAsync();
                
                return Ok(results);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }
           
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                //async serve pra esperar ir no banco de dados e retornar os eventos
                //cada chamada do controller será criada uma instancia e uma nova thread será aberta. Cada chamada aberta terá uma espera, não travando o recurso
                var results = await _contexto.Eventos.FirstAsync(x => x.EventoId == id);
                
                return Ok(results);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
