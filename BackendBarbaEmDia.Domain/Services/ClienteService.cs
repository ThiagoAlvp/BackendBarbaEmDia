using BackendBarbaEmDia.Domain.Extensions;
using BackendBarbaEmDia.Domain.Interfaces.Repositories;
using BackendBarbaEmDia.Domain.Interfaces.Services;
using BackendBarbaEmDia.Domain.Models.Database;
using BackendBarbaEmDia.Domain.Models.Requests;
using BackendBarbaEmDia.Domain.Models.Responses;
using System.Text.RegularExpressions;

namespace BackendBarbaEmDia.Domain.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;


        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<ServiceResult<LoginClienteResponse>> VerificaClienteLogin(LoginClienteRequest request)
        {
            try
            {
                Cliente? cliente = await _clienteRepository.GetFirstAsync(c => c.Telefone == request.NrTelefone);

                if (cliente is not null)
                    return new ServiceResult<LoginClienteResponse>(new LoginClienteResponse(new(cliente)));

                cliente = new Cliente
                {
                    Telefone = request.NrTelefone,
                    Nome = request.Nome
                };

                if (cliente.Telefone.Length < 11)
                    return new ServiceResult<LoginClienteResponse>(false, "Número de telefone deve ter pelo menos 11 digítos.");

                if (!Regex.Match(cliente.Telefone, @"^\d{11}$").Success)
                    return new ServiceResult<LoginClienteResponse>(false, "Número de telefone deve conter apenas números.");

                await _clienteRepository.AddAsync(cliente);

                return new ServiceResult<LoginClienteResponse>(new LoginClienteResponse(new(cliente)));
            }
            catch (Exception ex)
            {
                return new ServiceResult<LoginClienteResponse>(false, ex.GetFullMessage(), true);
            }            
        }
    }
}
