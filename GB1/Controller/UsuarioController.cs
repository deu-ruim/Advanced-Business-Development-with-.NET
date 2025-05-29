using FluentValidation;
using GB1.Application.DTO.Request;
using GB1.Application.DTO.Response;
using GB1.Application.UseCases;
using GB1.Application.Validators;
using GB1.Domain.Entitiy;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace TDSPM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Tags("CRUD Usuario")]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioUseCase _usuarioUseCase;
        private readonly CreateUsuarioRequestValidator _validationUsuario;

        public UsuarioController(UsuarioUseCase usuarioUseCase, CreateUsuarioRequestValidator validationUsuario)
        {
            _usuarioUseCase = usuarioUseCase;
            _validationUsuario = validationUsuario;
        }

        /// <summary>
        /// Get all Usuarios
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CreateUsuarioResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetUsuarios()
        {
            var usuarios = await _usuarioUseCase.GetAllUsuariosAsync();
            return Ok(usuarios);
        }

        /// <summary>
        /// Get usuario por ID
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CreateUsuarioResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetUsuario(int id)
        {
            var usuario = await _usuarioUseCase.GetByIdAsync(id);
            if (usuario == null)
                return NotFound();

            return Ok(usuario);
        }

        /// <summary>
        /// Cria um novo Usuario
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(CreateUsuarioResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PostUsuario([FromBody] CreateUsuarioRequest request)
        {
            _validationUsuario.ValidateAndThrow(request);

            var usuarioResponse = await _usuarioUseCase.CreateUsuarioAsync(request);
            return CreatedAtAction(nameof(GetUsuario), new { id = usuarioResponse.Id }, usuarioResponse);
        }

        /// <summary>
        /// Atualiza um Usuario existente
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PutUsuario(int id, [FromBody] UpdateUsuarioRequest request)
        {
            var updated = await _usuarioUseCase.UpdateUsuarioAsync(id, request);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        /// <summary>
        /// Deleta um Usuario
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var deleted = await _usuarioUseCase.DeleteUsuarioAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}