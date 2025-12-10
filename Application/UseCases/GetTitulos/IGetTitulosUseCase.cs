namespace Application.UseCases.GetTitulos
{
    public interface IGetTitulosUseCase
    {
        Task Execute();

        void SetOutputPort(IOutputPort outputPort);
    }
}
