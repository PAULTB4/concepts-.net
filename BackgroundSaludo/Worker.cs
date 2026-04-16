using System.Net;
using System.Net.Mail;
using System.Text.Json;
using Microsoft.Extensions.Options;

namespace BackgroundSaludo
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly HttpClient _httpClient;
        private readonly ApiSettings _apiSettings;
        private readonly EmailSettings _emailSettings;

        public Worker(
            ILogger<Worker> logger,
            HttpClient httpClient,
            IOptions<ApiSettings> apiOptions,
            IOptions<EmailSettings> emailOptions)
        {
            _logger = logger;
            _httpClient = httpClient;
            _apiSettings = apiOptions.Value;
            _emailSettings = emailOptions.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Worker iniciado a las: {Time}", DateTimeOffset.Now);

            using var timer = new PeriodicTimer(TimeSpan.FromSeconds(_apiSettings.IntervaloSegundos));

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await EjecutarProcesoAsync(stoppingToken);
                }
                catch (OperationCanceledException)
                {
                    _logger.LogInformation("Worker cancelado.");
                    break;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Ocurrió un error consumiendo la API o enviando el correo");
                }

                await timer.WaitForNextTickAsync(stoppingToken);
            }
        }

        private async Task EjecutarProcesoAsync(CancellationToken stoppingToken)
        {
            string url =
                $"{_apiSettings.BaseUrl}/api/saludo?idioma={_apiSettings.Idioma}&nombre={_apiSettings.Nombre}";

            _logger.LogInformation("Consumiendo API: {Url}", url);

            var response = await _httpClient.GetAsync(url, stoppingToken);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync(stoppingToken);
                _logger.LogError(
                    "Error al consumir la API. StatusCode: {StatusCode}, Detalle: {Error}",
                    response.StatusCode,
                    error);
                return;
            }

            var json = await response.Content.ReadAsStringAsync(stoppingToken);

            var resultado = JsonSerializer.Deserialize<SaludoResponse>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            if (resultado == null)
            {
                _logger.LogWarning("La respuesta JSON no pudo deserializarse.");
                return;
            }

            _logger.LogInformation(
                "Respuesta API -> Idioma: {Idioma}, Nombre: {Nombre}, Mensaje: {Mensaje}",
                resultado.Idioma,
                resultado.Nombre,
                resultado.Mensaje);

            await EnviarCorreoAsync(resultado.Mensaje);
        }

        private async Task EnviarCorreoAsync(string mensaje)
        {
            using var mail = new MailMessage();
            mail.From = new MailAddress(_emailSettings.Remitente);
            mail.To.Add(_emailSettings.Destinatario);
            mail.Subject = _emailSettings.Asunto;
            mail.Body = mensaje;

            using var smtp = new SmtpClient(_emailSettings.SmtpHost, _emailSettings.SmtpPort)
            {
                Credentials = new NetworkCredential(_emailSettings.Remitente, _emailSettings.Password),
                EnableSsl = _emailSettings.EnableSsl
            };

            await smtp.SendMailAsync(mail);

            _logger.LogInformation(
                "Correo enviado correctamente a {Destinatario}",
                _emailSettings.Destinatario);
        }
    }

    public class SaludoResponse
    {
        public string Idioma { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Mensaje { get; set; } = string.Empty;
    }

    public class ApiSettings
    {
        public string BaseUrl { get; set; } = string.Empty;
        public string Idioma { get; set; } = "en";
        public string Nombre { get; set; } = "Talia";
        public int IntervaloSegundos { get; set; } = 60;
    }

    public class EmailSettings
    {
        public string Remitente { get; set; } = string.Empty;
        public string Destinatario { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Asunto { get; set; } = "Saludo automático desde el worker";
        public string SmtpHost { get; set; } = "smtp.gmail.com";
        public int SmtpPort { get; set; } = 587;
        public bool EnableSsl { get; set; } = true;
    }
}