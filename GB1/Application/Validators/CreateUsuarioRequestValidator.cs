using FluentValidation;
using GB1.Application.DTO.Request;

namespace GB1.Application.Validators
{
    public class CreateUsuarioRequestValidator : AbstractValidator<CreateUsuarioRequest>
    {
        public CreateUsuarioRequestValidator()
        {
            RuleFor(u => u.Username)
                .NotEmpty()
                .WithMessage("O username é obrigatório.")
                .MaximumLength(50);

            RuleFor(u => u.Email)
                .NotEmpty()
                .WithMessage("Email é obrigatório.")
                .MaximumLength(255)
                .WithMessage("Motorização deve ter no máximo 50 caracteres.")
                .Must(email => email.Contains("@"))
                .WithMessage("Email deve conter '@'.");

            RuleFor(u => u.Senha)
             .NotEmpty()
             .WithMessage("Senha é obrigatória.")
             .MaximumLength(255)
             .WithMessage("Senha deve ter no máximo 255 caracteres.")
             .Must(s =>
                 s.Any(char.IsUpper) &&                       // Pelo menos uma letra maiúscula
                 s.Any(ch => !char.IsLetterOrDigit(ch)) &&    // Pelo menos um caractere especial
                 s.Length > 8)                                // Maior que 8 caracteres
             .WithMessage("Senha deve conter pelo menos uma letra maiúscula, um caractere especial e ter mais de 8 caracteres.");
        }
    }
}
