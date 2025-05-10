

using FluentValidation;

namespace Museum.Application.SQRS.MuseumEvents.Common
{
    public class PhotoUploadDtoValidator : AbstractValidator<PhotoUploadDto>
    {
        public PhotoUploadDtoValidator()
        {
            RuleFor(photoDto => photoDto.Name).NotEmpty();
            RuleFor(photoDto => photoDto.Content)
                .Must(content => content.Length != 0)
                .WithMessage("Invalid Photo.");

            RuleFor(photoDto => photoDto.Content)
                .Must(content => content.Length <= 10 * 1024 * 1024)
                .WithMessage("The size of the photo should be no more than 10 MB.");
        }
    }
}
