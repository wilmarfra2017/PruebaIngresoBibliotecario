using System;

public class UserHasLoanException : Exception
{
    public string IdentificacionUsuario { get; }

    public UserHasLoanException(string identificacionUsuario)
        : base($"El usuario con identificacion {identificacionUsuario} ya tiene un libro prestado por lo cual no se le puede realizar otro prestamo")
    {
        IdentificacionUsuario = identificacionUsuario;
    }
}
