using FluentValidation;
using Rishvi.Modules.Users.Models.DTOs;
using System.Linq;

namespace Rishvi.Modules.Users.Validators
{
    public class UserEditValidator : AbstractValidator<CourierServiceDto>
    {
        public UserEditValidator()
        {
            //RuleFor(v => v.Name).NotEmpty().MaximumLength(100);
        }
    }
}