using GB1.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace GB1.Application.DTO.Request
{
    public class UpdateDesastreRequest
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }

        /// <example>30/05/2025 14:13:27</example>
        public DateTime DataDesastre { get; set; }

        [EnumDataType(typeof(Uf))]
        public Uf Uf { get; set; }
        
        [EnumDataType(typeof(Severidade))]
        public Severidade Severidade { get; set; }
        public long? UsuarioId { get; set; }
    }
}
