using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Tickets.DB;

namespace Tickets.Commands
{
    public interface ISeatInterface
    {
        Seat getById(int? seat_id);
        Voyage getByVoyageId(int? voyage_id);
        void add_voyage(Voyage voyage);
        void add_seat(Seat seat);
        void update(Seat seat);
        void update_bought(int? seat_id);
        int get_voyage_id(string City_of_departure, string City_of_arrival, string Date, string Time);
        int get_any_voyage_id(string City_of_departure, string City_of_arrival);
        int get_seat_id(string City_of_departure, string City_of_arrival, string Type, string Seat, string Carrige, string Date, string Time);
    }
    class SeatCommand : ISeatInterface
    {
        private Tickets_db context;
        public SeatCommand()
        {
            context = new Tickets_db();
        }
        public Seat getById(int? seat_id)
        {
            return context.Seats.FirstOrDefault(x => x.id == seat_id);
        }
        public Voyage getByVoyageId(int? voyage_id)
        {
            return context.Voyages.FirstOrDefault(x => x.id == voyage_id);
        }
        public void add_voyage(Voyage voyage)
        {
            context.Voyages.Add(voyage);
            context.SaveChanges();
        }
        public void add_seat(Seat seat)
        {
            context.Seats.Add(seat);
            context.SaveChanges();
        }
        public int get_any_voyage_id(string City_of_departure, string City_of_arrival)
        {
            int city_id_out = 0, city_id_in = 0, voyage_id = 0;

            foreach (City city in context.Citys)
            {
                if (city.city1 == City_of_departure)
                {
                    city_id_out = city.id; break;
                }
            }
            foreach (City city in context.Citys)
            {
                if (city.city1 == City_of_arrival)
                {
                    city_id_in = city.id; break;
                }
            }
            if (city_id_out != 0 && city_id_in != 0)
            {
                Voyage v = context.Voyages.FirstOrDefault(x => x.id_city___of_departure == city_id_out && x.id_city___of_arrival == city_id_in);
                voyage_id = v.id;
            }
            return voyage_id;
        }
        public int get_voyage_id(string City_of_departure, string City_of_arrival, string Date, string Time)
        {
            int city_id_out = 0, city_id_in = 0, voyage_id = 0;

            foreach (City city in context.Citys)
            {
                if (city.city1 == City_of_departure)
                {
                    city_id_out = city.id; break;
                }
            }
            foreach (City city in context.Citys)
            {
                if (city.city1 == City_of_arrival)
                {
                    city_id_in = city.id; break;
                }
            }
            foreach (Voyage voyage in context.Voyages)
            {
                if ((voyage.id_city___of_departure == city_id_out) && (voyage.id_city___of_arrival == city_id_in) && (voyage.date_of_departure == Date) && (voyage.time_of_departure == Time))
                {
                    voyage_id = voyage.id;
                }
            }

            return voyage_id;
        }
        public int get_seat_id(string City_of_departure, string City_of_arrival, string Type, string Seat, string Carrige, string Date, string Time)
        {
            int seat_id = 0, seat_type_id = 0, voyage_id = get_voyage_id(City_of_departure, City_of_arrival, Date, Time), num_of_seat = 0, num_of_carriage = 0;

            foreach (Seat_types seat_type in context.Seat_types)
            {
                if (seat_type.seat_type == Type)
                {
                    seat_type_id = seat_type.id;
                }
            }
            num_of_seat = Convert.ToInt32(Seat);
            num_of_carriage = Convert.ToInt32(Carrige);

            foreach (Seat seat in context.Seats)
            {
                if ((seat.type_of_seat == seat_type_id) && (seat.voyage_id == voyage_id) && (seat.num_of_seat == num_of_seat) && (seat.num_of_carriage == num_of_carriage))
                {
                    seat_id = seat.id; break;
                }
            }
            return seat_id;
        }
        public void update(Seat seat)
        {
            var tmp = context.Seats.FirstOrDefault(x => x.id == seat.id);

            if (tmp != null)
            {
                if (tmp.is_free == true)
                    tmp.is_free = false;

            }
            context.SaveChanges();
        }
        public void update_bought(int? seat_id)
        {
            Seat seat = getById(seat_id);
            var tmp = context.Seats.FirstOrDefault(x => x.id == seat.id);

            if (tmp != null)
            {
                if (tmp.is_free == false)
                    tmp.is_free = true;

            }

            context.SaveChanges();
        }
    }
}
