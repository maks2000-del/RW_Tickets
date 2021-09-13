using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tickets.DB;

namespace Tickets.Commands
{
    public interface IAnnouncementInterface
    {
        IEnumerable<Ticket> getAll();
        void delete(Ticket ticket);
        void add(Ticket ticket);
        IEnumerable<Ticket> getByUserId(int id);
    }
    class TicketCommand : IAnnouncementInterface
    {
        private Tickets_db context;

        public TicketCommand()
        {
            context = new Tickets_db();
        }

        public IEnumerable<Ticket> getAll()
        {
            return context.Tickets;
        }

        public void delete(Ticket ticket)
        {
            context.Tickets.Remove(context.Tickets.FirstOrDefault(x => x.id == ticket.id));
            context.SaveChanges();
        }
        public void add(Ticket ticket)
        {
            context.Tickets.Add(ticket);
            context.SaveChanges();
        }

        public IEnumerable<Ticket> getByUserId(int userID)
        {       
            return context.Tickets.Where(x => x.client_id == userID).ToList();
        }
    }
}
