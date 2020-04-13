using System.Threading.Tasks;
using AppCadastro.Api.Conventions;
using AppCadastro.Api.Data;
using AppCadastro.Api.Filters;
using AppCadastro.Domain.Requests;
using AppCadastro.Domain.Response;
using AppCadastro.Domain.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace AppCadastro.Api.Controllers
{
    [Route("api/usuario")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]        
        [ApiConventionMethod(typeof(UsuarioApiConvention), nameof(UsuarioApiConvention.Get))]
        public async Task<IActionResult> Get()
        {
            var result = await _usuarioService.GetUsuariosAsync();

            return Ok(result);            
        }

        [HttpGet("{id:int}")]
        [ApiConventionMethod(typeof(UsuarioApiConvention), nameof(UsuarioApiConvention.Get))]
        [UsuarioExiste]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _usuarioService.GetUsuarioAsync(
                new GetUsuarioRequest
                {
                    UsuarioId = id
                });

            return Ok(result);
        }

        [HttpPost]
        [Route("search")]
        [ApiConventionMethod(typeof(UsuarioApiConvention), nameof(UsuarioApiConvention.Get))]
        public async Task<IActionResult> Search(
            [FromBody] SearchUsuarioRequest request)
        {
            var result = await _usuarioService.Search(
                new SearchUsuarioRequest
                {
                    Nome = request.Nome,
                    Ativo = request.Ativo
                });
            
            return Ok(result);
        }

        [HttpPost]
        [ApiConventionMethod(typeof(UsuarioApiConvention), nameof(UsuarioApiConvention.Create))]
        public async Task<IActionResult> Post(AddUsuarioRequest request)
        {
            var result = await _usuarioService.AddUsuarioAsync(request);

            return CreatedAtAction(nameof(GetById), new
            {
                usuarioId = result.UsuarioId
            }, null);
        }

        [HttpPut("{id:int}")]
        [ApiConventionMethod(typeof(UsuarioApiConvention), nameof(UsuarioApiConvention.Update))]
        [UsuarioExiste]
        public async Task<IActionResult> Put(int id, EditUsuarioRequest request)
        {
            if (request.UsuarioId != id)
                return BadRequest();
            
            request.UsuarioId = id;
            var result = await _usuarioService.EditUsuarioAsync(request);

            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        [ApiConventionMethod(typeof(UsuarioApiConvention), nameof(UsuarioApiConvention.Delete))]
        [UsuarioExiste]
        public async Task<IActionResult> Delete(int id)
        {
            var request = new DeleteUsuarioRequest { UsuarioId = id };
            await _usuarioService.DeleteUsuarioAsync(request);

            return NoContent();
        }
    }
}