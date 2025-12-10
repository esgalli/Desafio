using Application.DTO;
using Application.UseCases.GetTitulos;
using Asp.Versioning;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;

namespace Webapi.UseCases.V1.Titulos.GetTitulos
{
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/titulos", Name = "Títulos")]
    [ApiController]    
    public class TitulosController : ControllerBase, IOutputPort
    {
        private IActionResult? _viewModel;        

        [HttpGet, MapToApiVersion("1.0")]
        [Route("atrasados")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetTitulosResponse))]
        public async Task<IActionResult> TitulosAtrasados([FromServices] IGetTitulosUseCase useCase)
        {
            useCase.SetOutputPort(this);

            await useCase.Execute()
                .ConfigureAwait(false);

            return this._viewModel!;
        }

        void IOutputPort.Ok(IList<TituloDto> titulos)
        {
            this._viewModel = Ok(titulos.Select(t => new GetTitulosResponse()
            {                
                Numero = t.Numero,
                NomeDevedor = t.NomeDevedor,
                QuantidadeParcelas = t.QuantidadeParcelas,
                ValorOriginal = t.ValorOriginal,
                DataVencimento = t.DataVencimento,
                DiasEmAtraso = t.DiasEmAtraso,
                Multa = t.Multa,
                Juros = t.Juros,
                ValorAtualizado = t.ValorAtualizado
            }).ToList());
        }
    }
}
