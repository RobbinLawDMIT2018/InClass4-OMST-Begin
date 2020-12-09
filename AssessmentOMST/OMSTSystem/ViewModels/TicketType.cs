using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMSTSystem.ViewModels
{
    public class Category
    {
        public string Description { get;set;}
        public decimal TicketPrice { get; set; }
    }
    public class RecentTickets
    {
        public int TicketID { get; set; }
        public int ShowTimeID { get; set; }
        public int TicketCategoryID { get; set; }
        public decimal TicketPrice { get; set; }
        public decimal TicketPremium { get; set; }

    }
    public class TicketType
    {
        public string CategoryDescription { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public TicketType()
        {

        }
        public TicketType(string description, int qty, decimal price)
        {
            CategoryDescription = description;
            Quantity = qty;
            Price = price;
        }
    }
}
