using System;
using System.Threading;
using PawnshopLibrary;
using System.IO;

namespace PawnshopApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Pawnshop<RegisteredUser> pawnshop = new Pawnshop<RegisteredUser>("Paw", Update());
            bool alive = true;

            while (alive)
            {
                bool flag = false;
                int command = 0;
                string entered_ID = "0";
                while (!flag)
                {
                    Clear();
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\t\t\tRegistration desk");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("1. Register \n2. Log in");
                    command = 0;
                    try
                    {
                        command = Convert.ToInt32(Console.ReadLine());
                    }
                    catch (Exception ex)
                    {
                        OutputException(ex);
                    }
                    try
                    {
                        //-----------Registration------------

                        switch (command)
                        {
                            case 1:
                                entered_ID = Convert.ToString(Register(pawnshop));
                                flag = true;
                                break;
                            case 2:
                                entered_ID = LogIn(pawnshop);
                                if (entered_ID == "")
                                {
                                    throw new Exception("This account does not exist.");
                                }
                                flag = true;
                                break;
                            default:
                                throw new Exception("Wrong input values.");
                        }
                    }
                    catch (Exception ex)
                    {
                        OutputException(ex);
                    }
                }
                bool in_account_alive;
                try
                {
                    in_account_alive = true;
                    int int_real_ID = Int32.Parse(entered_ID);
                    --int_real_ID;
                    while (in_account_alive)
                    {
                        Clear();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"Welcome, {pawnshop.allusers[int_real_ID]._username}! Account ID: {int_real_ID + 1}. Choose an action:");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("1. Hold your stuff \n2. Buy stuff back \n3. Open a store \n4. Cabinet \n5. Lose a day \n6.Change an account \n7. Exit a programm");

                        try
                        {
                            command = Convert.ToInt32(Console.ReadLine());

                            switch (command)
                            {
                                case 1:
                                    HoldStuff(pawnshop, int_real_ID);
                                    break;
                                case 2:
                                    BuyBack(pawnshop, int_real_ID);
                                    break;
                                case 3:
                                    ShowCatalog(pawnshop, int_real_ID);
                                    break;
                                case 4:
                                    EnterCabinet(pawnshop, int_real_ID);
                                    break;
                                case 5:
                                    DayIncrement(pawnshop);
                                    break;
                                case 6:
                                    in_account_alive = false;
                                    continue;
                                case 7:
                                    in_account_alive = false;
                                    alive = false;
                                    continue;
                                default:
                                    throw new Exception("Wrong input values.");
                            }
                        }
                        catch (Exception ex)
                        {
                            OutputException(ex);
                        }
                    }
                }
                catch (FormatException)
                {
                    in_account_alive = true;
                    while (in_account_alive)
                    {
                        Clear();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"Welcome, {pawnshop.admin._username}! Special account №: {pawnshop.admin._adminID}. Choose an action:");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("1. Open a store \n2. Open a loan archive \n3. Enter a company account \n4. Day Increment \n5. Exit an account \n6. Exit a programm");
                        try
                        {
                            command = Convert.ToInt32(Console.ReadLine());
                            switch (command)
                            {
                                case 1:
                                    ShowCatalog(pawnshop);
                                    break;
                                case 2:
                                    ShowLoanHistory(pawnshop);
                                    break;
                                case 3:
                                    EnterCompanyCabinet(pawnshop);
                                    break;
                                case 4:
                                    DayIncrement(pawnshop);
                                    break;
                                case 5:
                                    in_account_alive = false;
                                    continue;
                                case 6:
                                    in_account_alive = false;
                                    alive = false;
                                    continue;
                                default:
                                    throw new Exception("Wrong input values.");
                            }
                        }
                        catch (Exception ex)
                        {
                            OutputException(ex);
                        }
                    }
                }
            }
        }
        //-------------------------Act functions--------------------------
        private static void EnterCabinet(Pawnshop<RegisteredUser> pawnshop, int ID)
        {
            Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"\t\t\tCabinet ID № {ID+1}");
            Console.WriteLine($"Now you have {pawnshop.allusers[ID]._usersum} on account. \nYou would like to... \n");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("1. Withdraw \n2. Put \n3. Check your rating \n4. Check your loan history ?");
            int command = Convert.ToInt32(Console.ReadLine());
            switch (command)
            {
                case 1:
                    Withdraw(pawnshop, ID);
                    break;
                case 2:
                    Put(pawnshop, ID);
                    break;
                case 3:
                    ShowRate(pawnshop, ID);
                    break;
                case 4:
                    ShowLoanHistory(pawnshop, ID);
                    break;
                default:
                    throw new Exception("Wrong input values.");
            }
            Thread.Sleep(1500);
        }

        private static void EnterCompanyCabinet(Pawnshop<RegisteredUser> pawnshop)
        {
            Clear();
            Console.ForegroundColor = ConsoleColor.Yellow; 
            Console.WriteLine("\t\t\tCompany cabinet");
            decimal[] companybill = CompanyState(pawnshop);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"The company has {companybill[0]} on bill.");
            if ((double)companybill[1] >= 0)
            {
                Console.WriteLine($"Currently income: {companybill[1]}");
            }
            else
            {
                Console.WriteLine($"Currently consumption: {companybill[1]}");
            }
            Console.WriteLine("\n You would like to... \n");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("1. Withdraw \n2. Put ?");
            int command = Convert.ToInt32(Console.ReadLine());
            switch (command)
            {
                case 1:
                    Withdraw(pawnshop);
                    break;
                case 2:
                    Put(pawnshop);
                    break;
                default:
                    throw new Exception("Wrong input values.");
            }
            Thread.Sleep(1500);
        }

        private static void Withdraw(Pawnshop<RegisteredUser> pawnshop, int ID)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("How much would you like to withdraw?");
            Console.ForegroundColor = ConsoleColor.White;
            decimal sum = Convert.ToDecimal(Console.ReadLine());
            pawnshop.allusers[ID].Withdraw(sum);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Done!");
            Thread.Sleep(1500);
        }

        private static void Withdraw(Pawnshop<RegisteredUser> pawnshop)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("How much would You like to withdraw?");
            Console.ForegroundColor = ConsoleColor.White;
            decimal sum = Convert.ToDecimal(Console.ReadLine());
            pawnshop._companysum = RegisteredAdmin.WithdrawPawnshop(pawnshop._companysum, sum);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Done!");
            Thread.Sleep(1500);
        }
        private static void Put(Pawnshop<RegisteredUser> pawnshop, int ID)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("How much would you like to put on your bill?");
            Console.ForegroundColor = ConsoleColor.White;
            decimal sum = Convert.ToDecimal(Console.ReadLine());
            pawnshop.allusers[ID].Put(sum);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Done!");
            Thread.Sleep(1500);
        }

        private static void Put(Pawnshop<RegisteredUser> pawnshop)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("How much would you like to put on your bill?");
            Console.ForegroundColor = ConsoleColor.White;
            decimal sum = Convert.ToDecimal(Console.ReadLine());
            pawnshop._companysum = RegisteredAdmin.PutPawnshop(pawnshop._companysum, sum);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Done!");
            Thread.Sleep(1500);
        }

        private static int Register(Pawnshop<RegisteredUser> pawnshop)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Enter your username:");
            string username = Console.ReadLine();
            int ID = 0;
            if (username != "" && username != " ")
            {
                RegisteredUser user = new RegisteredUser(username);
                ID = pawnshop.RegisterUser(user);
                pawnshop.allusers[ID].Notify += DisplayMessage;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Registration success");
            }
            else { throw new Exception("Wrong input value of username. Please, use symbols."); }
            return ++ID;
        }
        private static string LogIn(Pawnshop<RegisteredUser> pawnshop)
        {
            string entered_ID;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Please, enter your ID:");
            Console.ForegroundColor = ConsoleColor.White;
            entered_ID = Console.ReadLine();
            try
            {
                int real_ID = Convert.ToInt32(entered_ID) - 1;
                if (!pawnshop.IsAccountExist(real_ID))
                {
                    entered_ID = "";
                }
            }
            catch (FormatException)
            {
                if (entered_ID != "a")
                {
                    entered_ID = "";
                }
            }
            return entered_ID;
        }

        private static void HoldStuff(Pawnshop<RegisteredUser> pawnshop, int ID)
        {
            Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\t\t\tChoosing a kind of stuff to hold");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Choose a category of stuff:");
            Console.ForegroundColor = ConsoleColor.White;
            ShowAllCategories(pawnshop);
            int num = pawnshop._categories.Length,
                category = num + 1,
                stuff = 0;
            bool flag = false;
            try
            {
                while (!flag)
                {
                    category = Convert.ToInt32(Console.ReadLine());
                    if (category <= num && category>0)
                    {
                        flag = true;
                    }
                    else
                    {
                        throw new Exception("Wrong unput value. Please, try again.");
                    }
                }
                flag = false;
                try
                {
                    while (!flag)
                    {
                        if (category <= num)
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("Choose a stuff:");
                            Console.ForegroundColor = ConsoleColor.White;
                            ShowAllStuff(pawnshop, category - 1);
                            num = pawnshop._categories[category - 1]._stuffamount;
                            try
                                {
                                while (!flag)
                                {
                                    stuff = Convert.ToInt32(Console.ReadLine());
                                    if (stuff <= num && stuff>0)
                                    {
                                        flag = true;
                                    }
                                    else
                                    {
                                        throw new Exception("Wrong input value. Please, try again.");
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                OutputException(ex);
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Wrong value. Please, try again");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                    }
                }
                catch (Exception ex)
                {
                    OutputException(ex);
                }
                MakeLoan(pawnshop, category, stuff, ID);
            }
            catch (Exception ex)
            {
                flag = true;
                OutputException(ex);
            }
        }
        private static void BuyBack(Pawnshop<RegisteredUser> pawnshop, int ID)
        {
            Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Buying back menu");
            Console.ForegroundColor = ConsoleColor.White;
            ShowLoanHistory(pawnshop, ID);

            if (pawnshop.allusers[ID]._userhistory._loans != null && pawnshop.allusers[ID]._userhistory.IsActive())
            {
                Console.WriteLine("Choose a stuff to buy back. If nothing, write down '0':");
                int command;
                Console.ForegroundColor = ConsoleColor.White;
                command = Convert.ToInt32(Console.ReadLine());
                if (command != 0)
                {
                    pawnshop.allusers[ID].BuyBack(command - 1);
                    if (pawnshop.allusers[ID]._userrating < 5)
                    {
                        pawnshop.allusers[ID]._userrating += 1;
                    }
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Bought!\nYour rating was increased.");
                }
            }
            Thread.Sleep(1500);
        }

        //---------------------------Helpful to act fucnctions----------------------------

        private static decimal[] CompanyState(Pawnshop<RegisteredUser> pawnshop)
        {
            return pawnshop.CheckBill();
        }

        private static StuffCategory[] Update()
        {
            string path = @"C:\\Stuff.txt";
            StreamReader stuffdata = new StreamReader(path);
            string line = stuffdata.ReadLine();
            string[] values = line.Split(new char[] { ' ' });   // values[0] - categories, values[1] - stuff
            int categoriesamount = Convert.ToInt32(values[0]);
            int stuffamount = Convert.ToInt32(values[1]);

            StuffCategory[] categories = new StuffCategory[categoriesamount];
            string[] data;
            for (int i = 0; i < categoriesamount; i++)
            {
                categories[i] = new StuffCategory();
                categories[i]._categoryname = stuffdata.ReadLine();
                Stuff[] stufftocategory = new Stuff[stuffamount];
                for (int j = 0; j < stuffamount; j++)
                {
                    data = stuffdata.ReadLine().Split(new char[] { '/' });  // name/cost/rate/days
                    Stuff tmp = new Stuff(data[0], Convert.ToDecimal(data[1]), Convert.ToDouble(data[2]), Convert.ToInt32(data[3]));
                    stufftocategory[j] = tmp;
                }
                categories[i]._stuffamount = stuffamount;
                categories[i]._stuffthere = stufftocategory;
            }

            Console.WriteLine("Data was updated!");
            Thread.Sleep(1000);
            return categories;
        }

        private static void DayIncrement(Pawnshop<RegisteredUser> pawnshop)
        {
            pawnshop.DayIncrease();
        }
        public static void MakeLoan(Pawnshop<RegisteredUser> pawnshop, int category, int stuff, int ID)
        {
            if (pawnshop._categories[category-1]._stuffthere[stuff-1]._stuffcost <= pawnshop._companysum)
            {
                ConsoleColor color = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("=================A Deal==================" +
                    $"\nUser: {pawnshop.allusers[ID]._username}" +
                    $"\nID: {ID + 1}" +
                    $"\n\n\tHas a desire to hold his {pawnshop._categories[category - 1]._stuffthere[stuff - 1]._stuffname} in our pawnshop. " +
                    $"\nUser receives: {pawnshop._categories[category - 1]._stuffthere[stuff - 1]._stuffcost}" +
                    $"\nRate per a day: {pawnshop._categories[category - 1]._stuffthere[stuff - 1]._stuffrate}" +
                    $"\nDeadline for buying back: {pawnshop._categories[category - 1]._stuffthere[stuff - 1]._stuffdaysleft}");
                Console.ForegroundColor = color;
                Console.WriteLine("Are you sure? No or Yes");
                bool flag = false;
                string command = "No";
                while (!flag)
                {
                    command = Console.ReadLine();
                    if (command == "No" || command == "Yes") { flag = true; }
                    else { Console.WriteLine("Please, answer the question correctly."); }
                }
                if (command == "Yes")
                {
                    pawnshop.allusers[ID].Hold(pawnshop._categories[category - 1], stuff - 1);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Thank you for a great deal!");
                }
                else
                {
                    Console.WriteLine("A deal interrupted.");
                }
            }
            else
            {
                throw new Exception("A pawnshop is not able to pay you for holding the stuff. Please take our apologies");
            }
        }

        //-------------------------"Show" functions-----------------------------

        private static void ShowLoanHistory(Pawnshop<RegisteredUser> pawnshop, int userID)
        {
            Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\t\t\tYout loan history");
            Console.ForegroundColor = ConsoleColor.Blue;
            LoanHistory history = pawnshop.allusers[userID]._userhistory;
            int hist_size = history._loanamount;
            Loan[] hist_loan = history._loans;
            if (hist_size == 0 || hist_loan == null)
            {
                Console.WriteLine("Your user history is empty.");
            }
            else
            {
                for (int i = 0; i < hist_size; i++)
                {
                    Console.Write($"{i + 1}  {hist_loan[i]._stuff}  {hist_loan[i]._loanstatus} ");
                    if (hist_loan[i]._loanstatus == Loanstatus.Active)
                    {
                        Console.WriteLine($"cost: { hist_loan[i]._cost}," +
                            $" { hist_loan[i]._rate} %  { hist_loan[i]._daysleft}" +
                            "d.left");
                    }
                    else { Console.WriteLine(""); }
                }
            }
            Thread.Sleep(1500);
        }
        private static void ShowLoanHistory(Pawnshop<RegisteredUser> pawnshop)
        {
            Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\t\t\tLoan archive");
            Console.ForegroundColor = ConsoleColor.White;
            try
            {
                if(RegisteredUser.usercounter == 0)
                {
                    Console.WriteLine("There is no user yet.");
                }
                for (int j = 0; j < RegisteredUser.usercounter + 1; j++)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"User {pawnshop.allusers[j]._username}, ID: {pawnshop.allusers[j]._userID+1}, rating: {pawnshop.allusers[j]._userrating}/5");
                    LoanHistory history = pawnshop.allusers[j]._userhistory;
                    Loan[] hist_loan = history._loans;
                    int hist_size = history._loanamount;
                    if(hist_size == 0)
                    {
                        Console.WriteLine("Empty");
                    }
                    for (int i = 0; i < hist_size; i++)
                    {
                        Console.Write($"{i + 1}  {hist_loan[i]._stuff}  {hist_loan[i]._loanstatus} ");
                        if (hist_loan[i]._loanstatus == Loanstatus.Active)
                        {
                            Console.WriteLine($"cost: { hist_loan[i]._cost}," +
                                $" { hist_loan[i]._rate} %  { hist_loan[i]._daysleft}" +
                                "d.left");
                        }
                        else { Console.WriteLine(""); }
                    }
                }
                Console.ForegroundColor = ConsoleColor.White;
                Thread.Sleep(1500);
            }
            catch (Exception)
            {
                Exception ex = new Exception("There is no user yet.");
                OutputException(ex);
            }
        }

        private static void ShowCatalog(Pawnshop<RegisteredUser> pawnshop)
        {
            Clear();
            LoanHistory history = pawnshop.store;
            Loan[] hist_loan = history._loans;
            int hist_size = history._loanamount;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\t\t\tWelcome on digital pawnshop catalog!");
            Console.ForegroundColor = ConsoleColor.Blue;
            for (int i = 0; i < hist_size; i++)
            {
                Console.WriteLine($"{i + 1}  {hist_loan[i]._stuff}, cost: {hist_loan[i]._cost}");
            }
            if (hist_size == 0)
            {
                Console.WriteLine("Sorry, the store has no product now. Visit our later!");
            }
            Thread.Sleep(1500);
        }

        private static void ShowCatalog(Pawnshop<RegisteredUser> pawnshop, int userID)
        {
            Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\t\t\tWelcome on digital pawnshop catalog!");
            Console.ForegroundColor = ConsoleColor.Blue;
            LoanHistory catalog = pawnshop.store;
            int catalog_size = catalog._loanamount;
            if (catalog_size == 0)
            {
                Console.WriteLine("Sorry, the store has no product now. Visit our later!");
            }
            else
            {
                Loan[] products = catalog._loans;
                for (int i = 0; i < catalog_size; i++)
                {
                    Console.WriteLine($"{i + 1}  {products[i]._stuff}, cost: {(double)products[i]._cost}");
                }

                Console.WriteLine("Would you like to buy something? Choose! If nothing, write down '0'");
                int command;
                bool flag = false;
                while (!flag)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    command = Convert.ToInt32(Console.ReadLine());
                    if (command <= catalog_size && command > 0)
                    {
                        pawnshop.allusers[userID].BuyStuff(command);
                        flag = true;
                    }
                    else if(command!=0)
                    {
                        throw new Exception("You are inputting a wrong value. Try again.");
                    }
                }
            }
            Thread.Sleep(1500);
        }

        private static void ShowRate(Pawnshop<RegisteredUser> pawnshop, int ID)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Your rating is {pawnshop.allusers[ID]._userrating}");
            Console.ForegroundColor = ConsoleColor.White;
            Thread.Sleep(1500);
        }

        private static void ShowAllStuff(Pawnshop<RegisteredUser> pawnshop, int category)
        {
            StuffCategory stuffcategory = pawnshop._categories[category];
            int count = 0;
            foreach (Stuff j in stuffcategory._stuffthere)
            {
                ++count;
                Console.WriteLine($"\t{count}) {j._stuffname}");
            }
        }
        private static void ShowAllCategories(Pawnshop<RegisteredUser> pawnshop)
        {
            StuffCategory[] categories = pawnshop._categories;
            int count = 0;
            foreach (StuffCategory i in categories)
            {
                ++count;
                Console.WriteLine($"{count}. {i._categoryname}");
            }
        }
        //-------------------Managing functions-----------------------
        private static void OutputException(Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(ex.Message);
            Console.ForegroundColor = ConsoleColor.White;
            Thread.Sleep(1500);
        }
        private static void Clear()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("    _______  ____   __      __\n" +
                "   /\\______\\/\\___\\ |\\_\\ _  /\\_\\\n" +
            "  / /  __ // /   | || |\\_\\/ / /\n" +
            " / /  /_/// /  __| || |/_ \\/ /\n" +
            "/ /  ___// /  /_|| ||  /||  /\n" +
            "\\/__/    \\/_ / |_| \\|_/ \\|_/  Pawnshop Kyiv");
            Console.ForegroundColor = ConsoleColor.White;
        }

        private static void DisplayMessage(object sender, AccountEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"Transaction sum: {e.Sum}");
            Console.WriteLine(e.Message);
            Thread.Sleep(1000);
        }
    }
}
