using GB1.Application.DTO.Request;
using GB1.Application.DTO.Response;
using GB1.Domain.Entitiy;
using GB1.Domain.Enums;
using GB1.Infrastructure.Context;
using GB1.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GB1.Application.UseCases
{
    public class DesastreUseCase
    {
        private readonly IRepository<Desastre> _repository;
        private readonly DeuRuimContext _context;

        public DesastreUseCase(IRepository<Desastre> repository, DeuRuimContext context)
        {
            _repository = repository;
            _context = context;
        }

        public async Task<CreateDesastreResponse> CreateDesastreAsync(CreateDesastreRequest request)
        {
            var desastre = Desastre.Create(
                request.Titulo,
                request.Uf,
                request.Descricao,
                request.DataDesastre,
                request.Severidade,
                request.UsuarioId
            );

            await _repository.AddAsync(desastre);
            await _repository.SaveChangesAsync();

            return new CreateDesastreResponse
            {
                Id = desastre.Id,
                Titulo = desastre.Titulo,
                Descricao = desastre.Descricao,
                DataDesastre = desastre.DataDesastre,
                Uf = desastre.Uf.ToString(),
                Severidade = desastre.Severidade.ToString()
            };
        }

        public async Task<List<CreateDesastreResponse>> GetAllDesastreAsync()
        {
            var desastres = await _context.Desastres
                .Include(d => d.Usuario)
                .ToListAsync();

            return desastres.Select(d => new CreateDesastreResponse
            {
                Id = d.Id,
                Titulo = d.Titulo,
                Descricao = d.Descricao,
                DataDesastre = d.DataDesastre,
                Uf = d.Uf.ToString(),
                Severidade = d.Severidade.ToString(),
                Usuario = d.Usuario == null ? null : new CreateUsuarioResponse
                {
                    Id = d.Usuario.Id,
                    Email = d.Usuario.Email,
                    Username = d.Usuario.Username,
                    Senha = d.Usuario.Senha,
                    Uf = d.Usuario.Uf.ToString(),
                    Nivel = d.Usuario.Nivel.ToString()
                }
            }).ToList();
        }

        public async Task<CreateDesastreResponse?> GetByIdAsync(long id)
        {
            var desastre = await _context.Desastres
                .Include(d => d.Usuario)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (desastre == null) return null;

            return new CreateDesastreResponse
            {
                Id = desastre.Id,
                Titulo = desastre.Titulo,
                Descricao = desastre.Descricao,
                DataDesastre = desastre.DataDesastre,
                Uf = desastre.Uf.ToString(),
                Severidade = desastre.Severidade.ToString(),
                Usuario = desastre.Usuario == null ? null : new CreateUsuarioResponse
                {
                    Id = desastre.Usuario.Id,
                    Email = desastre.Usuario.Email,
                    Username = desastre.Usuario.Username,
                    Senha = desastre.Usuario.Senha,
                    Uf = desastre.Usuario.Uf.ToString(),
                    Nivel = desastre.Usuario.Nivel.ToString()
                }
            };
        }


        public async Task<bool> UpdateDesastreAsync(long id, UpdateDesastreRequest request)
        {
            var desastre = await _repository.GetByIdAsync(id);
            if (desastre == null) return false;

            desastre.Atualizar(
                request.Titulo,
                request.Uf,
                request.Descricao,
                request.DataDesastre,
                request.Severidade,
                request.UsuarioId
            );

            _repository.Update(desastre);
            await _repository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteDesastreAsync(long id)
        {
            var desastre = await _repository.GetByIdAsync(id);
            if (desastre == null) return false;

            _repository.Delete(desastre);
            await _repository.SaveChangesAsync();
            return true;
        }
    }
}
