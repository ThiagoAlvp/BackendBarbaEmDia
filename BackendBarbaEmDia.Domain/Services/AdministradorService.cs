using BackendBarbaEmDia.Domain.Extensions;
using BackendBarbaEmDia.Domain.Interfaces.Repositories;
using BackendBarbaEmDia.Domain.Interfaces.Services;
using BackendBarbaEmDia.Domain.Models.Database;
using BackendBarbaEmDia.Domain.Models.Requests;
using BackendBarbaEmDia.Domain.Models.Responses;

namespace BackendBarbaEmDia.Domain.Services
{
    public class AdministradorService : IAdministradorService
    {
        private readonly IAdministradorRepository _administradorRepository;

        public AdministradorService(IAdministradorRepository administradorRepository)
        {
            _administradorRepository = administradorRepository;
        }

        public async Task<ServiceResult<LoginAdministradorResponse>> VerificaAdministradorLogin(LoginAdministradorRequest request)
        {
            try
            {
                Administrador? administrador = await _administradorRepository
                    .GetFirstAsync(x => x.Username == request.Username);

                if (administrador is null)
                    return new(false, "Usuário não encontrado.");

                if (!BCrypt.Net.BCrypt.Verify(request.Senha, administrador.Senha))
                    return new(false, "Senha inválida.");

                LoginAdministradorResponse response = new()
                {
                    Administrador = new(administrador)
                };

                return new(response);
            }
            catch (Exception ex)
            {
                return new(false, $"Erro ao verificar login do administrador: {ex.GetFullMessage()}", true);
            }
        }

        public async Task<ServiceResult> CreateAsync(AddUpdateAdministradorRequest administrador)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(administrador.Username))
                    return new(false, "Nome de usuário do administrador é obrigatório.");
                if (string.IsNullOrWhiteSpace(administrador.Senha))
                    return new(false, "Senha do administrador é obrigatória.");

                Administrador administradorDb = new Administrador
                {
                    Username = administrador.Username,
                    Senha = BCrypt.Net.BCrypt.HashPassword(administrador.Senha)
                };
                await _administradorRepository.AddAsync(administradorDb);

                return new("Administrador criado com sucesso!");
            }
            catch (Exception ex)
            {
                return new(false, $"Erro ao criar administrador: {ex.GetFullMessage()}", true);
            }
        }

        public async Task<ServiceResult> UpdateAsync(AddUpdateAdministradorRequest administrador, int id)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(administrador.Username))
                    return new(false, "Nome de usuário do administrador é obrigatório.");
                if (string.IsNullOrWhiteSpace(administrador.Senha))
                    return new(false, "Senha do administrador é obrigatória.");

                Administrador? administradorDb = await _administradorRepository
                    .GetFirstAsync(x => x.Id == id);

                if (administradorDb is null)
                    return new(false, "Administrador não encontrado.");


                administradorDb.Username = administrador.Username;

                if (!string.IsNullOrWhiteSpace(administrador.Senha))
                    administradorDb.Senha = BCrypt.Net.BCrypt.HashPassword(administrador.Senha);

                administradorDb.Telefone = administrador.Telefone;
                administradorDb.Nome = administrador.Nome;


                await _administradorRepository.UpdateAsync(administradorDb);
                return new("Administrador atualizado com sucesso!");
            }
            catch (Exception ex)
            {
                return new(false, $"Erro ao atualizar administrador: {ex.GetFullMessage()}", true);
            }
        }
        public async Task<ServiceResult<List<AdministradorResponse>>> GetAllAsync()
        {
            try
            {
                List<Administrador> administradores = await _administradorRepository.GetListAsync();

                List<AdministradorResponse> response = administradores.Select(a => new AdministradorResponse(a)).ToList();

                return new(response);
            }
            catch (Exception ex)
            {
                return new(false, $"Erro ao obter lista de administradores: {ex.GetFullMessage()}", true);
            }
        }

        public async Task<ServiceResult<AdministradorResponse>> GetByIdAsync(int id)
        {
            try
            {
                Administrador? administrador = await _administradorRepository.GetByIdAsync(id);

                if (administrador is null)
                    return new(false, "Administrador não encontrado.");

                AdministradorResponse response = new(administrador);

                return new(response);
            }
            catch (Exception ex)
            {
                return new(false, $"Erro ao obter administrador: {ex.GetFullMessage()}", true);
            }
        }
    }
}
