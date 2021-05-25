#include "Loan.h"

enum Loanstatus {
    Active,
    Bought,
    Fired
};

    //ID
    private string _stuff;
    private int _daysleft;
    private decimal _cost;
    private double _rate;
    private Loanstatus _loanstatus;
    private int _loanerID;
    private static int daycounter;

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
        _stuff = stuff.GetName();
        _daysleft = stuff.GetDays();
        _cost = stuff.GetCost();
        _rate = stuff.GetRate();
        _loanerID = loanerID;
        _loanstatus = Loanstatus.Active;
    }
    public void DayIncrement()
    {
        ++daycounter;
    }

    public void SetStuff(string stuff) {
        _stuff = stuff;
    }
    _daysleft = 0;
    _cost = 0;
    _rate = 0;
    _loanerID = 0;
    _loanstatus = Loanstatus.Fired;
}
}