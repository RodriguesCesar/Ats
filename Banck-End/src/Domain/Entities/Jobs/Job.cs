using Microsoft.Extensions.Hosting.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Totvs.Ats.Domain.Common;
using Totvs.Ats.Domain.Entities.Applicants;
using Totvs.Ats.Domain.Entities.Applicants.ValueObjects;
using Totvs.Ats.Domain.Entities.Jobs.Enums;
using Totvs.Ats.Domain.Entities.Jobs.ValueObjects;

namespace Totvs.Ats.Domain.Entities.Jobs
{
    public sealed record Job(
         JobId Id,
         string? Title,
         string? Description,
         string? Country,
         string? City,
         string? ContactPhone,
         string? ContactEmail,
         string? Manager,
         JobType JobType,
         JobExperience JobExperience,
         string? RequiredSkills,
         DateTime PostDate,
         float SalaryFrom,
         float SalaryTo,
        ICollection<Applicant>? Applicants,
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
        public JobId Id { get; set; } = Id;
        public string? Title { get; set; } = Title;
        public string? Description { get; set; } = Description;
        public string? Country { get; set; } = Country;
        public string? City { get; set; } = City;
        public string? ContactPhone { get; set; } = ContactPhone;
        public string? ContactEmail { get; set; } = ContactEmail;
        public string? Manager { get; set; } = Manager;
        public JobType JobType { get; set; } = JobType;
        public JobExperience JobExperience { get; set; } = JobExperience;
        public string? RequiredSkills { get; set; } = RequiredSkills;
        public DateTime PostDate { get; set; } = PostDate;
        public float SalaryFrom { get; set; } = SalaryFrom;
        public float SalaryTo { get; set; } = SalaryTo;
        public ICollection<Applicant>? Applicants { get; set; } = Applicants;

        public void AddApplicant(Applicant applicant)
        {
            if (Applicants == null) Applicants = new List<Applicant>();

            if (applicant == null) return;

            if (Applicants.Any(x => x.Id == applicant.Id)) return;

            Applicants.Add(applicant);


        }
        public void RemoveApplicant(Applicant applicant)
        {
            if (Applicants == null) Applicants = new List<Applicant>();

            if (applicant == null) return;

            if (!Applicants.Any(x => x.Id == applicant.Id)) return;

            Applicants.Remove(applicant);


        }


        public static Job Create(
         Guid id,
         string? title,
         string? description,
         string? country,
         string? city,
         string? contactPhone,
         string? contactEmail,
         string? manager,
         JobType jobType,
         JobExperience jobExperience,
         string? requiredSkills,
         DateTime postDate,
         float salaryFrom,
         float salaryTo,
         ICollection<Applicant>? applicants,
         DateTimeOffset createdOn,
         Guid createdBy)
        {
            return new Job(
              Id: new JobId(id),
              Title: title,
              Description: description,
              Country: country,
              City: city,
              ContactPhone: contactPhone,
              ContactEmail: contactEmail,
              Manager: manager,
              JobType: jobType,
              JobExperience: jobExperience,
              RequiredSkills: requiredSkills,
              PostDate: postDate,
              SalaryFrom: salaryFrom,
              SalaryTo: salaryTo,
              Applicants: applicants,
              CreatedOn: createdOn,
              CreatedBy: createdBy,
              LastModifiedOn: createdOn,
              LastModifiedBy: createdBy
                );
        }

        public void Update(
         string? title,
         string? description,
         string? country,
         string? city,
         string? contactPhone,
         string? contactEmail,
         string? manager,
         JobType jobType,
         JobExperience jobExperience,
         string? requiredSkills,
         float salaryFrom,
         float salaryTo,
        DateTimeOffset modifiedOn,
        Guid modifiedBy)
        {
            Title = title;
            Description = description;
            Country = country;
            City = city;
            ContactPhone = contactPhone;
            ContactEmail = contactEmail;
            Manager = manager;
            JobType = jobType;
            JobExperience = jobExperience;
            RequiredSkills = requiredSkills;
            SalaryFrom = salaryFrom;
            SalaryTo = salaryTo;           
            base.ModifiedBy(
                lastModifiedOn: modifiedOn,
                modifiedBy: modifiedBy);
        }

    }
}
