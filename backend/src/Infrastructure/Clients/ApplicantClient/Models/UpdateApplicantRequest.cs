using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Totvs.Ats.Infrastructure.Clients.ApplicantClient.Models
{
    internal readonly record struct UpdateApplicantRequest(Guid ApplicantId, int UnitsChange);
}
