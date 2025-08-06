namespace ControleEstoque.Application.Exceptions;

public class ValorInvalidoException : Exception
{
    public ValorInvalidoException(string campo, decimal valor)
        : base($"O valor '{valor}' informado para o campo '{campo}' é inválido. Deve ser maior que zero.") { }
}
