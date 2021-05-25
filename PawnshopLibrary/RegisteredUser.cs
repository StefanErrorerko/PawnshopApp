using System;
using System.Collections.Generic;
using System.Text;

namespace PawnshopLibrary
{
    public class RegisteredUser : User
    {
        public delegate void AccountHandler(object sender, AccountEventArgs e);
        public event AccountHandler Notify;
        public int _userrating { get; set; }
        public static int usercounter { get; private set; } = -1;
        public decimal _usersum { get; set; }
        public int _userID { get; private set; }
        public LoanHistory _userhistory { get; private set; }
        internal static decimal _sumcounter { get; set; } = 0;


        public RegisteredUser(string UserName)
        {
            _username = UserName;
            _userstatus = Usertype.RegisteredUser;
            _userID = ++usercounter;
            _userrating = 5;
            _userhistory = new LoanHistory(); 
        }
        public void BuyBack(int loanID)
        {
            Loan loan = _userhistory._loans[loanID];
            decimal cost = loan._cost;               // increase profit
            
            if(loan._loanstatus != Loanstatus.Active)
            {
                throw new Exception("This stuff is already bought or fired.");
            }
            else if (cost > _usersum )
            {
                throw new Exception("Too low sum on wallet. Operation is cancelled."); 
            }
            else
            {
                _sumcounter += cost;
                
                _usersum -= loan._cost;   // decrease bill
                loan._daysleft = -1;
                loan._loanstatus = Loanstatus.Bought;
            }
        }

        public void Hold(StuffCategory category, int kindofstuff)
        {
            if (_userrating <= 1)
            {
                throw new Exception("Too low rating to do this operation.");
            }
            else {
                decimal cost = category._stuffthere[kindofstuff]._stuffcost;
                _sumcounter -= cost;
                _usersum += cost;
                Loan[] tmploans = _userhistory._loans;
                Loan[] newloans = new Loan[_userhistory._loanamount + 1];
                for (int i = 0; i < _userhistory._loanamount + 1; i++)
                {
                    if(i!= _userhistory._loanamount)
                    {
                        newloans[i] = tmploans[i];
                    }
                    else { newloans[i] = new Loan (category._stuffthere[kindofstuff], _userID); }
                }
                _userhistory._loans = newloans;
                _userhistory._loanamount += 1;
            }
        }

        public void BuyStuff(decimal sum)
        {
            if (sum <= _usersum)
            {
                _usersum -= sum;
                _sumcounter += sum;
            }
            else
            {
                throw new Exception("Too low sum on wallet. Operation cancelled");
            }
        }
      
        public void Withdraw(decimal sum)
        {
            if (sum < 0)
            {
                throw new Exception("The input value is wrong.");
            }
            else if (sum < _usersum)
            {
                _usersum -= sum;
                Notify?.Invoke(this, new AccountEventArgs($"Your bill was reduced on {sum}", sum));
            }
            else { throw new Exception("There is too low sum on your digital wallet. Operation cancelled."); }
        }
        public void Put(decimal sum)
        {
            if (sum < 0)
            {
                throw new Exception("The input value is wrong.");
            }
            _usersum += sum;
            Notify?.Invoke(this, new AccountEventArgs($"Your bill was expanded with {sum}", sum));
        }

        public void BuyStuff(int userID, Pawnshop<RegisteredUser> pawnshop, int stuffinstore)
        {
            string stuff = pawnshop.store._loans[stuffinstore - 1]._stuff;
            _usersum -= pawnshop.store._loans[stuffinstore - 1]._cost;
            pawnshop._companysum += pawnshop.store._loans[stuffinstore - 1]._cost;
            pawnshop.store._loanamount -= 1;
            Loan[] tmpcatalog = pawnshop.store._loans;
            Loan[] newcatalog = new Loan[pawnshop.store._loanamount];
            int u = 0;
            for (int v = 0; v < pawnshop.store._loanamount; v++)
            {
                if (v != stuffinstore - 1)
                {
                    newcatalog[v] = tmpcatalog[u + v];
                }
                else { ++u; }
            }
            pawnshop.store._loans = newcatalog;
            Console.WriteLine($"You have successfully bought {stuff}");
        }
    }
}
