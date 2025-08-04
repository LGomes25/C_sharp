using ControleEstoque.Application.DTOs.Request;
using ControleEstoque.Application.DTOs.Response;
using ControleEstoque.Application.Exceptions;
using ControleEstoque.Application.Mappers;
using ControleEstoque.Application.Services.Interfaces;
using ControleEstoque.Infrastructure.Repositories.Interfaces;

namespace ControleEstoque.Application.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoService(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task<List<ProdutoResponseDto>> ListarTodos()
        {
            var produtos = await _produtoRepository.ObterTodos();
            return produtos.Select(p => p.ToResponseDto()).ToList();
        }

        public async Task<ProdutoResponseDto?> BuscarPorId(int id)
        {
            var produto = await _produtoRepository.ObterPorId(id);

            if (produto == null) 
                throw new ProdutoPorIdNaoEncontradoException(id);

            return produto.ToResponseDto();
        }

        public async Task<ProdutoResponseDto?> BuscarPorNumeroPart(string numeroPart)
        {
            var produto = await _produtoRepository.ObterPorNumeroPart(numeroPart);

            if (produto == null)
                throw new ProdutoNaoEncontradoException(numeroPart);

            return produto.ToResponseDto();
        }

        public async Task<ProdutoResponseDto> CriarProduto(ProdutoRequestDto dto)
        {
            var numeroPartExistente = await _produtoRepository.ObterPorNumeroPart(dto.NumeroPart);
            if (numeroPartExistente != null)
                throw new ProdutoDuplicadoException(dto.NumeroPart);

            var produto = dto.ToModel();
            produto.Ativo = true;

            produto.Id = await _produtoRepository.Criar(produto);
            return produto.ToResponseDto();
        }

        public async Task<ProdutoResponseDto?> AtualizarProduto(int id, ProdutoRequestDto dto)
        {
            var produto = await _produtoRepository.ObterPorId(id);
            if (produto == null)
                throw new ProdutoPorIdNaoEncontradoException(id);

            if (produto.NumeroPart != dto.NumeroPart)
                throw new AlteracaoDeNumeroPartNaoPermitidaException(produto.NumeroPart, dto.NumeroPart);


            produto.Nome = dto.Nome;
            produto.Preco = dto.Preco;
            produto.Quantidade = dto.Quantidade;
            produto.MinimaQuantidade = dto.MinimaQuantidade;

            var atualizado = await _produtoRepository.Atualizar(produto);

            if (!atualizado)
                throw new FalhaAoAtualizarProdutoException(produto.NumeroPart);

            return produto.ToResponseDto();
        }

        public async Task<ProdutoResponseDto?> AtualizarProdutoPorNumeroPart(string numeroPart, ProdutoRequestDto dto)
        {
            var produto = await _produtoRepository.ObterPorNumeroPart(numeroPart);
            if (produto == null)
                throw new ProdutoNaoEncontradoException(numeroPart);

            if (produto.NumeroPart != dto.NumeroPart)
                throw new AlteracaoDeNumeroPartNaoPermitidaException(produto.NumeroPart, dto.NumeroPart);

            produto.Nome = dto.Nome;
            produto.Preco = dto.Preco;
            produto.Quantidade = dto.Quantidade;
            produto.MinimaQuantidade= dto.MinimaQuantidade;

            var atualizado = await _produtoRepository.AtualizarPorNumeroPart(produto);

            if (!atualizado)
                throw new FalhaAoAtualizarProdutoException(produto.NumeroPart);

            return produto.ToResponseDto();
        }

        public async Task<ProdutoResponseDto?> DecrementoEstoquePorNumeroPart(string numeroPart, ProdutoDecrementoRequestDto dto)
        {
            var produto = await _produtoRepository.ObterPorNumeroPart(numeroPart);
            if (produto == null) 
                throw new ProdutoNaoEncontradoException(numeroPart);

            if (produto.Quantidade < dto.Quantidade) 
                throw new EstoqueInsuficienteException(numeroPart, produto.Quantidade, dto.Quantidade);

            var novaQuantidade = produto.Quantidade - dto.Quantidade;

            var atualizado = await _produtoRepository.AtualizaEstoquePorNumeroPart(numeroPart, novaQuantidade);

            if (!atualizado)
                throw new FalhaAoAtualizarProdutoException(produto.NumeroPart);

            produto.Quantidade = novaQuantidade;

            return produto.ToResponseDto();

        }

        public async Task<bool> Excluir(int id)
        {
            var produto = await _produtoRepository.ObterPorId(id);
            if (produto == null)
                throw new ProdutoPorIdNaoEncontradoException(id);

            var remover = await _produtoRepository.Excluir(id);
            if (!remover)
                throw new FalhaAoExcluirProdutoException($"ID {id}");

            return true;
        }

        public async Task<bool> ExcluirPorNumeroPart(string numeroPart)
        {
            var produto = await _produtoRepository.ObterPorNumeroPart(numeroPart);
            if (produto == null)
                throw new ProdutoNaoEncontradoException(numeroPart);

            var remover = await _produtoRepository.ExcluirPorNumeroPart(numeroPart);
            if (!remover)
                throw new FalhaAoExcluirProdutoException($"numeroPart {numeroPart}");

            return true;
        }
    }
}
