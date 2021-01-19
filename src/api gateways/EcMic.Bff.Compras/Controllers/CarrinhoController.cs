using EcMic.Bff.Compras.Models;
using EcMic.Bff.Compras.Services;
using EcMic.Bff.Compras.Services.gRPC;
using EMic.WebApi.Core.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EcMic.Bff.Compras.Controllers
{
    [Authorize]
    public class CarrinhoController : MainController
    {
        private readonly ICarrinhoService _carrinhoService;
        private readonly ICarrinhoGrpcService _carrinhoGrpcService;
        private readonly ICatalogoService _catalogoService;
        private readonly IPedidoService _pedidoService;

        public CarrinhoController(ICarrinhoService ICarrinhoService,
                                  ICarrinhoGrpcService ICarrinhoGrpcService,
                                  ICatalogoService ICatalogoService,
                                  IPedidoService   IPedidoService)
        {
            _carrinhoService = ICarrinhoService;
            _catalogoService = ICatalogoService;
            _pedidoService   = IPedidoService;
            _carrinhoGrpcService = ICarrinhoGrpcService;
        }

        [HttpGet]
        [Route("compras/carrinho")]
        public async Task<IActionResult> Index()
        {
            return CustomResponse(await _carrinhoGrpcService.ObterCarrinho());
        }

        [HttpGet]
        [Route("compras/carrinho-quantidade")]
        public async Task<int> ObterQuantidadeCarrinho()
        {
            var quantidade = await _carrinhoGrpcService.ObterCarrinho();
            return quantidade?.Itens.Sum(s => s.Quantidade) ?? 0;
        }

        [HttpPost]
        [Route("compras/carrinho/items")]
        public async Task<IActionResult> AdicionarItemCarrinho(ItemCarrinhoDTO itemProduto)
        {
            var produto = await _catalogoService.ObterPorId(itemProduto.ProdutoId);

            await ValidarItemCarrinho(produto, itemProduto.Quantidade);

            if (!OperacaoValida()) return CustomResponse();

            itemProduto.Nome   = produto.Nome;
            itemProduto.Valor  = produto.Valor;
            itemProduto.Imagem = produto.Imagem;

            var resposta = await _carrinhoService.AdicionarItemCarrinho(itemProduto);

            return CustomResponse(resposta);
        }

        [HttpPut]
        [Route("compras/carrinho/items/{produtoId}")]
        public async Task<IActionResult> AtualizarItemCarrinho(Guid produtoId, ItemCarrinhoDTO itemProduto)
        {
            var produto = await _catalogoService.ObterPorId(produtoId);

            await ValidarItemCarrinho(produto, itemProduto.Quantidade);
            if (!OperacaoValida()) return CustomResponse();

            var resposta = await _carrinhoService.AtualizarItemCarrinho(produtoId, itemProduto);

            return CustomResponse(resposta);
        }

        [HttpDelete]
        [Route("compras/carrinho/items/{produtoId}")]
        public async Task<IActionResult> RemoverItemCarrinho(Guid produtoId)
        {
            var produto = await _catalogoService.ObterPorId(produtoId);

            if(produto == null)
            {
                AdicionarErroProcessamento("Produto informado não existe");
                return CustomResponse();
            }

            var resposta = await _carrinhoService.RemoverItemCarrinho(produtoId);

            return CustomResponse(resposta);
        }

        [HttpPost]
        [Route("compras/carrinho/aplicar-voucher")]
        public async Task<IActionResult> AplicarVoucher([FromBody] string voucherCodigo)
        {
            var voucher = await _pedidoService.ObterVoucherPorCodigo(voucherCodigo);

            if(voucher is null)
            {
                AdicionarErroProcessamento("Voucher inválido ou não encontrado");
                return CustomResponse();
            }

            var resposta = await _carrinhoService.AplicarVoucherCarrinho(voucher);

            return CustomResponse(resposta);
        }

        private async Task ValidarItemCarrinho(ItemProdutoDTO produto, int quantidade)
        {
            if (produto == null)  AdicionarErroProcessamento("Produto não inexistente");

            if (quantidade < 1)  AdicionarErroProcessamento($"Informe ao menos uma quantidade para o produto: {produto.Nome}");

            var carrinho = await _carrinhoService.ObterCarrinho();
            var itemCarrinho = carrinho.Itens.FirstOrDefault(f => f.ProdutoId == produto.Id);

            if(itemCarrinho != null && itemCarrinho.Quantidade + quantidade > produto.QuantidadeEstoque)
            {
                AdicionarErroProcessamento($"O produto {produto.Nome} possui {produto.QuantidadeEstoque} unidades em estoque, você selecionou {quantidade}    ");
                return;
            }

            if (quantidade > produto.QuantidadeEstoque)
                AdicionarErroProcessamento($"O produto {produto.Nome} possui {produto.QuantidadeEstoque} em estoque");
        }
    }
}
