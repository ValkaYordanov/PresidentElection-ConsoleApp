using System;
using System.Collections.Generic;
using System.Text;

namespace President
{
    public interface IVotable
    {
        Ballot Vote();

        int GetPercentNotToVote();
        int GetPercentForInvalidBallot();
        int GetPercentToVoteForOtherCandidate();
    }
}
