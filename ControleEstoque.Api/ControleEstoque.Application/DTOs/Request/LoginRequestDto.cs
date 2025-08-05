using System.ComponentModel.DataAnnotations;

namespace ControleEstoque.Application.DTOs.Request;

public class LoginRequestDto
{
    [Required(ErrorMessage = "O Email é obrigatório.")]
    [EmailAddress(ErrorMessage = "Email em formato inválido.")]
    public required string Email { get; set; }

    [Required(ErrorMessage = "A senha é obrigatoria.")]
    public required string Senha { get; set; }
}
