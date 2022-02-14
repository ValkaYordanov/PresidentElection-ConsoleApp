using System;
using System.Collections.Generic;
using System.Text;

namespace President
{
    public class MiddleClassVoter : Voter
    {
        public MiddleClassVoter(string name, string gender, string city, Candidate candidate, bool paid)
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
            return 30;
        }
        //public override void Vote(List<Candidate> allCandidates)
        //{
        //    int percentageNotGoing = 30;
        //    int percentageToChangeCandidate = 30;
        //    List<Voter> listOfVolters = new List<Voter>();

        //    listOfVolters = this.GetCampaign().middleClassVoters;

        //    int chanceNotToGo = random.Next(1, 101);
        //    if (chanceNotToGo < percentageNotGoing)
        //    {
        //        this.SetGoinToVote(false);
        //        listOfVolters.Remove(this);
        //    }

        //    ChangeCandidate(percentageToChangeCandidate, allCandidates);

        //    if (this.GetGoingToVote())
        //    {
        //        this.GetCampaign().allVotesForCampaignThatGoesToPoll++;

        //        int chanceToFail = random.Next(1, 101);
        //        if (chanceToFail < 10)
        //        {
        //            this.invalidVote = true;
        //            this.campaign.invalidVotes++;
        //        }

        //    }

        //}
    }
}
