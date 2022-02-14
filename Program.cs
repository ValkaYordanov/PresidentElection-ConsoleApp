using System;
using System.Collections.Generic;
using System.Linq;

namespace President
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Campaign> allCampaigns = new List<Campaign>();

            List<Candidate> allCandidates = new List<Candidate>();

            List<Voter> allVotersList = new List<Voter>();

            MobsterCandidate mobsterA = new MobsterCandidate("Yordano Petrov", Educations.education.OY.ToString(), 20000, "Varna");
            allCandidates.Add(mobsterA);
            MobsterCandidate mobsterB = new MobsterCandidate("Petyr Georgiev", Educations.education.OY.ToString(), 25000, "Sofia");
            allCandidates.Add(mobsterB);
            MobsterCandidate mobsterC = new MobsterCandidate("Georgi Ivanov", Educations.education.PG.ToString(), 30000, "Varna");
            allCandidates.Add(mobsterC);
            ShowmanCandidate showmanA = new ShowmanCandidate("Darina Ivailova", Educations.education.OY.ToString(), 30000, "Plovdiv");
            allCandidates.Add(showmanA);
            ShowmanCandidate showmanB = new ShowmanCandidate("Teodor Kaloqnov", Educations.education.VI.ToString(), 15000, "Plovdiv");
            allCandidates.Add(showmanB);
            ShowmanCandidate showmanC = new ShowmanCandidate("Darin Kazakov", Educations.education.OY.ToString(), 25000, "Plovdiv");
            allCandidates.Add(showmanC);
            PoliticCandidate politicA = new PoliticCandidate("Ivan Ivanov", Educations.education.VI.ToString(), 10000, "Sofia");
            allCandidates.Add(politicA);
            PoliticCandidate politicB = new PoliticCandidate("Joro Petrov", Educations.education.VI.ToString(), 10000, "Veliko Tyrnovo");
            allCandidates.Add(politicB);
            PoliticCandidate politicC = new PoliticCandidate("Dimityr Yovkov", Educations.education.PG.ToString(), 35000, "Ruse");
            allCandidates.Add(politicC);
            PoliticCandidate politicD = new PoliticCandidate("Dragomir Ivanov", Educations.education.VI.ToString(), 40000, "Stara Zagora");
            allCandidates.Add(politicD);


            for (int i = 0; i < allCandidates.Count; i++) 
            {
                Campaign campaign = allCandidates[i].makeCampaign(new DateTime(2015, 03, 25), new DateTime(2015, 03, 01));
                allCampaigns.Add(campaign);
                List<Voter> tempVoters = new List<Voter>(campaign.makeVoters());
                allVotersList = allVotersList.Concat(tempVoters).ToList();
            }

            CIK cik = CIK.Instance;
            cik.candidates = allCandidates;

            var listOfAllVotersSortedByCity =
            from voter in allVotersList
            group voter by voter.getCity() into newGroup
            orderby newGroup.Key
            select newGroup;

           
            Dictionary<string, Dictionary<string, int>> resultsFromVoting = cik.StartVoting(listOfAllVotersSortedByCity, allCandidates);

            allVotersList.Clear();
            for (int i = 0; i < allCampaigns.Count; i++)
            {
               
                allVotersList.AddRange(allCampaigns[i].GetCampaignVoters());
            }
            listOfAllVotersSortedByCity =
            from voter in allVotersList
            group voter by voter.getCity() into newGroup
            orderby newGroup.Key
            select newGroup;

            foreach (var candidate in resultsFromVoting)
            {
                Console.WriteLine(candidate.Key + ":");
                foreach (var city in resultsFromVoting[candidate.Key])
                {
                    Console.WriteLine("      " + city.Key + " - " + city.Value);
                }
            }

            WinnerORunnerUp winner = cik.FindWinner(resultsFromVoting);
            Console.WriteLine("Winner is: " + winner.getName() + " with: " + winner.getVote() + " votes");

            WinnerORunnerUp runnerUp = cik.FindRunnerUp(resultsFromVoting);
            Console.WriteLine("Runner up is: " + runnerUp.getName() + " with: " + runnerUp.getVote() + " votes");

            Console.WriteLine("All voters/ballots: " + allVotersList.Count());

            Console.WriteLine("Election activity: " + cik.CalculateElectionActivity(allCampaigns) + "%");

            Dictionary<string, double> dictionaryOfAllCity = cik.CalculateCityActivityForEachCity(listOfAllVotersSortedByCity, allVotersList, allCampaigns);

            foreach (var city in dictionaryOfAllCity)
            {
                Console.WriteLine("Activity in " + city.Key + " is: " + city.Value + "%");
            }

            Console.WriteLine("Paid votes: " + cik.CalculatePercentageOfAllPaidVotes(allCampaigns) + "%");

            Console.WriteLine("Invalid ballots: " + cik.FindPercentageOfInvalidBallots(allCampaigns) + "%");

            var citiesAndCandidates = cik.ReturnAllCitiesAndCandidatesInEachCity(allCandidates);
            foreach (var city in citiesAndCandidates)
            {
                Console.WriteLine(city.Key + ":");
                foreach (var candidateName in city.Value)
                {
                    Console.WriteLine("      " + candidateName);
                }
            }

            var favCandidate = cik.CalculateFavoriteCandidateForEachVoterType(allVotersList);
            foreach (var type in favCandidate)
            {
                Console.WriteLine(type.Key + "->" + type.Value);
            }

            Console.WriteLine("City with max vote: " + cik.FindCityWithMaxVotes(resultsFromVoting));

            Console.WriteLine("City with minimum invalid vote: " + cik.FindCityWithMinInvalidVotes(allVotersList));

            Console.WriteLine("City with maximum paid vote: " + cik.FindCityWithMaxPaidVotes(allCampaigns));

            Dictionary<string,int> educationListWithVotes = new Dictionary<string, int>(cik.educationVotesList);
            foreach (var education in educationListWithVotes)
            {
                Console.WriteLine(education.Key + " -> " + education.Value);
            }

        }
    }
}
