using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("[controller]/[action]")]
    public class RepositoriosController : Controller
    {
        private readonly IFavoritoStorage _favoritoStorage;

        public RepositoriosController(IFavoritoStorage favoritoStorage)
        {
            _favoritoStorage = favoritoStorage;
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
    }
}
