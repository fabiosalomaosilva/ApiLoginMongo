using ApiLoginMongo.Dtos;
using FluentValidation;

namespace ApiLoginMongo.Validations
{
    public class UserValidator : AbstractValidator<RegisterDto>
    {
        public UserValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("O campo Nome é obrigatório.");
            RuleFor(x => x.CellphoneNumber).NotNull().WithMessage("O campo Celular é obrigatório.");
            RuleFor(x => x.Email).NotNull().WithMessage("O campo de E-mail é obrigatório.").EmailAddress().WithMessage("O E-mail informado é inválido.");
            RuleFor(x => x.Password).NotNull().WithMessage("A Senha é obrigatória.");
            RuleFor(x => x.ConfirmPassword).NotNull().WithMessage("A Confirmação de Senha é obrigatória.");
            RuleFor(x => x.ConfirmPassword).Equal(user => user.Password).WithMessage("A Confirmação é diferente da senha.");
        }
    }
}
