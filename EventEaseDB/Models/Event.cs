using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventEaseDB.Models;

public partial class Event
{
    [Required]
    public string EventId { get; set; } = null!;

    [Required(ErrorMessage = "Event name is required.")]
    [StringLength(100)]
    public string? EventName { get; set; }

    [Required(ErrorMessage = "Event date is required.")]
    [DataType(DataType.Date)]
    public string? EventDate { get; set; }

    [StringLength(500)]
    public string? Description { get; set; }

    [Required(ErrorMessage = "Venue selection is required.")]
    public string VenueId { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual Venue? Venue { get; set; }
}
