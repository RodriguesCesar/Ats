using System.ComponentModel.DataAnnotations;

namespace Totvs.Ats.Domain.Entities.Jobs.Enums
{
    public enum JobType
    {
        [Display(Name = "Full-time")]
        FullTime = 0,
        [Display(Name = "Part-time")]
        PartTime = 1,
        Remote = 2,
        Hybrid = 3,
        Contract = 4,
        Internship = 5,
        Other = 6
    }
}
