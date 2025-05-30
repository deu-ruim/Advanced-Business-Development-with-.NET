using GB1.Domain.Enums;
using GB1.Domain.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Text.RegularExpressions;

namespace GB1.Domain.Entitiy
{
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; private set; } 
        public string Username { get; private set; }
        public string Email { get; private set; }
        public string Senha { get; private set; }
        public Uf Uf { get; private set; }
        public Nivel Nivel { get; private set; }

        private Usuario(string username, string email, string senha, Uf uf, Nivel nivel)
        {
            Username = username;
            Email = email;
            Senha = senha;
            Uf = uf;
            Nivel = nivel;
        }

        public void Atualizar(string username, string email, string senha, Uf uf, Nivel nivel)
        {
            Username = username;
            Email = email;
            Senha = senha;
            Uf = uf;
            Nivel = nivel;
        }



        internal static Usuario Create(string username, string email, string senha, Uf uf, Nivel nivel)
        {
            return new Usuario(username, email, senha, uf, nivel);
        }

        public Usuario() { }
    }
}
