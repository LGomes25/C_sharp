using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ControleEstoque.Application.Exceptions;

namespace ControleEstoque.Api.Filters;

public class HttpResponseExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        var exception = context.Exception;

        context.Result = exception switch
        {
            // PRODUTO
            ProdutoPorIdNaoEncontradoException or ProdutoNaoEncontradoException =>
                new NotFoundObjectResult(new { erro = exception.Message }),

            EstoqueInsuficienteException or AlteracaoDeNumeroPartNaoPermitidaException =>
                new BadRequestObjectResult(new { erro = exception.Message }),

            ProdutoDuplicadoException =>
                new ConflictObjectResult(new { erro = exception.Message }),

            FalhaAoAtualizarProdutoException or FalhaAoExcluirProdutoException =>
                new ObjectResult(new { erro = exception.Message }) { StatusCode = 500 },


            // USUARIO
            UsuarioPorIdNaoEncontradoException or UsuarioPorEmailNaoEncontradoException =>
                new NotFoundObjectResult(new { erro = exception.Message }),

            EmailDuplicadoException or AlteracaoDeEmailNaoPermitidaException =>
                new BadRequestObjectResult(new { erro = exception.Message }),

            FalhaAoAtualizarUsuarioException or FalhaAoExcluirUsuarioException =>
                new ObjectResult(new { erro = exception.Message }) { StatusCode = 500 },

            //LOGIN
            LoginInvalidoException =>
                new UnauthorizedObjectResult(new { erro = exception.Message }),

            // OUTROS
            _ => new ObjectResult(new { erro = "Erro interno inesperado." }) { StatusCode = 500 }
        };

        context.ExceptionHandled = true;
    }
}
