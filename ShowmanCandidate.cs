using Microsoft.OData.Edm;
using System;
using System.Collections.Generic;
using System.Text;

namespace President
{
    public class ShowmanCandidate : Candidate
    {
        public ShowmanCandidate(string name, string education, decimal money, string city) : base(name, education, money, city)
        { }

        public override Campaign makeCampaign(Date startDate, Date endDate)
        {

            LegalCampaign campaign = new LegalCampaign(startDate, endDate, this.money, this);
            return campaign;

        }
    }
}
