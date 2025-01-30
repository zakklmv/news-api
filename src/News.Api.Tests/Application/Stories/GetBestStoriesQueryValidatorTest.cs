using FluentAssertions;
using News.Api.Application.Stories.v1;

namespace News.Api.Tests.Application.Stories
{
    public class GetBestStoriesQueryValidatorTest
    {
        public static IEnumerable<object[]> TestData => new List<object[]>
        {
            new object[]
            {
                new GetBestStoriesQuery { Quantity = -1 },
                new []
                {
                    "Quantity must be 1 or more"
                }
            },
            new object[]
            {
                new GetBestStoriesQuery { Quantity = 0 },
                new []
                {
                    "Quantity must be 1 or more"
                }
            }
        };


        [Theory]
        [MemberData(nameof(TestData))]
        public async Task Should_Errors(GetBestStoriesQuery query, string[] errorMessages)
        {
            var validator = new GetBestStoriesQueryValidator();
            var result = await validator.ValidateAsync(query);

            result.Errors.Should().HaveCount(errorMessages.Length);
            result.Errors.Select(s => s.ErrorMessage).Should().BeEquivalentTo(errorMessages);
        }
    }
}
