using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace President
{
    public class LegalCampaign : Campaign
    {
        private const int percentageForCalculatingNumberOfVotersForOneDay = 100;

        public LegalCampaign(DateTime startDate, DateTime endDate, decimal compaignMoney, Candidate candidate) : base(startDate, endDate, compaignMoney, candidate)
        { }

        public override List<Voter> makeVoters()
        {
            int days = (int)(this.startDate - this.endDate).TotalDays;
            int totalVoters = days * percentageForCalculatingNumberOfVotersForOneDay;
            List<Voter> allVoters = new List<Voter>();

            for (int i = 0; i < totalVoters; i++)
            {
                allVotersInCampaign.Add(this.CreateOneVoter());
            }
            allVoters.AddRange(allVotersInCampaign);
            allVotesForCampaign = allVoters.Count();


            return allVoters;
        }


    }
}
