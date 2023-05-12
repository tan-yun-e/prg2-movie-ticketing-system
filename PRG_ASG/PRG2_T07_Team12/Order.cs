//============================================================
// Student Number : S10222081, S10223135
// Student Name : Fun Gao Wei, Farrell , Tan Yun-E
// Module Group : T07
//============================================================
using System;
using System.Collections.Generic;

namespace PRG2_T07_Team12
{
    public class Order 
    {
        public int OrderNo { get; set; }
        public DateTime OrderDateTime { get; set; }
        public double Amount { get; set; }
        public string Status { get; set; }
        public List<Ticket> TicketList { get; set; } = new List<Ticket>();
        
        public Order(){}

        public Order(int on, DateTime odt)
        {
            OrderNo = on;
            OrderDateTime = odt;
        }

        public void AddTicket(Ticket ticket)
        {
            TicketList.Add(ticket);
        }

        public override string ToString()
        {
            return $"{OrderNo}{OrderDateTime}{Amount}{Status}";
        }
    }
}