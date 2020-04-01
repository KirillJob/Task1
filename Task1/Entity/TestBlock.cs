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
        int limRounds;
        List<Candidate> candidates = new List<Candidate>();
        List<Elector> electors = new List<Elector>();

        public TestBlock (string[] testBlockAsRow)
        {
            numCandidate = GetNumCandidate(testBlockAsRow[0]);
            candidates = GetCandidates(numCandidate, testBlockAsRow);
            electors = GetElectors(testBlockAsRow);
            limRounds = electors[0].Bulletin.Length;
        }

        private int GetNumCandidate(string s)
        {
            int numCandidates = ProcessingContent.ConvertStrToInt(s);
            CheckLimCandidates(numCandidates);
            return numCandidates;
        }

        private List<Candidate> GetCandidates(int num, string[] testBlockAsRow)
        {
            List<Candidate> candidates = new List<Candidate>(); 
            for (int i=1; i <= num; i++)
            {
                string name = testBlockAsRow[i];
                Candidate candidate = new Candidate(name, i);
                candidates.Add(candidate);
            }
            return candidates;
        }
        
        private List<Elector> GetElectors(string[] testBlockAsRow)
        {
            List<Elector> electors = new List<Elector>();
            for (int i = numCandidate + 1; i < testBlockAsRow.Length; i++)
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
            for (int i = 0; i < limRounds; i++)
            {
                StartRoundOfEletions();
                if (HasWinners()) break;
                CleaningElector();
            }

            PrintWinners(IsWonFriendship());
        }

        private void StartRoundOfEletions()
        {
            foreach (Elector elector in electors)
            {
                if (elector.WinNum != 0)
                {
                    candidates[elector.WinNum - 1].Votes++;
                }
            }
        }

        private bool HasWinners()
        {
            SetCandidateInDef();
            int maxVotesForOneCandidate = candidates.Max(Candidate => Candidate.Votes);

            if (maxVotesForOneCandidate >= electors.Count/2)
            {
                foreach (Candidate candidate in candidates)
                {
                    candidate.IsWinner = candidate.Votes == maxVotesForOneCandidate ? true : false;
                }
                return true;
            }
            else
            {
                foreach (Candidate candidate in candidates)
                {
                    candidate.IsIntermedWin = candidate.Votes == maxVotesForOneCandidate ? true : false;
                }
                return false;
            }
        }

        private void CleaningElector()
        {
            foreach (Elector elector in electors)
            {
                foreach (Candidate candidate in candidates)
                {
                    if (candidate.IsIntermedWin)
                    {
                        if (elector.WinNum == candidate.NumInBulletin)
                            elector.DestroyBulletin();
                    }
                }
                elector.GetNextNumOfWin();
            }
        }

        private void SetCandidateInDef()
        {
            foreach (Candidate candidate in candidates)
            {
                candidate.IsIntermedWin = false;
            }
        }

        private bool IsWonFriendship()
        {
            foreach (Candidate candidate in candidates)
            {
                if (candidate.IsIntermedWin != true)
                    return false;
            }
            return true;
        }

        private void PrintWinners(bool wonFriendship)
        {
            if (wonFriendship)
            {
                foreach (Candidate candidate in candidates)
                {
                    Console.WriteLine(candidate.Name);
                }
                Console.WriteLine();
            }
            else
            {
                foreach (Candidate candidate in candidates)
                {
                    if (candidate.IsWinner) Console.WriteLine(candidate.Name);
                }
                Console.WriteLine();
            }
        }
    }
}
