using System;
using System.Collections.Generic;

namespace DIMS.BL.Interfaces
{
    public interface IVService<T> : IDisposable
    {
        T GetById(int id);
        IEnumerable<T> GetAll();
    }
}
