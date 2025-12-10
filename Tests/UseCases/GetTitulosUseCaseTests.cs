using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTO;
using Application.UseCases.GetTitulos;
using Domain.Entities;
using Domain.Repositories;
using Domain.Services;
using Moq;
using Xunit;

namespace Tests.UseCases
{
    class TestOutputPort : IOutputPort
    {
        public IList<TituloDto>? Result { get; private set; }
        public void Ok(IList<TituloDto> titulos) => Result = titulos;
    }

    public class GetTitulosUseCaseTests
    {
        [Fact]
        public async Task Execute_InvokesOutputPortWithMappedDtos_OrderByDiasDesc()
        {
            var titulo = new Titulo
            {
                Numero = "001",
                NomeDevedor = "John",
                QuantidadeParcelas = 1,
                ValorOriginal = 100m,
                DataVencimento = DateTime.Today.AddDays(-2)
            };

            var titulo2 = new Titulo
            {
                Numero = "002",
                NomeDevedor = "Mary",
                QuantidadeParcelas = 2,
                ValorOriginal = 200m,
                DataVencimento = DateTime.Today.AddDays(-1)
            };

            var mockTituloRepo = new Mock<ITituloRepository>();
            mockTituloRepo.Setup(r => r.ObterTitulosAtrasados()).ReturnsAsync(new List<Titulo> { titulo, titulo2 });

            var mockFinanceRepo = new Mock<IFinanceRepository>();
            mockFinanceRepo.Setup(f => f.GetCurrentAsync(default)).ReturnsAsync(new TaxaFinanceira { MultaPercentual = 2m, JurosDiarioPercentual = 1m });

            var mockCalculadora = new Mock<ICalculadoraService>();

            mockCalculadora.Setup(c => c.CalcularMulta(It.IsAny<Titulo>(), 2m)).Returns<Titulo, decimal>((t, p) => decimal.Round(t.ValorOriginal * (p / 100), 2));
            mockCalculadora.Setup(c => c.CalcularJuros(It.IsAny<Titulo>(), 1m)).Returns<Titulo, decimal>((t, p) => decimal.Round(t.ValorOriginal * (p / 100) * t.DiasEmAtraso(), 2));
            mockCalculadora.Setup(c => c.CalcularValorAtualizado(It.IsAny<Titulo>(), 2m, 1m)).Returns<Titulo, decimal, decimal>((t, p1, p2) =>
            {
                var v = decimal.Round(t.ValorOriginal, 2);
                var multa = decimal.Round(v * (p1 / 100), 2);
                var juros = decimal.Round(v * (p2 / 100) * t.DiasEmAtraso(), 2);
                return decimal.Round(v + multa + juros, 2);
            });

            var useCase = new GetTitulosUseCase(mockTituloRepo.Object, mockFinanceRepo.Object, mockCalculadora.Object);

            var output = new TestOutputPort();
            useCase.SetOutputPort(output);

            await useCase.Execute();

            Assert.NotNull(output.Result);
            var result = output.Result!;

            Assert.Equal("001", result.First().Numero);
            Assert.Equal(2, result.First().DiasEmAtraso);
            Assert.Equal(1, result.Last().DiasEmAtraso);

            Assert.Equal(decimal.Round(titulo.ValorOriginal, 2), result.First().ValorOriginal);
            Assert.Equal(decimal.Round(titulo2.ValorOriginal, 2), result.Last().ValorOriginal);
        }
    }
}