using System.ComponentModel.DataAnnotations;

namespace Totvs.Ats.Domain.Entities.Jobs.Enums
{
    public enum JobExperience
    {
        Internship = 0,
        [Display(Name = "Entry level")]
        EntryLevel = 1,
        Junior = 2,
        Mid = 3,
        [Display(Name = "Mid-Senior level")]
        MidSenior = 4,
        Senior = 5,
        Director = 6,
        Executive = 7
    }
}
