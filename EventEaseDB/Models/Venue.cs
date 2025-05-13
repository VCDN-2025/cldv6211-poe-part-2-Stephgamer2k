using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventEaseDB.Models;

public partial class Venue
{
    [Required]
    public string VenueId { get; set; } = null!;

    [Required(ErrorMessage = "Venue name is required.")]
    [StringLength(100)]
    public string? VenueName { get; set; }

    [Required(ErrorMessage = "Location is required.")]
    [StringLength(200)]
    public string? Location { get; set; }

    [Required(ErrorMessage ="Capacity is required.")]
    [Range(1,10000,ErrorMessage = "Capacity must be a number between 1 and 10000.")]
    public string? Capacity { get; set; }

    public string? ImageUrl { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();
}
