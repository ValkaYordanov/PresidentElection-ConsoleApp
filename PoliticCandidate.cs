using Microsoft.OData.Edm;
using System;
using System.Collections.Generic;
using System.Text;

namespace President
{
    public class PoliticCandidate : Candidate
    {
        public PoliticCandidate(string name, string education, decimal money, string city) : base(name, education, money, city)
        { }

        public override Campaign makeCampaign(Date startDate, Date endDate)
        {
            Random random = new Random();
            bool chanceForCampaign = random.Next(0, 2) > 0;

            if (chanceForCampaign)
            {
                IllegalCampaign campaign = new IllegalCampaign(startDate, endDate, this.money, this);
                return campaign;
            }
            else
            {
                LegalCampaign campaign = new LegalCampaign(startDate, endDate, this.money, this);
                return campaign;
            }
        }
    }
}
