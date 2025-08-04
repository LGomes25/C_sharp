using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ControleEstoque.Application.DTOs.Request;
using ControleEstoque.Application.DTOs.Response;
using ControleEstoque.Domain.Entities;
using ControleEstoque.Domain.Enums;

namespace ControleEstoque.Application.Mappers;

public static class ProdutoExtensions
{
    public static ProdutoResponseDto ToResponseDto(this Produto produto)
    {
        return new ProdutoResponseDto
        {
            Id = produto.Id,
            Nome = produto.Nome,
            Preco = produto.Preco,
            Quantidade = produto.Quantidade,
            MinimaQuantidade = produto.MinimaQuantidade,
            NumeroPart = produto.NumeroPart,
            MensagemEstoque = produto.Quantidade <= produto.MinimaQuantidade ? StatusEstoque.PedidoReposicao : StatusEstoque.EstoqueOk
        };
    }

    public static Produto ToModel(this ProdutoRequestDto dto)
    {
        return new Produto
        {
            Nome = dto.Nome,
            Preco = dto.Preco,
            Quantidade = dto.Quantidade,
            MinimaQuantidade = dto.MinimaQuantidade,
            NumeroPart = dto.NumeroPart
        };
    }
}

