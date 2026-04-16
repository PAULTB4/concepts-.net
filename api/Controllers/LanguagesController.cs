using Microsoft.AspNetCore.Mvc;
using api.Services;

namespace api.Controllers
{
    [Route("api")]
    [ApiController]
    public class LanguagesController : ControllerBase
    {
        private readonly ILanguageService _languageService;
        private readonly IGreetingService _greetingService;

        public LanguagesController(
            ILanguageService languageService,
            IGreetingService greetingService)
        {
            _languageService = languageService;
            _greetingService = greetingService;
        }

        [HttpGet("")]
        public IActionResult Home()
        {
            return Ok("API corriendo correctamente");
        }

        [HttpGet("lenguajes")]
        public IActionResult ObtenerLenguajes()
        {
            var lenguajes = _languageService.ObtenerLenguajes();
            return Ok(lenguajes);
        }

        [HttpGet("saludo")]
        public IActionResult ObtenerSaludo(string idioma, string nombre)
        {
            if (string.IsNullOrWhiteSpace(idioma) || string.IsNullOrWhiteSpace(nombre))
            {
                return BadRequest("Debes enviar idioma y nombre");
            }

            if (!_languageService.ExisteIdioma(idioma))
            {
                return BadRequest("Idioma no soportado");
            }

            var mensaje = _greetingService.ObtenerSaludo(idioma, nombre);

            return Ok(new
            {
                idioma = idioma.Trim().ToLower(),
                nombre = nombre.Trim(),
                mensaje
            });
        }
    }
}