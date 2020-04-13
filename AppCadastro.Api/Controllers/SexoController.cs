using System.Threading.Tasks;
using AppCadastro.Domain.Requests;
using AppCadastro.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace AppCadastro.Api.Controllers
{
    [Route("api/sexo")]
    [ApiController]
    public class SexoController : ControllerBase
    {
        private readonly ISexoService _sexoService;

        public SexoController(ISexoService sexoService)
        {
            _sexoService = sexoService;
        }

        // api/sexo
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _sexoService.GetSexoAsync();

            return Ok(result);
        }

        // api/sexo/1
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _sexoService.GetSexoByIdAsync(
                new GetSexoRequest
                {
                    SexoId = id
                });

            return Ok(result);
        }

        // api/sexo
        [HttpPost]
        public async Task<IActionResult> Post(AddSexoRequest request)
        {
            var result = await _sexoService.AddSexoAsync(request);

            return CreatedAtAction(nameof(GetById), new
            {
                sexoId = result.SexoId
            }, null);
        }
    }
}