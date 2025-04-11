using Application.Interfaces;
using Domain.Entities;
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

        [HttpPost]
        public IActionResult AdicionarFavorito([FromBody] Repositorio favorito)
        {
            _favoritoStorage.Adicionar(favorito);
            return Ok();
        }

        [HttpGet]
        public IActionResult ListarFavoritos()
        {
            var favoritos = _favoritoStorage.ListarFavoritos();
            return Ok(favoritos);
        }

        [HttpDelete("{id}")]
        public IActionResult RemoverFavorito(int id)
        {
            _favoritoStorage.Remover(id);
            return NoContent();
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> ListarRepositoriosPorNome(string name)
        {
            return Ok(await repositorioService.ListarRepositoriosPorNome(name));
        }
    }
}
