using System;
using System.Collections.Generic;
using System.Text;

namespace PawnshopLibrary
{
    public enum Usertype
    {
        Unregistered,
        RegisteredUser,
        RegisteredAdmin
    };
    public abstract class User
    {
        public string _username { get; set; }
        protected Usertype _userstatus { get; set; }
        public User()
        {
            _username = "";
            _userstatus = Usertype.Unregistered;
        }
    }
}
