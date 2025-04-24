using BackendBarbaEmDia.Domain.Extensions;
using BackendBarbaEmDia.Domain.Interfaces.Repositories;
using BackendBarbaEmDia.Domain.Interfaces.Services;
using BackendBarbaEmDia.Domain.Models.Database;
using BackendBarbaEmDia.Domain.Models.Requests;
using BackendBarbaEmDia.Domain.Models.Responses;
using System.Linq.Expressions;

namespace BackendBarbaEmDia.Domain.Services
{
    public class TravamentoService : ITravamentoService
    {
        private readonly ITravamentosRepository _travamentoRepository;

        public TravamentoService(ITravamentosRepository travamentoRepository)
        {
            _travamentoRepository = travamentoRepository;
        }

        public async Task<ServiceResult> Atualizar(int id, AddUpdateTravamentoRequest travamento)
        {
            try
            {
                Travamento? travamentoDb = await _travamentoRepository.GetByIdAsync(id);

                if (travamentoDb == null)
                    return new(false, "Travamento não encontrado");

                ServiceResult validaTravamento = await ValidaTravamento(travamento);

                if (validaTravamento.Success)
                    return validaTravamento;

                travamentoDb.DataHoraInicio = travamento.DataHoraInicio;
                travamentoDb.DataHoraFim = travamento.DataHoraFim;
                travamentoDb.IdBarbeiro = travamento.IdBarbeiro;
                travamentoDb.Motivo = travamento.Motivo;

                await _travamentoRepository.UpdateAsync(travamentoDb);

                return new("Travamento atualizado com sucesso");
            }
            catch (Exception ex)
            {
                return new(false, $"Erro ao atualizar travamento: {ex.GetFullMessage()}", true);
            }
        }

        public async Task<ServiceResult> Criar(AddUpdateTravamentoRequest travamento)
        {
            try
            {
                ServiceResult validaTravamento = await ValidaTravamento(travamento);

                if (!validaTravamento.Success)
                    return validaTravamento;

                Travamento travamentoDb = new()
                {
                    IdBarbeiro = travamento.IdBarbeiro,
                    DataHoraInicio = travamento.DataHoraInicio,
                    DataHoraFim = travamento.DataHoraFim,
                    Motivo = travamento.Motivo
                };

                await _travamentoRepository.AddAsync(travamentoDb);

                return new("Travamento criado com sucesso");
            }
            catch (Exception ex)
            {
                return new(false, $"Erro ao criar travamento: {ex.GetFullMessage()}", true);
            }
        }

        public async Task<ServiceResult> Deletar(int id)
        {
            try
            {
                Travamento? travamento = await _travamentoRepository.GetByIdAsync(id);

                if (travamento == null)
                    return new(false, "Travamento não encontrado");

                await _travamentoRepository.DeleteAsync(travamento);

                return new("Travamento apagado com sucesso");
            }
            catch (Exception ex)
            {
                return new(false, $"Erro ao apagar travamento: {ex.GetFullMessage()}", true);
            }
        }

        private async Task<ServiceResult> ValidaTravamento(AddUpdateTravamentoRequest travamento)
        {
            if (travamento.DataHoraInicio > travamento.DataHoraFim)
                return new(false, "Data de início não pode ser maior que a data de fim");

            if (travamento.DataHoraInicio < DateTime.Now)
                return new(false, "Data de início não pode ser menor que a data atual");

            if (travamento.DataHoraFim < DateTime.Now)
                return new(false, "Data de fim não pode ser menor que a data atual");

            if (travamento.DataHoraInicio == travamento.DataHoraFim)
                return new(false, "Data de início não pode ser igual a data de fim");

            if (string.IsNullOrWhiteSpace(travamento.Motivo))
                return new(false, "Motivo não pode ser vazio");

            if (travamento.IdBarbeiro <= 0)
                return new(false, "Id do barbeiro não informado");

            bool existeTravamento = await _travamentoRepository.ExistsAsync(
                x => x.IdBarbeiro == travamento.IdBarbeiro &&
                     x.DataHoraInicio <= travamento.DataHoraFim &&
                     x.DataHoraFim >= travamento.DataHoraInicio
            );

            if (existeTravamento)
                return new(false, "Já existe um travamento para o barbeiro nesse período");

            return new("Validado");
        }

        private async Task<ServiceResult<List<TravamentoResponse>>> ListarTravamentosInternal(Expression<Func<Travamento, bool>>? expression = null)
        {
            try
            {
                List<Travamento> travamentos = await _travamentoRepository
                    .GetListWithIncludesAsync(
                        expression,
                        x => x.Barbeiro
                    );

                List<TravamentoResponse> travamentosResponse = travamentos.Select(x => new TravamentoResponse(x)).ToList();

                return new(travamentosResponse);
            }
            catch (Exception ex)
            {
                return new(false, $"Erro ao listar travamentos: {ex.GetFullMessage()}", true);
            }
        }

        public async Task<ServiceResult<List<TravamentoResponse>>> ListarTravamentos()
        {
            return await ListarTravamentosInternal();
        }

        public async Task<ServiceResult<List<TravamentoResponse>>> ListarTravamentosPorBarbeiroId(int barbeiroId)
        {
            return await ListarTravamentosInternal(x => x.IdBarbeiro == barbeiroId);
        }

        public async Task<ServiceResult<List<TravamentoResponse>>> ListarTravamentosPorData(DateTime data)
        {
            return await ListarTravamentosInternal(x => x.DataHoraInicio.Date == data.Date);
        }

        public async Task<ServiceResult<List<TravamentoResponse>>> ListarTravamentosPorDataEBarbeiro(DateTime data, int barbeiroId)
        {
            return await ListarTravamentosInternal(x => x.DataHoraInicio.Date == data.Date && x.IdBarbeiro == barbeiroId);
        }


        public async Task<ServiceResult<TravamentoResponse>> ObterTravamentoPorId(int id)
        {
            try
            {
                Travamento? travamento = await
                    _travamentoRepository
                    .GetFirstWithIncludesAsync(
                        x => x.Id == id,
                        x => x.Barbeiro
                    );

                if (travamento is null)
                    return new(false, "Travamento não encontrado");

                TravamentoResponse travamentoResponse = new(travamento);

                return new(travamentoResponse);
            }
            catch (Exception ex)
            {
                return new(false, $"Erro ao obter travamento: {ex.GetFullMessage()}", true);
            }
        }
    }
}
