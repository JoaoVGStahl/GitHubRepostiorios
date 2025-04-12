using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("[controller]/[action]")]
    public class RepositoriosController : Controller
    {
        private readonly IRepositorioService _repositorioService;

        public RepositoriosController(IRepositorioService repositorioService)
        {
            _repositorioService = repositorioService;
        }
        
        [HttpGet]
        public async Task<IActionResult> ListarPorNome([FromQuery]  string nome)
        {
            return Ok(await _repositorioService.ListarPorNomeAsync(nome));
        }

        [HttpGet]
        public async Task<IActionResult> ListarDoUsuario([FromQuery]  string nome)
        {
            return Ok(await _repositorioService.ListarDoUsuarioAsync(nome));
        }

        [HttpGet]
        public async Task<IActionResult> ListarPorRelevancia([FromQuery] string nome, [FromQuery] bool asc)
        {
            return Ok(await _repositorioService.ListarPorRelevanciaAsync(nome,asc));
        }
    }
}
