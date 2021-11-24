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
    public class EstoqueController : ControllerBase
    {
        private readonly ProdutoContext Context;
        public EstoqueController()
        {
            Context = new ProdutoContext();
        }

        [HttpGet]

        public ActionResult Ola()
        {
            return Ok("Estoque");
        }


        [HttpPost("DebitarEstoque/{Id}")]
        public ActionResult DebitarEstoque(string id, [FromBody] ProdutoEstoque produtoEstoque)
        {
            var resultado = Context._produtos.Find<Produto>(p => p.Id == id).FirstOrDefault();

            if (resultado == null)
            {
                return NotFound("O produto não existe na base de dados");
            }
            
            if (produtoEstoque.qtde > resultado.EstoqueAtual)
            {
                return BadRequest("O produto não tem estoque suficiente");
            }

            resultado.EstoqueAtual = resultado.EstoqueAtual - produtoEstoque.qtde;

            Context._produtos.ReplaceOne<Produto>(p => p.Id == id, resultado);

            return Ok("Produto debitado do estoque com sucesso!");

           
            
        }
    }
}