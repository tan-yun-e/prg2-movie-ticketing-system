//============================================================
// Student Name : Fun Gao Wei, Farrell , Tan Yun-E
// Module Group : T07
//============================================================
namespace PRG2_T07_Team12
{
    public class Ticket
    {
        public Ticket()
        {
        }

        public Ticket(Screening s)
        {
            Screening = s;
        }

        public Screening Screening { get; set; }

        //To be changed after document given
        public virtual double CalculatePrice()
        {
            return 0.00;
        }

        public override string ToString()
        {
            return $"{"Screening: "} {Screening}";
        }
    }
}
