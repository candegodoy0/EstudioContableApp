using  System.Text.Json.Serialization;

namespace EstudioContableApp.Models
{
    // este modelo representa un cliente del estudio 

    public class Cliente
    {
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Nombre { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Vencimiento { get; set; } = "IVA - 20/05";
    }
}