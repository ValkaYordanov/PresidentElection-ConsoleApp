using System;
using System.Collections.Generic;
using System.Text;

namespace President
{
    public class Ballot
    {
        private bool isValid { get; set; }
        private Candidate candidate { get; set; }
        private string city { get; set; }


        public void SetIsValid(bool isValid)
        {
            this.isValid = isValid;
        }
        public bool GetIsValid()
        {
            return isValid;
        }
        public void SetCity(string city)
        {
            this.city = city;
        }
        public string GetCity()
        {
            return city;
        }

        public void SetCandidate(Candidate candidate)
        {
            this.candidate = candidate;
        }

        public Candidate GetCandidate()
        {
            return candidate;
        }
    }
}
