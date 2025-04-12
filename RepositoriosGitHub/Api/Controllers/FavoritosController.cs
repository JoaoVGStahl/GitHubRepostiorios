using Application.DTOs;
using Application.Interfaces;
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
        public IActionResult AdicionarFavorito([FromBody] RepositorioDTO favorito)
        {
            _favoritosService.Adicionar(favorito);
            return NoContent();
        }

        [HttpGet]
        public IActionResult ListarFavoritos()
        {
            return Ok(_favoritosService.ListarFavoritos());
        }

        [HttpDelete]
        public IActionResult RemoverFavorito([FromQuery] int id)
        {
            _favoritosService.Remover(id);
            return NoContent();
        }
    }
}
