namespace ControleEstoque.Application.Exceptions;

public class FalhaAoAtualizarUsuarioException : Exception
{
    public FalhaAoAtualizarUsuarioException(string nome)
        : base($"Falha ao atualizar o usuario '{nome}'.") { }
}
