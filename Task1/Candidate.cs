using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    class Candidate
    {
        //string _name = string.Empty;
        //bool _isWinner = false;

        public string Name { get; set; }
        public bool IsWinner { get; set; }

        public Candidate ()
        {
            IsWinner = false;
        }
    }
}
