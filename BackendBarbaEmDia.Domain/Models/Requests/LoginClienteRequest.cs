﻿namespace BackendBarbaEmDia.Domain.Models.Requests
{
    public class LoginClienteRequest
    {
        public required string NrTelefone { get; set; }
        public string? Nome { get; set; }
    }
}
