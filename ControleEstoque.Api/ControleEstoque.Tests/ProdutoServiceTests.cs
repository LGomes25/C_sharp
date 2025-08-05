using ControleEstoque.Application.DTOs.Request;
using ControleEstoque.Application.Services;
using ControleEstoque.Application.Mappers;
using ControleEstoque.Domain.Entities;
using ControleEstoque.Infrastructure.Repositories.Interfaces;
using Moq;
using System.ComponentModel;

namespace ControleEstoque.Tests;

public class ProdutoServiceTests
{
    private readonly Mock<IProdutoRepository> _repositoryMock;
    private readonly ProdutoService _service;

    public ProdutoServiceTests()
    {
        _repositoryMock = new Mock<IProdutoRepository>();
        _service = new ProdutoService( _repositoryMock.Object );
    }

    [Trait("Leo Gomes", "25-08-04")]
    [Description("Teste Positivo - Verifica: retorno DTO ao criar produto, duplicidade de numeroPart")]
    [Fact]
    public async Task CriarProduto_RetornarProdutoResponseDto()
    {
        //Arrange
        var dto = new ProdutoRequestDto
        {
            Nome = "Anel de vedacao 221",
            NumeroPart = "000.25221",
            Preco = 1.24m,
            Quantidade = 50,
            MinimaQuantidade = 5
        };

        var produtoCriado = dto.ToModel();
        produtoCriado.Id = 1;
        produtoCriado.Ativo = true;

        _repositoryMock.Setup(r => r.ObterPorNumeroPart(dto.NumeroPart)).ReturnsAsync((Produto?)null);
        _repositoryMock.Setup(r => r.Criar(It.IsAny<Produto>())).ReturnsAsync(1);

        // Act
        var result = await _service.CriarProduto(dto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("Anel de vedacao 221", result.Nome);
        Assert.Equal("Estoque ok", result.MensagemEstoque);
    }

    [Trait("Leo Gomes", "25-08-04")]
    [Description("Teste Positivo - Listar: retorna lista com todos os produtos")]
    [Fact]
    public async Task ListarTodos_DeveRetornarListaDeProdutos()
    {
        // Arrange
        var listaProdutos = new List<Produto>
        {
            new Produto
            {
                Id = 1,
                Nome = "Anel de vedacao 221",
                NumeroPart = "000.25221",
                Preco = 1.24m,
                Quantidade = 50,
                MinimaQuantidade = 5,
                Ativo = true
            },
            new Produto
            {
                Id = 2,
                Nome = "Bateria de Litio - 7cell",
                NumeroPart = "776.00876",
                Preco = 2500.56m,
                Quantidade = 60,
                MinimaQuantidade = 6,
                Ativo = true
            }
        };

        _repositoryMock.Setup(r => r.ObterTodos()).ReturnsAsync(listaProdutos);

        // Act
        var resultado = await _service.ListarTodos();

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal(2, resultado.Count);
        Assert.Equal("Anel de vedacao 221", resultado[0].Nome);
        Assert.Equal("Bateria de Litio - 7cell", resultado[1].Nome);
    }

    [Trait("Leo Gomes", "25-08-04")]
    [Description("Teste Positivo - Buscar por id: retorna produto por Id")]
    [Fact]
    public async Task BuscarPorId_DeveProdutoPorId()
    {
        // Arrange
        var id = 1;

        var produtoExistente = new Produto
        {
            Id = id,
            Nome = "Anel de vedacao 221",
            NumeroPart = "000.25221",
            Preco = 1.26m,
            Quantidade = 50,
            MinimaQuantidade = 20,
            Ativo = true
        };

        _repositoryMock.Setup(r => r.ObterPorId(id)).ReturnsAsync(produtoExistente);

        // Act
        var resultado = await _service.BuscarPorId(id);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal("Anel de vedacao 221", resultado.Nome);
        Assert.Equal("000.25221", resultado.NumeroPart);
    }

    [Trait("Leo Gomes", "25-08-04")]
    [Description("Teste Positivo - Buscar por numeroPart: retorna produto por numeroPart")]
    [Fact]
    public async Task BuscarPorNumeroPart_DeveProdutoPorId()
    {
        // Arrange
        var numeroPart = "000.25221";

        var produtoExistente = new Produto
        {
            Id = 1,
            Nome = "Anel de vedacao 221",
            NumeroPart = numeroPart,
            Preco = 1.26m,
            Quantidade = 50,
            MinimaQuantidade = 20,
            Ativo = true
        };

        _repositoryMock.Setup(r => r.ObterPorNumeroPart(numeroPart)).ReturnsAsync(produtoExistente);

        // Act
        var resultado = await _service.BuscarPorNumeroPart(numeroPart);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal("Anel de vedacao 221", resultado.Nome);
        Assert.Equal("000.25221", resultado.NumeroPart);
    }

    [Trait("Leo Gomes", "25-08-04")]
    [Description("Teste Positivo - Atualizar Produto: manter numeroPart, confirmar mudanca para persistencia")]
    [Fact]
    public async Task AtualizarProduto_DeveAlterarCamposEDevolverProdutoAtualizado()
    {
        // Arrange
        var id = 1;

        var produtoExistente = new Produto
        {
            Id = id,
            Nome = "Anel vedacao",
            NumeroPart = "000.25221",
            Preco = 1.26m,
            Quantidade = 50,
            MinimaQuantidade = 20,
            Ativo = true
        };

        var dto = new ProdutoRequestDto
        {
            Nome = "Anel vedacao 221",
            NumeroPart = "000.25221", 
            Preco = 1.52m,
            Quantidade = 80,
            MinimaQuantidade = 30
        };

        _repositoryMock.Setup(r => r.ObterPorId(id)).ReturnsAsync(produtoExistente);
        _repositoryMock.Setup(r => r.Atualizar(It.IsAny<Produto>())).ReturnsAsync(true);

        // Act
        var resultado = await _service.AtualizarProduto(id, dto);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal("Anel vedacao 221", resultado.Nome);
        Assert.Equal(1.52m, resultado.Preco);
        Assert.Equal(80, resultado.Quantidade);
    }

    [Trait("Leo Gomes", "25-08-04")]
    [Description("Teste Positivo - Atualizar Produto por numeroPart: manter numeroPart, confirmar mudanca para persistencia")]
    [Fact]
    public async Task AtualizarProdutoPorNumeroPart_DeveAlterarCamposEDevolverProdutoAtualizado()
    {
        // Arrange
        var numeroPart = "000.25221";

        var produtoExistente = new Produto
        {
            Id = 1,
            Nome = "Anel vedacao",
            NumeroPart = numeroPart,
            Preco = 1.26m,
            Quantidade = 50,
            MinimaQuantidade = 20,
            Ativo = true
        };

        var dto = new ProdutoRequestDto
        {
            Nome = "Anel vedacao 221",
            NumeroPart = numeroPart,
            Preco = 1.52m,
            Quantidade = 80,
            MinimaQuantidade = 30
        };

        _repositoryMock.Setup(r => r.ObterPorNumeroPart(numeroPart)).ReturnsAsync(produtoExistente);
        _repositoryMock.Setup(r => r.AtualizarPorNumeroPart(It.IsAny<Produto>())).ReturnsAsync(true);

        // Act
        var resultado = await _service.AtualizarProdutoPorNumeroPart(numeroPart, dto);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal("Anel vedacao 221", resultado.Nome);
        Assert.Equal(1.52m, resultado.Preco);
        Assert.Equal(80, resultado.Quantidade);
    }

    [Trait("Leo Gomes", "25-08-04")]
    [Description("Teste Positivo - Excluir: remover um produto")]
    [Fact]
    public async Task Excluir_DeveRetornarTrue_QuandoSucesso()
    {
        // Arrange
        var id = 1;

        var produtoExistente = new Produto
        {
            Id = id,
            Nome = "Anel vedacao 221",
            NumeroPart = "000.25221",
            Preco = 1.26m,
            Quantidade = 50,
            MinimaQuantidade = 20,
            Ativo = true
        };

        _repositoryMock.Setup(r => r.ObterPorId(id)).ReturnsAsync(produtoExistente);
        _repositoryMock.Setup(r => r.Excluir(id)).ReturnsAsync(true);

        // Act
        var resultado = await _service.Excluir(id);

        // Assert
        Assert.True(resultado);
    }

    [Trait("Leo Gomes", "25-08-04")]
    [Description("Teste Positivo - Excluir por numeroPart: remover um produto pelo numeroPart")]
    [Fact]
    public async Task ExcluirPorNumeroPart_DeveRetornarTrue_QuandoSucesso()
    {
        // Arrange
        var numeroPart = "000.25221";

        var produtoExistente = new Produto
        {
            Id = 1,
            Nome = "Anel vedacao 221",
            NumeroPart = numeroPart,
            Preco = 1.26m,
            Quantidade = 50,
            MinimaQuantidade = 20,
            Ativo = true
        };

        _repositoryMock.Setup(r => r.ObterPorNumeroPart(numeroPart)).ReturnsAsync(produtoExistente);
        _repositoryMock.Setup(r => r.ExcluirPorNumeroPart(numeroPart)).ReturnsAsync(true);

        // Act
        var resultado = await _service.ExcluirPorNumeroPart(numeroPart);

        // Assert
        Assert.True(resultado);
    }

    [Trait("Leo Gomes", "25-08-04")]
    [Description("Teste Positivo - Decrementar estoque: diminuir saldo de unidades do estoque")]
    [Fact]
    public async Task DecrementoEstoque_DeveAtualizarQuantidadeERetornarProduto()
    {
        // Arrange
        var numeroPart = "000.25221";

        var produtoExistente = new Produto
        {
            Id = 1,
            Nome = "Anel vedacao 221",
            NumeroPart = numeroPart,
            Preco = 1.26m,
            Quantidade = 50,
            MinimaQuantidade = 20,
            Ativo = true
        };

        var dto = new ProdutoDecrementoRequestDto
        {
            NumeroPart = numeroPart,
            Quantidade = 3
        };

        _repositoryMock.Setup(r => r.ObterPorNumeroPart(numeroPart)).ReturnsAsync(produtoExistente);

        _repositoryMock.Setup(r => r.AtualizaEstoquePorNumeroPart(numeroPart, 47)).ReturnsAsync(true);

        // Act
        var resultado = await _service.DecrementoEstoquePorNumeroPart(numeroPart, dto);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal(47, resultado.Quantidade);
        Assert.Equal("Anel vedacao 221", resultado.Nome);
    }
}
