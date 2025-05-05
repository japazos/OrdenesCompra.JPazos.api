using FluentValidation;
using OrdenesCompra.JPazos.application.dto.security;


namespace OrdenesCompra.JPazos.application.Validators.Seguridad
{
    public class SignUpDtoValidator : AbstractValidator<SignUpDto>
    {
        public SignUpDtoValidator()
        {
            RuleFor(x => x.Nombres)
                .NotEmpty().WithMessage("The field Nombres is required.")
                .MinimumLength(5).WithMessage("Minimun numbre of characters for FirstName is 5");

            RuleFor(x => x.Apellidos)
                .NotEmpty()
                .WithMessage("The field Apellidos is required.")
                .MinimumLength(5).WithMessage("Minimun numbre of characters for FirstName is 5");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("The field Email is required.")
                .MinimumLength(5).WithMessage("Minimun numbre of characters for FirstName is 5")
                .EmailAddress()
                .WithMessage("Email address is not valid.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("The field Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
                .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches(@"\d").WithMessage("Password must contain at least one number.")
                .Matches(@"[\!\?\*\.@#\$%\^&\+=]").WithMessage("Password must contain at least one special character (!?*.@#$%^&+=).");
        }
    }
}
