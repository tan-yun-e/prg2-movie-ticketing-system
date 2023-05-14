//============================================================
// Student Name : Fun Gao Wei, Farrell , Tan Yun-E
// Module Group : T07
//============================================================
using System;

namespace PRG2_T07_Team12
{
    public class Screening: IComparable<Screening>
    {
        public Screening()
        {
        }

        public Screening(int sn, DateTime sdt, string st, Cinema c, Movie m)
        {
            ScreeningNo = sn;
            ScreeningDateTime = sdt;
            ScreeningType = st;
            Cinema = c;
            Movie = m;
        }

        public int ScreeningNo { get; set; }
        public DateTime ScreeningDateTime { get; set; }
        public string ScreeningType { get; set; }
        
        public int SeatsRemaining { get; set; }
        public Cinema Cinema { get; set; }
        public Movie Movie { get; set; }

        public override string ToString()
        {
            return $"{ScreeningNo,-20}{ScreeningDateTime,-25}{ScreeningType,-15}{Cinema.Name,-15}{Movie.Title,-30}";
        }

        public int CompareTo(Screening screening)
        {
            if (SeatsRemaining > screening.SeatsRemaining) return -1;
            if (SeatsRemaining == screening.SeatsRemaining) return 0;
            return 1;
        }
    }
}
