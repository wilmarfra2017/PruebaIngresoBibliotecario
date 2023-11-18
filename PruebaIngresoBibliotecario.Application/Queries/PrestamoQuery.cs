using MediatR;
using PruebaIngresoBibliotecario.Application.Dto;
using System;

namespace PruebaIngresoBibliotecario.Application.Queries
{
    public class PrestamoQuery : IRequest<PrestamoResponseGet>
    {
        public Guid Isbn { get; set; }

        public PrestamoQuery(Guid isbn)
        {
            Isbn = isbn;
        }
    }
}
