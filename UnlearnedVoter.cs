using System;
using System.Collections.Generic;
using System.Text;

namespace President
{
    public class UnlearnedVoter : Voter
    {
        public UnlearnedVoter(string name, string gender, string city, Candidate candidate, bool paid) 
            : base(name, gender, city, candidate, paid) { }
       
        public override int GetPercentNotToVote()
        {
            return 10;
        }
        public override int GetPercentForInvalidBallot()
        {
            return 40;
        }

        public override int GetPercentToVoteForOtherCandidate()
        {
            return 0;
        }

        //public override void Vote(List<Candidate> allCandidates)
        //{
        //    int percentageNotGoing = 10;
        //    List<Voter> listOfVolters = new List<Voter>();

        //    listOfVolters = this.GetCampaign().unlearnedVoters;

        //    int chanceNotToGo = random.Next(1, 101);
        //    if (chanceNotToGo < percentageNotGoing)
        //    {
        //        this.SetGoinToVote(false);
        //        listOfVolters.Remove(this);
        //    }

        //    if (this.GetGoingToVote())
        //    {
        //        this.GetCampaign().allVotesForCampaignThatGoesToPoll++;


        //        int chanceToFail = random.Next(1, 101);
        //        if (chanceToFail < 40)
        //        {
        //            this.invalidVote = true;
        //            this.campaign.invalidVotes++;
        //        }

        //    }

    //}
    }
}
