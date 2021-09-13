using Tickets.ViewModels;
namespace Tickets.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;
    //using Tickets.ViewModels;

    public partial class Ticket
    {
        public int id { get; set; }

        public int? client_id { get; set; }

        public int? voyage_id { get; set; }

        public int? seat_id { get; set; }

        public string order_date { get; set; }

        public string order_time { get; set; }

        public virtual Seat Seat { get; set; }

        public virtual User User { get; set; }

        public virtual Voyage Voyage { get; set; }

        private Tickets_db context = new Tickets_db();
        public Ticket()
        {
        }

        public Ticket(int? voyage_id, int? seat_id)
        {
            DateTime date = DateTime.Now;
            this.client_id = CurrentUser.myId();
            this.voyage_id = voyage_id ?? throw new ArgumentNullException(nameof(voyage_id));
            this.seat_id = seat_id ?? throw new ArgumentNullException(nameof(seat_id));
            this.order_date = date.ToShortDateString();
            this.order_time = date.ToLongTimeString();
        }
        public string VoyageName
        {
            get
            {
                Voyage v = new Voyage();
                v = context.Voyages.FirstOrDefault(x => x.id == voyage_id);
                return v.name; 
            }
        }
        public string OrderTime
        {
            get { return order_time; }
        }
        
    }
}
