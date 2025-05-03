using BackendBarbaEmDia.Domain.Extensions;
using BackendBarbaEmDia.Domain.Interfaces.Repositories;
using BackendBarbaEmDia.Domain.Interfaces.Services;
using BackendBarbaEmDia.Domain.Models.Database;
using BackendBarbaEmDia.Domain.Models.Requests;
using BackendBarbaEmDia.Domain.Models.Responses;

namespace BackendBarbaEmDia.Domain.Services
{
    public class BarbeiroServicoService : IBarbeiroServicoService
    {
        private readonly IBarbeiroServicoRepository _barbeiroServicoRepository;

        public BarbeiroServicoService(IBarbeiroServicoRepository barbeiroServicoRepository)
        {
            _barbeiroServicoRepository = barbeiroServicoRepository;
        }

        public async Task<ServiceResult> CreateBarbeiroServicoAsync(AddUpdateBarbeiroServicoRequest barbeiroServico)
        {
            try
            {
                if (barbeiroServico.IdBarbeiro == 0)
                    return new(false, "Identificador do barbeiro não informado.");

                if (barbeiroServico.IdServico == 0)
                    return new(false, "Identificador do serviço não informado.");

                BarbeiroServico barbeiroServicoDb = new()
                {
                    IdBarbeiro = barbeiroServico.IdBarbeiro,
                    IdServico = barbeiroServico.IdServico,
                    TempoPersonalizado = barbeiroServico.TempoPersonalizado,
                };

                await _barbeiroServicoRepository.AddAsync(barbeiroServicoDb);

                return new(true, "Barbeiro e serviço vinculados com sucesso.");
            }
            catch (Exception ex)
            {
                return new(false, $"Erro ao vincular serviço e barbeiro. {ex.GetFullMessage()}", true);
            }
        }

        public async Task<ServiceResult> DeleteBarbeiroServicoAsync(int barbeiroId, int servicoId)
        {
            try
            {
                BarbeiroServico? barbeiroServico = await _barbeiroServicoRepository.GetFirstAsync(x => x.IdBarbeiro == barbeiroId && x.IdServico == servicoId);

                if (barbeiroServico is null)
                    return new(false, "Barbeiro e serviço não encontrados.");

                await _barbeiroServicoRepository.DeleteAsync(barbeiroServico);

                return new(true, "Barbeiro e serviço desvinculados com sucesso.");
            }
            catch (Exception ex)
            {
                return new(false, $"Erro ao desvincular serviço e barbeiro. {ex.GetFullMessage()}", true);
            }
        }

        public async Task<ServiceResult> UpdateBarbeiroServicoAsync(AddUpdateBarbeiroServicoRequest barbeiroServico, int id)
        {
            try
            {
                BarbeiroServico? barbeiroServicoDb = await _barbeiroServicoRepository.GetByIdAsync(id);

                if (barbeiroServicoDb is null)
                    return new(false, "Barbeiro e serviço não encontrados.");

                if (barbeiroServico.IdBarbeiro == 0)
                    return new(false, "Identificador do barbeiro não informado.");

                if (barbeiroServico.IdServico == 0)
                    return new(false, "Identificador do serviço não informado.");

                barbeiroServicoDb.IdBarbeiro = barbeiroServico.IdBarbeiro;
                barbeiroServicoDb.IdServico = barbeiroServico.IdServico;
                barbeiroServicoDb.TempoPersonalizado = barbeiroServico.TempoPersonalizado;

                await _barbeiroServicoRepository.UpdateAsync(barbeiroServicoDb);

                return new(true, "Barbeiro e serviço atualizados com sucesso.");
            }
            catch (Exception ex)
            {
                return new(false, $"Erro ao atualizar barbeiro e serviço. {ex.GetFullMessage()}", true);
            }
        }
    }
}
