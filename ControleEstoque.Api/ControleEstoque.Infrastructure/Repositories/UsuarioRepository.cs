using ControleEstoque.Domain.Entities;
using ControleEstoque.Infrastructure.Context;
using ControleEstoque.Infrastructure.Repositories.Interfaces;
using Dapper;

namespace ControleEstoque.Infrastructure.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly DapperContext _context;

    public UsuarioRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<List<Usuario>> ListarTodos()
    {
        var query = "SELECT * FROM Usuarios WHERE Ativo = TRUE";

        using var connection = _context.CreateConnection();
        var usuarios = await connection.QueryAsync<Usuario>(query);
        return usuarios.ToList();
    }

    public async Task<Usuario?> BuscarPorId(int id)
    {
        var query = "SELECT * FROM Usuarios WHERE Id = @Id AND Ativo = TRUE";

        using var connection = _context.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<Usuario>(query, new { Id = id });
    }

    public async Task<Usuario?> BuscarPorEmail(string email)
    {
        var query = "SELECT * FROM Usuarios WHERE Email = @Email AND Ativo = TRUE";

        using var connection = _context.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<Usuario>(query, new { Email = email });
    }

    public async Task<int> CriarUsuario(Usuario usuario)
    {
        var query = @"INSERT INTO Usuarios (Email, Nome, Senha, Ativo)
                          VALUES (@Email, @Nome, @Senha, @Ativo)
                          RETURNING Id;";

        using var connection = _context.CreateConnection();
        return await connection.ExecuteScalarAsync<int>(query, usuario);
    }

    public async Task<bool> AtualizarUsuarioPorId(Usuario usuario)
    {
        var query = @"UPDATE Usuarios
                          SET Nome = @Nome, Email = @Email, Senha = @Senha
                          WHERE Id = @Id AND Ativo = TRUE";

        using var connection = _context.CreateConnection();
        var linhasAfetadas = await connection.ExecuteAsync(query, usuario);
        return linhasAfetadas > 0;
    }

    public async Task<bool> AtualizarUsuarioPorEmail(Usuario usuario)
    {
        var query = @"UPDATE Usuarios
                          SET Nome = @Nome, Email = @Email, Senha = @Senha
                          WHERE Email = @Email AND Ativo = TRUE";

        using var connection = _context.CreateConnection();
        var linhasAfetadas = await connection.ExecuteAsync(query, usuario);
        return linhasAfetadas > 0;
    }

    public async Task<bool> ExcluirPorId(int id)
    {
        var query = "UPDATE Usuarios SET Ativo = FALSE WHERE Id = @Id";

        using var connection = _context.CreateConnection();
        var linhasAfetadas = await connection.ExecuteAsync(query, new { Id = id });
        return linhasAfetadas > 0;
    }

    public async Task<bool> ExcluirPorEmail(string email)
    {
        var query = "UPDATE Usuarios SET Ativo = FALSE WHERE Email = @Email";

        using var connection = _context.CreateConnection();
        var linhasAfetadas = await connection.ExecuteAsync(query, new { Email = email });
        return linhasAfetadas > 0;
    }

}
