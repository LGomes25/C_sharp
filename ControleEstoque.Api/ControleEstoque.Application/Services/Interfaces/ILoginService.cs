using ControleEstoque.Application.DTOs.Request;
using ControleEstoque.Application.DTOs.Response;

namespace ControleEstoque.Application.Services.Interfaces;

public interface ILoginService
{
    Task<UsuarioResponseDto> LoginAsync(LoginRequestDto dto);
}
