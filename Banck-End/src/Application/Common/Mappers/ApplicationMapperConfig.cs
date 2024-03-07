using Totvs.Ats.Domain.Entities.Applicants;
using Totvs.Ats.Domain.Entities.Applicants.ValueObjects;
using Totvs.Ats.Application;
using Totvs.Ats.Domain.Entities.Jobs.Enums;
using Totvs.Ats.Domain.Entities.Jobs.ValueObjects;
using Totvs.Ats.Domain.Entities.Jobs;
using Totvs.Ats.Application.ATS.Jobs.DTOs;
using Totvs.Ats.Application.ATS.Applicants.DTOs;

namespace Totvs.Ats.Application.Common.Mappers;

public static class ApplicationMapperConfig
{
    public static void AddMappingConfigs()
    {
        //Job

        TypeAdapterConfig<Guid, JobId>
        .ForType()
        .MapWith(value => new JobId(value));

        TypeAdapterConfig<JobId, Guid>
       .ForType()
       .MapWith(applicant => applicant.Value);

        TypeAdapterConfig<string, JobExperience>
        .ForType()
        .MapWith(job => job.ParseEnum<JobExperience>());

        TypeAdapterConfig<JobExperience, string>
        .ForType()
        .MapWith(job => Enum.GetName(job.GetType(), job)!);

        TypeAdapterConfig<string, JobType>
        .ForType()
        .MapWith(job => job.ParseEnum<JobType>());

        TypeAdapterConfig<JobType, string>
        .ForType()
        .MapWith(job => Enum.GetName(job.GetType(), job)!);

 
        //Applicant

        TypeAdapterConfig<Applicant, ApplicantDto>
        .ForType();
        TypeAdapterConfig<ApplicantDto, Applicant>
        .ForType();


        TypeAdapterConfig<Guid, ApplicantId>
         .ForType()
         .MapWith(value => new ApplicantId(value));

        TypeAdapterConfig<ApplicantId, Guid>
       .ForType()
       .MapWith(applicant => applicant.Value);

        TypeAdapterConfig<string, ApplicantId>
       .ForType()
       .MapWith(value => new ApplicantId(new Guid(value)));


    }
}