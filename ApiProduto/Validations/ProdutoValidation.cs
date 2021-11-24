using ApiProduto.models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiProduto.Validations
{
    public class ProdutoValidation : AbstractValidator<Produto>
    {
        public ProdutoValidation()
        {
            RuleFor(Produto => Produto.Codigo)
                
                .NotEmpty().WithMessage("O codigo é obrigatorio, tente novamente")
                .NotNull().WithMessage("Codigo não informado, tente novamente")
                ;

            RuleFor(Produto => Produto.Nome)
                .NotEmpty().WithMessage("Campo nome vazio, tente novamente")
                .NotNull().WithMessage("Campo nome não informado, tente novamente")
                .MinimumLength(3).WithMessage("Tamanho minimo não atingido")
                .MaximumLength(250).WithMessage("Tamanho maximo excedido de 250 caracteres")
                ;

            RuleFor(Produto => Produto.DescricaoP)
                .NotEmpty().WithMessage("Descrião do produto obrigatorio, tente novamente")
                .NotNull().WithMessage("A descrição do produto não foi informada, tente novamente")
                .MinimumLength(3).WithMessage("Tamanho minimo de 3 caracteres não atingido")
                .MaximumLength(500).WithMessage("Tamanho maximo excedido de 500 caracteres")
                ;
            RuleFor(Produto => Produto.PrecoVenda)
                .NotEmpty().WithMessage("Preço do produto é obrigatorio, tente novamente")
                .NotNull().WithMessage("O preço do produto não foi informado, tente novamente")
                .GreaterThan(0).WithMessage("Preço de venda tem que ser maior que 0")
                ;

            RuleFor(Produto => Produto.PrecoCusto)
                .NotEmpty().WithMessage("Preço do produto é obrigatorio, tente novamente")
                .NotNull().WithMessage("O preço do produto não foi informado, tente novamente")
                .GreaterThan(0).WithMessage("Preço de custo tem que ser maior que 0")
                ;


            RuleFor(Produto => Produto.DataCadastro)
                .NotEmpty().WithMessage("Data de cadastro é obrigatoria, tente novamente")
                .NotNull().WithMessage("Data de cadastro não foi informada, tente novamente")
                ;


            RuleFor(Produto => Produto.Ativo)

                .NotEmpty().WithMessage("Campo ativo ou inativo é obrigatorio, tente novamente")
                .NotNull().WithMessage(" Campo ativo ou inativo não foi informado, tente novamente")

                ;


            RuleFor(Produto => Produto.Estoque)

               .NotEmpty().WithMessage("Campo estoque é obrigatorio, tente novamente")
               .NotNull().WithMessage(" Campo estoque não foi informado, tente novamente")
               .GreaterThanOrEqualTo(0).WithMessage("O estoque tem que ser maior ou igual 0")
               ;

            RuleFor(Produto => Produto.Imagem)
               .NotEmpty().WithMessage("A imagem é obrigatoria, tente novamente")
               .NotNull().WithMessage(" Imagem não foi informada, tente novamente");

        }

    }
}
