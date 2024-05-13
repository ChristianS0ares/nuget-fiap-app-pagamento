
namespace nuget_fiap_app_pagamento_common.Models
{
    public class WebhookNotificacao
    {
        public required int IdPedido { get; set; }
        public required bool Aprovado { get; set; }
        public string? Motivo { get; set; }
    }
}
