namespace Totvs.Ats.Domain.Common.Validators;

public record struct ValidationError (
    string Code,
    string Message,
    string? Hint);