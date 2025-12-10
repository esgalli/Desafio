using System;
using Domain.Entities;
using Xunit;

namespace Tests.Entities
{
    public class TituloTests
    {
        [Fact]
        public void EstaAtrasado_ReturnsTrue_WhenVencimentoBeforeToday()
        {
            var titulo = new Titulo
            {
                DataVencimento = DateTime.Today.AddDays(-5)
            };

            Assert.True(titulo.EstaAtrasado());
            Assert.Equal(5, titulo.DiasEmAtraso());
        }

        [Fact]
        public void EstaAtrasado_ReturnsFalse_WhenVencimentoIsTodayOrFuture()
        {
            var tituloToday = new Titulo { DataVencimento = DateTime.Today };
            var tituloFuture = new Titulo { DataVencimento = DateTime.Today.AddDays(3) };

            Assert.False(tituloToday.EstaAtrasado());
            Assert.Equal(0, tituloToday.DiasEmAtraso());

            Assert.False(tituloFuture.EstaAtrasado());
            Assert.Equal(0, tituloFuture.DiasEmAtraso());
        }
    }
}