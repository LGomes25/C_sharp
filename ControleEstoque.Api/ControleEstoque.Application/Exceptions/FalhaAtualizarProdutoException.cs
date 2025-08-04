namespace ControleEstoque.Application.Exceptions;

public class FalhaAoAtualizarProdutoException : Exception
{
    public FalhaAoAtualizarProdutoException(string referencia)
        : base($"Falha ao atualizar o produto '{referencia}'.") { }
}
