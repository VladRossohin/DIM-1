﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.BL.Interfaces
{
    public interface IAuthService<T>
    {
        Task<string> GenerateTokenAsync(T item);
    }
}