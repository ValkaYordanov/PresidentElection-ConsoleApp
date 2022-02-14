using System;
using System.Collections.Generic;
using System.Text;

namespace President
{
    public class Ballot
    {
        public bool isValid { get; set; }
        public Candidate candidate { get; set; }
        public string city { get; set; }
    }
}
