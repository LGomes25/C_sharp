using ControleEstoque.Application.DTOs.Request;
using ControleEstoque.Application.DTOs.Response;

namespace ControleEstoque.Application.Services.Interfaces;

public interface IUsuarioService
{
    Task<List<UsuarioResponseDto>> ListarTodos();
    Task<UsuarioResponseDto> BuscarPorId(int id);
    Task<UsuarioResponseDto> BuscarPorEmail(string email);
    Task<UsuarioResponseDto> CriarUsuario(UsuarioRequestDto dto);
    Task<UsuarioResponseDto> AtualizarUsuario(int id, UsuarioRequestDto dto);
    Task<UsuarioResponseDto> AtualizarUsuarioPorEmail(string email, UsuarioRequestDto dto);
    Task<bool> ExcluirPorId(int id);
    Task<bool> ExcluirPorEmail(string email);
}
