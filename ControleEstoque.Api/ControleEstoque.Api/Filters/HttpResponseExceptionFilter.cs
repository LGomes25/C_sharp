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
            ProdutoPorIdNaoEncontradoException or ProdutoNaoEncontradoException =>
                new NotFoundObjectResult(new { erro = exception.Message }),

            EstoqueInsuficienteException or AlteracaoDeNumeroPartNaoPermitidaException =>
                new BadRequestObjectResult(new { erro = exception.Message }),

            ProdutoDuplicadoException =>
                new ConflictObjectResult(new { erro = exception.Message }),

            FalhaAoAtualizarProdutoException or FalhaAoExcluirProdutoException =>
                new ObjectResult(new { erro = exception.Message })
                {
                    StatusCode = 500
                },

            _ => new ObjectResult(new { erro = "Erro interno inesperado." })
            {
                StatusCode = 500
            }
        };

        context.ExceptionHandled = true;
    }
}
