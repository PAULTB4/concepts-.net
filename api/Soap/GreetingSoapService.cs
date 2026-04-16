using api.Services;

namespace api.Soap
{
    public class GreetingSoapService : IGreetingSoapService
    {
        private readonly ILanguageService _languageService;
        private readonly IGreetingService _greetingService;

        public GreetingSoapService(
            ILanguageService languageService,
            IGreetingService greetingService)
        {
            _languageService = languageService;
            _greetingService = greetingService;
        }

        public string[] ObtenerLenguajes()
        {
            return _languageService.ObtenerLenguajes();
        }

        public SaludoResponse ObtenerSaludo(string idioma, string nombre)
        {
            if (string.IsNullOrWhiteSpace(idioma) || string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("Debes enviar idioma y nombre");

            if (!_languageService.ExisteIdioma(idioma))
                throw new ArgumentException("Idioma no soportado");

            return new SaludoResponse
            {
                Idioma = idioma.Trim().ToLower(),
                Nombre = nombre.Trim(),
                Mensaje = _greetingService.ObtenerSaludo(idioma, nombre)
            };
        }
    }
}