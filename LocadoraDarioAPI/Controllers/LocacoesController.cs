using LocadoraDarioAPI.Data;
using LocadoraDarioAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LocadoraDarioAPI.Controllers
{
    [ApiController]
    [Route("v1/locacoes")]
    public class LocacoesController : ControllerBase
    {
        [HttpPost]
        [Route("alugar")]

        public async Task<ActionResult<Locacao>> Alugar([FromServices] DataContext context, [FromBody] Locacao locacao)
        {
            if (locacao.ClientId != 0 && locacao.FilmeId != 0 && locacao.DataEntrega != DateTime.MinValue)
            {
                var filme = context.Filmes.FirstOrDefault(x => x.Id == locacao.FilmeId);
                var cliente = context.Filmes.FirstOrDefault(x => x.Id == locacao.ClientId);

                if (filme == null)
                {
                    return Content("Filme não encontrado");
                }
                else if (!filme.Disponivel)
                {
                    return Content($"O Filme {filme.Nome} não esta disponível");
                }
                else if (cliente == null)
                {
                    return Content("Cliente não encontrado");
                }
                else
                {
                    filme.Disponivel = false;
                    context.Filmes.Update(filme);

                    locacao.DataLocacao = DateTime.Now;
                    context.Locacoes.Add(locacao);

                    await context.SaveChangesAsync();

                    return Content($" Filme {filme.Nome} alugado com sucesso");
                }
            }
            else
            {
                return Content("Informe o Id do cliente, do Filme e a Data da Entrega yyyy-MM-dd!");
            }
        }

        [HttpPost]
        [Route("devolver")]
        public async Task<ActionResult<Locacao>> Devolver([FromServices] DataContext context, [FromBody] Locacao locacao)
        {
            if (locacao.ClientId != 0 && locacao.FilmeId != 0)
            {
                var filme = context.Filmes.FirstOrDefault(x => x.Id == locacao.FilmeId);
                var cliente = context.Clientes.FirstOrDefault(x => x.Id == locacao.ClientId);

                if (cliente == null)
                {
                    return Content("Cliente não encontrado");
                }

                else if (filme == null)
                {
                    return Content("Filme não encontrado");
                }

                else if (filme.Disponivel)
                {
                    return Content($"O Filme {filme.Nome} esta disponível");
                }


                filme.Disponivel = true;
                locacao.DataDevolvido = DateTime.Now;

                context.Filmes.Update(filme);
                context.Locacoes.Update(locacao);

                await context.SaveChangesAsync();

                var locacaoObj = context.Locacoes.FirstOrDefault(x => x.ClientId == locacao.ClientId && x.FilmeId == locacao.FilmeId);

                if (DateTime.Now > locacaoObj.DataEntrega)
                {
                    return Content("Filme Devolvido após a entrega prevista, cobrar a diferença");
                }
                else
                {
                    return Content("Filme Devolvido com sucesso");
                }
            }
            else
            {
                return Content("Informe o Id do cliente, do Filme e a Data da Entrega yyyy-MM-dd!");
            }
        }
    }
}
