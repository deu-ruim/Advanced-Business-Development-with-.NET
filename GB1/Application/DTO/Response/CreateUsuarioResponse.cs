using GB1.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace GB1.Application.DTO.Response
{
    public class CreateUsuarioResponse
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Senha { get; set; }
        public string Uf { get; set; }
        public string Nivel { get; set; }
    }
}
