using System;
using System.Collections.Generic;

namespace EcMic.WebApp.MVC.Models
{

    public class CarrinhoViewModel
    {
        public decimal ValorTotal { get; set; }
        public IList<ItemProdutoViewModel> Itens { get; set; } = new List<ItemProdutoViewModel>();
    }

    public class ItemProdutoViewModel
    {
        public Guid ProdutoId { get; set; }
        public string Nome { get; set; }
        public int Quantidade { get; set; }
        public decimal Valor { get; set; }
        public string Imagem { get; set; }
    }
}
