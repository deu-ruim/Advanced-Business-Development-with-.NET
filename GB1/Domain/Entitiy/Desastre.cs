using GB1.Domain.Enums;
using GB1.Domain.Exceptions;
using System.IO;
using System.Text.RegularExpressions;

namespace GB1.Domain.Entitiy
{
    public class Desastre
    {
        public int Id { get; private set; }
        public string Titulo { get; private set; }

        public Uf Uf { get; private set; }
        public string Descricao { get; private set; }
        public DateTime DataDesastre { get; private set; }
        public Severidade Severidade { get; private set; }

        //Relacionamento 1..N
        public int? UsuarioId { get; private set; }
        public virtual Usuario Usuario { get; private set; }

       

        private Desastre(string titulo, Uf uf, string descricao, DateTime dataDesastre, Severidade severidade, int usuarioId)
        {
           Titulo = titulo;
           Uf = uf;
           Descricao = descricao;
           DataDesastre = dataDesastre;
           Severidade = severidade;
           UsuarioId = usuarioId;

        }


        internal static Desastre Create(string titulo, Uf uf, string descricao, DateTime dataDesastre, Severidade severidade, int usuarioId)
        {
            return new Desastre(titulo, uf, descricao, dataDesastre, severidade, usuarioId);
        }

        public Desastre() { }
    }
}
