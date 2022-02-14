using System;
using System.Collections.Generic;
using System.Text;

namespace President
{
    public abstract class Voter
    {
        protected string name;
        protected string gender;
        protected string city;
        protected Candidate candidate;
        protected bool paid;
        protected Campaign campaign;
        protected bool invalidVote;
        protected bool goingToVote;

        protected Random random = new Random();

        public void SetGoinToVote(bool goingToVote)
        {
            this.goingToVote = goingToVote;
        }

        public bool GetGoingToVote()
        {
            return goingToVote;
        }
        public void SetToInvalidVote(bool invalidVote)
        {
            this.invalidVote = invalidVote;
        }

        public bool GetInvalidVote()
        {
            return invalidVote;
        }
        public void SetCampaign(Campaign campaign)
        {
            this.campaign = campaign;
        }

        public Campaign GetCampaign()
        {
            return campaign;
        }
        public void setPaid(bool paid)
        {
            this.paid = paid;
        }

        public string getCity()
        {
            return city;
        }

        public string getCandidateName()
        {
            return candidate.getNameOfCandidate();
        }

        public void setCandidate(Candidate candidate)
        {
            this.candidate = candidate;
        }

        public Candidate getCandidate()
        {
            return candidate;
        }
        public Voter(string name, string gender, string city, Candidate candidate, bool paid)
        {
            this.name = name;
            this.gender = gender;
            this.city = city;
            this.candidate = candidate;
            this.paid = paid;
        }

        public Ballot Vote()
        {
            Ballot ballot = new Ballot();

            var percentToVote = GetPercentNotToVote();

            int chanceNotToGo = random.Next(1, 101);
            if (chanceNotToGo < percentToVote)
            {
                return null;
            }

            var percentToFail = GetPercentForInvalidBallot();

            int chanceToFail = random.Next(1, 101);
            if (chanceToFail < percentToFail)
            {
                ballot.isValid = false;
                return ballot;
            }

            ballot.isValid = true;
            ballot.city = this.getCity();

            var percentToVoteForOtherCandidate = GetPercentToVoteForOtherCandidate();

            int chanceToVoteForOtherCandidate = random.Next(1, 101);
            if (chanceToVoteForOtherCandidate > percentToVoteForOtherCandidate)
            {
                ballot.candidate = this.candidate;
            }
            else
            {
                CIK cik = CIK.Instance;
                ballot.candidate = cik.GetRandomCandidate(this.candidate);
            }


            return ballot;
        }

        public abstract int GetPercentNotToVote();
        public abstract int GetPercentForInvalidBallot();
        public abstract int GetPercentToVoteForOtherCandidate();
        protected void ChangeCandidate(int percentage, List<Candidate> allCandidates)
        {
            int percentageForOtherCandidate = percentage;

            int chanceNotToChangeCandidate = random.Next(1, 101);
            if (chanceNotToChangeCandidate < percentageForOtherCandidate)
            {
                List<Candidate> tempList = new List<Candidate>(allCandidates);
                int newRandomCandidate = random.Next(0, allCandidates.Count - 1);
                tempList.Remove(this.getCandidate());

                this.setCandidate(tempList[newRandomCandidate]);
                tempList.Clear();
            }
        }
    }
}
