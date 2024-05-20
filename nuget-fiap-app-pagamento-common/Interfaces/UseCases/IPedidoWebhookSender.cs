using nuget_fiap_app_pagamento_common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nuget_fiap_app_pagamento_server.Interface.Services
{
    public interface IPedidoWebhookSender
    {
        Task NotificaPagamentoAoPedido(Pagamento pagamento);
    }
}
