using ControleEstoque.Domain.Entities;

namespace ControleEstoque.Infrastructure.Repositories.Interfaces;

public interface IUsuarioRepository
{
    Task<List<Usuario>> ListarTodos();
    Task<Usuario?> BuscarPorId(int id);
    Task<Usuario?> BuscarPorEmail(string email);
    Task<int> CriarUsuario(Usuario usuario);
    Task<bool> AtualizarUsuarioPorId(Usuario usuario);
    Task<bool> AtualizarUsuarioPorEmail(Usuario usuario);
    Task<bool> ExcluirPorId(int id);
    Task<bool> ExcluirPorEmail(string email);
}
