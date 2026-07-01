namespace EstudioContableApp.Validators
{
    /// <summary>
    /// Contiene las reglas de validación para los datos de los clientes.
    /// </summary>
    public static class ClienteValidator
    {
        /// <summary>
        /// Verifica que el nombre no esté vacío ni contenga solo espacios.
        /// </summary>
        public static bool NombreValido(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                return false;

            return nombre.All(c => char.IsLetter(c) || char.IsWhiteSpace(c));
        }

        /// <summary>
        /// Verifica que el email tenga un formato básico válido.
        /// </summary>
        public static bool EmailValido(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            return email.Contains("@") && email.Contains(".");
        }
    }
}