using Totvs.Ats.Domain.Common.Validators;

namespace Totvs.Ats.Application.CommonTests.Builders;

public static class ValidationErrorBuilder
{
    public static IEnumerable<ValidationError> GetValidationErrors()
    {
        return new List<ValidationError>
        {
            new("Applicant_Error", "You are getting error", "Try it again!")
        };
    }
}