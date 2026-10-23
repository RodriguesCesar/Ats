using System.Diagnostics;

namespace Totvs.Ats.Application.Pipelines;

public class ExceptionEnrichingPipelineBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (Exception ex)
        {
            Activity.Current?.AddTag("Error", true);
            ex.Data["ActionName"] = typeof(TRequest).Name;
            throw;
        }
    }
}