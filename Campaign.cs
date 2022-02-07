using System;
using System.Collections.Generic;
using System.Text;

namespace President
{
    public abstract class Campaign
    {
        protected DateTime startDate;
        protected DateTime endDate;
        protected decimal compaignMoney;
        public Candidate candidate;
        public int allVotesForCampaign;
        public int allVotesForCampaignThatGoesToPoll;
        public int paidVotes;
        public Dictionary<string, int> citiesWithMaxPaidVotes = new Dictionary<string, int>();
        public List<Voter> unlearnedVoters = new List<Voter>();
        public List<Voter> middleClassVoters = new List<Voter>();
        public List<Voter> richVoters = new List<Voter>();

        public Campaign(DateTime startDate, DateTime endDate, decimal compaignMoney, Candidate candidate)
        {
            this.startDate = startDate;
            this.endDate = endDate;
            this.compaignMoney = compaignMoney;
            this.candidate = candidate;
        }

        public abstract List<Voter> makeVoters();
    }
}
