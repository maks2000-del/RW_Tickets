namespace Tickets.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Voyage
    {
        public Voyage()
        {
            Seats = new HashSet<Seat>();
            Tickets = new HashSet<Ticket>();
        }

        public int id { get; set; }

        [StringLength(50)]
        public string name { get; set; }

        [Column("id_city_​​of_departure")]
        public int? id_city___of_departure { get; set; }

        [StringLength(20)]
        public string date_of_departure { get; set; }

        [StringLength(8)]
        public string time_of_departure { get; set; }

        [Column("id_city_​​of_arrival")]
        public int? id_city___of_arrival { get; set; }

        [StringLength(20)]
        public string date_of_arrival { get; set; }

        [StringLength(8)]
        public string time_of_arrival { get; set; }

        [Column(TypeName = "money")]
        public decimal? cost { get; set; }

        public virtual City City { get; set; }

        public virtual City City1 { get; set; }

        public virtual ICollection<Seat> Seats { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }


        public Voyage(string name, int? id_out, string date_out, string time_out, int? id_in, string date_in, string time_in, decimal cost)
        {
            this.name = name ?? throw new ArgumentNullException(nameof(name));
            this.id_city___of_departure = id_out ?? throw new ArgumentNullException(nameof(id_out));
            this.id_city___of_arrival = id_in ?? throw new ArgumentNullException(nameof(id_in));
            this.date_of_departure = date_out ?? throw new ArgumentNullException(nameof(date_out));
            this.date_of_arrival = date_in ?? throw new ArgumentNullException(nameof(date_in));
            this.time_of_departure = time_out ?? throw new ArgumentNullException(nameof(time_out));
            this.time_of_arrival = time_in ?? throw new ArgumentNullException(nameof(time_in));
            this.cost = cost;
        }
    }
}
