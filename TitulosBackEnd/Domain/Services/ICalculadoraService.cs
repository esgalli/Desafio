using Domain.Entities;

namespace Domain.Services;
public interface ICalculadoraService
{
    decimal CalcularMulta(Titulo t, decimal multaPercentual);
    decimal CalcularJuros(Titulo t, decimal jurosDiarioPercentual);
    decimal CalcularValorAtualizado(Titulo t, decimal multaPercentual, decimal jurosDiarioPercentual);
}