using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using FluentAssertions;
using nuget_fiap_app_pagamento_common.Models;
using nuget_fiap_app_pagamento.Controllers;
using nuget_fiap_app_pagamento_common.Interfaces.UseCases;

namespace nuget_fiap_app_pagamento_test.Controller
{
    public class PagamentoControllerTest
    {
        private readonly Mock<IPagamentoUseCase> _pagamentoUseCaseMock;
        private readonly PagamentoController _controller;

        public PagamentoControllerTest()
        {
            _pagamentoUseCaseMock = new Mock<IPagamentoUseCase>();
            _controller = new PagamentoController(_pagamentoUseCaseMock.Object);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task Post_ShouldReturn201Created_WhenPagamentoIsCreated()
        {
            var retorno = "";
            var pag = new Pagamento();
            _pagamentoUseCaseMock.Setup(x => x.GeraQRCodeParaPagamento(It.IsAny<Pagamento>())).ReturnsAsync("1");

            var result = await _controller.GeraQRCodeParaPagamento(pag);

            result.Should().BeOfType<CreatedAtRouteResult>();
            var createdResult = result as CreatedAtRouteResult;
            createdResult.Value.Should().BeEquivalentTo(retorno);
        }
    }
}
