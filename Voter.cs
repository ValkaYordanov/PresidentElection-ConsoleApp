using System;
using System.Collections.Generic;
using System.Text;

namespace President
{
    public abstract class Voter
    {
        protected string name;
        protected string gender;
        protected string city;
        protected Candidate candidate;
        protected bool paid;

        public void setPaid(bool paid)
        {
            this.paid = paid;
        }

        public string getCity()
        {
            return city;
        }

        public string getCandidateName()
        {
            return candidate.getNameOfCandidate();
        }
        public Voter(string name, string gender, string city, Candidate candidate, bool paid)
        {
            this.name = name;
            this.gender = gender;
            this.city = city;
            this.candidate = candidate;
            this.paid = paid;
        }

    }
}
