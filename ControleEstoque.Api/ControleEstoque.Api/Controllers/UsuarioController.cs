using ControleEstoque.Application.DTOs.Request;
using ControleEstoque.Application.DTOs.Response;
using ControleEstoque.Application.Services.Interfaces;
using ControleEstoque.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ControleEstoque.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;

    public UsuarioController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Usuario>>> ListarTodos()
    {
        var usuarios = await _usuarioService.ListarTodos();
        return Ok(usuarios);
    }

    [HttpGet("id/{id}")]
    public async Task<ActionResult<UsuarioResponseDto>> BuscarPorId(int id)
    {
        var usuario = await _usuarioService.BuscarPorId(id);
        return Ok(usuario);
    }

    [HttpGet("email/{email}")]
    public async Task<ActionResult<UsuarioResponseDto>> BuscarPorEmail(string email)
    {
        var usuario = await _usuarioService.BuscarPorEmail(email);
        return Ok(usuario);
    }

    [HttpPost]
    public async Task<ActionResult<UsuarioResponseDto>> CriarUsuario([FromBody] UsuarioRequestDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var novoUsuario = await _usuarioService.CriarUsuario(dto);
        return CreatedAtAction(nameof(BuscarPorId), new { id = novoUsuario.Id }, novoUsuario);
    }

    [HttpPut("id/{id}")]
    public async Task<ActionResult<UsuarioResponseDto>> AtualizarUsuario(int id, [FromBody] UsuarioRequestDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var atualizado = await _usuarioService.AtualizarUsuario(id, dto);
        return Ok(atualizado);
    }

    [HttpPut("email/{email}")]
    public async Task<ActionResult<UsuarioResponseDto>> AtualizarUsuarioPorEmail(string email, [FromBody] UsuarioRequestDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var atualizado = await _usuarioService.AtualizarUsuarioPorEmail(email, dto);
        return Ok(atualizado);
    }

    [HttpDelete("id/{id}")]
    public async Task<IActionResult> ExcluirPorId(int id)
    {
        await _usuarioService.ExcluirPorId(id);
        return NoContent();
    }

    [HttpDelete("email/{email}")]
    public async Task<IActionResult> ExcluirPorEmail(string email)
    {
        await _usuarioService.ExcluirPorEmail(email);
        return NoContent();
    }

}
