namespace ControleEstoque.Application.Exceptions;

public class AlteracaoDeNumeroPartNaoPermitidaException : Exception
{
    public AlteracaoDeNumeroPartNaoPermitidaException(string original, string novo)
        : base($"Não é permitido alterar o número de parte. Original: '{original}', novo: '{novo}'.") { }
}
