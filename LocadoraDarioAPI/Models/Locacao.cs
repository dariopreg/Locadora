using System;

namespace LocadoraDarioAPI.Models
{
    public class Locacao
    {
        public int Id { get; set; }

        public int ClientId { get; set; }

        public int FilmeId { get; set; }

        public DateTime DataLocacao { get; set; }

        public DateTime DataEntrega { get; set; }

        public DateTime DataDevolvido { get; set; }
    }
}
