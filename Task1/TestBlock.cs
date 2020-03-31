using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    class TestBlock
    {
        int numCandidate;
        int limNumOfCandidate = 20;
        int limNumElectors = 1000;
        List<Candidate> candidates = null;
        List<Elector> electors = null;

        public TestBlock (string[] testBlockAsRow)
        {
            numCandidate = GetNumCandidate(testBlockAsRow[0]);
            candidates = GetCandidates(numCandidate, testBlockAsRow);
            electors = GetElectors(testBlockAsRow);
        }

        private int GetNumCandidate(string s)
        {
            int numCandidates = ProcessingContent.ConvertStrToInt(s);
            CheckLimCandidates(numCandidates);
            return numCandidates;
        }

        private List<Candidate> GetCandidates(int num, string[] testBlockAsRow)
        {
            List<Candidate> candidates = null; 
            for (int i=1; i <= num; i++)
            {
                string name = testBlockAsRow[i];
                Candidate candidate = new Candidate(name);
                candidates.Add(candidate);
            }
            return candidates;
        }
        
        private List<Elector> GetElectors(string[] testBlockAsRow)
        {
            List<Elector> electors = null;
            for (int i = numCandidate + 2; i <= testBlockAsRow.Length; i++)
            {
                int[] bulletin = ProcessingContent.ConvertStrToIntArr(testBlockAsRow[i]);

                Elector elector = new Elector(bulletin);
                electors.Add(elector);
            }
            ChecklimNumElectors(electors.Count);
            return electors;
        }

        private void CheckLimCandidates(int i)
        {
            if (i > limNumOfCandidate)
            {
                throw new Exception("Недопустимое количество кондидатов");
            }
        }

        private void ChecklimNumElectors(int i)
        {
            if (i >= limNumElectors)
            {
                throw new Exception("Недопустимое количество избирателей");
            }
        }

        public void StartElections()
        {
            StartRoundOfEletions();
            CalcOfResult();
        }

        private void StartRoundOfEletions()
        {
            foreach (Elector elector in electors)
            {
                candidates[elector.WinNum - 1].Votes++;
            }
        }

        private void CalcOfResult()
        {
            foreach (Candidate candidate in candidates)
            {
                if(candidate.Votes >= electors.Count/2)
                {

                }
            }
        }
    }
}
