using System;
using System.Collections.Generic;
using System.Text;

namespace PawnshopLibrary
{
    public class StuffCategory : Stuff
    {
        public string _categoryname { get; set; }
        public Stuff[] _stuffthere { get; set; }
        public int _stuffamount { get; set; }
        public StuffCategory()
        {
            _stuffthere = new Stuff[0];
            _stuffamount = 0;
            _categoryname = "";
        }
        public StuffCategory(Stuff[] stuffs, string categoryname, int amount)
        {
            _stuffamount = amount;
            _categoryname = categoryname;
            _stuffthere = new Stuff[amount];
            for (int i=0; i<amount; i++)
            {
                _stuffthere[i] = stuffs[i];
            }
        }
    }
}
