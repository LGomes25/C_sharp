namespace ControleEstoque.Application.Exceptions;

public class LoginInvalidoException : Exception
{
    public LoginInvalidoException()
        : base ("Email ou senha invalidos.") { }
}
