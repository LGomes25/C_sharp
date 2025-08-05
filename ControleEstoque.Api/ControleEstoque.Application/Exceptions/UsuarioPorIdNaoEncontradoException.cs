namespace ControleEstoque.Application.Exceptions;

public class UsuarioPorIdNaoEncontradoException : Exception
{
    public UsuarioPorIdNaoEncontradoException(int id)
        : base($"Usuario com ID '{id}' não foi encontrado.") { }
}
