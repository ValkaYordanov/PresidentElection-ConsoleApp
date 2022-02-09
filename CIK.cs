using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace President
{
    public class CIK
    {

        private static CIK instance = null;
        public Dictionary<string, int> educationVotesList = new Dictionary<string, int>();
        private CIK() { }

        public static CIK Instance
        {
            get
            {
                if (instance == null)
                    instance = new CIK();
                return instance;

            }
        }



        Random random = new Random();
        Dictionary<string, Dictionary<string, int>> candidatesResults = new Dictionary<string, Dictionary<string, int>>();
        Dictionary<string, int> cityWithInvalidBallots = new Dictionary<string, int>();
        public int invalidBallots = 0;


        public Dictionary<string, Dictionary<string, int>> startVoting(IOrderedEnumerable<IGrouping<string, Voter>> queryForAllVoters, List<Candidate> allCandidates)
        {

            foreach (var cityKey in queryForAllVoters)
            {
                foreach (var voter in cityKey)
                {

                    if (voter.Vote(voter, allCandidates))
                    {

                        if (voter is UnlearnedVoter)
                        {
                            int chanceToFail = random.Next(1, 101);
                            if (chanceToFail < 40)
                            {
                                invalidBallots++;
                                cityWithInvalidVotes(voter.getCity());
                                continue;
                            }

                        }
                        else if (voter is MiddleClassVoter)
                        {
                            int chanceToFail = random.Next(1, 101);
                            if (chanceToFail < 10)
                            {
                                invalidBallots++;
                                cityWithInvalidVotes(voter.getCity());
                                continue;
                            }
                        }

                        VotesBasedOnEducation(voter);

                        if (!candidatesResults.ContainsKey(voter.getCandidateName()))
                        {
                            candidatesResults.Add(voter.getCandidateName(), new Dictionary<string, int>());
                            candidatesResults[voter.getCandidateName()].Add(cityKey.Key, 1);
                        }
                        else
                        {
                            if (candidatesResults[voter.getCandidateName()].ContainsKey(cityKey.Key))
                            {
                                candidatesResults[voter.getCandidateName()][cityKey.Key]++;
                            }
                            else
                            {
                                candidatesResults[voter.getCandidateName()].Add(cityKey.Key, 1);
                            }
                        }
                    }
                    else { continue; }

                }
            }
            return candidatesResults;
        }

        public void VotesBasedOnEducation(Voter voter)
        {
            Candidate candidate = voter.getCandidate();

            if (!educationVotesList.ContainsKey(candidate.GetEducation()))
            {
                educationVotesList.Add(candidate.GetEducation(), 1);
            }
            else
            {
                educationVotesList[candidate.GetEducation()]++;
            }
        }

        private void cityWithInvalidVotes(string city)
        {
            if (!cityWithInvalidBallots.ContainsKey(city))
            {
                cityWithInvalidBallots.Add(city, 1);
            }
            else
            {
                cityWithInvalidBallots[city]++;
            }
        }

        public WinnerORunnerUp findWinner(Dictionary<string, Dictionary<string, int>> resultsFromVoting)
        {
            WinnerORunnerUp winner = new WinnerORunnerUp();
            int vote = 0;
            int maxVote = 0;

            foreach (var candidate in resultsFromVoting)
            {
                vote = 0;
                foreach (var city in resultsFromVoting[candidate.Key])
                {
                    vote += city.Value;
                }
                if (vote > maxVote)
                {
                    maxVote = vote;
                    winner.setName(candidate.Key);
                    winner.setVote(maxVote);
                }
            }
            return winner;

        }
        public WinnerORunnerUp runnerUp(Dictionary<string, Dictionary<string, int>> resultsFromVoting)
        {
            Dictionary<string, Dictionary<string, int>> resultsFromVotingTemp = new Dictionary<string, Dictionary<string, int>>();

            foreach (var candidate in resultsFromVoting)
            {
                foreach (var city in resultsFromVoting[candidate.Key])
                {
                    if (!resultsFromVotingTemp.ContainsKey(candidate.Key))
                    {
                        resultsFromVotingTemp.Add(candidate.Key, new Dictionary<string, int>());
                        resultsFromVotingTemp[candidate.Key].Add(city.Key, city.Value);
                    }
                    else
                    {
                        //resultsFromVotingTemp[candidate.Key].Add(city.Key, city.Value);
                        if (!resultsFromVotingTemp[candidate.Key].ContainsKey(city.Key))
                        {
                            resultsFromVotingTemp[candidate.Key].Add(city.Key, city.Value);
                        }
                    }
                }

            }


            WinnerORunnerUp winnerMax = findWinner(resultsFromVotingTemp);
            resultsFromVotingTemp.Remove(winnerMax.getName());
            WinnerORunnerUp runnerUp = new WinnerORunnerUp();
            int vote = 0;
            int maxVote = 0;

            foreach (var candidate in resultsFromVotingTemp)
            {
                vote = 0;
                foreach (var city in resultsFromVotingTemp[candidate.Key])
                {
                    vote += city.Value;
                }
                if (vote > maxVote)
                {
                    maxVote = vote;
                    runnerUp.setName(candidate.Key);
                    runnerUp.setVote(maxVote);
                }
            }
            return runnerUp;

        }

        public double electionActivity(List<Campaign> allCampaigns)
        {
            double activity;
            int allVotes = 0;
            int votesThatGoes = 0;

            for (int i = 0; i < allCampaigns.Count; i++)
            {
                allVotes += allCampaigns[i].allVotesForCampaign;
                votesThatGoes += allCampaigns[i].allVotesForCampaignThatGoesToPoll;
            }

            activity = Math.Round((double)votesThatGoes * 100 / allVotes, 2);
            return activity;
        }


        public double findInvalidBallots(List<Campaign> allCampaigns)
        {
            double percentage;
            int allVotes = 0;

            for (int i = 0; i < allCampaigns.Count; i++)
            {
                allVotes += allCampaigns[i].allVotesForCampaign;

            }
            percentage = Math.Round((double)invalidBallots * 100 / allVotes, 2);
            return percentage;
        }

        public Dictionary<string, double> cityActivity(IOrderedEnumerable<IGrouping<string, Voter>> queryForAllVoters, List<Voter> allVotersList, List<Campaign> allCampaigns)
        {
            Dictionary<string, double> citiesActivity = new Dictionary<string, double>();
            Dictionary<string, int> citiesActivityVotes = new Dictionary<string, int>();

            foreach (var cityKey in queryForAllVoters)
            {
                if (!citiesActivity.ContainsKey(cityKey.Key))
                {
                    citiesActivity.Add(cityKey.Key, 0.0);
                    citiesActivityVotes.Add(cityKey.Key, 0);
                }
            }

            for (int i = 0; i < allVotersList.Count; i++)
            {
                citiesActivityVotes[allVotersList[i].getCity()]++;
            }

            int allVotes = returnAllGoingVotes(allCampaigns);

            Dictionary<string, int> citiesAndTheirAllVotes = new Dictionary<string, int>(ReturnAllCitiesWithAllVotes(allCampaigns));

            foreach (var city in citiesActivityVotes)
            {
                double percentage = 0.0;
                percentage = Math.Round((double)citiesActivityVotes[city.Key] * 100 / citiesAndTheirAllVotes[city.Key], 2);
                citiesActivity[city.Key] = percentage;
            }



            return citiesActivity;
        }

        private Dictionary<string, int> ReturnAllCitiesWithAllVotes(List<Campaign> allCampaigns)
        {
            Dictionary<string, int> citiesAndAllVotes = new Dictionary<string, int>();

            foreach (var campaign in allCampaigns)
            {
                foreach (var city in campaign.allVotesPerCity)
                {
                    if (!citiesAndAllVotes.ContainsKey(city.Key))
                    {
                        citiesAndAllVotes.Add(city.Key, city.Value);
                    }
                    else
                    {
                        citiesAndAllVotes[city.Key] += city.Value;
                    }

                }

            }

            return citiesAndAllVotes;
        }

        public double paidVotes(List<Campaign> allCampaigns)
        {
            double percentage;
            int allVotes = 0;
            int paid = 0;

            for (int i = 0; i < allCampaigns.Count; i++)
            {
                allVotes += allCampaigns[i].allVotesForCampaign;

                if (allCampaigns[i] is IllegalCampaign)
                {
                    paid += allCampaigns[i].allVotesForCampaign;
                }

            }
            percentage = Math.Round((double)paid * 100 / allVotes, 2);


            return percentage;
        }

        private int returnAllGoingVotes(List<Campaign> allCampaigns)
        {
            int allVotes = 0;

            for (int i = 0; i < allCampaigns.Count; i++)
            {
                allVotes += allCampaigns[i].allVotesForCampaignThatGoesToPoll;

            }

            return allVotes;
        }

        public Dictionary<string, string> favCandidate(List<Voter> allVotersList)
        {
            Dictionary<string, Dictionary<string, int>> favBasedOnVoterType = new Dictionary<string, Dictionary<string, int>>();
            Dictionary<string, string> favcandidate = new Dictionary<string, string>();

            foreach (var voter in allVotersList)
            {
                if (!favBasedOnVoterType.ContainsKey(voter.GetType().ToString()))
                {
                    favBasedOnVoterType.Add(voter.GetType().ToString(), new Dictionary<string, int>());
                }
                else
                {
                    if (!favBasedOnVoterType[voter.GetType().ToString()].ContainsKey(voter.getCandidateName()))
                    {
                        favBasedOnVoterType[voter.GetType().ToString()].Add(voter.getCandidateName(), 0);
                    }
                    else
                    {
                        favBasedOnVoterType[voter.GetType().ToString()][voter.getCandidateName()]++;
                    }
                }
            }

            foreach (var type in favBasedOnVoterType)
            {
                string name = "";
                int vote = 0;
                foreach (var candidate in type.Value)
                {
                    if (candidate.Value > vote)
                    {
                        vote = candidate.Value;
                        name = candidate.Key;
                    }
                }
                favcandidate.Add(type.Key, name);

            }

            return favcandidate;
        }

        public string cityWithMaxVotes(Dictionary<string, Dictionary<string, int>> resultsFromVoting)
        {
            string cityName = "";
            int votes = 0;
            Dictionary<string, int> cities = new Dictionary<string, int>();

            foreach (var candidate in resultsFromVoting)
            {
                foreach (var city in resultsFromVoting[candidate.Key])
                {
                    if (!cities.ContainsKey(city.Key))
                    {
                        cities.Add(city.Key, city.Value);
                    }
                    else
                    {
                        cities[city.Key] += city.Value;
                    }
                }
            }

            foreach (var city in cities)
            {
                if (city.Value > votes)
                {
                    votes = city.Value;
                    cityName = city.Key;
                }
            }



            return cityName;
        }

        public string findCityWithMinVotes()
        {
            int minVote = Int32.MaxValue;
            string nameOfCity = "";

            foreach (var city in cityWithInvalidBallots)
            {

                if (city.Value < minVote)
                {
                    minVote = city.Value;
                    nameOfCity = city.Key;
                }
            }

            return nameOfCity;
        }

        public string cityWithMaxPaidVotes(List<Campaign> allCampaigns)
        {
            string cityName = "";
            int votes = Int32.MaxValue;

            Dictionary<string, int> cities = new Dictionary<string, int>();

            foreach (var campaign in allCampaigns)
            {
                foreach (var city in campaign.citiesWithMaxPaidVotes)
                {
                    if (!cities.ContainsKey(city.Key))
                    {
                        cities.Add(city.Key, city.Value);
                    }
                    else
                    {
                        cities[city.Key] += city.Value;
                    }
                }

            }

            foreach (var city in cities)
            {
                if (city.Value < votes)
                {
                    votes = city.Value;
                    cityName = city.Key;
                }
            }


            return cityName;
        }

        public Dictionary<string, List<string>> CandidateWithCity(List<Candidate> allCandidate)
        {
            Dictionary<string, List<string>> citiesAndCandidates = new Dictionary<string, List<string>>();

            foreach (var candidate in allCandidate)
            {
                if (!citiesAndCandidates.ContainsKey(candidate.GetCity()))
                {
                    citiesAndCandidates.Add(candidate.GetCity(), new List<string>());
                    citiesAndCandidates[candidate.GetCity()].Add(candidate.getNameOfCandidate());
                }
                else
                {

                    citiesAndCandidates[candidate.GetCity()].Add(candidate.getNameOfCandidate());

                }
            }

            return citiesAndCandidates;
        }

    }
}
