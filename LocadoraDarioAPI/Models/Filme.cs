using System;

namespace LocadoraDarioAPI.Models
{
    public class Filme
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public bool Ativo { get; set; }

        public bool Disponivel { get; set; }

        public DateTime DataCriacao { get; set; }
    }
}
