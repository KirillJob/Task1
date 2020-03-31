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


        public int NumOfWin { get; set; }
        
        public Elector(string bulletin)
        {
            this.bulletin = ProcessingContent.ConvertStrToIntArr(bulletin);
            NumOfWin = bulletin[0];
        }

        public void GetNextNumOfWin()
        {
            if (counter < bulletin.Length)
            {
                NumOfWin = bulletin[counter];
                counter++;
            }
        }
    }

}
