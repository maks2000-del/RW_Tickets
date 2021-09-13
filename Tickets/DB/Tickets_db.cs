using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Tickets.DB
{
    public partial class Tickets_db : DbContext
    {
        public Tickets_db()
            : base("name=Tickets_db")
        {
        }

        public virtual DbSet<City> Citys { get; set; }
        public virtual DbSet<Seat_types> Seat_types { get; set; }
        public virtual DbSet<Seat> Seats { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Voyage> Voyages { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>()
                .HasMany(e => e.Voyages)
                .WithOptional(e => e.City)
                .HasForeignKey(e => e.id_city___of_departure);

            modelBuilder.Entity<City>()
                .HasMany(e => e.Voyages1)
                .WithOptional(e => e.City1)
                .HasForeignKey(e => e.id_city___of_arrival);

            modelBuilder.Entity<Seat_types>()
                .HasMany(e => e.Seats)
                .WithOptional(e => e.Seat_types)
                .HasForeignKey(e => e.type_of_seat);

            modelBuilder.Entity<Seat>()
                .HasMany(e => e.Tickets)
                .WithOptional(e => e.Seat)
                .HasForeignKey(e => e.seat_id);

            modelBuilder.Entity<User>()
                .Property(e => e.telNumber)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.passport_id)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.sex)
                .IsFixedLength();

            modelBuilder.Entity<User>()
                .HasMany(e => e.Tickets)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.client_id);

            modelBuilder.Entity<Voyage>()
                .Property(e => e.cost)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Voyage>()
                .HasMany(e => e.Seats)
                .WithOptional(e => e.Voyage)
                .HasForeignKey(e => e.voyage_id);

            modelBuilder.Entity<Voyage>()
                .HasMany(e => e.Tickets)
                .WithOptional(e => e.Voyage)
                .HasForeignKey(e => e.voyage_id);
        }
    }
}
