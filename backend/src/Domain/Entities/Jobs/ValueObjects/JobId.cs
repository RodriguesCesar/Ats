using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Totvs.Ats.Domain.Entities.Jobs.ValueObjects
{
    public readonly record struct JobId(Guid Value);
}
