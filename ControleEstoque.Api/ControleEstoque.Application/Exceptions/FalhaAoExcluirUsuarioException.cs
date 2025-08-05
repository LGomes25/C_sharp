namespace ControleEstoque.Application.Exceptions;

public class FalhaAoExcluirUsuarioException : Exception
{
    public FalhaAoExcluirUsuarioException(string referencia)
        : base($"Falha ao excluir o usuario '{referencia}'.") { }
}
