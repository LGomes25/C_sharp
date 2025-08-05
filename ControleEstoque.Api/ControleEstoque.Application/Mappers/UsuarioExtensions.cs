using ControleEstoque.Application.DTOs.Request;
using ControleEstoque.Application.DTOs.Response;
using ControleEstoque.Domain.Entities;

namespace ControleEstoque.Application.Mappers;

public static class UsuarioExtensions
{
    public static UsuarioResponseDto ToResponseDto(this Usuario usuario)
    {
        return new UsuarioResponseDto
        {
            Id = usuario.Id,
            Nome = usuario.Nome,
            Email = usuario.Email
        };
    }

    public static Usuario ToModel(this UsuarioRequestDto dto)
    {
        return new Usuario
        {
            Nome = dto.Nome,
            Email = dto.Email,
            Senha = dto.Senha
        };
    }
}
