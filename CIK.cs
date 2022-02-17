﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace President
{
    public class CIK
    {

        private static CIK instance = null;
        Random random = new Random();
        Dictionary<string, Dictionary<string, int>> candidatesResults = new Dictionary<string, Dictionary<string, int>>();
        public List<Candidate> candidates = new List<Candidate>();
        public List<Ballot> ballots = new List<Ballot>();
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

        public Candidate GetRandomCandidate(Candidate candidateToExclude)
        {
            candidates.Remove(candidateToExclude);
            int randomCandidate = random.Next(0, candidates.Count());
            Candidate candidate = candidates[randomCandidate];
            candidates.Add(candidateToExclude);

            return candidate;
        }

        public Dictionary<string, Dictionary<string, int>> StartVoting(IOrderedEnumerable<IGrouping<string, Voter>> listOfAllVotersSortedByCity, List<Candidate> allCandidates)
        {
            foreach (var cityKey in listOfAllVotersSortedByCity)
            {
                foreach (var voter in cityKey)
                {
                    Ballot ballot = voter.Vote();

                    if (ballot != null)
                    {
                        ballots.Add(ballot);
                    }

                    if (ballot != null && ballot.GetIsValid())
                    {
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
                }
            }
            return candidatesResults;
        }


        public Dictionary<string, int> CalculateVotesBasedOnEducation()
        {
            Dictionary<string, int> educationsAndVotes = new Dictionary<string, int>();

            foreach (var ballot in ballots)
            {
                if (ballot.GetIsValid())
                {
                    if (!educationsAndVotes.ContainsKey(ballot.GetCandidate().GetEducation()))
                    {
                        educationsAndVotes.Add(ballot.GetCandidate().GetEducation(), 1);
                    }
                    else
                    {
                        educationsAndVotes[ballot.GetCandidate().GetEducation()]++;
                    }
                }
            }

            return educationsAndVotes;
        }

        private Dictionary<Candidate,int> CalculateVotesForEachCandidate()
        {
            Dictionary<Candidate, int> candidatesAndTheirVotes = new Dictionary<Candidate, int>();
            foreach (var ballot in ballots)
            {
                if (ballot.GetIsValid())
                {
                    if (!candidatesAndTheirVotes.ContainsKey(ballot.GetCandidate()))
                    {
                        candidatesAndTheirVotes.Add(ballot.GetCandidate(), 1);
                    }
                    else
                    {
                        candidatesAndTheirVotes[ballot.GetCandidate()]++;
                    }
                }
            }

            return candidatesAndTheirVotes;
        }

        private WinnerORunnerUp FindWinnerOrRunnerUp(Dictionary<Candidate, int> candidatesAndTheirVotes)
        {
            int maxVote = 0;
            WinnerORunnerUp winnerOrRunnerUpCandidate = new WinnerORunnerUp();
            foreach (var candidate in candidatesAndTheirVotes)
            {

                if (candidate.Value > maxVote)
                {
                    maxVote = candidate.Value;
                    winnerOrRunnerUpCandidate.setName(candidate.Key.getNameOfCandidate());
                    winnerOrRunnerUpCandidate.setVote(maxVote);
                }
            }

            return winnerOrRunnerUpCandidate;
        }

        public WinnerORunnerUp FindWinner()
        {
            WinnerORunnerUp winner = new WinnerORunnerUp();
            Dictionary<Candidate, int> candidatesAndTheirVotes = new Dictionary<Candidate, int>(CalculateVotesForEachCandidate());
            winner = FindWinnerOrRunnerUp(candidatesAndTheirVotes);

            return winner;

        }

        public WinnerORunnerUp FindRunnerUp()
        {
            Dictionary<Candidate, int> candidatesAndTheirVotes = new Dictionary<Candidate, int>(CalculateVotesForEachCandidate());

            WinnerORunnerUp winnerMax = FindWinner();
            Candidate winnerCandidate = candidates.Find(x => x.getNameOfCandidate() == winnerMax.getName());
            candidatesAndTheirVotes.Remove(winnerCandidate);

            WinnerORunnerUp runnerUp = new WinnerORunnerUp();
            runnerUp = FindWinnerOrRunnerUp(candidatesAndTheirVotes);

            
            return runnerUp;

        }

        public double CalculateElectionActivity(List<Campaign> allCampaigns)
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

        public double FindPercentageOfInvalidBallots()
        {
            double percentage;
            int invalidBallots = 0;

            foreach (var ballot in ballots)
            {
                if (!ballot.GetIsValid())
                {
                    invalidBallots++;
                }
            }

            percentage = Math.Round((double)invalidBallots * 100 / ballots.Count, 2);
            return percentage;
        }

        public Dictionary<string, double> CalculateCityActivityForEachCity(IOrderedEnumerable<IGrouping<string, Voter>> queryForAllVoters, List<Voter> allVotersList, List<Campaign> allCampaigns)
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

            int allVotes = ReturnNumberOfAllGoingVotes(allCampaigns);

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

        public double CalculatePercentageOfAllPaidVotes(List<Campaign> allCampaigns)
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

        private int ReturnNumberOfAllGoingVotes(List<Campaign> allCampaigns)
        {
            int allVotes = 0;

            for (int i = 0; i < allCampaigns.Count; i++)
            {
                allVotes += allCampaigns[i].allVotesForCampaignThatGoesToPoll;

            }

            return allVotes;
        }

        public Dictionary<string, string> CalculateFavoriteCandidateForEachVoterType(List<Voter> allVotersList)
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

        public string FindCityWithMaxVotes(Dictionary<string, Dictionary<string, int>> resultsFromVoting)
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


        public string FindCityWithMinInvalidVotes()
        {
            Dictionary<string, int> cityWithInvalidBallots = new Dictionary<string, int>();
            int minVote = Int32.MaxValue;
            string nameOfCity = "";

            foreach (var ballot in ballots)
            {
                if (!ballot.GetIsValid())
                {
                    if (!cityWithInvalidBallots.ContainsKey(ballot.GetCity()))
                    {
                        cityWithInvalidBallots.Add(ballot.GetCity(), 1);
                    }
                    else
                    {
                        cityWithInvalidBallots[ballot.GetCity()]++;
                    }
                }
                else
                {
                    continue;
                }

            }


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



        public string FindCityWithMaxPaidVotes(List<Campaign> allCampaigns)
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

        public Dictionary<string, List<string>> ReturnAllCitiesAndCandidatesInEachCity(List<Candidate> allCandidate)
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
