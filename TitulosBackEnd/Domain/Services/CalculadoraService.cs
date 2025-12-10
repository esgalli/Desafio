using Domain.Entities;

namespace Domain.Services;
public class CalculadoraService : ICalculadoraService
{
    public decimal CalcularMulta(Titulo t, decimal multaPercentual)
    {
        if (!t.EstaAtrasado()) return 0m;
        var valor = decimal.Round(t.ValorOriginal, 2);
        return decimal.Round(valor * (multaPercentual/100), 2);
    }

    public decimal CalcularJuros(Titulo t, decimal jurosDiarioPercentual)
    {
        if (!t.EstaAtrasado()) return 0m;
        var valor = decimal.Round(t.ValorOriginal, 2);
        var dias = t.DiasEmAtraso();
        return decimal.Round(valor * (jurosDiarioPercentual/100) * dias, 2);
    }

    public decimal CalcularValorAtualizado(Titulo t, decimal multaPercentual, decimal jurosDiarioPercentual)
    {
        var valor = decimal.Round(t.ValorOriginal, 2);
        if (!t.EstaAtrasado()) return valor;
        var multa = CalcularMulta(t, multaPercentual);
        var juros = CalcularJuros(t, jurosDiarioPercentual);
        return decimal.Round(valor + multa + juros, 2);
    }
}