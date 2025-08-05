namespace ControleEstoque.Application.Exceptions;

public class AlteracaoDeEmailNaoPermitidaException : Exception
{
    public AlteracaoDeEmailNaoPermitidaException(string original, string novo)
        : base($"Não é permitido alterar o email. Original: '{original}', novo: '{novo}'.") { }
}
