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

        protected Random random = new Random();

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
        public Voter(string name, string gender, string city, Candidate candidate, bool paid, Campaign campaign)
        {
            this.name = name;
            this.gender = gender;
            this.city = city;
            this.candidate = candidate;
            this.paid = paid;
            this.campaign = campaign;
        }

        public Ballot Vote()
        {
            Ballot ballot = new Ballot();
            ballot.SetIsValid(true);
            ballot.SetCity(this.getCity());

            var percentToVote = GetPercentNotToVote();

            int chanceNotToGo = random.Next(1, 101);
            if (chanceNotToGo < percentToVote)
            {
                this.campaign.allVotersInCampaign.Remove(this);
                return null;
            }
            this.GetCampaign().allVotesForCampaignThatGoesToPoll++;
            var percentToFail = GetPercentForInvalidBallot();

            int chanceToFail = random.Next(1, 101);
            if (chanceToFail < percentToFail)
            {
                ballot.SetIsValid(false);
                this.campaign.invalidVotes++;
                return ballot;
            }



            var percentToVoteForOtherCandidate = GetPercentToVoteForOtherCandidate();

            int chanceToVoteForOtherCandidate = random.Next(1, 101);
            if (chanceToVoteForOtherCandidate > percentToVoteForOtherCandidate)
            {
                ballot.SetCandidate(this.candidate);
            }
            else
            {
                CIK cik = CIK.Instance;
                ballot.SetCandidate(cik.GetRandomCandidate(this.candidate));
            }


            return ballot;
        }

        public abstract int GetPercentNotToVote();
        public abstract int GetPercentForInvalidBallot();
        public abstract int GetPercentToVoteForOtherCandidate();
        
    }
}
