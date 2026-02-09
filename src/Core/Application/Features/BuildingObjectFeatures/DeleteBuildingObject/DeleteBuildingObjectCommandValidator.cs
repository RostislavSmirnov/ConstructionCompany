using FluentValidation;
namespace Application.Features.BuildingObjectFeatures.DeleteBuildingObject
{
    public class DeleteBuildingObjectCommandValidator : AbstractValidator<DeleteBuildingObjectCommand>
    {
        public DeleteBuildingObjectCommandValidator() 
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id не может быть пустым");
        }
    }
}
