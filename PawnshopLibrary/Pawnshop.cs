using System;
using System.Collections.Generic;
using System.Text;

namespace PawnshopLibrary
{
    public class Pawnshop<T> where T : RegisteredUser
    {
        public T[] allusers { get; set; }
        public StuffCategory[] _categories { get; set; }
        public LoanHistory store { get; set; }
        public RegisteredAdmin admin;
        public decimal _companysum { get; set; }
        public string _name { get; private set; }

        public Pawnshop(string name, StuffCategory[] categories){
            _name = name;
            _categories = categories;
            admin = new RegisteredAdmin("admin");
            _companysum = 50000;
            store = new LoanHistory();
        }
        public int RegisterUser(RegisteredUser user)
        {
            T new_user = null;
            new_user = user as T;
            if (new_user == null)
            {
                throw new Exception("While registering, errupted error.");
            }
            if (allusers == null)
            {
                allusers = new T[] { new_user };
            }
            else
            {
                T[] tmpusers = new T[allusers.Length + 1];
                for(int i=0; i<allusers.Length; i++)
                {
                    tmpusers[i] = allusers[i];
                }
                tmpusers[tmpusers.Length - 1] = new_user;
                allusers = tmpusers;
            }
            int ID = Convert.ToInt32(allusers[allusers.Length - 1]._userID);
            return ID;
        }
        public void DayIncrease()
        {
            if (allusers != null)
            {
                for (int i = 0; i < allusers.Length; i++)
                { 
                    for (int j=0; j < allusers[i]._userhistory._loanamount;  j++)
                    {
                        if(allusers[i]._userhistory != null)
                        {
                            LoanDaysLeftDecrease(allusers[i]._userhistory._loans[j]);
                            if (allusers[i]._userhistory._loans[j]._daysleft == 0)
                            {
                                if (store != null) {
                                    store._loanamount += 1;
                                    Loan[] tmpcatalog = store._loans;
                                    Loan[] newcatalog = new Loan[store._loanamount];
                                    for (int v = 0; v < store._loanamount-1; v++)
                                    {
                                        newcatalog[v] = tmpcatalog[v];
                                    }
                                    newcatalog[store._loanamount-1] = allusers[i]._userhistory._loans[j];
                                    newcatalog[store._loanamount-1]._cost = CalculateCostOnStore(newcatalog[store._loanamount-1]._cost);
                                    store._loans = newcatalog;
                                }
                                else
                                {
                                    
                                    Loan[] newcatalog = new Loan[1];
                                    newcatalog[0] = allusers[i]._userhistory._loans[j];
                                    store = new LoanHistory(newcatalog, 1);
                                }
                                allusers[i]._userhistory._loans[j]._loanstatus = Loanstatus.Fired;
                                if (allusers[i]._userrating > 0)
                                {
                                    allusers[i]._userrating -= 1;
                                }
                                RegisteredUser._sumcounter += allusers[i]._userhistory._loans[j]._cost;
                                _companysum += allusers[i]._userhistory._loans[j]._cost;
                                allusers[i]._userhistory._loans[j]._daysleft = -1;
                            }
                        }
                    }
                }
                _companysum += RegisteredUser._sumcounter;
                RegisteredUser._sumcounter = 0;
            }
        }

        public void LoanDaysLeftDecrease(Loan loan)
        {
            if (loan._daysleft > 0)
            {
                --loan._daysleft;
                CalculateCost(loan);
            }
        }

        public void CalculateCost(Loan loan)
        {
            loan._cost *= (decimal)(1.0 + 0.01 * loan._rate);
        }

        public decimal[] CheckBill()
        { 
            decimal[] bill;
            bill = new decimal[2];
            bill[0] = _companysum;
            bill[1] = RegisteredUser._sumcounter;
            return bill;
        }
        
        public bool IsAccountExist(int ID)
        {
            try
            {
                if (allusers != null && ID < allusers.Length && ID >= 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (NullReferenceException)
            {
                return false;
            }
        }
        public decimal CalculateCostOnStore(decimal cost)
        {
            //standard overcost is 1.5 bigger than start cost
            double rate = 1.5;
            return cost * (decimal)rate;
        }
    }
}
