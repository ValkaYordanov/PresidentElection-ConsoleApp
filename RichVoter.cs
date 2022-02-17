using System;
using System.Collections.Generic;
using System.Text;

namespace President
{
    public class RichVoter : Voter
    {
        public RichVoter(string name, string gender, string city, Candidate candidate, bool paid, Campaign campaign)
            : base(name, gender, city, candidate, paid, campaign) { }

        private const int percentNotToVote = 50;
        private const int percentForInvalidBallot = 0;
        private const int percentToVoteForOtherCandidate = 50;
        public override int GetPercentNotToVote()
        {
            return percentNotToVote;
        }
        public override int GetPercentForInvalidBallot()
        {
            return percentForInvalidBallot;
        }

        public override int GetPercentToVoteForOtherCandidate()
        {
            return percentToVoteForOtherCandidate;
        }
       
    }
}
