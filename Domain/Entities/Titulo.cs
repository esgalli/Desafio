using System;
using Domain.Services;

namespace Domain.Entities;
public class Titulo
{
    public long Id { get; set; }    
    public string Numero { get; set; } = string.Empty;
    public string NomeDevedor { get; set; } = string.Empty;
    public int QuantidadeParcelas { get; set; }
    public decimal ValorOriginal { get; set; }
    public DateTime DataVencimento { get; set; }
    public bool EstaAtrasado() => DateTime.Today > DataVencimento;
    public int DiasEmAtraso() => EstaAtrasado() ? (DateTime.Today - DataVencimento).Days : 0;    
}