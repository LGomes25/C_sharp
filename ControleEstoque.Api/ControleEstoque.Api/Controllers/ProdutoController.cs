using ControleEstoque.Application.DTOs.Request;
using ControleEstoque.Application.DTOs.Response;
using ControleEstoque.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace ControleEstoque.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProdutoController : ControllerBase
{
    private readonly IProdutoService _produtoService;

    public ProdutoController(IProdutoService produtoService)
    {
        _produtoService = produtoService;
    }

    [HttpGet]
    public async Task<ActionResult<List<ProdutoResponseDto>>> BuscarTodos()
    {
        var produtos = await _produtoService.ListarTodos();
        return Ok(produtos);
    }

    [HttpGet("id/{id}")]
    public async Task<ActionResult<ProdutoResponseDto>> BuscarPorId(int id)
    {
        var produto = await _produtoService.BuscarPorId(id);
        return Ok(produto);
    }

    [HttpGet("part/{numeroPart}")]
    public async Task<ActionResult<ProdutoResponseDto>> BuscarPorNumeroPart(string numeroPart)
    {
        var produto = await _produtoService.BuscarPorNumeroPart(numeroPart);
        return Ok(produto);
    }

    [HttpPost]
    public async Task<ActionResult<ProdutoResponseDto>> Criar([FromBody] ProdutoRequestDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var novo = await _produtoService.CriarProduto(dto);
        return CreatedAtAction(nameof(BuscarPorId), new { id = novo.Id }, novo);
    }

    [HttpPut("id/{id}")]
    public async Task<ActionResult<ProdutoResponseDto>> Atualizar(int id, [FromBody] ProdutoRequestDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var atualizado = await _produtoService.AtualizarProduto(id, dto);
        return Ok(atualizado);
    }

    [HttpPut("part/{numeroPart}")]
    public async Task<ActionResult<ProdutoResponseDto>> AtualizarPorNumeroPart(string numeroPart, [FromBody] ProdutoRequestDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var atualizado = await _produtoService.AtualizarProdutoPorNumeroPart(numeroPart, dto);
        return Ok(atualizado);
        
    }

    [HttpPut("decrementaEstoque/{numeroPart}")]
    public async Task<ActionResult<ProdutoResponseDto>> DecrementoEstoquePorNumeroPart(string numeroPart, [FromBody] ProdutoDecrementoRequestDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var atualizado = await _produtoService.DecrementoEstoquePorNumeroPart(numeroPart,dto);
        return Ok(atualizado);
    }


    [HttpDelete("id/{id}")]
    public async Task<IActionResult> Excluir(int id)
    {
        await _produtoService.Excluir(id);
        return NoContent();
    }

    [HttpDelete("part/{numeroPart}")]
    public async Task<IActionResult> ExcluirPorNumeroPart(string numeroPart)
    {
        await _produtoService.ExcluirPorNumeroPart(numeroPart);
        return NoContent();
    }
}
