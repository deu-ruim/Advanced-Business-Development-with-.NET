using FluentValidation;
using GB1.Application.DTO.Request;

namespace GB1.Application.Validators
{
    public class CreateDesastreRequestValidator : AbstractValidator<CreateDesastreRequest>
    {
        public CreateDesastreRequestValidator()
        {
            RuleFor(u => u.Titulo)
                .NotEmpty()
                .WithMessage("O Titulo é obrigatório.")
                .MaximumLength(100);

            RuleFor(u => u.Descricao)
                .NotEmpty()
                .WithMessage("Descricao é obrigatório.")
                .MaximumLength(1000);
        }
    }
}
