using System.Net;
using Moq;
using Moq.Protected;

namespace Totvs.Ats.Infrastructure.UnitTests.Clients.ApplicantClient;


public class ApplicantClientTest
{
    [Fact]
    public async Task WHEN_called_api_works_THEN_no_throw()
    {
        var httpClient = GetHttpClientWithMockedMessageHandler();

        var sut = new Infrastructure.Clients.ApplicantClient.ApplicantClient(httpClient);

        await FluentActions
            .Awaiting(() =>
                sut.UpdateApplicant(Guid.Empty, 1))
            .Should()
            .NotThrowAsync();
    }

    [Fact]
    public async Task WHEN_called_api_has_problems_THEN_throws()
    {
        var httpClient = GetThrowingHttpClientWithMockedMessageHandler();

        var sut = new Infrastructure.Clients.ApplicantClient.ApplicantClient(httpClient);

        await FluentActions
            .Awaiting(() =>
                sut.UpdateApplicant(Guid.Empty, 1))
            .Should()
            .ThrowAsync<Exception>();
    }

    private HttpClient GetHttpClientWithMockedMessageHandler()
    {
        var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(""),
            })
            .Verifiable();
        return new HttpClient(handlerMock.Object) { BaseAddress = new Uri("https://somewhere.com/")};
    }

    private HttpClient GetThrowingHttpClientWithMockedMessageHandler()
    {
        var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ThrowsAsync(new Exception())
            .Verifiable();
        return new HttpClient(handlerMock.Object);
    }
}