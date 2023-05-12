//============================================================
// Student Number : S10222081, S10223135
// Student Name : Fun Gao Wei, Farrell , Tan Yun-E
// Module Group : T07
//============================================================
using System;

namespace PRG2_T07_Team12
{
    public class Student : Ticket
    {
        public Student()
        {
        }

        public Student(Screening sc, string los) : base(sc)
        {
            Screening = sc;
            LevelOfStudy = los;
        }

        public string LevelOfStudy { get; set; }

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
                else
                {
                    if (Screening.ScreeningDateTime - Screening.Movie.OpeningDate > new TimeSpan(7, 0, 0, 0))
                    {
                        price = 7;
                    }
                    else price = 8.5;
                }
            }

            if (Screening.ScreeningType == "3D")
            {
                if (Screening.ScreeningDateTime.DayOfWeek == DayOfWeek.Friday ||
                    Screening.ScreeningDateTime.DayOfWeek == DayOfWeek.Saturday ||
                    Screening.ScreeningDateTime.DayOfWeek == DayOfWeek.Sunday)
                {
                    price = 14;
                }
                else
                {
                    if (Screening.ScreeningDateTime - Screening.Movie.OpeningDate > new TimeSpan(7, 0, 0, 0))
                    {
                        price = 8;
                    }

                    else price = 11;
                }
            }

            return price;
        }

        public override string ToString()
        {
            return $"{"Screening: "}{Screening}{" Level of study: "}{LevelOfStudy}";
        }
    }
}