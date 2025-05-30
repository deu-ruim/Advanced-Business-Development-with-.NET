    using GB1.Domain.Enums;
    using System.ComponentModel.DataAnnotations;

    namespace GB1.Application.DTO.Response
    {
        public class CreateDesastreResponse
        {
            public long Id { get; set; }
            public string Titulo { get; set; }
            public string Descricao { get; set; }

            /// <example>30/05/2025 14:13:27</example>
            public DateTime DataDesastre { get; set; }
            public string Uf { get; set; }
            public string Severidade { get; set; }
            public CreateUsuarioResponse Usuario { get; set; }
        }
    }
