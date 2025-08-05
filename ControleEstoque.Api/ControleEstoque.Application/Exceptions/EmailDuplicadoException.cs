namespace ControleEstoque.Application.Exceptions;

public class EmailDuplicadoException : Exception
{
    public EmailDuplicadoException(string email)
        : base($"Já existe um usuario com o email '{email}'.") { }
}
