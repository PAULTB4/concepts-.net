using System.Text.Json;

namespace BackgroundSaludo
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            string apiBaseUrl = "https://localhost:7191";

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var handler = new HttpClientHandler();
                    handler.ServerCertificateCustomValidationCallback = (_, _, _, _) => true;

                    using HttpClient client = new HttpClient(handler);

                    string url = $"{apiBaseUrl}/api/saludo?idioma=en&nombre=Juan";

                    var response = await client.GetAsync(url, stoppingToken);

                    if (!response.IsSuccessStatusCode)
                    {
                        var error = await response.Content.ReadAsStringAsync(stoppingToken);
                        _logger.LogError("Error al consumir la API: {Error}", error);
                    }
                    else
                    {
                        var json = await response.Content.ReadAsStringAsync(stoppingToken);

                        var resultado = JsonSerializer.Deserialize<SaludoResponse>(
                            json,
                            new JsonSerializerOptions
                            {
                                PropertyNameCaseInsensitive = true
                            });

                        _logger.LogInformation(
                            "Respuesta API -> Idioma: {Idioma}, Nombre: {Nombre}, Mensaje: {Mensaje}",
                            resultado?.Idioma,
                            resultado?.Nombre,
                            resultado?.Mensaje);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Ocurrió un error consumiendo la API");
                }

                await Task.Delay(10000, stoppingToken);
            }
        }
    }

    public class SaludoResponse
    {
        public string Idioma { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Mensaje { get; set; } = string.Empty;
    }
}