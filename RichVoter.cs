﻿using System;
using System.Collections.Generic;
using System.Text;

namespace President
{
    public class RichVoter : Voter
    {
        public RichVoter(string name, string gender, string city, Candidate candidate, bool paid, Campaign campaign, bool invalidVote, bool goingToVote)
            : base(name, gender, city, candidate, paid, campaign, invalidVote, goingToVote) { }

        public override void Vote(List<Candidate> allCandidates)
        {
            int percentageNotGoing = 50;
            int percentageToChangeCandidate = 50;
            List<Voter> listOfVolters = new List<Voter>();

            listOfVolters = this.GetCampaign().richVoters;

            int chanceNotToGo = random.Next(1, 101);
            if (chanceNotToGo < percentageNotGoing)
            {
                this.SetGoinToVote(false);
                listOfVolters.Remove(this);
            }

            ChangeCandidate(percentageToChangeCandidate, allCandidates);

            if (this.GetGoingToVote())
            {
                this.GetCampaign().allVotesForCampaignThatGoesToPoll++;
            }

        }
    }
}
