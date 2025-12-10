using Domain.Entities;

namespace Domain.Repositories;
public interface IFinanceRepository
{
    Task<TaxaFinanceira> GetCurrentAsync(CancellationToken cancellationToken = default);
}