using System;
using System.Collections.Generic;
using System.Text;

namespace PawnshopLibrary
{
    public class LoanHistory : Loan
    {
        public Loan[] _loans { get; set; }
        public int _loanamount { get; set; }

        public LoanHistory()
        {
            _loans = null;
            _loanamount = 0;
        }
        public LoanHistory(Loan[] loans, int loanamount)
        {
            _loanamount = loanamount;
            _loans = new Loan[loanamount];
            for (int i=0; i < loanamount; i++)
            {
                _loans[i] = loans[i];
            }
        }

        public bool IsActive()
        {
            bool flag = false;
            for (int i=0; i<_loanamount; i++)
            {
                if(_loans[i]._loanstatus == Loanstatus.Active)
                {
                    flag = true;
                }
            }
            return flag;
        }
    }
}
