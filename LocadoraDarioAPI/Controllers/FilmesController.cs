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
    [Route("v1/filmes")]
    public class FilmesController : ControllerBase
    {
        [HttpGet]
        [Route("selecionar/{id:int?}")]

        public async Task<ActionResult<List<Filme>>> Selecionar([FromServices] DataContext context, int id)
        {
            var filmes = await context.Filmes.ToListAsync();
            if (id > 0)
            {
                return filmes.FindAll(a => a.Id == id);
            }
            else
            {
                return filmes;
            }
        }

        [HttpPost]
        [Route("cadastrar")]

        public async Task<ActionResult<Filme>> Cadastrar([FromServices] DataContext context, [FromBody] Filme filme)
        {
            if (ModelState.IsValid)
            {
                var objCliente = await context.Filmes.FirstOrDefaultAsync(x => x.Nome == filme.Nome.Trim());

                if (objCliente == null)
                {
                    filme.Nome = filme.Nome.Trim();
                    filme.Ativo = true;
                    filme.DataCriacao = DateTime.Now;
                    context.Filmes.Add(filme);
                    await context.SaveChangesAsync();
                    return filme;
                }
                else
                {
                    return Content("O Filme ja possui cadastro!");
                }
            }
            else
            {
                return BadRequest(ModelState);
            }

        }
    }
}
