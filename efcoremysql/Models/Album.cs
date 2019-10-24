using System.Collections.Generic;

namespace efcoremysql
{
    public class Album
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Ano { get; set; }
        public string Observacoes { get; set; }
        public string Email { get; set; }

        public virtual List<Musica> Musicas { get; set; }
    }


    public class Musica
    {
        public long Id { get; set; }
        public string Nome { get; set; }

        public virtual Album Album { get; set; }
    }

}