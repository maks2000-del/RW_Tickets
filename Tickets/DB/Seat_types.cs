namespace Tickets.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Seat_types
    {
        public Seat_types()
        {
            Seats = new HashSet<Seat>();
        }

        public int id { get; set; }

        [StringLength(50)]
        public string seat_type { get; set; }

        public virtual ICollection<Seat> Seats { get; set; }
    }
}