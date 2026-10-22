using System.Diagnostics.Metrics;

namespace Totvs.Ats.Api.Telemetry;

public class ApplicationMetrics
{
    public const string ServiceMetricName = "ATS";
    private static Meter _meter = new(ServiceMetricName, "1.0.0");
    private static Counter<int> _applicantsAdded = _meter.CreateCounter<int>("applicants-added");
    public static void NewApplicantAdded() => _applicantsAdded.Add(1);
}
