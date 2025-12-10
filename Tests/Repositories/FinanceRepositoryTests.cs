using System.Threading.Tasks;
using Infrastructure.Repositories;
using Xunit;

namespace Tests.Repositories
{
    public class FinanceRepositoryTests
    {
        [Fact]
        public async Task GetCurrentAsync_ReturnsMostRecentTaxa()
        {
            var repo = new FinanceRepository();

            var taxa = await repo.GetCurrentAsync();
           
            Assert.NotNull(taxa);
            Assert.Equal(2m, taxa.MultaPercentual);
        }
    }
}