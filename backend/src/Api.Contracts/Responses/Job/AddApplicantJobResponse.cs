namespace Totvs.Ats.Totvs.Ats.Api.Contracts.Responses.Job;

public class AddApplicantJobResponse
{
    public Guid JobId { get; set; }
    public Guid ApplicantId { get; set; }


    public AddApplicantJobResponse(Guid jobId, Guid applicantId)
    {
        JobId = jobId;
        ApplicantId = applicantId;
    }
}