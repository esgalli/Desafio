using Application.DTO;
using Domain.Entities;
using Domain.Repositories;
using Domain.Services;

namespace Application.UseCases.GetTitulos;
public class GetTitulosUseCase : IGetTitulosUseCase
{
    private IOutputPort _outputPort = null!;
    private readonly ITituloRepository _tituloRepository;
    private readonly IFinanceRepository _financeRepository;
    private readonly ICalculadoraService _calculadora;

    public GetTitulosUseCase(
        ITituloRepository tituloRepository,
        IFinanceRepository financeRepository,
        ICalculadoraService calculadora)
    {
        _tituloRepository = tituloRepository;
        _financeRepository = financeRepository;
        _calculadora = calculadora;
    }

    public async Task Execute()
    {
        var taxa = await _financeRepository.GetCurrentAsync();

        var titulos = await _tituloRepository.ObterTitulosAtrasados();

        var multaPercentual = taxa?.MultaPercentual ?? 0m;
        var jurosDiarioPercentual = taxa?.JurosDiarioPercentual ?? 0m;

        var dtos = (titulos ?? new List<Titulo>())
            .Select(t =>
            {
                var dias = t.DiasEmAtraso();
                var valorOriginalRounded = decimal.Round(t.ValorOriginal, 2);
                var multa = _calculadora.CalcularMulta(t, multaPercentual);
                var juros = _calculadora.CalcularJuros(t, jurosDiarioPercentual);
                var valorAtualizado = _calculadora.CalcularValorAtualizado(t, multaPercentual, jurosDiarioPercentual);

                return new TituloDto
                {
                    Numero = t.Numero,
                    NomeDevedor = t.NomeDevedor,
                    QuantidadeParcelas = t.QuantidadeParcelas,
                    ValorOriginal = valorOriginalRounded,
                    DataVencimento = t.DataVencimento,
                    DiasEmAtraso = dias,
                    Multa = multa,
                    Juros = juros,
                    ValorAtualizado = valorAtualizado
                };
            })
            .OrderByDescending(x => x.DiasEmAtraso)
            .ToList();

        _outputPort.Ok(dtos);
    }

    public void SetOutputPort(IOutputPort outputPort) => _outputPort = outputPort;
}
