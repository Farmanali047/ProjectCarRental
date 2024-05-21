using System.Data.SqlClient;
using Microsoft.Data.SqlClient;

namespace ProjectCarRental.Models
{
    public class BookingRepository
    {
        public void Add(Bookingform bookingform)
        {
            try { 
            string connection = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=booking;Integrated Security=True";
        SqlConnection connection1 = new SqlConnection(connection);
            connection1.Open();
            string query = "INSERT INTO Bookingform VALUES (@PersonName ,@CarName ,@Cnic ,@PhoneNumber ,@Email ,@CarPlateNumber ,@CarColor ,@PickupLocation ,@PickUpDate ,@ReturnDate ,@CarType ,@SomeInformation )";
         SqlCommand command = new SqlCommand(query, connection1);

                command.Parameters.AddWithValue("@PersonName", bookingform.PersonName);
                command.Parameters.AddWithValue("@CarName", bookingform.CarName);
                command.Parameters.AddWithValue("@Cnic", bookingform.Cnic);
                command.Parameters.AddWithValue("@PhoneNumber", bookingform.PhoneNumber);
                command.Parameters.AddWithValue("@Email", bookingform.Email);
                command.Parameters.AddWithValue("@CarPlateNumber", bookingform.CarPlateNumber);
                command.Parameters.AddWithValue("@CarColor", bookingform.CarColor);
                command.Parameters.AddWithValue("@PickupLocation", bookingform.PickupLocation);
                command.Parameters.AddWithValue("@PickUpDate", bookingform.PickUpDate );
                command.Parameters.AddWithValue("@ReturnDate", bookingform.ReturnDate);
                command.Parameters.AddWithValue("@CarType", bookingform.CarType);
                command.Parameters.AddWithValue("@SomeInformation", bookingform.SomeInformation);
             command.ExecuteNonQuery();
            connection1.Close();
                Console.WriteLine("Successfull");
            }
            catch(Exception ex)
            {
               Console.WriteLine( ex.Message);
               Console.ReadLine();
            }

        }

        public void delete(Bookingform bookingform)
        {
            try
            {
                string connection = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=booking;Integrated Security=True";
                SqlConnection connection1 = new SqlConnection(connection);
                connection1.Open();
                string query = "Delete Bookingform where Cnic =@Cnic";
                SqlCommand command = new SqlCommand(query, connection1);
                command.Parameters.AddWithValue("@Cnic", bookingform.Cnic);
                command.ExecuteNonQuery();
                connection1.Close();
                Console.WriteLine("Successfull");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }

        }

        public List<Bookingform> GetAll()
        {
            string connection = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=booking;Integrated Security=True";
            SqlConnection connection1 = new SqlConnection(connection);
            connection1.Open();
            string query = "Select * from Bookingform";
            SqlCommand command = new SqlCommand(query, connection1);
            SqlDataReader reader = command.ExecuteReader();
            List<Bookingform> bookings = new List<Bookingform>();   
            while (reader.Read())
            {
               bookings.Add(new Bookingform { PersonName = (string)reader[0],CarName= (string)reader[1], Cnic= (string)reader[2], PhoneNumber = (string)reader[3], Email= (string)reader[4] ,
               CarPlateNumber = (string)reader[5], CarColor = (string)reader[6], PickupLocation= (string)reader[7], PickUpDate = (DateTime)reader[8],ReturnDate = (DateTime)reader[9],
                   CarType = (string)reader[10],SomeInformation= (string)reader[11]
               });
            }
            return bookings;
        }
    }
}
