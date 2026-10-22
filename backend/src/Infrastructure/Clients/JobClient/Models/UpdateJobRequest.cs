using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Totvs.Ats.Infrastructure.Clients.JobClient.Models;

internal readonly record struct UpdateJobRequest(Guid JobId, int UnitsChange);
