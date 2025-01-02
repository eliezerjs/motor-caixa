using Moq;
using FluentAssertions;
using IntegraCVP.Application.Interfaces;

namespace IntegraCVP.Application.Tests.Services
{
    public class GeradorCodigoCepNetServiceTest
    {
        private readonly Mock<IGeradorCodigoCepNetService> _mockGeradorCodigoCifService;

        public GeradorCodigoCepNetServiceTest()
        {
            _mockGeradorCodigoCifService = new Mock<IGeradorCodigoCepNetService>();
        }

        [Fact]
        public void GerarCodigo_DeveGerarCodigoCorretamente()
        {
            // Arrange
            string COD_CEP = "12345678";
            string clienteCif = "4567891234";
            string categoriaRegiao = "01";
            string cnae = "6201";

            string esperado = "1234500780000012345007800001450123456789000000000010000000000000006201|";

            _mockGeradorCodigoCifService
                .Setup(s => s.GerarCodigo(COD_CEP, clienteCif, categoriaRegiao, cnae))
                .Returns(esperado);

            var service = _mockGeradorCodigoCifService.Object;

            // Act
            string resultado = service.GerarCodigo(COD_CEP, clienteCif, categoriaRegiao, cnae);

            // Assert
            resultado.Should().Be(esperado);
        }
    }
}
