using System;
using System.Collections.Generic;
using System.Text;

namespace President
{
    public class RichVoter : Voter
    {
        public RichVoter(string name, string gender, string city, Candidate candidate, bool paid, Campaign campaign) : base(name, gender, city, candidate, paid, campaign) { }

    }
}
