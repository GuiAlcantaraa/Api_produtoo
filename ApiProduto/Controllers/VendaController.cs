using ApiProduto.Context;
using ApiProduto.models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiProduto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendaController : ControllerBase
    {

        private readonly ProdutoContext Context;
        public VendaController()
        {
            Context = new ProdutoContext();

        }

        [HttpGet]

        public ActionResult ola()
        {
            return Ok("metodo padrão");
        }


        [HttpPost("RegistrarVenda")]
        public ActionResult RegistrarVenda(Venda venda)
        {

            foreach (var item in venda.Itens)
            {
                var resultado = Context._produtos.Find<Produto>(p => p.Id == item.CodigoProduto).FirstOrDefault();
                if (resultado == null)
                {
                    return NotFound($"O produto {item.CodigoProduto} não existe na base de dados, venda nao pde ser feita");
                }

                if (resultado.EstoqueAtual < item.qtde)
                {
                    return BadRequest($"O produto {item.qtde} nao pode ter venda relizada, Venda maior que o estoque atual!");
                }

                resultado.EstoqueAtual = resultado.EstoqueAtual - item.qtde;

                Context._produtos.ReplaceOne<Produto>(p => p.Id == resultado.Id, resultado);

            }
             Context._VendaProduto.InsertOne(venda);
            return Ok(venda);

            
        }

    }
}
