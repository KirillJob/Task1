using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    class Candidate
    {
        int limLengthName = 80;

        public string Name { get; }
        public int Votes { get; set; }
        public bool IsWinner { get; set; }
        public bool IsIntermedWin { get; set; }
        public int NumInBulletin { get; }

        public Candidate (string name, int numInBulletin)
        {
            CheckLength(name);
            Name = name;
            Votes = 0;
            IsWinner = false;
            IsIntermedWin = false;
            NumInBulletin = numInBulletin;
        }

        private void CheckLength(string s)
        {
            if (s.Length > limLengthName)
            {
                throw new Exception("Недопустимая длина имени");
            }
        }
    }
}
