using System;
using SCParking.Domain.Interfaces;

namespace SCParking.Domain.Common
{
    public class DateRange:IRange<DateTime>
    {
        public DateRange(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }

        public DateTime Start { get; private set; }
        public DateTime End { get; private set; }

        public bool Includes(DateTime value)
        {
            bool include = Start.CompareTo(value) <= 0 && End.CompareTo(value) >= 0;
            return include;
        } 

        public bool Includes(IRange<DateTime> range) => Start.CompareTo(range.Start) < 0 && End.CompareTo(range.End) > 0;
    }
}
