using Microsoft.OData.Edm;
using System;
using System.Collections.Generic;
using System.Text;

namespace President
{
    public class MobsterCandidate : Candidate
    {
       public MobsterCandidate(string name, string education, decimal money):base(name,education,money)
        {}

        public override Campaign makeCampaign(Date startDate, Date endDate)
        {
            IllegalCampaign campaign = new IllegalCampaign(startDate, endDate, this.money, this);
            return campaign;
        }
    }
}
