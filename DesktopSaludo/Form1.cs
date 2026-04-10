using System.Net.Http;
using System.Text.Json;

namespace DesktopSaludo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            cmbIdioma.Items.Add("es");
            cmbIdioma.Items.Add("en");
            cmbIdioma.Items.Add("fr");

            cmbIdioma.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbIdioma.SelectedIndex = 0;
        }

        private async void btnSaludar_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text.Trim();
            string idioma = cmbIdioma.Text;
            string apiBaseUrl = "https://localhost:7191";

            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(idioma))
            {
                lblResultado.Text = "Debes ingresar nombre y seleccionar idioma.";
                return;
            }

            string url = $"{apiBaseUrl}/api/saludo?idioma={idioma}&nombre={Uri.EscapeDataString(nombre)}";

            try
            {
                var handler = new HttpClientHandler();
                handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;

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

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {

        }
    }

    public class SaludoResponse
    {
        public string Idioma { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Mensaje { get; set; } = string.Empty;
    }
}