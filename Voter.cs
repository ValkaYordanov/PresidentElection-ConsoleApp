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
        public Voter(string name, string gender, string city, Candidate candidate, bool paid, Campaign campaign, bool invalidVote, bool goingToVote)
        {
            this.name = name;
            this.gender = gender;
            this.city = city;
            this.candidate = candidate;
            this.paid = paid;
            this.campaign = campaign;
            this.invalidVote = invalidVote;
            this.goingToVote = goingToVote;
        }

        public abstract void Vote(List<Candidate> allCandidates);
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
