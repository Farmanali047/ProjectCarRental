namespace ProjectCarRental.Models.Interfaces
{
    public interface ICarRegisteration
    {

        public void Updatestatus(string name);

        public List<CarRegisteration> GetAll();

        public List<CarRegisteration> GetAll1();

        public List<CarRegisteration> Available();

        public List<CarRegisteration> Booked();
    }
}
