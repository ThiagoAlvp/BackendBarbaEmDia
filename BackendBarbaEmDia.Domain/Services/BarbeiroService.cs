using BackendBarbaEmDia.Domain.Extensions;
using BackendBarbaEmDia.Domain.Interfaces.Repositories;
using BackendBarbaEmDia.Domain.Interfaces.Services;
using BackendBarbaEmDia.Domain.Models.Database;
using BackendBarbaEmDia.Domain.Models.Requests;
using BackendBarbaEmDia.Domain.Models.Responses;

namespace BackendBarbaEmDia.Domain.Services
{
    public class BarbeiroService : IBarbeiroService
    {
        private readonly IBarbeiroRepository _barbeiroRepository;

        public BarbeiroService(IBarbeiroRepository barbeiroRepository)
        {
            _barbeiroRepository = barbeiroRepository;
        }

        public async Task<ServiceResult> CreateAsync(AddUpdateBarbeiroRequest barbeiro)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(barbeiro.Nome))
                    return new(false, "Nome do barbeiro é obrigatório.");

                Barbeiro barbeiroDb = new Barbeiro
                {
                    Nome = barbeiro.Nome
                };

                await _barbeiroRepository.AddAsync(barbeiroDb);

                return new("Barbeiro criado com sucesso!");
            }
            catch (Exception ex)
            {
                return new(false, $"Erro ao criar barbeiro: {ex.GetFullMessage()}", true);
            }
        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            try
            {
                Barbeiro? barbeiro = await 
                    _barbeiroRepository
                    .GetFirstWithIncludesAsync(
                        x => x.Id == id,
                        x => x.Agendamentos);

                if (barbeiro is null)
                    return new(false, "Barbeiro não encontrado.");

                if (barbeiro.Agendamentos.Any())
                    return new(false, "Barbeiro não pode ser excluído, pois possui agendamentos.");

                await _barbeiroRepository.DeleteAsync(barbeiro);

                return new("Barbeiro excluido com sucesso!");
            }
            catch (Exception ex)
            {
                return new(false, $"Erro ao excluir barbeiro: {ex.GetFullMessage()}", true);
            }
        }

        public async Task<ServiceResult<List<BarbeiroResponse>>> GetAllAsync()
        {
            return await GetAllAsync(false);
        }

        public async Task<ServiceResult<List<BarbeiroResponse>>> GetAllAtivosAsync(int? idServico = null)
        {
            return await GetAllAsync(true);
        }

        private async Task<ServiceResult<List<BarbeiroResponse>>> GetAllAsync(bool somenteAtivos, int? idServico = null)
        {
            try
            {
                List<Barbeiro> barbeirosDb = [];

                if (somenteAtivos)
                    barbeirosDb = await _barbeiroRepository.GetBarbeirosAtivos(idServico);
                else
                    barbeirosDb = await _barbeiroRepository.GetListAsync();

                List<BarbeiroResponse> barbeiros = barbeirosDb
                    .Select(x => new BarbeiroResponse(x))
                    .ToList();

                return new(barbeiros);
            }
            catch (Exception ex)
            {
                return new(false, $"Erro ao buscar barbeiros: {ex.GetFullMessage()}", true);
            }
        }

        public async Task<ServiceResult<BarbeiroResponse>> GetByIdAsync(int id)
        {
            try
            {
                Barbeiro? barbeiroDb = await _barbeiroRepository.GetBarbeiroEServicosById(id);

                if (barbeiroDb is null)
                    return new(false, "Barbeiro não encontrado.");

                BarbeiroResponse barbeiro = new(barbeiroDb);

                return new(barbeiro);
            }
            catch (Exception ex)
            {
                return new(false, $"Erro ao buscar barbeiro: {ex.GetFullMessage()}", true);
            }
        }

        public async Task<ServiceResult> UpdateAsync(AddUpdateBarbeiroRequest barbeiro, int id)
        {
            try
            {
                Barbeiro? barbeiroDb = await _barbeiroRepository.GetByIdAsync(id);

                if (barbeiroDb is null)
                    return new(false, "Barbeiro não encontrado.");

                if (string.IsNullOrWhiteSpace(barbeiro.Nome))
                    return new(false, "Nome do barbeiro é obrigatório.");

                barbeiroDb.Nome = barbeiro.Nome;
                barbeiroDb.Ativo = barbeiro.Ativo;

                await _barbeiroRepository.UpdateAsync(barbeiroDb);

                return new("Barbeiro atualizado com sucesso!");
            }
            catch (Exception ex)
            {
                return new(false, $"Erro ao atualizar barbeiro: {ex.GetFullMessage()}", true);
            }
        }

        public async Task<ServiceResult> AtivarAsync(int id)
        {
            try
            {
                Barbeiro? barbeiroDb = await _barbeiroRepository.GetByIdAsync(id);

                if (barbeiroDb is null)
                    return new(false, "Barbeiro não encontrado.");

                barbeiroDb.Ativo = true;

                await _barbeiroRepository.UpdateAsync(barbeiroDb);

                return new("Barbeiro ativado com sucesso!");
            }
            catch (Exception ex)
            {
                return new(false, $"Erro ao ativar barbeiro: {ex.GetFullMessage()}", true);
            }
        }

        public async Task<ServiceResult> InativarAsync(int id)
        {
            try
            {
                Barbeiro? barbeiroDb = await _barbeiroRepository.GetByIdAsync(id);

                if (barbeiroDb is null)
                    return new(false, "Barbeiro não encontrado.");

                barbeiroDb.Ativo = false;

                await _barbeiroRepository.UpdateAsync(barbeiroDb);

                return new("Barbeiro inativado com sucesso!");
            }
            catch (Exception ex)
            {
                return new(false, $"Erro ao inativar barbeiro: {ex.GetFullMessage()}", true);
            }
        }
    }
}
