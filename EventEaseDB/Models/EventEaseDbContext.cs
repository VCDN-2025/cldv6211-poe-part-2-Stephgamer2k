using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EventEaseDB.Models;

public partial class EventEaseDbContext : DbContext
{
    public EventEaseDbContext()
    {
    }

    public EventEaseDbContext(DbContextOptions<EventEaseDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<Venue> Venues { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=STEPHSLAPTOP\\SQLEXPRESS01; Database=EventEaseDB; Trusted_Connection=True; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PK__Booking__73951ACD3B0176EC");

            entity.ToTable("Booking");

            entity.Property(e => e.BookingId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("BookingID");
            entity.Property(e => e.BookingDate)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.EventId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("EventID");
            entity.Property(e => e.VenueId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("VenueID");

            entity.HasOne(d => d.Event).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.EventId)
                .HasConstraintName("FK__Booking__EventID__29572725");

            entity.HasOne(d => d.Venue).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.VenueId)
                .HasConstraintName("FK__Booking__VenueID__2A4B4B5E");
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.EventId).HasName("PK__Event__7944C8707C679152");

            entity.ToTable("Event");

            entity.Property(e => e.EventId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("EventID");
            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.EventDate)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.EventName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.VenueId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("VenueID");

            entity.HasOne(d => d.Venue).WithMany(p => p.Events)
                .HasForeignKey(d => d.VenueId)
                .HasConstraintName("FK__Event__VenueID__267ABA7A");
        });

        modelBuilder.Entity<Venue>(entity =>
        {
            entity.HasKey(e => e.VenueId).HasName("PK__Venue__3C57E5D20C2EFD23");

            entity.ToTable("Venue");

            entity.Property(e => e.VenueId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("VenueID");
            entity.Property(e => e.Capacity)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Location)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.VenueName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
