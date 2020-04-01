using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    class Elector
    {
        private int counter = 1;

        public int WinNum { get; set; }
        public int[] Bulletin { get; set; }
        
        public Elector(int[] bulletin)
        {
            Bulletin = bulletin;
            WinNum = Bulletin[0];
        }

        public void GetNextNumOfWin()
        {
            if (Bulletin != null)
            {
                if (counter < Bulletin.Length)
                {
                    WinNum = Bulletin[counter];
                    counter++;
                }
            }
            else
            {
                WinNum = 0;
            }
        }

        public void DestroyBulletin()
        {
            Bulletin = null;
        }
    }

}
