namespace ControleEstoque.Application.Exceptions;

public class ProdutoDuplicadoException : Exception
{
    public ProdutoDuplicadoException(string numeroPart)
        : base($"Já existe um produto com o numeroPart '{numeroPart}'.") { }
}
