using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shkorpilovtsi.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shkorpilovtsi.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>(entity => entity.Property(x => x.Name).HasMaxLength(255));
            builder.Entity<IdentityRole>(entity => entity.Property(x => x.NormalizedName).HasMaxLength(255));
            builder.Entity<IdentityRole>(entity => entity.Property(x => x.Id).HasMaxLength(255));
            builder.Entity<IdentityUser>(entity => entity.Property(x => x.Id).HasMaxLength(255));
            base.OnModelCreating(builder);
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Bed> Beds { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<Bungalow> Bungalows { get; set; }
        public virtual DbSet<BedsInRoom> BedsInRooms { get; set; }
        public virtual DbSet<RoomsInBungalow> RoomsInBungalows { get; set; }
        public virtual DbSet<UserCategory> UserCategories {  get; set; }
        public virtual DbSet<Shift> Shifts { get; set; }
        public virtual DbSet<Price> Prices { get; set; }
        public virtual DbSet<SpecialFee> SpecialFees { get; set; }
        public virtual DbSet<Reservation> Reservations { get; set; }
        public virtual DbSet<ReservationDetail> ReservationDetails { get; set; }
    }
}
