using System;
using System.Collections.Generic;
using System.Text;

namespace PawnshopLibrary
{
    public enum Loanstatus
    {
        Active,
        Bought,
        Fired
    };
    public class Loan
    {
        //ID
        public string _stuff { get; private set; }
        public int _daysleft { get; set; }
        public decimal _cost { get; set; }
        public double _rate { get; private set; }
        public Loanstatus _loanstatus { get; set; }
        internal int _loanerID { get; set; }

        public Loan()
        {
            _stuff = "";
            _daysleft = 0;
            _cost = 0;
            _rate = 0;
            _loanerID = 0;
            _loanstatus = Loanstatus.Fired;
        }
        public Loan(Stuff stuff, int loanerID)
        {
            _stuff = stuff._stuffname;
            _daysleft = stuff._stuffdaysleft;
            _cost = stuff._stuffcost;
            _rate = stuff._stuffrate;
            _loanerID = loanerID;
            _loanstatus = Loanstatus.Active;
        }
    }
}
