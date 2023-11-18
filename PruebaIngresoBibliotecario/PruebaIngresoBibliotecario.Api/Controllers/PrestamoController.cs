using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PruebaIngresoBibliotecario.Application.Commands;
using PruebaIngresoBibliotecario.Application.Queries;
using PruebaIngresoBibliotecario.Domain.Aggregates;
using System;
using System.Threading.Tasks;

namespace PruebaIngresoBibliotecario.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrestamoController : ControllerBase
    {
        private readonly ILogger<PrestamoController> _logger;
        private readonly IMediator _mediator;

        public PrestamoController(IMediator mediator, ILogger<PrestamoController> logger)
        {
            _logger = logger;
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost]
        public async Task<IActionResult> CrearPrestamo([FromBody] Prestamo prestamo)
        {
            _logger.Log(LogLevel.Information, "Metodo CrearPrestamo - Controller - Api");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var command = new PrestamoCommand
                {
                    Isbn = prestamo.Isbn,
                    IdentificacionUsuario = prestamo.IdentificacionUsuario,
                    TipoUsuario = prestamo.TipoUsuario
                };

                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (UserHasLoanException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
            catch (FormatException)
            {
                return BadRequest(new { mensaje = "El formato de GUID proporcionado es inválido." });
            }
        }


        [HttpGet("{isbn}")]
        public async Task<IActionResult> GetPrestamo(Guid isbn)
        {
            _logger.Log(LogLevel.Information, "Metodo GetPrestamo - Controller - Api");

            try
            {
                var query = new PrestamoQuery(isbn);
                var prestamo = await _mediator.Send(query);

                if (prestamo == null)
                {
                    return NotFound(new { mensaje = $"El préstamo con id {isbn} no existe." });
                }

                return Ok(prestamo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el préstamo.");
                return StatusCode(500, new { mensaje = "Error al obtener el préstamo." });
            }
        }
    }
}
