using ControleEstoque.Application.DTOs.Request;
using ControleEstoque.Application.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleEstoque.Application.Services.Interfaces;

public interface IProdutoService
{
    Task<List<ProdutoResponseDto>> ListarTodos();
    Task<ProdutoResponseDto?> BuscarPorId(int id);
    Task<ProdutoResponseDto?> BuscarPorNumeroPart(string numeroPart);
    Task<ProdutoResponseDto> CriarProduto(ProdutoRequestDto dto);
    Task<ProdutoResponseDto?> AtualizarProduto(int id, ProdutoRequestDto dto);
    Task<ProdutoResponseDto?> AtualizarProdutoPorNumeroPart(string numeroPart, ProdutoRequestDto dto);
    Task<ProdutoResponseDto?> DecrementoEstoquePorNumeroPart(string numeroPart, ProdutoDecrementoRequestDto dto);
    Task<bool> Excluir(int id);
    Task<bool> ExcluirPorNumeroPart(string numeroPart);
}
