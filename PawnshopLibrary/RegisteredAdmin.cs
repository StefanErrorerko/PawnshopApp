using System;
using System.Collections.Generic;
using System.Text;

namespace PawnshopLibrary
{
    public class RegisteredAdmin : User
    {
        public char _adminID { get; private set; }
        private static char admincounter = '`';         // ` in ASCII table is in front of 'a'

        public RegisteredAdmin(string UserName)
        {
            _username = UserName;
            _userstatus = Usertype.RegisteredAdmin;
            _adminID = ++admincounter;
        }
        public static decimal WithdrawPawnshop(decimal companysum, decimal sum)
        {
            if (sum < 0)
            {
                ConsoleColor color = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                throw new Exception("The input value is wrong.");
                Console.ForegroundColor = color;
            }
            else if (sum < companysum)
            {
                companysum -= sum;
            }
            else
            {
                throw new Exception("There is too low sum on company's digital wallet. Operation cancelled.");
            }
            return companysum;
        }
        public static decimal PutPawnshop(decimal companysum, decimal sum)
        {
            if (sum < 0)
            {
                throw new Exception("The input value is wrong.");
            }
            return companysum + sum;
        }
    }
}
