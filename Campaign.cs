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
        protected int paidVotes;
        public int invalidVotes = 0;
        public Dictionary<string, int> citiesWithMaxPaidVotes = new Dictionary<string, int>();
        public Dictionary<string, int> allVotesPerCity = new Dictionary<string, int>();
        public List<Voter> allVotersInCampaign = new List<Voter>();
        public enum VoterTypes { UnlearnedVoter = 1, MiddleClassVoter = 2, RichVoter = 3 }
        Random random = new Random();
        List<string> listOfNamesAndGenders = new List<string> { "Valentin male", "Georgi male", "Petyr male", "Galena female", "Ivan male", "Strahilka female", "Petra female", "Dancho male", "Gery female", "Vasil male", "Ivanka female" };
        List<string> listOfCities = new List<string> { "Varna", "Sofia", "Veliko Tyrnovo", "Byrgas", "Smolqn", "Kazanlak", "Pernik" };
        public Campaign(DateTime startDate, DateTime endDate, decimal compaignMoney, Candidate candidate)
        {
            this.startDate = startDate;
            this.endDate = endDate;
            this.compaignMoney = compaignMoney;
            this.candidate = candidate;
        }

        public abstract List<Voter> makeVoters();

       

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

        protected Voter CreateOneVoter()
        {
            int randomVoterType = random.Next(1, 4);
            int indexOfRandomName = random.Next(listOfNamesAndGenders.Count);
            string name = listOfNamesAndGenders[indexOfRandomName].Split(' ').FirstOrDefault();
            string gender = listOfNamesAndGenders[indexOfRandomName].Split(' ').Skip(1).FirstOrDefault();

            int indexOfRandomCity = random.Next(listOfCities.Count);
            string city = listOfCities[indexOfRandomCity];
            VotesPerCity(city);

            if (randomVoterType == (int)VoterTypes.UnlearnedVoter)
            {
                UnlearnedVoter unlearnedVoter = new UnlearnedVoter(name, gender, city, this.candidate, false, this);
                return unlearnedVoter;
            }
            else if (randomVoterType == (int)VoterTypes.MiddleClassVoter)
            {
                MiddleClassVoter middleClassVoter = new MiddleClassVoter(name, gender, city, this.candidate, false, this);
                return middleClassVoter;
            }
            else if (randomVoterType == (int)VoterTypes.RichVoter)
            {
                RichVoter richVoter = new RichVoter(name, gender, city, this.candidate, false, this);
                return richVoter;
            }
            return null;
        }
    }
}
