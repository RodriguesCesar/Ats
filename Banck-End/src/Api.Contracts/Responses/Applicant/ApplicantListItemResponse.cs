namespace Totvs.Ats.Totvs.Ats.Api.Contracts.Responses.Applicant;

public class ApplicantListItemResponse
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }

    public ApplicantListItemResponse(Guid id, string firstName)
    {
        Id = id;
        FirstName = firstName;
    }
}