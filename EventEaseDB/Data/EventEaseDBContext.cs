using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EventEaseDB.Models;

    public class EventEaseDBContext : DbContext
    {
        public EventEaseDBContext (DbContextOptions<EventEaseDBContext> options)
            : base(options)
        {
        }

        public DbSet<EventEaseDB.Models.Venue> Venue { get; set; } = default!;

public DbSet<EventEaseDB.Models.Event> Event { get; set; } = default!;

public DbSet<EventEaseDB.Models.Booking> Booking { get; set; } = default!;
    }
