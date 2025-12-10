using System;
using Domain.Entities;
using Domain.Services;
using Xunit;

namespace Tests.Services
{
    public class CalculadoraServiceTests
    {
        private readonly ICalculadoraService _service = new CalculadoraService();

        [Fact]
        public void CalcularMulta_ReturnsZero_WhenNotAtrasado()
        {
            var t = new Titulo { ValorOriginal = 100m, DataVencimento = DateTime.Today.AddDays(1) };

            var multa = _service.CalcularMulta(t, 2m);

            Assert.Equal(0m, multa);
        }

        [Fact]
        public void CalcularMulta_CalculatesPercentRounded()
        {
            var t = new Titulo { ValorOriginal = 100.235m, DataVencimento = DateTime.Today.AddDays(-2) };

            var multa = _service.CalcularMulta(t, 2m);

            Assert.Equal(2.00m, multa);
        }

        [Fact]
        public void CalcularJuros_ReturnsZero_WhenNotAtrasado()
        {
            var t = new Titulo { ValorOriginal = 50m, DataVencimento = DateTime.Today.AddDays(2) };

            var juros = _service.CalcularJuros(t, 1m);

            Assert.Equal(0m, juros);
        }

        [Fact]
        public void CalcularJuros_CalculatesCorrectly()
        {
            var t = new Titulo { ValorOriginal = 100m, DataVencimento = DateTime.Today.AddDays(-3) };

            var juros = _service.CalcularJuros(t, 1m);

            Assert.Equal(3.00m, juros);
        }

        [Fact]
        public void CalcularValorAtualizado_ReturnsOriginal_WhenNotAtrasado()
        {
            var t = new Titulo { ValorOriginal = 123.456m, DataVencimento = DateTime.Today.AddDays(1) };

            var valor = _service.CalcularValorAtualizado(t, 2m, 1m);

            Assert.Equal(decimal.Round(t.ValorOriginal, 2), valor);
        }

        [Fact]
        public void CalcularValorAtualizado_ReturnsSumOfOriginalMultaJuros_WhenAtrasado()
        {
            var t = new Titulo { ValorOriginal = 200m, DataVencimento = DateTime.Today.AddDays(-2) };

            var valor = _service.CalcularValorAtualizado(t, 1m, 0.5m);

            Assert.Equal(204.00m, valor);
        }
    }
}