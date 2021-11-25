using LocadoraDarioAPI.Data;
using LocadoraDarioAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocadoraDarioAPI.Controllers
{
    [ApiController]
    [Route("v1/clientes")]
    public class ClientesController : ControllerBase
    {
        [HttpGet]
        [Route("selecionar/{id:int?}")]

        public async Task<ActionResult<List<Cliente>>> Selecionar([FromServices] DataContext context, int id)
        {
            var clientes = await context.Clientes.ToListAsync();
            if (id > 0)
            {
                return clientes.FindAll(a => a.Id == id);
            }
            else
            {
                return clientes;
            }
        }

        [HttpPost]
        [Route("cadastrar")]

        public async Task<ActionResult<Cliente>> Cadastrar([FromServices] DataContext context, [FromBody] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                var objCliente = await context.Clientes.FirstOrDefaultAsync(x => x.Nome == cliente.Nome.Trim());

                if (objCliente == null )
                {
                    cliente.Nome = cliente.Nome.Trim();
                    cliente.Ativo = true;
                    cliente.DataCriacao = DateTime.Now;
                    context.Clientes.Add(cliente);
                    await context.SaveChangesAsync();
                    return cliente;
                }
                else
                {
                    return Content("O Cliente ja possui cadastro!");
                }
            }
            else
            {
                return BadRequest(ModelState);
            }

        }

    }
}
