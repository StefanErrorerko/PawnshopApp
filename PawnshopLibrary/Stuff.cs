using System;
using System.Collections.Generic;
using System.Text;

namespace PawnshopLibrary
{
    public class Stuff
    {
        public string _stuffname { get; private set; }
        public decimal _stuffcost { get; private set; }
        public double _stuffrate { get; private set; }
        public int _stuffdaysleft { get; private set; }
        public Stuff()
        {
            _stuffname = "";
            _stuffcost = Decimal.Zero;
            _stuffrate = 0.0;
            _stuffdaysleft = 0;
        }
        public Stuff(string name, decimal cost, double rate, int termin)
        {
            _stuffname = name;
            _stuffcost = cost;
            _stuffrate = rate;
            _stuffdaysleft = termin;
        }
    }
}
