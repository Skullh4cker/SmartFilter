using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalVoyage
{
    public class Time
    {
        public int Days { get; set; }
        public int Hours { get; set; }
        public int Minutes { get; set; }
        public Time(int days, int hours, int minutes)
        {
            Days = days;
            Hours = hours;
            Minutes = minutes;
        }
    }
}
