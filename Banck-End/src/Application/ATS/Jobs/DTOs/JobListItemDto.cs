namespace Totvs.Ats.Application.ATS.Jobs.DTOs;

public readonly record struct JobListItemDto(Guid Id, string Title, string Description, string Country, string ContactPhone);