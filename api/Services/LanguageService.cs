namespace api.Services
{
    public class LanguageService : ILanguageService
    {
        private readonly string[] _lenguajes =
        {
            "es",
            "en",
            "fr",
            "pt"
        };

        public string[] ObtenerLenguajes()
        {
            return _lenguajes;
        }

        public bool ExisteIdioma(string idioma)
        {
            if (string.IsNullOrWhiteSpace(idioma))
                return false;

            var codigo = idioma.Trim().ToLower();
            return _lenguajes.Contains(codigo);
        }
    }
}