using ApiProduto.Context;
using ApiProduto.models;
using ApiProduto.Validations;
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
    public class CategoriaController : ControllerBase
    {

        private readonly ProdutoContext Context;
        public CategoriaController()
        {
            Context = new ProdutoContext();
        }


        [HttpGet]
        public ActionResult Ola()
        {
            return Ok("olaaaaaaa");
        }




        [HttpGet("ObterPorId/{Id}")]
        public ActionResult ObterPorId(string id)
        {
            return Ok(Context._categoria.Find<Categoria>(p => p.Id == id).FirstOrDefault());

        }
        



        [HttpPost("AdicionarCategoria")]

        public ActionResult AdicionarCategoria(Categoria categoria)

        {
            CategoriaValidation produtoValidation = new CategoriaValidation();
            var validacao = produtoValidation.Validate(categoria);

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




            Context._categoria.InsertOne(categoria);
            return Ok("Categoria cadastrada");


        }





        [HttpPut("Atualizar/{Id}")]
        public ActionResult Atualizar(string id, [FromBody] Categoria categoria)
        {
            var pResultado = Context._categoria.Find<Categoria>(p => p.Id == id).FirstOrDefault();
            if (pResultado == null) return
            NotFound("Id não encontrado, atualizacao não realizada!");

            categoria.Id = id;
            Context._categoria.ReplaceOne<Categoria>(p => p.Id == id, categoria);

            return Ok("Categoria atualizada com sucesso");



        }



        [HttpPut("Desativar/{Id}")]
        public ActionResult Desativar(string id)
        {
            var CategoriaDesativada = Context._categoria.Find<Categoria>(P => P.Id == id).FirstOrDefault();
            if (CategoriaDesativada == null)
                return NotFound("Produto não pode ser desativado, pois id não existe");


            if (CategoriaDesativada != null &&
                CategoriaDesativada.Ativo == false)
                return BadRequest("Produto já está desativado, operação não realizada");



            CategoriaDesativada.Ativo = false;

            using (System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient())
            {
                smtp.Host = "smtp.outlook.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential("guilhermealcantara03@outlook.com", "tv88925431");
            }

            using (System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage())
            {
                mail.From = new System.Net.Mail.MailAddress("guilhermealcantara03@outlook.com");
                mail.To.Add(new System.Net.Mail.MailAddress("guilhermealcantara03@outlook.com"));


                mail.Subject = "Categoria Desativada";
                mail.Body = "Olá, atenção. A categoria " + id + " foi desativada do seu catalogo!";
            }

            return Ok("Categoria desativada");


        }



        [HttpDelete("Deletar/{Id}")]
        public ActionResult Deletar(string id)

        {
            var pResultado = Context._categoria.Find<Categoria>(p => p.Id == id).FirstOrDefault();
            if (pResultado == null) return
                    NotFound("Id não encontrada, atualizacao não realizada!");

            Context._categoria.DeleteOne<Categoria>(filter => filter.Id == id);
            return Ok("Categoria removida com sucesso");
        }



    }
}