using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProAgil.Domain;
using ProAgil.Repository;

namespace ProAgil.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventoController : ControllerBase //pode trabalhar com tudo relacionado a http
    {
        public readonly IProAgilRepository _repo;
        public EventoController(IProAgilRepository repo)
        {
            _repo = repo;

        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                //async serve pra esperar ir no banco de dados e retornar os eventos
                //cada chamada do controller será criada uma instancia e uma nova thread será aberta. Cada chamada aberta terá uma espera, não travando o recurso
                var results = await _repo.GetAllEventoAsync(true);
                
                return Ok(results);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }
        }

        [HttpGet("{EventoId}")]
        public async Task<IActionResult> Get(int EventoId)
        {
            try
            {
                //async serve pra esperar ir no banco de dados e retornar os eventos
                //cada chamada do controller será criada uma instancia e uma nova thread será aberta. Cada chamada aberta terá uma espera, não travando o recurso
                var results = await _repo.GetEventoAsyncById(EventoId, true);
                
                return Ok(results);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }
        }

        [HttpGet("getByTema/{tema}")]
        public async Task<IActionResult> Get(string tema)
        {
            try
            {
                //async serve pra esperar ir no banco de dados e retornar os eventos
                //cada chamada do controller será criada uma instancia e uma nova thread será aberta. Cada chamada aberta terá uma espera, não travando o recurso
                var results = await _repo.GetAllEventoAsyncByTema(tema, true);
                
                return Ok(results);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Evento model)
        {
            try
            {
                _repo.Add(model);

                if(await _repo.SaveChangesAsync()){//se retornou verdadeiro
                    return Created($"/api/evento/{model.Id}",model);//se conseguir fazer o post, chama o [HttpGet("{EventoId}")]    
                }
                
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }
            
            return BadRequest(); 
        }

        [HttpPut]
        public async Task<IActionResult> Put(int EventoId, Evento model)
        {
            try
            {
                var evento = await _repo.GetEventoAsyncById(EventoId, true);//não quero que faça join com nada
                if(evento == null){
                    return NotFound(); 
                }
                _repo.Update(model);

                if(await _repo.SaveChangesAsync()){//se retornou verdadeiro
                    return Created($"/api/evento/{model.Id}",model);//se conseguir fazer o post, chama o [HttpGet("{EventoId}")]    
                }
                
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }
            
            return BadRequest(); 
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int EventoId)
        {
            try
            {
                var evento = await _repo.GetEventoAsyncById(EventoId, true);//não quero que faça join com nada
                if(evento == null){
                    return NotFound(); 
                }
                _repo.Delete(evento);

                if(await _repo.SaveChangesAsync()){//se retornou verdadeiro
                    return Ok();
                }
                
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }
            
            return BadRequest(); 
        }

    }
}