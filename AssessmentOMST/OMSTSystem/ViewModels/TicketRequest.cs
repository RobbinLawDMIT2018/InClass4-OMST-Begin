using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OMSTSystem.ViewModels
{
    public class TicketRequest
    {
        public int ShowTimeID { get; set; }
        public decimal PremiumPrice { get; set; }
        public List<TicketType> RequireTickets { get; set; }
    }
}
