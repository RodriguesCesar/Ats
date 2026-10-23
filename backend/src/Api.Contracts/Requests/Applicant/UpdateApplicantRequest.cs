namespace Totvs.Ats.Totvs.Ats.Api.Contracts.Requests.Applicant;

public class UpdateApplicantRequest
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
    public DateTime? ModifiedOn { get; set; }

    public UpdateApplicantRequest(Guid id,
             string firstName,
             string lastName,
             string? summary,
             string? profilePhoto,
             string email,
             string? phone,
             string? address,
             string? skills,
             DateTime? modifiedOn)
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
        ModifiedOn = modifiedOn;
    }
}