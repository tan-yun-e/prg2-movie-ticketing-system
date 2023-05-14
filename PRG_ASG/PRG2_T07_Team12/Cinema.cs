//============================================================
// Student Name : Fun Gao Wei, Farrell , Tan Yun-E
// Module Group : T07
//============================================================
namespace PRG2_T07_Team12
{
    public class Cinema
    {
        public Cinema()
        {
        }

        public Cinema(string n, int hn, int cap)
        {
            Name = n;
            HallNo = hn;
            Capacity = cap;
        }

        public string Name { get; set; }
        public int HallNo { get; set; }
        public int Capacity { get; set; }

        public override string ToString()
        {
            return $"{Name, -15}{HallNo, -15}{Capacity}";
        }
    }
}
