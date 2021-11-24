using ApiProduto.models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiProduto.Validations
{
    public class CategoriaValidation : AbstractValidator<Categoria>
    {
        public CategoriaValidation()
        {

            RuleFor(Categoria => Categoria.IdCategoria)

                 .NotEmpty().WithMessage("O codigo é obrigatorio, tente novamente")
                 .NotNull().WithMessage("Codigo não informado, tente novamente")
                    ;
            RuleFor(Categoria => Categoria.NomeCategoria)

                .NotEmpty().WithMessage("Campo nome vazio, tente novamente")
                .NotNull().WithMessage("Campo nome não informado, tente novamente")
                .MinimumLength(3).WithMessage("Tamanho minimo não atingido")
                .MaximumLength(250).WithMessage("Tamanho maximo excedido de 250 caracteres")
                ;
        }
    }
}
