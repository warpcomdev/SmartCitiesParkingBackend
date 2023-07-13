using System;
using System.Collections.Generic;
using System.Text;

namespace SCParking.Domain.Interfaces
{
    public interface IRange<T> where T : IComparable<T>
    {
        T Start { get; }
        T End { get; }
        bool Includes(T value);
        bool Includes(IRange<T> range);
    }
}
