using System.Net;
using System.Numerics;
using Totvs.Ats.Domain.Entities.Applicants;
using Totvs.Ats.Domain.Entities.Applicants.ValueObjects;

namespace Totvs.Ats.Application.CommonTests.Builders;

public static class ApplicantBuilder
{
  
    public static Applicant GetApplicantEmpty()
    {
        return new Applicant
        (
            Id: default,
            FirstName: default!,
            LastName: default!,
            Summary: default,
            ProfilePhoto: default,
            Email: default!,
            Phone: default,
            Address: default,
            Skills: default,
            ApplyDate: default,
            CreatedOn: default,
            CreatedBy: default,
            LastModifiedOn: default,
            LastModifiedBy: default
        );
    }

    public static Applicant GetApplicant()
    {
        var createdBy = Guid.NewGuid();
        var createdOn = DateTimeOffset.Now;

        var applicant =  new Applicant
        (
            Id: new ApplicantId(Guid.NewGuid()),
            FirstName: "Cesar",
            LastName: "Rodrigues de Sousa",
            Summary: "Analyst",
            ProfilePhoto: "Cesar.jpg",
            Email: "cesar@gmail.com",
            Phone: "9722250000",
            Address: "Rua Marica",
            Skills: "Analyst",
            ApplyDate: DateTime.Now,
            CreatedOn: createdOn,
            CreatedBy: createdBy,
            LastModifiedOn: createdOn,
            LastModifiedBy: createdBy
        );
       

        return applicant;
    }

    public static Applicant GetApplicantWithId(Guid guid = new())
    {
        return new Applicant
        (
            Id: new ApplicantId(guid),
            FirstName: "Cesar",
            LastName: "Rodrigues de Sousa",
            Summary: "Dev",
            ProfilePhoto: "Cesar.jpg",
            Email: "cesar@gmail.com",
            Phone: "9722250000",
            Address: "Rua Marica",
            Skills: "Dev",
            ApplyDate: DateTime.Now,
            CreatedOn: default,
            CreatedBy: default,
            LastModifiedOn: default,
            LastModifiedBy: default
        );
    }

    public static Applicant GetApplicantWithFirstName(string firstName = "Cesar")
    {
        return new Applicant
        (
            Id: default,
            FirstName: firstName,
            LastName: default!,
            Summary: default,
            ProfilePhoto: default,
            Email: default!,
            Phone: default,
            Address: default,
            Skills: default,
            ApplyDate: default,
            CreatedOn: default,
            CreatedBy: default,
            LastModifiedOn: default,
            LastModifiedBy: default
        );
    }

    public static Applicant GetApplicantWithEmail(string email = "Applicant e-mail")
    {
        return new Applicant
        (
            Id: default,
            FirstName: default!,
            LastName: default!,
            Summary: default,
            ProfilePhoto: default,
            Email: email!,
            Phone: default,
            Address: default,
            Skills: default,
            ApplyDate: default,
            CreatedOn: default,
            CreatedBy: default,
            LastModifiedOn: default,
            LastModifiedBy: default
        );
    }

    public static Applicant GetApplicantWithSkills(string skills)
    {
        return new Applicant
        (
            Id: default,
            FirstName: default!,
            LastName: default!,
            Summary: default,
            ProfilePhoto: default,
            Email: default!,
            Phone: default,
            Address: default,
            Skills: skills,
            ApplyDate: default,
            CreatedOn: default,
            CreatedBy: default,
            LastModifiedOn: default,
            LastModifiedBy: default
        );
    }

    public static Applicant GetApplicantWithProfilePhoto(string profilePhoto)
    {
        return new Applicant
        (
            Id: default,
            FirstName: default!,
            LastName: default!,
            Summary: default,
            ProfilePhoto: profilePhoto,
            Email: default!,
            Phone: default,
            Address: default,
            Skills: default,
            ApplyDate: default,
            CreatedOn: default,
            CreatedBy: default,
            LastModifiedOn: default,
            LastModifiedBy: default
        );
    }
}