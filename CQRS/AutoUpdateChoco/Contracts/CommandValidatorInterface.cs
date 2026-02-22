namespace AutoUpdateChoco.Contracts
{
    public interface ICommandValidator<in TCommand>
    {
        Task ValidateAndThrowAsync(TCommand command);
    }
}
