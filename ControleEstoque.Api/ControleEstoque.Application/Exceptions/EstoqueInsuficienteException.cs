namespace ControleEstoque.Application.Exceptions;

public class EstoqueInsuficienteException : Exception
{
    public EstoqueInsuficienteException(string numeroPart, int disponivel, int requisitado)
        : base($"Estoque insuficiente para o produto '{numeroPart}'. Disponível: {disponivel}, solicitado: {requisitado}.") { }
}
