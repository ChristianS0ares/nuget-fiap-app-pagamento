using Newtonsoft.Json;
using nuget_fiap_app_pagamento_common.Models;
using nuget_fiap_app_pagamento_server.Interface.Services;
using System.Text;

namespace nuget_fiap_app_pagamento.Services
{
    public class PedidoWebhookSender : IPedidoWebhookSender
    {
        private readonly string webhookUrl;

        public PedidoWebhookSender()
        {
            webhookUrl = Environment.GetEnvironmentVariable("WEBHOOK_URL") ?? "http://localhost:8080/Pagamento";
        }
        public async Task NotificaPagamentoAoPedido(Pagamento pagamento)
        {
            var payload = new WebhookNotificacao { IdPedido = pagamento.IdPedido, Aprovado = true, Motivo = "Pagamento aprovado com sucesso!" };

            using (var httpClient = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
                await httpClient.PostAsync(webhookUrl, content);
            }
        }
    }
}
