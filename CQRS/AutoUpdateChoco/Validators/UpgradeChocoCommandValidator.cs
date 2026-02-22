using FluentValidation;
using AutoUpdateChoco.Commands;

namespace AutoUpdateChoco.Validators
{
    public class UpgradeChocoCommandValidator : AbstractValidator<UpgradeChocoCommand>
    {
        public UpgradeChocoCommandValidator()
        {
            // Keine Validierungsregeln nötig, da das Command keine Properties hat
            // Validator existiert für Konsistenz in der Pipeline
        }
    }
}
