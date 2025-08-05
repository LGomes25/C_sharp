using ControleEstoque.Application.DTOs.Request;
using ControleEstoque.Application.DTOs.Response;
using ControleEstoque.Application.Exceptions;
using ControleEstoque.Application.Mappers;
using ControleEstoque.Application.Services.Interfaces;
using ControleEstoque.Infrastructure.Repositories.Interfaces;

namespace ControleEstoque.Application.Services;

public class LoginService : ILoginService
{
    private readonly IUsuarioRepository _usuarioRepository;

    public LoginService(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task<UsuarioResponseDto> LoginAsync(LoginRequestDto dto)
    {
        var usuario = await _usuarioRepository.BuscarPorEmail(dto.Email);

        if (usuario == null)
            throw new LoginInvalidoException();

        return usuario.ToResponseDto();
    }
}
