using Application.DTO;
using Domain.Entities;

namespace Application.UseCases.GetTitulos
{
    public interface IOutputPort
    {
        void Ok(IList<TituloDto> titulos);
    }
}
