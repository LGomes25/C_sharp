namespace ControleEstoque.Application.Exceptions;

public class ProdutoPorIdNaoEncontradoException : Exception
{
    public ProdutoPorIdNaoEncontradoException(int id)
        : base($"Produto com ID '{id}' não foi encontrado.") { }
}
