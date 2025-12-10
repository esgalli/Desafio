using Domain.Entities;
using Domain.Repositories;

namespace Infrastructure.Repositories;
public class TituloRepository : ITituloRepository
{
    
    private static readonly List<Titulo> _seed = new()
    {
        new Titulo { Id = 1, Numero = "0001", NomeDevedor = "Empresa A", QuantidadeParcelas = 3, ValorOriginal = 1000m, DataVencimento = DateTime.UtcNow.Date.AddDays(-10) },
        new Titulo { Id = 2, Numero = "0002", NomeDevedor = "João Silva", QuantidadeParcelas = 1, ValorOriginal = 250m, DataVencimento = DateTime.UtcNow.Date.AddDays(-3) },
        new Titulo { Id = 3, Numero = "0003", NomeDevedor = "Maria S.", QuantidadeParcelas = 2, ValorOriginal = 500m, DataVencimento = DateTime.UtcNow.Date.AddDays(5) }, // não está em atraso
        new Titulo { Id = 4, Numero = "0004", NomeDevedor = "João Nogueira", QuantidadeParcelas = 1, ValorOriginal = 300m, DataVencimento = DateTime.UtcNow.Date.AddDays(-42) },
        new Titulo { Id = 5, Numero = "0005", NomeDevedor = "Lucas Santos", QuantidadeParcelas = 2, ValorOriginal = 3000m, DataVencimento = DateTime.UtcNow.Date.AddDays(10) },
        new Titulo { Id = 6, Numero = "0006", NomeDevedor = "George Bucho dos Santos", QuantidadeParcelas = 4, ValorOriginal = 3500m, DataVencimento = DateTime.UtcNow.Date.AddDays(-10) }
    };

    public Task<IEnumerable<Titulo>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult<IEnumerable<Titulo>>(_seed.ToList());
    }

    public Task<List<Titulo>> ObterTitulosAtrasados()
    {
        return Task.FromResult(_seed.Where(t => t.DataVencimento < DateTime.UtcNow.Date).ToList());
    }
}