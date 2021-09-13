using System;
using System.Collections.Generic;
using System.Linq;
using Tickets.DB;
using System.Text;
using System.Threading.Tasks;

namespace Tickets.Commands
{
    public interface ISeat_typeInterface
    {
        List<string> getSeat_types();
        string getSeat_type(int index);
        int getIdSeat_type(string region);
    }
    class Seat_typeCommand : ISeat_typeInterface
    {
        private Tickets_db context;

        public Seat_typeCommand()
        {
            context = new Tickets_db();
        }

        public List<string> getSeat_types()
        {
            List<string> tmp = new List<string>();

            foreach (Seat_types seat_type in context.Seat_types)
                tmp.Add(seat_type.seat_type);

            return tmp.Distinct().ToList();
        }

        public string getSeat_type(int index)
        {
            return context.Seat_types.FirstOrDefault(x => x.id == index).seat_type;
        }

        public int getIdSeat_type(string seat_type)
        {
            return context.Seat_types.FirstOrDefault(x => x.seat_type.Equals(seat_type)).id;
        }
    }
}
