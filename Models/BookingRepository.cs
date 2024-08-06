using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using Dapper;
namespace ProjectCarRental.Models
{
    public class BookingRepository : IBookingform
    {
        public void Add(Bookingform bookingform)
        {
            try { 
            string connection = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=booking;Integrated Security=True";
        SqlConnection connection1 = new SqlConnection(connection);
            connection1.Open();
            string query = "INSERT INTO Bookingform VALUES (@PersonName ,@CarName ,@Cnic ,@PhoneNumber ,@Email ,@CarPlateNumber ,@CarColor ,@PickupLocation ,@PickUpDate ,@ReturnDate ,@CarType ,@SomeInformation ,@Quantity)";
         SqlCommand command = new SqlCommand(query, connection1);
                connection1.Execute(query, bookingform);

                connection1.Close();
                Console.WriteLine("Successfull");
            }
            catch(Exception ex)
            {
               Console.WriteLine( ex.Message);
               Console.ReadLine();
            }

        }

        public void UpdateStatus(int id )
        {
            try
            {
                string connection = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=booking;Integrated Security=True";
                SqlConnection connection1 = new SqlConnection(connection);
                connection1.Open();
                string query = "Update Bookingform set Status = Accept where Id =@id";
                SqlCommand cmd = new SqlCommand(query, connection1);
                cmd.Parameters.AddWithValue("id", id);
                connection1.Close();
                Console.WriteLine("Successfull");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }

        }

        public List<Bookingform> GetAll1( string email)
        {
            string connection = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=booking;Integrated Security=True";
            SqlConnection connection1 = new SqlConnection(connection);
            connection1.Open();
            string query = $"Select * from Bookingform where Email =@d";
            SqlCommand command = new SqlCommand(query, connection1);

            return connection1.Query<Bookingform>(query,new {d=email}).ToList();
            //SqlDataReader reader = command.ExecuteReader();
            //List<Bookingform> bookings = new List<Bookingform>();   
            //while (reader.Read())
            //{
            //   bookings.Add(new Bookingform { PersonName = (string)reader[0],CarName= (string)reader[1], Cnic= (string)reader[2], PhoneNumber = (string)reader[3], Email= (string)reader[4] ,
            //   CarPlateNumber = (string)reader[5], CarColor = (string)reader[6], PickupLocation= (string)reader[7], PickUpDate = (DateTime)reader[8],ReturnDate = (DateTime)reader[9],
            //       CarType = (string)reader[10],SomeInformation= (string)reader[11]
            //   });
            //}
            //return bookings;
        }
        public List<Bookingform> GetAll()
        {
            string connection = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=booking;Integrated Security=True";
            SqlConnection connection1 = new SqlConnection(connection);
            connection1.Open();
            string query = "Select * from Bookingform";
            SqlCommand command = new SqlCommand(query, connection1);

            return connection1.Query<Bookingform>(query).ToList();
            
        }
        public List<Bookingform> GetByName(string name)
        {
            string connection = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=booking;Integrated Security=True";
            SqlConnection connection1 = new SqlConnection(connection);
            connection1.Open();
            string query = "Select * from Bookingform where PersonName =@name";
            SqlCommand command = new SqlCommand(query, connection1);

            return connection1.Query<Bookingform>(query,new {name}).ToList();

        }
    }
}
