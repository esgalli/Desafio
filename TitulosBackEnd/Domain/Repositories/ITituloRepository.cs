using Domain.Entities;

namespace Domain.Repositories;
public interface ITituloRepository
{
    Task<IEnumerable<Titulo>> GetAllAsync(CancellationToken cancellationToken = default);
    Task <List<Titulo>> ObterTitulosAtrasados();
}