using System;
using System.Collections.Generic;
using System.Linq;
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
        public int invalidVotes = 0;
        public Dictionary<string, int> citiesWithMaxPaidVotes = new Dictionary<string, int>();
        public Dictionary<string, int> allVotesPerCity = new Dictionary<string, int>();
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

        public List<Voter> GetCampaignVoters ()
        {
            List<Voter> allVoters = new List<Voter>(unlearnedVoters.Concat(middleClassVoters).Concat(richVoters));
           
            return allVoters;
        }

        public void VotesPerCity(string city)
        {
            if (!allVotesPerCity.ContainsKey(city))
            {
                allVotesPerCity.Add(city, 1);
            }
            else
            {
                allVotesPerCity[city]++;
            }
        }
    }
}
