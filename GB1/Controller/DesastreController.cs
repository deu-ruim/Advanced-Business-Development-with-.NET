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
    [Tags("CRUD Desastre")]
    public class DesastreController : ControllerBase
    {
        private readonly DesastreUseCase _desastreUseCase;
        private readonly CreateDesastreRequestValidator _validationDesastre;

        public DesastreController(DesastreUseCase desastreUseCase, CreateDesastreRequestValidator validationDesastre)
        {
            _desastreUseCase = desastreUseCase;
            _validationDesastre = validationDesastre;
        }

        /// <summary>
        /// Get all Desastres
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CreateDesastreResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetDesastre()
        {
            var desastres = await _desastreUseCase.GetAllDesastreAsync();
            return Ok(desastres);
        }

        /// <summary>
        /// Get desastres por ID
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CreateDesastreResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetDesastre(long id)
        {
            var desastres = await _desastreUseCase.GetByIdAsync(id);
            if (desastres == null)
                return NotFound();

            return Ok(desastres);
        }

        /// <summary>
        /// Criar um novo desastres
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(CreateUsuarioResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PostDesastre([FromBody] CreateDesastreRequest request)
        {
            _validationDesastre.ValidateAndThrow(request);

            var desastreResponse = await _desastreUseCase.CreateDesastreAsync(request);
            return CreatedAtAction(nameof(GetDesastre), new { id = desastreResponse.Id }, desastreResponse);
        }

        /// <summary>
        /// Atualiza um desastres existente
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PutUsuario(long id, [FromBody] UpdateDesastreRequest request)
        {
            var updated = await _desastreUseCase.UpdateDesastreAsync(id, request);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        /// <summary>
        /// Deleta um desastres
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteDesastre(long id)
        {
            var deleted = await _desastreUseCase.DeleteDesastreAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}