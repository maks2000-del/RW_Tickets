using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tickets.DB;
using System.ComponentModel;

namespace Tickets.ViewModels
{
    class CurrentUser : BaseViewModel
    {
        private static User user;

        public static User User
        {
            get { return user; }
            set => user = value;
        }

        public static bool isAdmin()
        {
            if (User.privilege.Equals("admin"))
                return true;
            return false;
        }
        public static bool isFullRegistrathion()
        {
            if (!String.IsNullOrEmpty(User.firstName) && !String.IsNullOrEmpty(User.secondName) && !String.IsNullOrEmpty(User.patronymic) && !String.IsNullOrEmpty(User.mail) && !String.IsNullOrEmpty(User.telNumber) && !String.IsNullOrEmpty(User.date_of_birth) && !String.IsNullOrEmpty(User.passport_id))
                return true;
            return false;
        }
        public static int myId()
        {
            return User.id;
        }
        //public override string ToString()
        //{
        //    return User.FirstName + " " + User.SecondName;
        //}

    }
}

