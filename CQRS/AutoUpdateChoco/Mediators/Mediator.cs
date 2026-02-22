using AutoUpdateChoco.Contracts;



namespace AutoUpdateChoco.Mediators
{
    public class Mediator : IMediator
    {
        private readonly IServiceProvider _serviceProvider;

        public Mediator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task Send<TCommand>(TCommand command) where TCommand : ICommand
        {
            var handlerType = typeof(ICommandHandler<>).MakeGenericType(typeof(TCommand));
            var handler = _serviceProvider.GetService(handlerType);

            if (handler == null)
                throw new InvalidOperationException($"Kein Handler für {typeof(TCommand).Name} registriert");

            var method = handlerType.GetMethod("Handle");
            if (method == null)
                throw new InvalidOperationException("Handle-Methode nicht gefunden");

            await (Task)method.Invoke(handler, new object[] { command })!;
        }

        public async Task<TResponse> Send<TResponse>(ICommand<TResponse> command)
        {
            var commandType = command.GetType();
            var handlerType = typeof(ICommandHandler<,>).MakeGenericType(commandType, typeof(TResponse));
            var handler = _serviceProvider.GetService(handlerType);

            if (handler == null)
                throw new InvalidOperationException($"Kein Handler für {commandType.Name} mit Rückgabetyp {typeof(TResponse).Name} registriert");

            var method = handlerType.GetMethod("Handle");
            if (method == null)
                throw new InvalidOperationException("Handle-Methode nicht gefunden");

            return await (Task<TResponse>)method.Invoke(handler, new object[] { command })!;
        }

        public async Task<TResponse> SendQuery<TResponse>(IQuery<TResponse> query)
        {
            var queryType = query.GetType();
            var handlerType = typeof(IQueryHandler<,>).MakeGenericType(queryType, typeof(TResponse));
            var handler = _serviceProvider.GetService(handlerType);

            if (handler == null)
                throw new InvalidOperationException($"Kein Handler für {queryType.Name} registriert");

            var method = handlerType.GetMethod("Handle");
            if (method == null)
                throw new InvalidOperationException("Handle-Methode nicht gefunden");

            return await (Task<TResponse>)method.Invoke(handler, new object[] { query })!;
        }
    }
}
