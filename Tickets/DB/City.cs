namespace Tickets.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Citys")]
    public partial class City
    {
        public City()
        {
            Voyages = new HashSet<Voyage>();
            Voyages1 = new HashSet<Voyage>();
        }

        public int id { get; set; }

        [Column("city")]
        [StringLength(50)]
        public string city1 { get; set; }

        public virtual ICollection<Voyage> Voyages { get; set; }

        public virtual ICollection<Voyage> Voyages1 { get; set; }
    }
}
