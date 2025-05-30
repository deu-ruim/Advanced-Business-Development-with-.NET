using GB1.Application.DTO.Request;
using GB1.Application.DTO.Response;
using GB1.Domain.Entitiy;
using GB1.Infrastructure.Repositories;
using GB1.Domain.Enums;

namespace GB1.Application.UseCases
{
    public class UsuarioUseCase
    {
        private readonly IRepository<Usuario> _repository;

        public UsuarioUseCase(IRepository<Usuario> repository)
        {
            _repository = repository;
        }

        public async Task<CreateUsuarioResponse> CreateUsuarioAsync(CreateUsuarioRequest request)
        {
            var usuario = Usuario.Create(
                request.Username,
                request.Email,
                request.Senha,
                request.Uf,     
                request.Nivel
            );

            await _repository.AddAsync(usuario);
            await _repository.SaveChangesAsync();   

        return new CreateUsuarioResponse
            {
                Id = usuario.Id,
                Username = usuario.Username,
                Email = usuario.Email,
                Senha = usuario.Senha,
                Uf = usuario.Uf.ToString(),
                Nivel = usuario.Nivel.ToString()
            };
        }

        public async Task<List<CreateUsuarioResponse>> GetAllUsuariosAsync()
        {
            var usuarios = await _repository.GetAllAsync();
            return usuarios.Select(u => new CreateUsuarioResponse
            {
                Id = u.Id,
                Username = u.Username,
                Email = u.Email,
                Senha = u.Senha,
                Uf = u.Uf.ToString(),
                Nivel = u.Nivel.ToString()
            }).ToList();
        }

        public async Task<CreateUsuarioResponse?> GetByIdAsync(long id)
        {
            var usuario = await _repository.GetByIdAsync(id);
            if (usuario == null) return null;

            return new CreateUsuarioResponse
            {
                Id = usuario.Id,
                Username = usuario.Username,
                Email = usuario.Email,
                Senha=usuario.Senha,
                Uf = usuario.Uf.ToString(),
                Nivel = usuario.Nivel.ToString()
            };
        }

        public async Task<bool> UpdateUsuarioAsync(long id, UpdateUsuarioRequest request)
        {
            var usuario = await _repository.GetByIdAsync(id);
            if (usuario == null) return false;

            usuario.Atualizar(
                request.Username,
                request.Email,
                request.Senha,
                request.Uf,
                request.Nivel
            );
            _repository.Update(usuario);
            await _repository.SaveChangesAsync();
            return true;
        }


        public async Task<bool> DeleteUsuarioAsync(long id)
        {
            var usuario = await _repository.GetByIdAsync(id);
            if (usuario == null) return false;

            _repository.Delete(usuario);
            await _repository.SaveChangesAsync();
            return true;
        }
    }
}
