using ApiProduto.Context;
using ApiProduto.models;
using ApiProduto.token;
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
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioContext Context;
        public UsuarioController()
        {
            Context = new UsuarioContext();
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] Usuario model)
        {
            // Recupera o usuário
            var user = await Context._usuarios.Find<Usuario>
                (p => p.UserName == model.UserName).FirstOrDefaultAsync();

            // Verifica se o usuário existe
            if (user == null)
                return NotFound(new { message = "Usuário não existe" });

            if (user.Password != model.Password)
                return BadRequest(new { message = "Senha inválida" });

            // Gera o Token
            var token = TokenService.GenerateToken(user);

            // Oculta a senha
            user.Password = "";

            // Retorna os dados
            return new
            {
                user = user,
                token = token
            };
        }

        [HttpDelete("Deletar/{Id}")]
        public ActionResult Deletar(string id)

        {
            var pResultado = Context._usuarios.Find<Usuario>(p => p.Id == id).FirstOrDefault();
            if (pResultado == null) return
                    NotFound("Id não encontrada, atualizacao não realizada!");

            Context._usuarios.DeleteOne<Usuario>(filter => filter.Id == id);
            return Ok("usuario removidos com sucesso");


        }
    }
}
