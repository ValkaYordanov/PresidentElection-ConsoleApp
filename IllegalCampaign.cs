using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace President
{
    public class IllegalCampaign : Campaign
    {
        Random random = new Random();
        private const int percentageForCalculatingNumberOfPaidVoters = 50;
        private const int percentageForCalculatingNumberOfVotersForOneDay = 120;
        private const int minimummoneyToPayForPaidVoter = 30;
        private const int maximummoneyToPayForPaidVoter = 51;




        public IllegalCampaign(DateTime startDate, DateTime endDate, decimal compaignMoney, Candidate candidate) : base(startDate, endDate, compaignMoney, candidate)
        { }

        public override List<Voter> makeVoters()
        {
            int days = (int)(this.startDate - this.endDate).TotalDays;
            int totalVoters = days * percentageForCalculatingNumberOfVotersForOneDay;
            int paidVoters = (totalVoters / 100) * percentageForCalculatingNumberOfPaidVoters;
            List<Voter> allVoters = new List<Voter>();


            for (int i = 0; i < totalVoters; i++)
            {
                allVotersInCampaign.Add(this.CreateOneVoter());
            }
            allVoters.AddRange(allVotersInCampaign);
            allVotesForCampaign = allVoters.Count();

            for (int i = 0; i < paidVoters; i++)
            {
                int money = random.Next(minimummoneyToPayForPaidVoter, maximummoneyToPayForPaidVoter);

                if ((this.compaignMoney - money) < 0)
                {
                    break;
                }
                else
                {
                    allVoters[i].setPaid(true);
                    this.compaignMoney -= money;
                    paidVotes++;

                    if (!citiesWithMaxPaidVotes.ContainsKey(allVoters[i].getCity()))
                    {
                        citiesWithMaxPaidVotes.Add(allVoters[i].getCity(), 1);
                    }
                    else
                    {
                        citiesWithMaxPaidVotes[allVoters[i].getCity()]++;
                    }
                }
            }


            return allVoters;

        }

    }
}
