namespace Totvs.Ats.Totvs.Ats.Api.Contracts.Requests.Job;

public class AddApplicantJobRequest
{
    public Guid JobId { get; set; }
    public Guid ApplicantId { get; set; }


    public AddApplicantJobRequest(Guid jobId, Guid applicantId)
    {
        JobId = jobId;
        ApplicantId = applicantId;
    }
}