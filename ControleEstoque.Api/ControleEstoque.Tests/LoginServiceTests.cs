using ControleEstoque.Application.DTOs.Request;
using ControleEstoque.Application.Services;
using ControleEstoque.Domain.Entities;
using ControleEstoque.Infrastructure.Repositories.Interfaces;
using Moq;
using System.ComponentModel;

namespace ControleEstoque.Tests;

public class LoginServiceTests
{
    private readonly Mock<IUsuarioRepository> _repositoryMock;
    private readonly LoginService _service;

    public LoginServiceTests()
    {
        _repositoryMock = new Mock<IUsuarioRepository>();
        _service = new LoginService(_repositoryMock.Object);
    }

    [Trait("Leo Gomes", "25-08-05")]
    [Description("Teste Positivo - Verifica: usuario valido, retrna dto")]
    [Fact]
    public async Task LoginAsync_ValidadoRetornarUsuarioResponseDto()
    {
        //Arrange
        var dto = new LoginRequestDto
        {
            Email = "leo@gmail.com",
            Senha = "123456"
        };

        _repositoryMock.Setup(r => r.BuscarPorEmail(dto.Email)).ReturnsAsync(new Usuario
        {
            Id = 1,
            Nome = "Leo",
            Email = dto.Email,
            Senha = dto.Senha,
            Ativo = true
        });

        // Act
        var result = await _service.LoginAsync(dto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Leo", result.Nome);
        Assert.Equal(dto.Email, result.Email);
    }
}
