namespace AutoUpdateChoco.Contracts
{
    public interface IMediator
    {
        Task Send<TCommand>(TCommand command) where TCommand : ICommand;
        Task<TResponse> Send<TResponse>(ICommand<TResponse> command);
        Task<TResponse> SendQuery<TResponse>(IQuery<TResponse> query);
    }
}
