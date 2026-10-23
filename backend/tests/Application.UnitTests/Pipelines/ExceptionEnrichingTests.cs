using Totvs.Ats.Application.CommonTests.Builders;
using Totvs.Ats.Application.Pipelines;
using FluentResults;
using MediatR;
using Totvs.Ats.Application.ATS.Applicants.Commands.AddApplicant;
using Totvs.Ats.Application.ATS.Applicants.DTOs;

namespace Totvs.Ats.Application.UnitTests.Pipelines;

public class ExceptionEnrichingTests
{
    private readonly Mock<RequestHandlerDelegate<Result<ApplicantDto>>> _handlerMock = new();
    private readonly AddApplicantCommand _command;
    private readonly ExceptionEnrichingPipelineBehaviour<AddApplicantCommand, Result<ApplicantDto>> _sut = new();

    public ExceptionEnrichingTests()
    {
        _command = AddApplicantCommandBuilder.GetAddApplicantCommandEmpty();
    }

    [Fact]
    public async Task WHEN_handling_error_THEN_ActionName_data_should_be_filled()
    {
        _handlerMock
            .Setup(x => x.Invoke())
            .ThrowsAsync(new Exception());

        await FluentActions
            .Invoking(()  =>
                _sut.Handle(
                    _command,
                    _handlerMock.Object,
                    CancellationToken.None))
            .Should()
            .ThrowAsync<Exception>()
            .Where(x => x.Data.Contains("ActionName"));
    }

    [Fact]
    public async Task WHEN_no_error_happens_THEN_no_exception_should_be_thrown()
    {
        var applicantDto = ApplicantDtoBuilder.GetApplicantDtoEmpty();

        _handlerMock
            .Setup(x => x.Invoke())
            .ReturnsAsync(applicantDto);

        await FluentActions
            .Invoking(() =>
                _sut.Handle(
                    _command,
                    _handlerMock.Object,
                    CancellationToken.None))
            .Should()
            .NotThrowAsync<Exception>();
    }
}