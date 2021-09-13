using System;
using System.Collections.Generic;
using Tickets.DB;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tickets.Commands
{
    public interface IUserInterface
    {
        IEnumerable<User> getAll();
        void add(User user);
        User getByMail(string name);
        User getById(int? id);
        void update(User oldUser, User newUser);
        void changePrivelege(User user, string privelege);
    }
    class UserCommand : IUserInterface
    {
        private Tickets_db context;

        public UserCommand()
        {
            context = new Tickets_db();
        }
        public IEnumerable<User> getAll()
        {
            return context.Users;
        }
        public void add(User user)
        {
            context.Users.Add(user);
            context.SaveChanges();
        }
        public void delete(User user)
        {
            context.Users.Remove(context.Users.FirstOrDefault(x => x.id == user.id));
            context.SaveChanges();
        }
        public User getByMail(string mail)
        {
            return context.Users.FirstOrDefault(x => x.mail == mail);
        }
        public User getByName(string name)
        {
            return context.Users.FirstOrDefault(x => (x.firstName + " " + x.secondName) == name);
        }
        public User getById(int? id)
        {
            return context.Users.FirstOrDefault(x => x.id == id);
        }
        public void update(User oldUser, User newUser)
        {
            var tmp = context.Users.FirstOrDefault(x => x.id == oldUser.id);

            if (tmp != null)
            {
                tmp.firstName = newUser.firstName;
                tmp.secondName = newUser.secondName;
                tmp.patronymic = newUser.patronymic;
                tmp.mail = newUser.mail;
                tmp.telNumber = newUser.telNumber;
                tmp.date_of_birth = newUser.date_of_birth;
                tmp.passport_id = newUser.passport_id;
                tmp.sex = newUser.sex;
            }
            context.SaveChanges();
        }
        public void changePrivelege(User user, string privelege)
        {
            context.Users.FirstOrDefault(x => x.id == user.id).privilege = privelege;
            context.SaveChanges();
        }
    }
}
