using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using nuget_fiap_app_pagamento_common.Models;
using System.Net;
using System.Text;
using Xunit;

namespace nuget_fiap_app_pagamento_test.Controller
{
    public class PagamentoControllerIT : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public PagamentoControllerIT(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        private async Task<string> CreatePagamentoAsync()
        {
            var newPagamento = new Pagamento
            {
                IdPedido = 123,
                Valor = 105
            };
            var content = new StringContent(JsonConvert.SerializeObject(newPagamento), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/GeraQRCodeParaPagamento", content);
            response.EnsureSuccessStatusCode();

            var location = response.Headers.Location.ToString();
            var qrCode = location.Substring(location.LastIndexOf('/') + 1);
            return qrCode;
        }

        [Fact]
        [Trait("Category", "Integration")]
        public async Task Post_ShouldReturn201Created_WhenPagamentoIsCreated()
        {
            var qrCode = await CreatePagamentoAsync();
            var response = await _client.GetAsync("/PagaQRCode");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
