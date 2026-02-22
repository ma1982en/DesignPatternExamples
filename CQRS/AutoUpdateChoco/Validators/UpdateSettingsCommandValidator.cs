using FluentValidation;
using AutoUpdateChoco.Commands;

namespace AutoUpdateChoco.Validators
{
    public class UpdateSettingsCommandValidator : AbstractValidator<UpdateSettingsCommand>
    {
        public UpdateSettingsCommandValidator()
        {
            When(x => x.UpdateIntervalMinutes.HasValue, () =>
            {
                RuleFor(x => x.UpdateIntervalMinutes!.Value)
                    .GreaterThanOrEqualTo(5)
                    .WithMessage("Das Update-Intervall muss mindestens 5 Minuten betragen.")
                    .LessThanOrEqualTo(1440)
                    .WithMessage("Das Update-Intervall sollte maximal 1440 Minuten (24 Stunden) betragen.");
            });
        }
    }
}
