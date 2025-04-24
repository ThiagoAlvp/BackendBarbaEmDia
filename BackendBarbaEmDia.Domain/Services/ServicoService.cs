using BackendBarbaEmDia.Domain.Extensions;
using BackendBarbaEmDia.Domain.Interfaces.Repositories;
using BackendBarbaEmDia.Domain.Interfaces.Services;
using BackendBarbaEmDia.Domain.Models.Database;
using BackendBarbaEmDia.Domain.Models.Requests;
using BackendBarbaEmDia.Domain.Models.Responses;

namespace BackendBarbaEmDia.Domain.Services
{
    public class ServicoService : IServicoService
    {
        private readonly IServicoRepository _servicoRepository;

        public ServicoService(IServicoRepository servicoRepository)
        {
            _servicoRepository = servicoRepository;
        }

        public async Task<ServiceResult> CreateAsync(AddUpdateServicoRequest servico)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(servico.Descricao))
                    return new ServiceResult(false, "Descrição do serviço é obrigatória.");

                if (servico.DuracaoPadrao == TimeSpan.Zero)
                    return new ServiceResult(false, "Duração padrão do serviço é obrigatória.");

                if (servico.Preco <= 0)
                    return new ServiceResult(false, "Preço do serviço é obrigatório.");

                Servico servicoDb = new Servico
                {
                    Descricao = servico.Descricao,
                    DuracaoPadrao = servico.DuracaoPadrao,
                    Preco = servico.Preco
                };

                await _servicoRepository.AddAsync(servicoDb);

                return new ServiceResult("Serviço criado com sucesso!");
            }
            catch (Exception ex)
            {
                return new ServiceResult(false, $"Erro ao criar serviço: {ex.GetFullMessage()}", true);
            }
        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            try
            {
                Servico? servicoDb = await _servicoRepository
                    .GetFirstWithIncludesAsync(
                        x => x.Id == id,
                        x => x.Agendamentos);

                if (servicoDb is null)
                    return new ServiceResult(false, "Serviço não encontrado.");

                if (servicoDb.Agendamentos.Any())
                    return new ServiceResult(false, "Serviço não pode ser excluído, pois possui agendamentos.");

                await _servicoRepository.DeleteAsync(servicoDb);

                return new ServiceResult("Serviço excluído com sucesso!");
            }
            catch (Exception ex)
            {
                return new ServiceResult(false, $"Erro ao excluir serviço: {ex.GetFullMessage()}", true);
            }
        }

        public async Task<ServiceResult<List<ServicoResponse>>> GetAllAsync()
        {
            return await GetAllAsync(false);
        }

        public async Task<ServiceResult<List<ServicoResponse>>> GetAllAtivosAsync()
        {
            return await GetAllAsync(true);
        }

        private async Task<ServiceResult<List<ServicoResponse>>> GetAllAsync(bool somenteAtivos)
        {
            try
            {
                List<Servico> servicos = [];

                if (somenteAtivos)
                    servicos = await _servicoRepository.GetListAsync(x => x.Ativo);
                else
                    servicos = await _servicoRepository.GetListAsync();

                List<ServicoResponse> servicosResponse = servicos.Select(x => new ServicoResponse(x)).ToList();

                return new ServiceResult<List<ServicoResponse>>(servicosResponse);
            }
            catch (Exception ex)
            {
                return new ServiceResult<List<ServicoResponse>>(false, $"Erro ao buscar serviços: {ex.GetFullMessage()}", true);
            }
        }

        public async Task<ServiceResult<ServicoResponse>> GetByIdAsync(int id)
        {
            try
            {
                Servico? servicoDb = await _servicoRepository.GetByIdAsync(id);

                if (servicoDb is null)
                    return new ServiceResult<ServicoResponse>(false, "Serviço não encontrado.");

                ServicoResponse servicoResponse = new ServicoResponse(servicoDb);

                return new ServiceResult<ServicoResponse>(servicoResponse);
            }
            catch (Exception ex)
            {
                return new ServiceResult<ServicoResponse>(false, $"Erro ao buscar serviço: {ex.GetFullMessage()}", true);
            }
        }

        public async Task<ServiceResult> UpdateAsync(AddUpdateServicoRequest servico, int id)
        {
            try
            {
                Servico? servicoDb = await _servicoRepository.GetByIdAsync(id);

                if (servicoDb is null)
                    return new ServiceResult(false, "Serviço não encontrado.");

                if (string.IsNullOrWhiteSpace(servico.Descricao))
                    return new ServiceResult(false, "Descrição do serviço é obrigatória.");

                if (servico.DuracaoPadrao == TimeSpan.Zero)
                    return new ServiceResult(false, "Duração padrão do serviço é obrigatória.");

                if (servico.Preco <= 0)
                    return new ServiceResult(false, "Preço do serviço é obrigatório.");

                servicoDb.Descricao = servico.Descricao;
                servicoDb.DuracaoPadrao = servico.DuracaoPadrao;
                servicoDb.Preco = servico.Preco;
                servicoDb.Ativo = servico.Ativo;

                await _servicoRepository.UpdateAsync(servicoDb);

                return new ServiceResult("Serviço atualizado com sucesso!");
            }
            catch (Exception ex)
            {
                return new ServiceResult(false, $"Erro ao atualizar serviço: {ex.GetFullMessage()}", true);
            }
        }

        public async Task<ServiceResult> AtivarAsync(int id)
        {
            try
            {
                Servico? servicoDb = await _servicoRepository.GetByIdAsync(id);

                if (servicoDb is null)
                    return new ServiceResult(false, "Serviço não encontrado.");

                servicoDb.Ativo = true;

                await _servicoRepository.UpdateAsync(servicoDb);

                return new ServiceResult("Serviço ativado com sucesso!");
            }
            catch (Exception ex)
            {
                return new ServiceResult(false, $"Erro ao ativar serviço: {ex.GetFullMessage()}", true);
            }
        }

        public async Task<ServiceResult> InativarAsync(int id)
        {
            try
            {
                Servico? servicoDb = await _servicoRepository.GetByIdAsync(id);

                if (servicoDb is null)
                    return new ServiceResult(false, "Serviço não encontrado.");

                servicoDb.Ativo = false;

                await _servicoRepository.UpdateAsync(servicoDb);

                return new ServiceResult("Serviço inativado com sucesso!");
            }
            catch (Exception ex)
            {
                return new ServiceResult(false, $"Erro ao inativar serviço: {ex.GetFullMessage()}", true);
            }
        }
    }
}
