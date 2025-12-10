using System;

namespace Application.DTO;
public sealed class TituloDto
{
    public string Numero { get; init; } = string.Empty;
    public string NomeDevedor { get; init; } = string.Empty;
    public int QuantidadeParcelas { get; init; }
    public decimal ValorOriginal { get; init; }
    public DateTime DataVencimento { get; init; }
    public int DiasEmAtraso { get; init; }
    public decimal Juros { get; init; }
    public decimal Multa { get; init; }
    public decimal ValorAtualizado { get; init; }
}