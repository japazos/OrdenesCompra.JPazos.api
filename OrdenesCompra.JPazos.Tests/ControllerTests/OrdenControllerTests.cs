using Microsoft.AspNetCore.Mvc;
using Moq;
using OrdenesCompra.JPazos.api.Controllers;
using OrdenesCompra.JPazos.application.Dto.Orden;
using OrdenesCompra.JPazos.application.IServices;

namespace OrdenesCompra.JPazos.Tests.ControllerTests
{
    public class OrdenControllerTests
    {
        private readonly Mock<IOrdenService> _ordenServiceMock;
        private readonly OrdenController _controller;

        public OrdenControllerTests()
        {
            _ordenServiceMock = new Mock<IOrdenService>();
            _controller = new OrdenController(_ordenServiceMock.Object);
        }

        [Fact]
        public async Task CreateOrden_DeberiaRetornar_Status200_SiSeCreaCorrectamente()
        {
            // Arrange
            var ordenDto = new OrdenCreateDto
            {
                Cliente = "New Horizons",
                OrdenDetalle = new List<OrdenDetalleDto>
                {
                    new OrdenDetalleDto { Producto = "Laptop Dell XPS", Cantidad = 2, PrecioUnitario = 1500.00m },
                    new OrdenDetalleDto { Producto = "Monitor LG 27''", Cantidad = 1, PrecioUnitario = 300.00m },
                    new OrdenDetalleDto { Producto = "Mouse inalámbrico Logitech", Cantidad = 3, PrecioUnitario = 50.00m }
                }
                        };
            _ordenServiceMock.Setup(s => s.CreateOrden(ordenDto)).ReturnsAsync(true);

            // Act
            var result = await _controller.CreateOrden(ordenDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
        }
    }
}
