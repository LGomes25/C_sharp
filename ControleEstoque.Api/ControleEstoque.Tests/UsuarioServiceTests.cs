using ControleEstoque.Application.DTOs.Request;
using ControleEstoque.Application.Services;
using ControleEstoque.Application.Mappers;
using ControleEstoque.Domain.Entities;
using ControleEstoque.Infrastructure.Repositories.Interfaces;
using Moq;
using System.ComponentModel;

namespace ControleEstoque.Tests;

public class UsuarioServiceTests
{
    private readonly Mock<IUsuarioRepository> _repositoryMock;
    private readonly UsuarioService _service;

    public UsuarioServiceTests()
    {
        _repositoryMock = new Mock<IUsuarioRepository>();
        _service = new UsuarioService(_repositoryMock.Object);
    }

    [Trait("Leo Gomes", "25-08-04")]
    [Description("Teste Positivo - Verifica: retorno DTO ao criar usuario, duplicidade de email")]
    [Fact]
    public async Task CriarUsuario_RetornarUsuarioResponseDto()
    {
        //Arrange
        var dto = new UsuarioRequestDto
        {
            Nome = "Leo",
            Email = "leo@gmail.com",
            Senha = "123456"
        };

        var usuarioCriado = dto.ToModel();
        usuarioCriado.Id = 1;
        usuarioCriado.Ativo = true;

        _repositoryMock.Setup(r => r.BuscarPorEmail(dto.Email)).ReturnsAsync((Usuario?)null);
        _repositoryMock.Setup(r => r.CriarUsuario(It.IsAny<Usuario>())).ReturnsAsync(1);

        // Act
        var result = await _service.CriarUsuario(dto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("Leo", result.Nome);
        Assert.Equal("leo@gmail.com", result.Email);
    }

    [Trait("Leo Gomes", "25-08-04")]
    [Description("Teste Positivo - Listar: retorna lista com todos os usuarios")]
    [Fact]
    public async Task ListarTodos_DeveRetornarListaDeUsuarios()
    {
        // Arrange
        var listaUsuarios = new List<Usuario>
        {
            new Usuario
            {
                Id = 1,
                Nome = "Leo",
                Email = "leo@gmail.com",
                Senha = "123456",
                Ativo = true
            },
            new Usuario
            {
                Id = 2,
                Nome = "Lucas",
                Email = "lucas@gmail.com",
                Senha = "123456",
                Ativo = true
            }
        };

        _repositoryMock.Setup(r => r.ListarTodos()).ReturnsAsync(listaUsuarios);

        // Act
        var resultado = await _service.ListarTodos();

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal(2, resultado.Count);
        Assert.Equal("Leo", resultado[0].Nome);
        Assert.Equal("Lucas", resultado[1].Nome);
    }

    [Trait("Leo Gomes", "25-08-04")]
    [Description("Teste Positivo - Buscar por id: retorna usuario por Id")]
    [Fact]
    public async Task BuscarPorId_DeveUsuarioPorId()
    {
        // Arrange
        var id = 1;

        var usuarioExistente = new Usuario
        {
            Id = id,
            Nome = "Leo",
            Email = "leo@gmail.com",
            Senha = "123456",
            Ativo = true
        };

        _repositoryMock.Setup(r => r.BuscarPorId(id)).ReturnsAsync(usuarioExistente);

        // Act
        var resultado = await _service.BuscarPorId(id);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal("Leo", resultado.Nome);
        Assert.Equal("leo@gmail.com", resultado.Email);
    }

    [Trait("Leo Gomes", "25-08-04")]
    [Description("Teste Positivo - Buscar por email: retorna usuario por email")]
    [Fact]
    public async Task BuscarPorEmail_DeveUsuarioPorEmail()
    {
        // Arrange
        var email = "leo@gmail.com";

        var usuarioExistente = new Usuario
        {
            Id = 1,
            Nome = "Leo",
            Email = email,
            Senha = "123456",
            Ativo = true
        };

        _repositoryMock.Setup(r => r.BuscarPorEmail(email)).ReturnsAsync(usuarioExistente);

        // Act
        var resultado = await _service.BuscarPorEmail(email);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal("Leo", resultado.Nome);
        Assert.Equal("leo@gmail.com", resultado.Email);
    }

    [Trait("Leo Gomes", "25-08-04")]
    [Description("Teste Positivo - Atualizar Usuario por Id: manter email, confirmar mudanca para persistencia")]
    [Fact]
    public async Task AtualizarUsuario_DeveAlterarCamposEDevolverUsuarioAtualizado()
    {
        // Arrange
        var id = 1;

        var usuarioExistente = new Usuario
        {
            Id = id,
            Nome = "Lucas",
            Email = "leo@gmail.com",
            Senha = "123456",
            Ativo = true
        };

        var dto = new UsuarioRequestDto
        {
            Nome = "Leo",
            Email = "leo@gmail.com",
            Senha = "123456",
        };

        _repositoryMock.Setup(r => r.BuscarPorId(id)).ReturnsAsync(usuarioExistente);
        _repositoryMock.Setup(r => r.AtualizarUsuarioPorId(It.IsAny<Usuario>())).ReturnsAsync(true);

        // Act
        var resultado = await _service.AtualizarUsuario(id, dto);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal("Leo", resultado.Nome);
        Assert.Equal("leo@gmail.com", resultado.Email);
    }

    [Trait("Leo Gomes", "25-08-04")]
    [Description("Teste Positivo - Atualizar Usuario por email: manter email, confirmar mudanca para persistencia")]
    [Fact]
    public async Task AtualizarUsuarioPorEmail_DeveAlterarCamposEDevolverUsuarioAtualizado()
    {
        // Arrange
        var email = "leo@gmail.com";

        var usuarioExistente = new Usuario
        {
            Id = 1,
            Nome = "Lucas",
            Email = email,
            Senha = "123456",
            Ativo = true
        };

        var dto = new UsuarioRequestDto
        {
            Nome = "Leo",
            Email = email,
            Senha = "123456"
        };

        _repositoryMock.Setup(r => r.BuscarPorEmail(email)).ReturnsAsync(usuarioExistente);
        _repositoryMock.Setup(r => r.AtualizarUsuarioPorEmail(It.IsAny<Usuario>())).ReturnsAsync(true);

        // Act
        var resultado = await _service.AtualizarUsuarioPorEmail(email, dto);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal("Leo", resultado.Nome);
        Assert.Equal("leo@gmail.com", resultado.Email);
    }

    [Trait("Leo Gomes", "25-08-04")]
    [Description("Teste Positivo - Excluir: remover um usuario")]
    [Fact]
    public async Task Excluir_DeveRetornarTrue_QuandoSucesso()
    {
        // Arrange
        var id = 1;

        var usuarioExistente = new Usuario
        {
            Id = id,
            Nome = "Leo",
            Email = "leo@gmail.com",
            Senha = "123456",
            Ativo = true
        };

        _repositoryMock.Setup(r => r.BuscarPorId(id)).ReturnsAsync(usuarioExistente);
        _repositoryMock.Setup(r => r.ExcluirPorId(id)).ReturnsAsync(true);

        // Act
        var resultado = await _service.ExcluirPorId(id);

        // Assert
        Assert.True(resultado);
    }

    [Trait("Leo Gomes", "25-08-04")]
    [Description("Teste Positivo - Excluir por email: remover um usuarios pelo email")]
    [Fact]
    public async Task ExcluirPorEmail_DeveRetornarTrue_QuandoSucesso()
    {
        // Arrange
        var email = "leo@gmail.com";

        var usuarioExistente = new Usuario
        {
            Id = 1,
            Nome = "Leo",
            Email = email,
            Senha = "123456",
            Ativo = true
        };

        _repositoryMock.Setup(r => r.BuscarPorEmail(email)).ReturnsAsync(usuarioExistente);
        _repositoryMock.Setup(r => r.ExcluirPorEmail(email)).ReturnsAsync(true);

        // Act
        var resultado = await _service.ExcluirPorEmail(email);

        // Assert
        Assert.True(resultado);
    }

}
