using ApiLoginMongo.Dtos;
using FluentValidation;

namespace ApiLoginMongo.Validations
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(x => x.Email).NotNull().WithMessage("O campo de E-mail é obrigatório.").EmailAddress().WithMessage("O E-mail informado é inválido.");
            RuleFor(x => x.Password).NotNull().WithMessage("A Senha é obrigatória.");
        }
    }

    public class ChangePasswordDtoValidator : AbstractValidator<ChangePasswordDto>
    {
        public ChangePasswordDtoValidator()
        {
            RuleFor(x => x.Email).NotNull().WithMessage("O campo de E-mail é obrigatório.").EmailAddress().WithMessage("O E-mail informado é inválido.");
            RuleFor(x => x.OldPassword).NotNull().WithMessage("A Senha anterior é obrigatória.");
            RuleFor(x => x.Password).NotNull().WithMessage("A Nova Senha é obrigatória.");
            RuleFor(x => x.ConfirmPassword).NotNull().WithMessage("A Confirmação da Nova Senha é obrigatória.");
            RuleFor(x => x.ConfirmPassword).Equal(user => user.Password).WithMessage("A Confirmação é diferente da nova senha.");
        }
    }
}
