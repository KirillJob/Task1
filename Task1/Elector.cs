using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    class Elector
    {
        private int[] bulletin;
        private int counter = 1;


        public int WinNum { get; set; }
        
        public Elector(int[] bulletin)
        {
            this.bulletin = bulletin;
            WinNum = bulletin[0];
        }

        public void GetNextNumOfWin()
        {
            if (counter < bulletin.Length)
            {
                WinNum = bulletin[counter];
                counter++;
            }
        }
    }

}
