using System.ComponentModel.DataAnnotations;

namespace ControleEstoque.Application.DTOs.Request;

public class ProdutoDecrementoRequestDto
{
    [Required(ErrorMessage = "O número de parte é obrigatório.")]
    public required string NumeroPart { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "A quantidade requisitada não pode ser negativa.")]
    public int Quantidade { get; set; }
}
