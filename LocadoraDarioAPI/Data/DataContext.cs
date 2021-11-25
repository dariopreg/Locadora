using LocadoraDarioAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LocadoraDarioAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Locacao> Locacoes { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Filme> Filmes { get; set; }
    }
}
