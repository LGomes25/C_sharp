namespace ControleEstoque.Application.Exceptions;

public class UsuarioPorEmailNaoEncontradoException : Exception
{
    public UsuarioPorEmailNaoEncontradoException(string email)
        : base($"Usuario com email '{email}' não foi encontrado.") { }
}
