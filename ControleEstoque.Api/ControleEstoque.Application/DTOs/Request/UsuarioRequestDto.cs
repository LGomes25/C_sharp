using System.ComponentModel.DataAnnotations;

namespace ControleEstoque.Application.DTOs.Request;

public class UsuarioRequestDto
{
    [Required(ErrorMessage ="O nome é obrigatorio.")]
    public required string Nome { get; set; }

    [Required(ErrorMessage ="O Email é obrigatório.")]
    [EmailAddress(ErrorMessage = "Email em formato inválido.")]
    public required string Email { get; set; }

    [Required(ErrorMessage ="A senha é obrigatoria.")]
    [MinLength(6, ErrorMessage = "A senha deve ter pelo menos 6 caracteres.")]
    public required string Senha { get; set; }
}
