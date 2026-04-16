namespace api.Services
{
    public class GreetingService : IGreetingService
    {
        public string ObtenerSaludo(string idioma, string nombre)
        {
            var codigo = idioma.Trim().ToLower();
            var nombreLimpio = nombre.Trim();

            var saludos = new Dictionary<string, string>
            {
                { "es", "Hola" },
                { "en", "Hello" },
                { "fr", "Bonjour" },
                { "pt", "Olá" }
            };

            return $"{saludos[codigo]} {nombreLimpio}";
        }
    }
}