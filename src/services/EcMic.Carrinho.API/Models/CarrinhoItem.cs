using FluentValidation;
using System;
using System.Text.Json.Serialization;

namespace EcMic.Carrinho.API.Models
{
    public class CarrinhoItem
    {
        public CarrinhoItem()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public Guid ProdutoId { get; set; }
        public string Nome { get; set; }
        public int Quantidade { get; set; }
        public decimal Valor { get; set; }
        public string Imagem { get; set; }
        public Guid CarrinhoId { get; set; }

        [JsonIgnore] //Ignora a referência cíclica
        public CarrinhoCliente CarrinhoCliente { get; set; }

        internal void AssociarCarrinho(Guid carrinhoId)
        {
            CarrinhoId = carrinhoId;
        }

        internal decimal CalcularValor()
        {
            return Quantidade * Valor;
        }

        internal void AdicionarUnidades(int unidades)
        {
            Quantidade += unidades;
        }

        internal void AtualizarUnidades(int unidades)
        {
            Quantidade = unidades;
        }

        public class ItemCarrinhoValidation: AbstractValidator<CarrinhoItem>
        {
            public ItemCarrinhoValidation()
            {
                RuleFor(c => c.ProdutoId)
                     .NotEqual(Guid.Empty)
                     .WithMessage("Id do produto inválido");

                RuleFor(c => c.Nome)
                    .NotEmpty()
                    .WithMessage("O nome do produto não foi informado");

                RuleFor(c => c.Quantidade)
                    .GreaterThan(0)
                    .WithMessage(item => $"A quantidade mínima para o item {item.Nome} é 1.");

                RuleFor(c => c.Quantidade)
                    .LessThanOrEqualTo(5)
                    .WithMessage(item => $"A quantidade máxima do item {item.Nome} é 5");

                RuleFor(c => c.Valor)
                    .GreaterThan(0)
                    .WithMessage("O valor do item precisa ser maior que 0.");

            }
        }
    }
}
