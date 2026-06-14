using EstudioContableApp.Validators;

namespace EstudioContableApp.Tests.Validators
{
    public class ClienteValidatorTests
    {
        [Fact]
        public void NombreValido_ConNombreCompleto_DeberiaRetornarTrue()
        {
            // Arrange
            var nombre = "Candela Godoy";

            // Act
            var resultado = ClienteValidator.NombreValido(nombre);

            // Assert
            Assert.True(resultado);
        }

        [Fact]
        public void NombreValido_ConNombreVacio_DeberiaRetornarFalse()
        {
            // Arrange
            var nombre = "";

            // Act
            var resultado = ClienteValidator.NombreValido(nombre);

            // Assert
            Assert.False(resultado);
        }

        [Fact]
        public void EmailValido_ConEmailCorrecto_DeberiaRetornarTrue()
        {
            // Arrange
            var email = "cliente@test.com";

            // Act
            var resultado = ClienteValidator.EmailValido(email);

            // Assert
            Assert.True(resultado);
        }

        [Fact]
        public void EmailValido_ConEmailSinArroba_DeberiaRetornarFalse()
        {
            // Arrange
            var email = "clientetest.com";

            // Act
            var resultado = ClienteValidator.EmailValido(email);

            // Assert
            Assert.False(resultado);
        }
        [Theory]
        [InlineData("cliente@test.com", true)]
        [InlineData("cliente@gmail.com", true)]
        [InlineData("clientetest.com", false)]
        [InlineData("", false)]
        [InlineData("   ", false)]
        public void EmailValido_ConDistintosValores_DeberiaRetornarResultadoEsperado(
    string email,
    bool esperado)
        {
            // Act
            var resultado = ClienteValidator.EmailValido(email);

            // Assert
            Assert.Equal(esperado, resultado);
        }
    }
}