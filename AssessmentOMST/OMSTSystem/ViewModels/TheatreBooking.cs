using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMSTSystem.ViewModels
{
    public class TheatreBooking
    {
        public int TheatreID { get; set; }
        public List<MovieBooking> Movies { get; set; }
    }
}
