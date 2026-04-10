using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet("saludo")]
        public IActionResult ObtenerSaludo(string idioma, string nombre)
        {
            if (string.IsNullOrEmpty(idioma) || string.IsNullOrEmpty(nombre))
            {
                return BadRequest("Debes enviar idioma y nombre");
            }

            var saludos = new Dictionary<string, string>
            {
                { "es", "Hola" },
                { "en", "Hello" },
                { "fr", "Bonjour" },
                {"gt","anina" }
            };

            idioma = idioma.ToLower();

            if (!saludos.ContainsKey(idioma))
            {
                return NotFound("Idioma no soportado");
            }

            var mensaje = $"{saludos[idioma]} {nombre}";

            return Ok(new
            {
                idioma,
                nombre,
                mensaje
            });
        }
    }
}