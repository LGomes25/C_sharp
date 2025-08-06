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
        if (string.IsNullOrWhiteSpace(dto.Email))
            throw new CampoObrigatorioException("email");

        if (string.IsNullOrWhiteSpace(dto.Senha))
            throw new CampoObrigatorioException("senha");

        var usuario = await _usuarioRepository.BuscarPorEmail(dto.Email);

        if (usuario == null || usuario.Senha != dto.Senha)
            throw new LoginInvalidoException();

        return usuario.ToResponseDto();
    }
}
