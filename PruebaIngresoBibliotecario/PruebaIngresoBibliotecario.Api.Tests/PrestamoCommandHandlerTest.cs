using Moq;
using PruebaIngresoBibliotecario.Application.Commands;
using PruebaIngresoBibliotecario.Domain.Aggregates;
using PruebaIngresoBibliotecario.Domain.Aggregates.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace PruebaIngresoBibliotecario.Api.Tests
{
    public class PrestamoCommandHandlerTest
    {
        [Fact]
        public async void Handle_UserWithLoan_ThrowsUserHasLoanException()
        {
            // Arrange
            var prestamoServiceMock = new Mock<IPrestamoService<Prestamo>>();
            prestamoServiceMock.Setup(service => service.GuestUserHasLoan(It.IsAny<string>())).ReturnsAsync(true);

            var handler = new PrestamoCommandHandler(prestamoServiceMock.Object);
            var request = new PrestamoCommand
            {
                TipoUsuario = 3,
                IdentificacionUsuario = "TestUser123"
            };

            // Act & Assert
            await Assert.ThrowsAsync<UserHasLoanException>(() => handler.Handle(request, CancellationToken.None));
        }

        [Fact]
        public async void Handle_ValidRequest_ReturnsPrestamoResponsePost()
        {
            // Arrange
            var prestamoServiceMock = new Mock<IPrestamoService<Prestamo>>();
            prestamoServiceMock.Setup(service => service.GuestUserHasLoan(It.IsAny<string>())).ReturnsAsync(false);
            prestamoServiceMock.Setup(service => service.InsertAsync(It.IsAny<Prestamo>())).Returns(Task.CompletedTask);

            var handler = new PrestamoCommandHandler(prestamoServiceMock.Object);
            var request = new PrestamoCommand
            {
                Isbn = Guid.NewGuid(),
                IdentificacionUsuario = "TestUser123",
                TipoUsuario = 2,
                FechaMaximaDevolucion = DateTime.Now.AddDays(7)
            };

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(request.Isbn, result.Id);
            Assert.Equal(request.FechaMaximaDevolucion, result.FechaMaximaDevolucion);
        }
    }
}
