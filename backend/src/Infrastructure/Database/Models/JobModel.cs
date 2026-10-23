namespace Totvs.Ats.Infrastructure.Database.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Totvs.Ats.Domain.Entities.Jobs.ValueObjects;
using Totvs.Ats.Domain.Entities.Jobs;
using Totvs.Ats.Domain.Entities.Jobs.Enums;

public class JobModel
{

    [BsonId]
    public Guid Id { get; set; }

    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Country { get; set; } 
    public string? City { get; set; }
    public string? ContactPhone { get; set; } 
    public string? ContactEmail { get; set; } 
    public string? Manager { get; set; } 
    public JobType JobType { get; set; } 
    public JobExperience JobExperience { get; set; } 
    public string? RequiredSkills { get; set; } 
    public DateTime PostDate { get; set; } 
    public DateTime Expires { get; set; } 
    public float SalaryFrom { get; set; } 
    public float SalaryTo { get; set; } 
    public ICollection<ApplicantModel>? Applicants { get; set; }
    public DateTimeOffset CreatedOn { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTimeOffset LastModifiedOn { get; set; }
    public Guid LastModifiedBy { get; set; }

}