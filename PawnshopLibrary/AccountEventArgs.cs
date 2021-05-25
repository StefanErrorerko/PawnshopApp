using System;
using System.Collections.Generic;
using System.Text;

namespace PawnshopLibrary
{
    public class AccountEventArgs
    {
        public string Message { get; }
        public decimal Sum { get; }
        public AccountEventArgs(string mes, decimal sum)
        {
            Message = mes;
            Sum = sum;
        }
    }
}
