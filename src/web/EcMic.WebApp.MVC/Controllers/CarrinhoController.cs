using EcMic.WebApp.MVC.Models;
using EcMic.WebApp.MVC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcMic.WebApp.MVC.Controllers
{
    [Authorize]
    public class CarrinhoController : MainController
    {
        private readonly ICarrinhoService _carrinhoService;
        private readonly ICatalogoService _catalogoService;

        public CarrinhoController(ICarrinhoService ICarrinhoService, ICatalogoService ICatalogoService)
        {
            _carrinhoService = ICarrinhoService;
            _catalogoService = ICatalogoService;
        }

        [Route("Carrinho")]
        public async Task<IActionResult> Index()
        {
            return View(await _carrinhoService.ObterCarrinho());
        }

        [HttpPost]
        [Route("carrinho/adicionar-item")]
        public async Task<IActionResult> adicionarItemCarrinho(ItemProdutoViewModel itemProduto)
        {
            var produto = await _catalogoService.ObterPorId(itemProduto.ProdutoId);

            ValidarItemCarrinho(produto, itemProduto.Quantidade);
            if(!OperacaoValida()) return View("Index", await _carrinhoService.ObterCarrinho());

            itemProduto.Nome = produto.Nome;
            itemProduto.Valor = produto.Valor;
            itemProduto.Imagem = produto.Imagem;

            var resposta = await _carrinhoService.AdicionarItemCarrinho(itemProduto);

            if (ResponsePossuiErros(resposta)) return View("Index", await _carrinhoService.ObterCarrinho());

            return RedirectToAction("Index");

            //Diferença entre o retorno da View e o RedirectToAction neste caso: O Retorno "View", mostra os erros.
        }

        [HttpPost]
        [Route("carrinho/atualizar-item")]
        public async Task<IActionResult> AtualizarItemCarrinho(Guid produtoId, int quantidade)
        {
            var produto = await _catalogoService.ObterPorId(produtoId);

            ValidarItemCarrinho(produto, quantidade);
            if (!OperacaoValida()) return View("Index", await _carrinhoService.ObterCarrinho());

            var itemProduto = new ItemProdutoViewModel { ProdutoId = produtoId, Quantidade = quantidade };
            var resposta = await _carrinhoService.AtualizarItemCarrinho(produtoId, itemProduto);

            if (ResponsePossuiErros(resposta)) return View("Index", await _carrinhoService.ObterCarrinho());

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("carrinho/remover-item")]
        public async Task<IActionResult> removerItemCarrinho(Guid produtoId)
        {
            var produto = await _catalogoService.ObterPorId(produtoId);

            if (produto == null)
            {
                AdicionarErroValidacao("Produto informado não existe");
                return View("Index", await _carrinhoService.ObterCarrinho());
            }

            var resposta = await _carrinhoService.RemoverItemCarrinho(produtoId);
            if (ResponsePossuiErros(resposta)) return View("Index", await _carrinhoService.ObterCarrinho());

            return RedirectToAction("Index");
        }

        private void ValidarItemCarrinho(ProdutoViewModel produto, int quantidadeSelecionada)
        {
            if (produto == null) 
                AdicionarErroValidacao("Produto não existe");

            if (quantidadeSelecionada < 1) 
                AdicionarErroValidacao($"Informe ao menos uma quantidade para o produto: {produto.Nome}");

            if (quantidadeSelecionada > produto.QuantidadeEstoque)
                AdicionarErroValidacao($"O produto {produto.Nome} possui {produto.QuantidadeEstoque} em estoque");
        }
    }
}
