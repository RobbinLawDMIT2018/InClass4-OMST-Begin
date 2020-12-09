using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMSTSystem.ViewModels
{
    public class MovieShowTimes
    {
        public int TheatreNumber { get; set; } 
        public int ShowTimeID { get; set; }
        public DateTime Times { get; set; }
        public string ScheduleTime
        {
            get
            {
                return Times.TimeOfDay.ToString() + " (" + TheatreNumber.ToString() + ")";
            }
        }
    }
}
