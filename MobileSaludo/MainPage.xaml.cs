using System.Text.Json;

namespace MobileSaludo;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnSaludarClicked(object sender, EventArgs e)
    {
        string nombre = txtNombre.Text?.Trim() ?? string.Empty;
        string idioma = pickerIdioma.SelectedItem?.ToString() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(idioma))
        {
            lblResultado.Text = "Debes ingresar nombre y seleccionar idioma.";
            return;
        }

        string apiBaseUrl;

#if ANDROID
        apiBaseUrl = "https://10.0.2.2:7191";
#else
        apiBaseUrl = "https://localhost:7191";
#endif

        string url = $"{apiBaseUrl}/api/saludo?idioma={idioma}&nombre={Uri.EscapeDataString(nombre)}";

        try
        {
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (_, _, _, _) => true;

            using HttpClient client = new HttpClient(handler);

            var response = await client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                lblResultado.Text = $"Error: {error}";
                return;
            }

            var json = await response.Content.ReadAsStringAsync();

            var resultado = JsonSerializer.Deserialize<SaludoResponse>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            lblResultado.Text = resultado?.Mensaje ?? "Sin respuesta";
        }
        catch (Exception ex)
        {
            lblResultado.Text = $"Error al consumir la API: {ex.Message}";
        }
    }
}

public class SaludoResponse
{
    public string Idioma { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public string Mensaje { get; set; } = string.Empty;
}