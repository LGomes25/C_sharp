using ControleEstoque.Application.DTOs.Request;
using ControleEstoque.Application.DTOs.Response;
using ControleEstoque.Application.Exceptions;
using ControleEstoque.Application.Mappers;
using ControleEstoque.Application.Services.Interfaces;
using ControleEstoque.Infrastructure.Repositories.Interfaces;

namespace ControleEstoque.Application.Services;

public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository;

    public UsuarioService(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task<List<UsuarioResponseDto>> ListarTodos()
    {
        var usuarios = await _usuarioRepository.ListarTodos();
        return usuarios.Select(u => u.ToResponseDto()).ToList();
    }

    public async Task<UsuarioResponseDto> BuscarPorId(int id)
    {
        if (id <= 0)
            throw new CampoObrigatorioException("id");

        var usuario = await _usuarioRepository.BuscarPorId(id);

        if (usuario == null)
            throw new UsuarioPorIdNaoEncontradoException(id);

        return usuario.ToResponseDto();
    }

    public async Task<UsuarioResponseDto> BuscarPorEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new CampoObrigatorioException("email");

        var usuario = await _usuarioRepository.BuscarPorEmail(email);

        if (usuario == null)
            throw new UsuarioPorEmailNaoEncontradoException(email);

        return usuario.ToResponseDto();
    }

    public async Task<UsuarioResponseDto> CriarUsuario(UsuarioRequestDto dto)
    {
        var emailExitente = await _usuarioRepository.BuscarPorEmail(dto.Email);

        if (emailExitente != null)
            throw new EmailDuplicadoException(dto.Email);

        if (string.IsNullOrWhiteSpace(dto.Nome))
            throw new CampoObrigatorioException("nome");

        if (string.IsNullOrWhiteSpace(dto.Email))
            throw new CampoObrigatorioException("email");

        if (string.IsNullOrWhiteSpace(dto.Senha))
            throw new CampoObrigatorioException("senha");

        var usuario = dto.ToModel();
        usuario.Ativo = true;

        usuario.Id = await _usuarioRepository.CriarUsuario(usuario);
        return usuario.ToResponseDto();
    }

    public async Task<UsuarioResponseDto> AtualizarUsuario(int id, UsuarioRequestDto dto)
    {
        if (id <= 0)
            throw new CampoObrigatorioException("id");

        var usuarioExistente = await _usuarioRepository.BuscarPorId(id);

        if (usuarioExistente == null)
            throw new UsuarioPorIdNaoEncontradoException(id);

        if (string.IsNullOrWhiteSpace(dto.Nome))
            throw new CampoObrigatorioException("nome");

        if (string.IsNullOrWhiteSpace(dto.Email))
            throw new CampoObrigatorioException("email");

        if (string.IsNullOrWhiteSpace(dto.Senha))
            throw new CampoObrigatorioException("senha");

        usuarioExistente.Nome = dto.Nome;
        usuarioExistente.Email = dto.Email;
        usuarioExistente.Senha = dto.Senha;

        var atualizado = await _usuarioRepository.AtualizarUsuarioPorId(usuarioExistente);

        if(!atualizado)
            throw new FalhaAoAtualizarUsuarioException(usuarioExistente.Nome);

        return usuarioExistente.ToResponseDto();
    }

    public async Task<UsuarioResponseDto> AtualizarUsuarioPorEmail(string email, UsuarioRequestDto dto)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new CampoObrigatorioException("email");

        var usuarioExistente = await _usuarioRepository.BuscarPorEmail(email);

        if (usuarioExistente == null)
            throw new UsuarioPorEmailNaoEncontradoException(email);

        if (string.IsNullOrWhiteSpace(dto.Nome))
            throw new CampoObrigatorioException("nome");

        if (string.IsNullOrWhiteSpace(dto.Email))
            throw new CampoObrigatorioException("email");

        if (string.IsNullOrWhiteSpace(dto.Senha))
            throw new CampoObrigatorioException("senha");

        if (usuarioExistente.Email != dto.Email)
            throw new AlteracaoDeEmailNaoPermitidaException(usuarioExistente.Email, dto.Email);

        usuarioExistente.Nome = dto.Nome;
        usuarioExistente.Email = dto.Email;
        usuarioExistente.Senha = dto.Senha;

        var atualizado = await _usuarioRepository.AtualizarUsuarioPorEmail(usuarioExistente);

        if (!atualizado)
            throw new FalhaAoAtualizarUsuarioException(usuarioExistente.Nome);

        return usuarioExistente.ToResponseDto();
    }

    public async Task<bool> ExcluirPorId(int id)
    {
        if (id <= 0)
            throw new CampoObrigatorioException("id");

        var usuarioExistente = await _usuarioRepository.BuscarPorId(id);

        if (usuarioExistente == null)
            throw new UsuarioPorIdNaoEncontradoException(id);

        var remover = await _usuarioRepository.ExcluirPorId(id);

        if (!remover)
            throw new FalhaAoExcluirUsuarioException($"ID {id}");

        return true;
    }

    public async Task<bool> ExcluirPorEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new CampoObrigatorioException("email");

        var usuarioExistente = await _usuarioRepository.BuscarPorEmail(email);

        if (usuarioExistente == null)
            throw new UsuarioPorEmailNaoEncontradoException(email);

        var remover = await _usuarioRepository.ExcluirPorEmail(email);

        if (!remover)
            throw new FalhaAoExcluirUsuarioException($"Email {email}");

        return true;
    }
}
