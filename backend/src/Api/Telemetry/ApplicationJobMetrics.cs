using System.Diagnostics.Metrics;

namespace Totvs.Ats.Api.Telemetry;

public class ApplicationJobMetrics
{
    public const string ServiceMetricName = "Job";
    private static Meter _meter = new(ServiceMetricName, "1.0.0");
    private static Counter<int> _jobsAdded = _meter.CreateCounter<int>("jobs-added");

    public static void NewJobAdded() => _jobsAdded.Add(1);
}