﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtonTalent.Domain.Models
{
    public class Response<T>
    {
        public bool Success { get; set; }

        public T? Content { get; set; }
    }
}
