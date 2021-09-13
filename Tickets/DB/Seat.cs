namespace Tickets.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class Seat
    {
        private Tickets_db context;
        public Seat()
        {
            Tickets = new HashSet<Ticket>();
        }

        public int id { get; set; }

        public int? voyage_id { get; set; }

        public int? type_of_seat { get; set; }

        public int? num_of_seat { get; set; }

        public int? num_of_carriage { get; set; }

        public bool? is_free { get; set; }

        public virtual Seat_types Seat_types { get; set; }

        public virtual Voyage Voyage { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }

        public Seat(int? voyage_id, int? type_of_seat, int? num_of_carriage, int? num_of_seat)
        {
            this.voyage_id = voyage_id ?? throw new ArgumentNullException(nameof(voyage_id));
            this.type_of_seat = type_of_seat ?? throw new ArgumentNullException(nameof(type_of_seat));
            this.num_of_carriage = num_of_carriage ?? throw new ArgumentNullException(nameof(num_of_carriage));
            this.num_of_seat = num_of_seat ?? throw new ArgumentNullException(nameof(num_of_seat));
            this.is_free = true;
        }
    }
}
