using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace President
{
    public class LegalCampaign : Campaign
    {
        Random random = new Random();
        List<string> listOfNamesAndGenders = new List<string> { "Valentin male", "Georgi male", "Petyr male", "Galena female", "Ivan male", "Strahilka female", "Petra female", "Dancho male", "Gery female", "Vasil male", "Ivanka female" };
        List<string> listOfCities = new List<string> { "Varna", "Sofia", "Veliko Tyrnovo", "Byrgas", "Smolqn", "Kazanlak", "Pernik" };


        public LegalCampaign(DateTime startDate, DateTime endDate, decimal compaignMoney, Candidate candidate) : base(startDate, endDate, compaignMoney, candidate)
        { }

        public override List<Voter> makeVoters()
        {
            int days = (int)(this.startDate - this.endDate).TotalDays;
            int totalVoters = days * 100;
            List<Voter> allVoters = new List<Voter>();

            for (int i = 0; i < totalVoters; i++)
            {
                int randomVoterType = random.Next(1, 4);

                int indexOfRandomName = random.Next(listOfNamesAndGenders.Count);
                string name = listOfNamesAndGenders[indexOfRandomName].Split(' ').FirstOrDefault();
                string gender = listOfNamesAndGenders[indexOfRandomName].Split(' ').Skip(1).FirstOrDefault();

                int indexOfRandomCity = random.Next(listOfCities.Count);
                string city = listOfCities[indexOfRandomCity];
                VotesPerCity(city);

                if (randomVoterType == 1)
                {
                    UnlearnedVoter unlearnedVoter = new UnlearnedVoter(name, gender, city, this.candidate, false, this);
                    unlearnedVoters.Add(unlearnedVoter);
                }
                else if (randomVoterType == 2)
                {
                    MiddleClassVoter middleClassVoter = new MiddleClassVoter(name, gender, city, this.candidate, false, this);
                    middleClassVoters.Add(middleClassVoter);
                }
                else if (randomVoterType == 3)
                {
                    RichVoter richVoter = new RichVoter(name, gender, city, this.candidate, false, this);
                    richVoters.Add(richVoter);
                }

            }



            allVoters.AddRange(unlearnedVoters);
            allVoters.AddRange(middleClassVoters);
            allVoters.AddRange(richVoters);
            allVotesForCampaign = allVoters.Count();


            return allVoters;
        }
       

    }
}
