using  System.Text.Json.Serialization;
using SQLite;

namespace EstudioContableApp.Models
{
    // este modelo representa un cliente del estudio 
    // tambien se usa como tabla local para guardar datos con SQLite

    public class Cliente
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        // la APIdevuelve "name", pero en la app lo usamos como Nombre
        [JsonPropertyName("name")]
        [MaxLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [MaxLength(150)]
        public string Email { get; set; } = string.Empty;

        // dato propio de la app para simular vencimientos del estudio contable
        [MaxLength(50)]
        public string Vencimiento { get; set; } = "IVA - 20/05";

        public string Iniciales
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Nombre))
                    return "?";

                var palabras = Nombre.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                if (palabras.Length == 1)
                    return palabras[0][0].ToString().ToUpper();

                return $"{palabras[0][0]}{palabras[1][0]}".ToUpper();
            }
        }
    }
}