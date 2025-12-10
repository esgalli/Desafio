using System;

namespace Domain.Entities;
public class TaxaFinanceira
{
    public int Id { get; set; }
    public decimal MultaPercentual { get; set; }        // ex: 0.02 => 2%
    public decimal JurosDiarioPercentual { get; set; } // ex: 0.001 => 1% por mês
    public DateTime Vigencia { get; set; }
}