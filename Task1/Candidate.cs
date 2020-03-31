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

        public Candidate (string name)
        {
            CheckLength(name);
            Name = name;
            IsWinner = false;
            Votes = 0;
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
