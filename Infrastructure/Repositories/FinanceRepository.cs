using Domain.Entities;
using Domain.Repositories;

namespace Infrastructure.Repositories;
public class FinanceRepository : IFinanceRepository
{

    private static readonly List<TaxaFinanceira> _seed = new()
    {
        new TaxaFinanceira { Id = 1, MultaPercentual = 2M, JurosDiarioPercentual = (1M/30), Vigencia = DateTime.UtcNow.Date.AddDays(-3) },
        new TaxaFinanceira { Id = 1, MultaPercentual = 3M, JurosDiarioPercentual = (2M/30), Vigencia = DateTime.UtcNow.Date.AddDays(-30) },
        new TaxaFinanceira { Id = 1, MultaPercentual = 4M, JurosDiarioPercentual = (3M/30), Vigencia = DateTime.UtcNow.Date.AddDays(-90) },
    };

    public Task<TaxaFinanceira> GetCurrentAsync(CancellationToken cancellationToken = default)
    {
        var taxa = _seed
            .Where(t => t.Vigencia <= DateTime.UtcNow.Date)
            .OrderByDescending(t => t.Vigencia)
            .FirstOrDefault();

        if (taxa is null)
            throw new InvalidOperationException("No current TaxaFinanceira found.");

        return Task.FromResult(taxa);
    }
}