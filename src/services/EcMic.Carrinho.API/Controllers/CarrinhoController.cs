using EcMic.Carrinho.API.Data;
using EcMic.Carrinho.API.Models;
using EMic.WebApi.Core.Controllers;
using EMic.WebApi.Core.Usuario;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EcMic.Carrinho.API.Controllers
{
    [Authorize]
    public class CarrinhoController : MainController
    {
        private readonly IAspNetUser _user;
        private CarrinhoContext _context;

        public CarrinhoController(IAspNetUser aspNetUser, CarrinhoContext CarrinhoContext)
        {
            _user = aspNetUser;
            _context = CarrinhoContext;
        }

        [HttpGet("Carrinho")]
        public async Task<CarrinhoCliente> ObterCarrinho()
        {
            var retorno = await ObterCarrinhoCliente();

            return retorno ?? new CarrinhoCliente();
        }  
        
        [HttpPost("Carrinho")]
        public async Task<IActionResult> AdicionarItemCarrinho(CarrinhoItem item)
        {
            var carrinho = await ObterCarrinhoCliente();

            if (carrinho == null)            
                ManipularNovoCarrinho(item);            
            else            
                ManipularCarrinhoExistente(carrinho, item);
       
            if(!OperacaoValida()) return CustomResponse();

            await PersistirDados();

            return CustomResponse();
        }

        [HttpPut("Carrinho/{produtoId}")]
        public async Task<IActionResult> AtualizarItemCarrinho(Guid produtoId, CarrinhoItem item)
        {
            var carrinho = await ObterCarrinhoCliente();
            var itemCarrinho = await ObterItemCarrinhoValidado(produtoId, carrinho, item);

            if (itemCarrinho == null) return CustomResponse();

            carrinho.AtualizarUnidades(itemCarrinho, item.Quantidade);

            ValidarCarrinho(carrinho);
            if (!OperacaoValida()) return CustomResponse();

            _context.CarrinhoItens.Update(itemCarrinho);
            _context.CarrinhoCliente.Update(carrinho);

            await PersistirDados();

            return CustomResponse();
        }


        [HttpDelete("Carrinho/{produtoId}")]
        public async Task<IActionResult> RemoverItemCarrinho(Guid produtoId)
        {
            var carrinhoCliente = await ObterCarrinhoCliente();
            var itemCarrinho = await ObterItemCarrinhoValidado(produtoId, carrinhoCliente);

            if (itemCarrinho == null) return CustomResponse();

            ValidarCarrinho(carrinhoCliente);
            if (!OperacaoValida()) return CustomResponse();

            carrinhoCliente.RemoverItem(itemCarrinho);

            _context.CarrinhoItens.Remove(itemCarrinho);
            _context.CarrinhoCliente.Update(carrinhoCliente);

            await PersistirDados();

            return CustomResponse();
        }

        #region métodos privados da controller
        private void ManipularNovoCarrinho(CarrinhoItem item)
        {
            var carrinho = new CarrinhoCliente(_user.ObterUserId());
            carrinho.AdicionarItem(item);

            ValidarCarrinho(carrinho);

            _context.CarrinhoCliente.Add(carrinho);
        }

        private void ManipularCarrinhoExistente(CarrinhoCliente carrinho, CarrinhoItem Item)
        {
            var produtoItemExistente = carrinho.CarrinhoItemExistente(Item);

            carrinho.AdicionarItem(Item);

            ValidarCarrinho(carrinho);

            if (produtoItemExistente)
            {
                _context.CarrinhoItens.Update(carrinho.ObterPorProdutoId(Item.ProdutoId));
            }
            else
            {
                _context.CarrinhoItens.Add(Item);
            }
           

            _context.CarrinhoCliente.Update(carrinho);

        }

        private async Task<CarrinhoCliente> ObterCarrinhoCliente()
        {
            return await _context.CarrinhoCliente
                                 .Include(i => i.Itens)
                                 .FirstOrDefaultAsync(f => f.ClienteId == (_user.ObterUserId()));
        }

        private async Task<CarrinhoItem> ObterItemCarrinhoValidado(Guid produtoId, CarrinhoCliente carrinho, CarrinhoItem item = null)
        {
            if (item != null && produtoId != item.ProdutoId)
            {
                AdicionarErroProcessamento("O item não corresponde ao informado");
                return null;
            }

            if (carrinho == null)
            {
                AdicionarErroProcessamento("Carrinho não encontrado");
                return null;
            }

            var itemCarrinho = await _context.CarrinhoItens
                                             .FirstOrDefaultAsync(p => p.CarrinhoId == carrinho.Id && p.ProdutoId == produtoId);

            if (itemCarrinho == null || !carrinho.CarrinhoItemExistente(itemCarrinho))
            {
                AdicionarErroProcessamento("O item não está no carrinho");
                return null;
            }

            return itemCarrinho;
        }

        private async Task PersistirDados()
        {
            var result = await _context.SaveChangesAsync();
            if (result <= 0) AdicionarErroProcessamento("Não foi possível persistir os dados no banco");
        }

        private bool ValidarCarrinho(CarrinhoCliente carrinho)
        {
            if (carrinho.EhValido()) return true;

            carrinho.ValidationResult.Errors.ToList().ForEach(e => AdicionarErroProcessamento(e.ErrorMessage));
            return false;
        }
        #endregion métodos privados da controller


    }
}
