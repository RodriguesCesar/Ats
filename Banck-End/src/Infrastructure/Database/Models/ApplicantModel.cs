namespace Totvs.Ats.Infrastructure.Database.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class ApplicantModel
{

    [BsonId]
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
    public DateTimeOffset CreatedOn { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTimeOffset LastModifiedOn { get; set; }
    public Guid LastModifiedBy { get; set; }

}