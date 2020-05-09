using FluentValidation;
using StudentEducationBoardService.Api.AppModels;

namespace StudentEducationBoardService.Api.Validators
{
    public class SchoolValidator : AbstractValidator<CreateSchoolDto>
    {
        public SchoolValidator()
        {
            RuleFor(x => x.SchoolName)
                .NotEmpty()
                .WithMessage("School Name cannot be blank")
                .Length(2, 50)
                .WithMessage("School name can not be more than 50 characters");
        }
    }
}
