using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("[controller]/[action]")]
    public class RepositoriosController : Controller
    {
        private readonly IFavoritosStorage _favoritoStorage;
        private readonly IRepositorioService repositorioService;

        public RepositoriosController(IFavoritosStorage favoritoStorage, 
                                      IRepositorioService repositorioService)
        {
            _favoritoStorage = favoritoStorage;
            this.repositorioService = repositorioService;
        }
        
        [HttpGet("{nome}")]
        public async Task<IActionResult> ListarPorNome(string nome)
        {
            return Ok(await repositorioService.ListarPorNome(nome));
        }

        [HttpGet("{nome}")]
        public async Task<IActionResult> ListarDoUsuario(string nome)
        {
            return Ok(await repositorioService.ListarDoUsuario(nome));
        }

        [HttpGet("{asc}")]
        public async Task<IActionResult> ListarPorRelevancia(bool asc)
        {
            return Ok(await repositorioService.ListarPorRelevanciaAsync(asc));
        }
    }
}
