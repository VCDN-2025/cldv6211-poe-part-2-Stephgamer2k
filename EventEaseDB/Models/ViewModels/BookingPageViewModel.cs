namespace EventEaseDB.Models.ViewModels
{
    public class BookingPageViewModel
    {
        public IEnumerable<Booking> RawBookings { get; set; } = new List<Booking>();
        public IEnumerable<BookingDisplay> DisplayBookings { get; set; } = new List<BookingDisplay>();
    }
}
