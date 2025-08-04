namespace ControleEstoque.Application.Exceptions;

public class FalhaAoExcluirProdutoException : Exception
{
    public FalhaAoExcluirProdutoException(string referencia)
        : base($"Falha ao excluir o produto '{referencia}'.") { }
}
