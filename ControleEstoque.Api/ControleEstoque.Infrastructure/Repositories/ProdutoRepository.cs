using ControleEstoque.Domain.Entities;
using ControleEstoque.Infrastructure.Context;
using ControleEstoque.Infrastructure.Repositories.Interfaces;
using Dapper;

namespace ControleEstoque.Infrastructure.Repositories;

public class ProdutoRepository : IProdutoRepository
{
    private readonly DapperContext _context;

    public ProdutoRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<List<Produto>> ObterTodos()
    {
        var query = "SELECT * FROM Produtos WHERE Ativo = TRUE";

        using var connection = _context.CreateConnection();
        var produtos = await connection.QueryAsync<Produto>(query);
        return produtos.ToList();
    }

    public async Task<Produto?> ObterPorId(int id)
    {
        var query = "SELECT * FROM Produtos WHERE Id = @Id AND Ativo = TRUE";

        using var connection = _context.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<Produto>(query, new { Id = id });
    }

    public async Task<Produto?> ObterPorNumeroPart(string numeroPart)
    {
        var query = "SELECT * FROM Produtos WHERE NumeroPart = @NumeroPart AND Ativo = TRUE";

        using var connection = _context.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<Produto>(query, new { NumeroPart = numeroPart });
    }

    public async Task<int> Criar(Produto produto)
    {
        var query = @"INSERT INTO Produtos (NumeroPart, Nome, Preco, Quantidade, MinimaQuantidade, Ativo)
                          VALUES (@NumeroPart, @Nome, @Preco, @Quantidade, @MinimaQuantidade, @Ativo)
                          RETURNING Id;";

        using var connection = _context.CreateConnection();
        return await connection.ExecuteScalarAsync<int>(query, produto);
    }

    public async Task<bool> Atualizar(Produto produto)
    {
        var query = @"UPDATE Produtos
                          SET Nome = @Nome, Preco = @Preco, Quantidade = @Quantidade, MinimaQuantidade= @MinimaQuantidade, NumeroPart = @NumeroPart
                          WHERE Id = @Id AND Ativo = TRUE";

        using var connection = _context.CreateConnection();
        var linhasAfetadas = await connection.ExecuteAsync(query, produto);
        return linhasAfetadas > 0;
    }

    public async Task<bool> Excluir(int id)
    {
        var query = "UPDATE Produtos SET Ativo = FALSE WHERE Id = @Id";

        using var connection = _context.CreateConnection();
        var linhasAfetadas = await connection.ExecuteAsync(query, new { Id = id });
        return linhasAfetadas > 0;
    }

    public async Task<bool> ExcluirPorNumeroPart(string numeroPart)
    {
        var query = "UPDATE Produtos SET Ativo = FALSE WHERE NumeroPart = @NumeroPart";

        using var connection = _context.CreateConnection();
        var linhasAfetadas = await connection.ExecuteAsync(query, new { NumeroPart = numeroPart });
        return linhasAfetadas > 0;
    }
    public async Task<bool> AtualizarPorNumeroPart(Produto produto)
    {
        var query = @"UPDATE Produtos
                  SET Nome = @Nome, Preco = @Preco, Quantidade = @Quantidade, MinimaQuantidade = @MinimaQuantidade
                  WHERE NumeroPart = @NumeroPart AND Ativo = TRUE";

        using var connection = _context.CreateConnection();
        var linhasAfetadas = await connection.ExecuteAsync(query, produto);
        return linhasAfetadas > 0;
    }

    public async Task<bool> AtualizaEstoquePorNumeroPart(string numeroPart, int novaQuantidade)
    {
        var query = @"UPDATE Produtos
                    SET Quantidade = @novaQuantidade
                    WHERE NumeroPart = @numeroPart AND Ativo = TRUE";

        using var connection = _context.CreateConnection();
        var linhasAfetadas = await connection.ExecuteAsync(query, new
        {
            NumeroPart = numeroPart,
            NovaQuantidade  = novaQuantidade
        });
        return linhasAfetadas > 0;
    }

}
