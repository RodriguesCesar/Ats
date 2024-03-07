namespace Totvs.Ats.Totvs.Ats.Api.Contracts.Requests.Job;

public class AddJobRequest
{

    public string? Title { get; set; } 
    public string? Description { get; set; } 
    public string? Country { get; set; } 
    public string? City { get; set; } 
    public string? ContactPhone { get; set; } 
    public string? ContactEmail { get; set; } 
    public string? Manager { get; set; } 
    public string JobType { get; set; } 
    public string JobExperience { get; set; } 
    public string? RequiredSkills { get; set; }
    public DateTime PostDate { get; set; } 
    public float SalaryFrom { get; set; } 
    public float SalaryTo { get; set; } 


    public AddJobRequest(
         string? title,
         string? description,
         string? country,
         string? city,
         string? contactPhone,
         string? contactEmail,
         string? manager,
         string  jobType,
         string  jobExperience,
         string?  requiredSkills,
         DateTime postDate,
         float salaryFrom,
         float salaryTo)
    {
     
        Title = title;
        Description = description;
        Country = country;
        City = city;
        ContactPhone = contactPhone;
        ContactEmail = contactEmail;
        Manager = manager;
        JobType = jobType;
        JobExperience = jobExperience;
        RequiredSkills = requiredSkills;
        PostDate = postDate;
        SalaryFrom = salaryFrom;
        SalaryTo = salaryTo;
    }
}
