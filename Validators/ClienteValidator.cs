namespace EstudioContableApp.Validators
{
    public static class ClienteValidator
    {
        public static bool NombreValido(string nombre)
        {
            return !string.IsNullOrWhiteSpace(nombre);
        }

        public static bool EmailValido(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            return email.Contains("@") && email.Contains(".");
        }
    }
}