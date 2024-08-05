using nuget_fiap_app_pagamento_common.Models;

namespace nuget_fiap_app_pagamento_common.Interfaces.UseCases
{
    public interface IPagamentoUseCase
    {
        Task<string> GeraQRCodeParaPagamento(Pagamento pagamento);

        Task PagaQRCode(string QRCode);
    }
}
