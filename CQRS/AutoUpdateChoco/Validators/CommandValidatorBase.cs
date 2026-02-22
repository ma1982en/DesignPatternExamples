using AutoUpdateChoco.Contracts;
using FluentValidation;

namespace AutoUpdateChoco.Validators
{
    public class CommandValidator<TCommand> : ICommandValidator<TCommand>
    {
        private readonly IValidator<TCommand>? _validator;

        public CommandValidator(IServiceProvider serviceProvider)
        {
            _validator = serviceProvider.GetService(typeof(IValidator<TCommand>)) as IValidator<TCommand>;
        }

        public async Task ValidateAndThrowAsync(TCommand command)
        {
            if (_validator != null)
            {
                await _validator.ValidateAndThrowAsync(command);
            }
        }
    }
}
