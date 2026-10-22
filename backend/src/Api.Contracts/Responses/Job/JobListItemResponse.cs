namespace Totvs.Ats.Totvs.Ats.Api.Contracts.Responses.Job;

public class JobListItemResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Country { get; set; }
    public string ContactPhone { get; set; }
    public JobListItemResponse(Guid id, string title, string description, string country, string contactPhone)
    {
        Id = id;
        Title = title;
        Description = description;
        Country = country;
        Title = title;
        ContactPhone = contactPhone;
    }
}