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
        Random random = new Random();
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
        public Voter(string name, string gender, string city, Candidate candidate, bool paid, Campaign campaign)
        {
            this.name = name;
            this.gender = gender;
            this.city = city;
            this.candidate = candidate;
            this.paid = paid;
            this.campaign = campaign;
        }


        public bool Vote(Voter voter)
        {
            bool giveVote = true;
            int percentageNotGoing = 0;
            List<Voter> listOfVolters = new List<Voter>();

            if (voter is UnlearnedVoter)
            {
                listOfVolters = voter.GetCampaign().unlearnedVoters;
                percentageNotGoing = 10;
            }
            else if (voter is MiddleClassVoter)
            {
                percentageNotGoing = 30;
                listOfVolters = voter.GetCampaign().middleClassVoters;
            }
            else if (voter is RichVoter)
            {
                listOfVolters = voter.GetCampaign().richVoters;
                percentageNotGoing = 50;
            }


            int chanceNotToGo = random.Next(1, 101);
            if (chanceNotToGo < percentageNotGoing)
            {
                giveVote = false;
                listOfVolters.Remove(voter);
            }

            if (giveVote)
            {
                voter.GetCampaign().allVotesForCampaignThatGoesToPoll++;
            }


            return giveVote;
        }

    }
}
