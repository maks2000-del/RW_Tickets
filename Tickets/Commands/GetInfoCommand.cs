using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tickets.DB;

namespace Tickets.Commands
{
    public interface IGetInfoCommandInterface
    {
        string GetFullTicketInfo(Ticket ticket);
        string GetFullVoyageInfo(Voyage voyage);
        string GetShortTicketInfo(Ticket ticket);
        string GetUserInfo(User user);
    }
    class GetInfoCommand : IGetInfoCommandInterface
    {
        private Tickets_db context;

        TicketCommand ticketCommand = new TicketCommand();
        CityCommand cityCommand = new CityCommand();
        Seat_typeCommand seat_typeCommand = new Seat_typeCommand();
        SeatCommand seatCommand = new SeatCommand();
        UserCommand userCommand = new UserCommand();

        public GetInfoCommand()
        {
            context = new Tickets_db();
        }
        public string GetFullTicketInfo(Ticket ticket)
        {
            string str = "";
            Voyage voyage = seatCommand.getByVoyageId(ticket.voyage_id);
            Seat seat = seatCommand.getById(ticket.seat_id);
            str = $"Билет {voyage.name}\n\nДата выезда {voyage.date_of_departure}, время выезда {voyage.time_of_departure}\nДата приезда {voyage.date_of_arrival}, время приезда {voyage.time_of_arrival}\n" +
                $"{seat_typeCommand.getSeat_type(Convert.ToInt32(seat.type_of_seat))}, вагон {seat.num_of_carriage}, место {seat.num_of_seat}\n\n" +
                $"Дата заказа: {ticket.order_date}, время заказа: {ticket.order_time}.";
            return str;
        }
        public string GetFullVoyageInfo(Voyage voyage)
        {
            string str = "";
            string cost = voyage.cost.ToString();
            str = $"Маршрут {voyage.name}\n\nДата выезда {voyage.date_of_departure}, время выезда {voyage.time_of_departure}\nДата приезда {voyage.date_of_arrival}, время приезда {voyage.time_of_arrival}\n цена одного билета {cost.Remove(2, cost.Length - 2)}."; 
            return str;
        }
        public string GetShortTicketInfo(Ticket ticket)
        {
            string str = "";
            Voyage voyage = seatCommand.getByVoyageId(ticket.voyage_id);

            str = $"Билет {voyage.name}\nДата заказа: {ticket.order_date}, время заказа: {ticket.order_time}.\n";
            return str;
        }
        public string GetUserInfo(User user)
        {
            string str = "";
            str = $"Пользователь {user.firstName} {user.secondName}\nEmail: {user.mail}\n\n\n";
            return str;

        }

    }
}
