using System;
using System.Collections.Generic;
using System.Text;

namespace President
{
    public class UnlearnedVoter : Voter
    {
        public UnlearnedVoter(string name, string gender, string city, Candidate candidate, bool paid, Campaign campaign, bool invalidVote) 
            : base(name, gender, city, candidate, paid, campaign, invalidVote) { }
       
        public override void Vote(List<Candidate> allCandidates)
        {
            bool giveVote = true;
            int percentageNotGoing = 10;
            List<Voter> listOfVolters = new List<Voter>();

            listOfVolters = this.GetCampaign().unlearnedVoters;

            int chanceNotToGo = random.Next(1, 101);
            if (chanceNotToGo < percentageNotGoing)
            {
                giveVote = false;
                listOfVolters.Remove(this);
            }

            if (giveVote)
            {
                this.GetCampaign().allVotesForCampaignThatGoesToPoll++;


                int chanceToFail = random.Next(1, 101);
                if (chanceToFail < 40)
                {
                    this.invalidVote = true;
                    this.campaign.invalidVotes++;
                }

            }

        }
    }
}
