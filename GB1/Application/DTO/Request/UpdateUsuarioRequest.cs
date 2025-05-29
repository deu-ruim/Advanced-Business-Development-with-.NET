using GB1.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace GB1.Application.DTO.Request
{
    public class UpdateUsuarioRequest
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Senha { get; set; }

        [EnumDataType(typeof(Uf))]
        public Uf Uf { get; set; }

        [EnumDataType(typeof(Nivel))]
        public Nivel Nivel { get; set; }
    }
}
