using System;
using System.Collections.Generic;
using System.Text;

namespace President
{
    public class WinnerORunnerUp
    {
        private String name;
        private int vote;

        public string getName()
        {
            return name;
        }

        public void setName(string name)
        {
            this.name = name;
        }

        public int getVote()
        {
            return vote;
        }

        public void setVote(int vote)
        {
            this.vote = vote;
        }
    }
}
