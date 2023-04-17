using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtonTalent.Domain.Models
{
    public class Response<T>
    {
        public bool Success { get; init; }

        public T? Content { get; init; }
    }
}
