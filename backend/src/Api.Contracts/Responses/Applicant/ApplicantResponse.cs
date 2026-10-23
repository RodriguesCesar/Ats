namespace Totvs.Ats.Totvs.Ats.Api.Contracts.Responses.Applicant;

public class ApplicantResponse
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? Summary { get; set; }
    public string? ProfilePhoto { get; set; }
    public string Email { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public string? Skills { get; set; }
    public DateTime ApplyDate { get; set; }

    public ApplicantResponse(Guid id,
             string firstName,
             string lastName,
             string? summary,
             string? profilePhoto,
             string email,
             string? phone,
             string? address,
             string? skills,
             DateTime applyDate)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Summary = summary;
        ProfilePhoto = profilePhoto;
        Email = email;
        Phone = phone;
        Address = address;
        Skills = skills;
        ApplyDate = applyDate;
    }
}