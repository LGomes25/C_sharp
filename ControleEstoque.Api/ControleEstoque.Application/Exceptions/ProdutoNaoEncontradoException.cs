namespace ControleEstoque.Application.Exceptions;

public class ProdutoNaoEncontradoException : Exception
{
    public ProdutoNaoEncontradoException(string numeroPart)
        : base($"Produto com numeroPart '{numeroPart}' não foi encontrado.") { }
}
