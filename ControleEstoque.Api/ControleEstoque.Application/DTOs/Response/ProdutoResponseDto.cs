using ControleEstoque.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleEstoque.Application.DTOs.Response;

public class ProdutoResponseDto
{
    public int Id { get; set; }
    public string? Nome { get; set; }
    public decimal Preco { get; set; }
    public int Quantidade { get; set; }
    public int MinimaQuantidade { get; set; }
    public string? NumeroPart { get; set; }
    public string? MensagemEstoque { get; set; }
}
