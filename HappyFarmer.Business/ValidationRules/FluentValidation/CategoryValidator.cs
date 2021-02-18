using FluentValidation;
using HappyFarmer.Entities;

namespace HappyFarmer.Business.ValidationRules.FluentValidation
{
    public class CategoryValidator : AbstractValidator<FarmerCategory>
    {
        public CategoryValidator()
        {
            RuleFor(c => c.Name).NotEmpty();
            RuleFor(c => c.Name).MinimumLength(2);
        }
    }
}
