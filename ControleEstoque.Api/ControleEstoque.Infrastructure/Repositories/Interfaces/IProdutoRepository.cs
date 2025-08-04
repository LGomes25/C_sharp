using ControleEstoque.Domain.Entities;

namespace ControleEstoque.Infrastructure.Repositories.Interfaces
{
    public interface IProdutoRepository
    {
        Task<List<Produto>> ObterTodos();
        Task<Produto?> ObterPorId(int id);
        Task<Produto?> ObterPorNumeroPart(string numeroPart);
        Task<int> Criar(Produto produto);
        Task<bool> Atualizar(Produto produto);
        Task<bool> AtualizarPorNumeroPart(Produto produto);
        Task<bool> AtualizaEstoquePorNumeroPart(string numeroPart, int novaQuantidade);
        Task<bool> Excluir(int id);
        Task<bool> ExcluirPorNumeroPart(string numeroPart);
    }
}
