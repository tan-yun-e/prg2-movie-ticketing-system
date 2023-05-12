using System;
using System.Collections.Generic;

namespace PRG2_T07_Team12
{
    public class Order
    {
        public Order()
        {
        }

        public Order(int orderN, DateTime orderDT)
        {
            OrderNo = orderN;
            OrderDateTime = orderDT;
        }

        public int OrderNo { get; set; }
        public DateTime OrderDateTime { get; set; }
        public double Amount { get; set; }
        public string Status { get; set; }
        public List<Ticket> TicketList { get; set; } = new();

        public void AddTicket(Ticket ticket)
        {
            TicketList.Add(ticket);
        }
        
        public override string ToString()
        {
            return
                $"{"Order No: "} {OrderNo} {" Order Date Time: "} {OrderDateTime} {" Amount: "} {Amount} {" Status: "} {Status} {" Ticket List: "} {TicketList}";
        }
    }
}