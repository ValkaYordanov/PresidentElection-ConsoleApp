using Microsoft.OData.Edm;

namespace President
{
    public abstract class Candidate
    {
        protected string name;
        protected string education;
        protected decimal money;

        public string getNameOfCandidate()
        {
            return name;
        }
        public Candidate(string name, string education, decimal money)
        {
            this.name = name;
            this.education = education;
            this.money = money;
        }

        public abstract Campaign makeCampaign(Date startDate, Date endDate);
    }
}