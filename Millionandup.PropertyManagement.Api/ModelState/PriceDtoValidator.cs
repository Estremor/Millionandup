using FluentValidation;
using Millionandup.PropertyManagement.Aplication.Dtos;

namespace Millionandup.PropertyManagement.Api.ModelState
{
    public class PriceDtoValidator : AbstractValidator<PriceDto>
    {
        public PriceDtoValidator()
        {
            RuleFor(o => o.InernalCode).NotEmpty();
            RuleFor(o => o.Price).NotEmpty().NotEqual(0);
        }
    }
}
