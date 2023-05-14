//============================================================
// Student Name : Fun Gao Wei, Farrell , Tan Yun-E
// Module Group : T07
//============================================================
using System;

namespace PRG2_T07_Team12
{
    public class Adult : Ticket
    {
        public Adult()
        {
        }

        public Adult(Screening sc, bool pco) : base(sc)
        {
            Screening = sc;
            PopcornOffer = pco;
        }

        public bool PopcornOffer { get; set; }

        //To be ammended after document release
        public override double CalculatePrice()
        {
            double price = 0;
            if (Screening.ScreeningType == "2D")
            {
                if (Screening.ScreeningDateTime.DayOfWeek == DayOfWeek.Friday ||
                    Screening.ScreeningDateTime.DayOfWeek == DayOfWeek.Saturday ||
                    Screening.ScreeningDateTime.DayOfWeek == DayOfWeek.Sunday)
                {
                    price = 12.5;
                }
                else price = 8.5;
            }

            if (Screening.ScreeningType == "3D")
            {
                if (Screening.ScreeningDateTime.DayOfWeek == DayOfWeek.Friday ||
                    Screening.ScreeningDateTime.DayOfWeek == DayOfWeek.Saturday ||
                    Screening.ScreeningDateTime.DayOfWeek == DayOfWeek.Sunday)
                {
                    price = 14;
                }
                else price = 11;
            }

            if (PopcornOffer)
            {
                price = price + 3;
            }
            return price;
        }

        public override string ToString()
        {
            return $"{"Screening: "}{Screening} {" Popcorn offer: "}{PopcornOffer}";
        }
    }
}
