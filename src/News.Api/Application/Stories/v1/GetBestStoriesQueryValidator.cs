using FluentValidation;

namespace News.Api.Application.Stories.v1;

public class GetBestStoriesQueryValidator : AbstractValidator<GetBestStoriesQuery>
{
    public GetBestStoriesQueryValidator()
    {
        RuleFor(request => request.Quantity).GreaterThanOrEqualTo(1).WithMessage("Quantity must be 1 or more");
    }
}