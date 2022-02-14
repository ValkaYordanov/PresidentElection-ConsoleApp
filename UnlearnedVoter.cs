using System;
using System.Collections.Generic;
using System.Text;

namespace President
{
    public class UnlearnedVoter : Voter
    {
        public UnlearnedVoter(string name, string gender, string city, Candidate candidate, bool paid, Campaign campaign) 
            : base(name, gender, city, candidate, paid, campaign) { }
       
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

       
    }
}
