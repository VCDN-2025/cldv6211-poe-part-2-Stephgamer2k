using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventEaseDB.Models;

public partial class Booking
{
    [Required]
    public string BookingId { get; set; }

    [Required(ErrorMessage = "Event must be selected.")]
    public string? EventId { get; set; }

    [Required(ErrorMessage = "Venue must be selected.")]
    public string? VenueId { get; set; }

    [Required(ErrorMessage = "Booking date is required.")]
    [DataType(DataType.Date)]
    public DateTime BookingDate { get; set; }

    public virtual Event? Event { get; set; }

    public virtual Venue? Venue { get; set; }
    
}
