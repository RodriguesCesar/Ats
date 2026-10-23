using Totvs.Ats.Application.Common.Interfaces;

namespace Totvs.Ats.Infrastructure.Services;

internal class DateTimeService : IDateTimeService
{
    public DateTimeOffset Now => DateTimeOffset.UtcNow;
}