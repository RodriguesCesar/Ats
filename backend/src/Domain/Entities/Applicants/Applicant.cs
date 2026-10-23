using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Totvs.Ats.Domain.Common;
using Totvs.Ats.Domain.Entities.Applicants.ValueObjects;


namespace Totvs.Ats.Domain.Entities.Applicants;

public sealed record Applicant(
        ApplicantId Id,
        string FirstName,
        string LastName,
        string? Summary,
        string? ProfilePhoto,
        string Email,
        string? Phone,
        string? Address,
        string? Skills,
        DateTime ApplyDate,
        DateTimeOffset CreatedOn,
        Guid CreatedBy,
        DateTimeOffset LastModifiedOn,
        Guid LastModifiedBy)
    : AuditableEntity(
        CreatedOn,
        CreatedBy,
        LastModifiedOn,
        LastModifiedBy)
{

    public ApplicantId Id { get; set; } = Id;
    public string FirstName { get; set; } = FirstName;
    public string LastName { get; set; } = LastName;
    public string? Summary { get; set; } = Summary;
    public string? ProfilePhoto { get; set; } = ProfilePhoto;
    public string Email { get; set; } = Email;
    public string? Phone { get; set; } = Phone;
    public string? Address { get; set; } = Address;
    public string? Skills { get; set; } = Skills;
    public DateTime ApplyDate { get; set; } = ApplyDate;

    public static Applicant Create(
             Guid id,
             string firstName,
             string lastName,
             string? summary,
             string? profilePhoto,
             string email,
             string? phone,
             string? address,
             string? skills,
             DateTime applyDate,
             DateTimeOffset createdOn,
             Guid createdBy)
    {
        return new Applicant(
            Id: new ApplicantId(id),
            FirstName: firstName,
            LastName: lastName,
            Summary: summary,
            Email: email,
            Phone: phone,
            Address: address,
            Skills: skills,
            ProfilePhoto: profilePhoto,
            ApplyDate: applyDate,
            CreatedOn: createdOn,
            CreatedBy: createdBy,
            LastModifiedOn: applyDate,
            LastModifiedBy: createdBy
            );
    }

    public void Update(
     string firstName,
     string lastName,
     string? summary,
     string? profilePhoto,
     string email,
     string? phone,
     string? address,
     string? skills,
    DateTimeOffset modifiedOn,
    Guid modifiedBy)
    {
        FirstName = firstName;
        LastName = lastName;
        Summary = summary;
        ProfilePhoto = profilePhoto;
        Email = email;
        Phone = phone;
        Address = address;
        Skills = skills;
        base.ModifiedBy(
            lastModifiedOn: modifiedOn,
            modifiedBy: modifiedBy);
    }



}
