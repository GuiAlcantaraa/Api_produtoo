using ApiProduto.models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]

        public ActionResult ola()
        {
            return Ok("metodo padrão");
        }


        [HttpPost]

       public IActionResult RegistrarVenda(Venda venda)
        {
            
            

        }

    }
}
