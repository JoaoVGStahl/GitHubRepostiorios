using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("[controller]/[action]")]
    public class FavoritosController : Controller
    {
        private readonly IFavoritosService _favoritosService;
        public FavoritosController(IFavoritosService favoritosService)
        {
            _favoritosService = favoritosService;
        }

        [HttpPost]
        public IActionResult AdicionarFavorito([FromBody] Repositorio favorito)
        {
            _favoritosService.Adicionar(favorito);
            return Ok();
        }

        [HttpGet]
        public IActionResult ListarFavoritos()
        {
            return Ok(_favoritosService.ListarFavoritos());
        }

        [HttpDelete("{id}")]
        public IActionResult RemoverFavorito(int id)
        {
            _favoritosService.Remover(id);
            return Ok();
        }
    }
}
