//============================================================
// Student Number : S10222081, S10223135
// Student Name : Fun Gao Wei, Farrell , Tan Yun-E
// Module Group : T07
//============================================================

using System;
using System.Collections.Generic;

namespace PRG2_T07_Team12
{
    public class Movie : IComparable<Movie>
    {
        public Movie()
        {
        }

        public Movie(string t, int d, string cl, DateTime od, List<Screening> sl, int tc)
        {
            Title = t;
            Duration = d;
            Classification = cl;
            OpeningDate = od;
            ScreeningList = sl;
            TicketCount = tc;
        }

        public string Title { get; set; }
        public int Duration { get; set; }
        public string Classification { get; set; }
        public DateTime OpeningDate { get; set; }
        public List<string> GenreList { get; set; } = new List<string>();
        public List<Screening> ScreeningList { get; set; } = new List<Screening>();
        
        public int TicketCount { get; set; }

        public void AddGenre(string genre)
        {
            GenreList.Add(genre);
        }

        public void AddScreening(Screening screening)
        {
            ScreeningList.Add(screening);
        }


        public override string ToString()
        {
            string moviestring = null;
            foreach (string genre in GenreList)
            {
                moviestring = $"{Title,-30}{Duration,-15}{genre,-30}{Classification,-20}{OpeningDate}";
            }

            return moviestring;
        }
        
        public int CompareTo(Movie movie)
        {
            if (TicketCount > movie.TicketCount) return -1;
            if (TicketCount == movie.TicketCount) return 0;
            return 1;
        }
    }
}