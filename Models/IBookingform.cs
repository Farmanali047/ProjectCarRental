namespace ProjectCarRental.Models
{
    public interface IBookingform
    {
        public void Add(Bookingform bookingform);

        public void UpdateStatus(int id);

        public List<Bookingform> GetAll1(string email);

        public List<Bookingform> GetAll();

        public List<Bookingform> GetByName(string name);
    }
}
