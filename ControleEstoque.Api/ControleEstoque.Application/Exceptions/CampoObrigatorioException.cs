namespace ControleEstoque.Application.Exceptions;

public class CampoObrigatorioException : Exception
{
    public CampoObrigatorioException(string campo)
         : base($"O campo '{campo}' é obrigatório e não pode estar vazio.") { }
}
