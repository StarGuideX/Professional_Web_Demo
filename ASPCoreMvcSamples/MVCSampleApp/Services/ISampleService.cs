﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCSampleApp.Services
{
    public interface ISampleService
    {
        IEnumerable<string> GetSampleStrings();
    }
}
