using Domain.Entities;
using System.Security.Principal;

namespace Webapi.UseCases.V1.Titulos.GetTitulos
{
    public class GetTitulosResponse
    {
        public string Numero { get; set; } = string.Empty;
        public string NomeDevedor { get; set; } = string.Empty;
        public int QuantidadeParcelas { get; set; }
        public decimal ValorOriginal { get; set; }
        public DateTime DataVencimento { get; set; }
        public decimal ValorAtualizado { get; set; }
        public int DiasEmAtraso { get; set; }
        public decimal Multa { get; set; }
        public decimal Juros { get; set; }
    }
}
