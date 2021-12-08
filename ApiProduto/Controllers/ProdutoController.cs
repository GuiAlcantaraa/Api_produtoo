using ApiProduto.Context;
using ApiProduto.models;
using ApiProduto.Validations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ApiProduto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {

        private readonly ProdutoContext Context;
        public static IWebHostEnvironment _environment;
        public ProdutoController(IWebHostEnvironment environment)
        {
            Context = new ProdutoContext();
            _environment = environment;
        }

        [HttpGet]
        public ActionResult Ola()
        {
            return Ok("ola");
        }

        /// <summary>
        /// Consulta dados de uma pessoa a partir do ID
        /// Requer uso de token.
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>Objeto contendo os dados de uma pessoa.</returns>
        ///
        [Authorize]
        [HttpGet("ObterPorId/{id}")]
        public ActionResult ObterPorId(string id)
        {

            return Ok(Context._produtos.Find<Produto>(p => p.Id == id).FirstOrDefault());
        }

        [HttpPost("upload/{id}")]
        public async Task<ActionResult> EnviaArquivo(string id, [FromForm] IFormFile arquivo)
        {
            if (arquivo.Length > 0)
            {
                if (arquivo.ContentType != "image/jpeg" &&
                    arquivo.ContentType != "image/jpg" &&
                    arquivo.ContentType != "image/png"
                   )
                {
                    return BadRequest("Formato Inválido de imagens");
                }

                try
                {

                    string contentRootPath = _environment.ContentRootPath;
                    string path = "";
                    path = Path.Combine(contentRootPath, "imagens");

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    using (FileStream filestream = System.IO.File.Create(path + "\\" + arquivo.FileName))
                    {
                        await arquivo.CopyToAsync(filestream);
                        filestream.Flush();

                        var pResultado = Context._produtos.Find<Produto>(p => p.Id == id).FirstOrDefault();
                        if (pResultado == null) return

                        NotFound("Id não encontrado, atualizacao não realizada!");

                        pResultado.Imagem = arquivo.FileName;
                        Context._produtos.ReplaceOne<Produto>(p => p.Id == id, pResultado);
                        return Ok("Imagem enviada com sucesso " + arquivo.FileName);


                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.ToString());
                }
            }
            else
            {
                return BadRequest("Ocorreu uma falha no envio do arquivo...");
            }



        }


        [HttpPost("AdicionarP")]
        public ActionResult AdicionarP(Produto produto)
        {
            ProdutoValidation produtoValidation = new ProdutoValidation();

            var validacao = produtoValidation.Validate(produto);


            if (!validacao.IsValid)
            {
                List<string> erros = new List<string>();
                foreach (var failure in validacao.Errors)
                {
                    erros.Add("Property " + failure.PropertyName +
                        " failed validation. Error Was: "
                        + failure.ErrorMessage);
                }

                return BadRequest(erros);
            }


            Context._produtos.InsertOne(produto);

            return Ok("Produto cadastrado");

            return CreatedAtAction(nameof(AdicionarP), "");
        }




        [HttpPut("Atualizar/{id}")]
        public ActionResult Atualizar(string id, [FromBody] Produto produto)
        {
            var pResultado = Context._produtos.Find<Produto>(p => p.Id == id).FirstOrDefault();
            if (pResultado == null) return
            NotFound("Id não encontrado, atualizacao não realizada!");

            produto.Id = id;
            Context._produtos.ReplaceOne<Produto>(p => p.Id == id, produto);

            return Ok("Produto atualizado com sucesso");


        }



        [HttpPut("Desativar/{id}")]
        public ActionResult Desativar(string id)
        {
            var ProdutoDesativado = Context._produtos.Find<Produto>(P => P.Id == id).FirstOrDefault();
            if (ProdutoDesativado == null)
                return NotFound("Produto não pode ser desativado, pois id não existe");


            if (ProdutoDesativado != null &&
                ProdutoDesativado.Ativo == false)
                return BadRequest("Produto já está desativado, operação não realizada");



            ProdutoDesativado.Ativo = false;


            return Ok("Produto desativado");
        }



        [HttpDelete("Remover/{Id}")]
        public ActionResult Remover(string id)
        {


            var Resultado = Context._produtos.Find<Produto>(p => p.Id == id).FirstOrDefault();
            if (Resultado == null) return
                    NotFound("Id não encontrada, atualizacao não realizada!");

            Context._produtos.DeleteOne<Produto>(filter => filter.Id == id);
            return Ok("Produto removido com sucesso");
        }


        [HttpPut("AdicionarPromocao")]
        public ActionResult Promocao(List <Promocao> promocao)
        {
            foreach (var item in promocao)
            {
                var resultado = Context._produtos.Find<Produto>(p => p.Id == item.codigo).FirstOrDefault();
                if (resultado == null)
                {
                    return NotFound($"O produto {item.codigo} não existe na base de dados, promoção não pode ser adicionada");
                }

                if (resultado.PrecoVenda < item.precoPromocao)
                {
                    return BadRequest($"O produto {item.precoPromocao} nao pode ter promoão adicionada, preço promoção maior que preço venda!");
                }

                resultado.PrecoVenda = item.precoPromocao;

                Context._produtos.ReplaceOne<Produto>(p => p.Id == resultado.Id, resultado);

            }
            return Ok("Promoção do produto alterada com sucesso");

        }

    }
}


