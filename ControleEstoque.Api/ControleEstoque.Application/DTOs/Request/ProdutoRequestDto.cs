using System.ComponentModel.DataAnnotations;

namespace ControleEstoque.Application.DTOs.Request;

public class ProdutoRequestDto
{
    [Required(ErrorMessage = "O nome do produto é obrigatório.")]
    public required string Nome { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero.")]
    public decimal Preco { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "A quantidade não pode ser negativa.")]
    public int Quantidade { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "A quantidade mínima não pode ser negativa.")]
    public int MinimaQuantidade { get; set; }

    [Required(ErrorMessage = "O número de parte é obrigatório.")]
    public required string NumeroPart { get; set; }
}
