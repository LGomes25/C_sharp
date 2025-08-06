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
            // 400 - BAD REQUEST (Requisição mal formada ou inválida)
            CampoObrigatorioException
            or ValorInvalidoException
            or EstoqueInsuficienteException
            or AlteracaoDeNumeroPartNaoPermitidaException
            or AlteracaoDeEmailNaoPermitidaException
                => new BadRequestObjectResult(new { erro = exception.Message }),

            // 401 - UNAUTHORIZED (Credenciais inválidas)
            LoginInvalidoException
                => new UnauthorizedObjectResult(new { erro = exception.Message }),

            // 404 - NOT FOUND (Recurso não encontrado)
            ProdutoPorIdNaoEncontradoException
            or ProdutoNaoEncontradoException
            or UsuarioPorIdNaoEncontradoException
            or UsuarioPorEmailNaoEncontradoException
                => new NotFoundObjectResult(new { erro = exception.Message }),

            // 409 - CONFLICT (Conflito de dados)
            ProdutoDuplicadoException
            or EmailDuplicadoException
                => new ConflictObjectResult(new { erro = exception.Message }),

            // 500 - INTERNAL SERVER ERROR (Erro inesperado na persistência ou sistema)
            FalhaAoAtualizarProdutoException
            or FalhaAoExcluirProdutoException
            or FalhaAoAtualizarUsuarioException
            or FalhaAoExcluirUsuarioException
                => new ObjectResult(new { erro = exception.Message }) { StatusCode = 500 },

            // 500 - OUTROS ERROS NÃO MAPEADOS
            _ => new ObjectResult(new { erro = "Erro interno inesperado." }) { StatusCode = 500 }
        };

        context.ExceptionHandled = true;
    }
}
