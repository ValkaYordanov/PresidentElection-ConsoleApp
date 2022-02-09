using Microsoft.OData.Edm;

namespace President
{
    public abstract class Candidate
    {
        protected string name;
        protected string education;
        protected decimal money;
        protected string city;

        public string getNameOfCandidate()
        {
            return name;
        }

        public string GetCity()
        {
            return city;
        }

        public string GetEducation()
        {
            return education;
        }

        public Candidate(string name, string education, decimal money, string city)
        {
            this.name = name;
            this.education = education;
            this.money = money;
            this.city = city;
        }

        public abstract Campaign makeCampaign(Date startDate, Date endDate);
    }
}