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

            MobsterCandidate mobsterA = new MobsterCandidate("Yordano Petrov", "OY", 20000);
            allCandidates.Add(mobsterA);
            MobsterCandidate mobsterB = new MobsterCandidate("Petyr Georgiev", "OY", 25000);
            allCandidates.Add(mobsterB);
            MobsterCandidate mobsterC = new MobsterCandidate("Georgi Ivanov", "PG", 30000);
            allCandidates.Add(mobsterC);
            ShowmanCandidate showmanA = new ShowmanCandidate("Darina Ivailova", "OY", 30000);
            allCandidates.Add(showmanA);
            ShowmanCandidate showmanB = new ShowmanCandidate("Teodor Kaloqnov", "VI", 15000);
            allCandidates.Add(showmanB);
            ShowmanCandidate showmanC = new ShowmanCandidate("Darin Kazakov", "OY", 25000);
            allCandidates.Add(showmanC);
            PoliticCandidate politicA = new PoliticCandidate("Ivan Ivanov", "VI", 10000);
            allCandidates.Add(politicA);
            PoliticCandidate politicB = new PoliticCandidate("Joro Petrov", "VI", 10000);
            allCandidates.Add(politicB);
            PoliticCandidate politicC = new PoliticCandidate("Dimityr Yovkov", "PG", 35000);
            allCandidates.Add(politicC);
            PoliticCandidate politicD = new PoliticCandidate("Dragomir Ivanov", "VI", 40000);
            allCandidates.Add(politicD);


            for (int i = 0; i < allCandidates.Count; i++)
            {
               
                Campaign campaign = allCandidates[i].makeCampaign(new DateTime(2015, 03, 25), new DateTime(2015, 03, 01));
                allCampaigns.Add(campaign);
                List<Voter> tempVoters = new List<Voter>(campaign.makeVoters());
                allVotersList=allVotersList.Concat(tempVoters).ToList();

            }



            //MobsterCandidate politic = new MobsterCandidate("Valio", "EDU", 10000);
            //Campaign camp = politic.makeCampaign(new DateTime(2015, 03, 20), new DateTime(2015, 03, 01));
            //allCampaigns.Add(camp);
            //List<Voter> allVotersFromCampOne = new List<Voter>(camp.makeVoters());

            //ShowmanCandidate mobster = new ShowmanCandidate("Teo", "SUI", 10000);
            //Campaign camp2 = mobster.makeCampaign(new DateTime(2015, 03, 20), new DateTime(2015, 03, 01));
            //allCampaigns.Add(camp2);

            //List<Voter> allVotersfromCampTwo = new List<Voter>(camp2.makeVoters());

            //ShowmanCandidate showman = new ShowmanCandidate("LILI", "SDY", 50000);
            //Campaign camp3 = showman.makeCampaign(new DateTime(2015, 03, 20), new DateTime(2015, 03, 10));
            //List<Voter> allVotersfromCampThree = new List<Voter>(camp3.makeVoters());

            CIK cik = new CIK();

            //List<Voter> allVotersList = allVotersFromCampOne.Concat(allVotersfromCampTwo).ToList();

            var queryForAllVoters =
            from voter in allVotersList
            group voter by voter.getCity() into newGroup
            orderby newGroup.Key
            select newGroup;

            Dictionary<string, Dictionary<string, int>> resultsFromVoting = cik.startVoting(queryForAllVoters);

            foreach (var candidate in resultsFromVoting)
            {
                Console.WriteLine(candidate.Key + ":");
                foreach (var city in resultsFromVoting[candidate.Key])
                {
                    Console.WriteLine("      " + city.Key + " - " + city.Value);
                }
            }

            WinnerORunnerUp winner = cik.findWinner(resultsFromVoting);
            Console.WriteLine("Winner is: " + winner.getName() + " with: " + winner.getVote() + " votes");


            WinnerORunnerUp runnerUp = cik.runnerUp(resultsFromVoting);
            Console.WriteLine("Runner up is: " + runnerUp.getName() + " with: " + runnerUp.getVote() + " votes");

            Console.WriteLine("All voters/ballots: " + allVotersList.Count());

            Console.WriteLine("Election activity: " + cik.electionActivity(allCampaigns) + "%");

            Dictionary<string, double> dictionaryOfAllCity = cik.cityActivity(queryForAllVoters, allVotersList, allCampaigns);

            foreach (var city in dictionaryOfAllCity)
            {
                Console.WriteLine("Activity in " + city.Key + " is: " + city.Value);
            }

            Console.WriteLine("Paid votes: " + cik.paidVotes(allCampaigns) + "%");

            Console.WriteLine("Invalid ballots: " + cik.findInvalidBallots(allCampaigns) + "%");

            var favCandidate = cik.favCandidate(allVotersList);
            foreach (var type in favCandidate)
            {
                Console.WriteLine(type.Key + "->" + type.Value);
            }

            Console.WriteLine("City with max vote: " +cik.cityWithMaxVotes(resultsFromVoting));

            Console.WriteLine("City with minimum invalid vote: " + cik.findCityWithMinVotes());

            Console.WriteLine("City with maximum paid vote: " + cik.cityWithMaxPaidVotes(allCampaigns));
            //Console.WriteLine("Invalid ballots: "+cik.invalidBallots);
            //Console.WriteLine("Valid ballots: "+ (allVotersList.Count()- cik.invalidBallots));






        }
    }
}
