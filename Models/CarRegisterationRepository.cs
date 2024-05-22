using Microsoft.Data.SqlClient;

namespace ProjectCarRental.Models
{
    public class CarRegisterationRepository
    {
        public void  Add(CarRegisteration cr) 
        {
            //try
            //{
            //    string connection = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=booking;Integrated Security=True;";
            //    SqlConnection con = new SqlConnection(connection);
            //    con.Open();
            //    string query = "Insert Into CarRegisteration values(@CarName ,@CarColor ,@CarModel ,@RegisterationNumber ,@Rental,@ImgUrl)";
            //    SqlCommand cmd = new SqlCommand(query, con);
            //    cmd.Parameters.AddWithValue("@CarName", cr.CarName);
            //    cmd.Parameters.AddWithValue("@CarColor", cr.CarColor);
            //    cmd.Parameters.AddWithValue("@CarModel", cr.CarModel);
            //    cmd.Parameters.AddWithValue("@Rental", cr.Rental);
            //    //cmd.Parameters.AddWithValue("@ImgName", cr.ImageName);
            //    cmd.Parameters.AddWithValue("@ImgUrl", cr.ImgUrl);
            //    //cmd.Parameters.AddWithValue("@CarIamge", cr.CarImage);
            //    cmd.Parameters.AddWithValue("@RegisterationNumber", cr.RegisterationNumber);
            //    cmd.ExecuteNonQuery();
            //    con.Close();
            //    Console.WriteLine("Succesfull");
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}

        }
        public void Update(CarRegisteration cr)
        {
        //    try
        //    {
        //        string connection = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=booking;Integrated Security=True;";
        //        SqlConnection con = new SqlConnection(connection);
        //        con.Open();
        //        string updateQuery = "UPDATE CarRegisteration SET Rental =@Rental,CarName =@CarName,CarColor= @CarColor,CarModel= @CarModel,RegisterationNumber= @RegisterationNumber WHERE Id= @Id";
        //        SqlCommand updateCmd = new SqlCommand(updateQuery, con);
        //        updateCmd.Parameters.AddWithValue("@Rental", cr.Rental);
        //        updateCmd.Parameters.AddWithValue("@RegisterationNumber", cr.RegisterationNumber);
        //        updateCmd.Parameters.AddWithValue("@CarName", cr.CarName);
        //        updateCmd.Parameters.AddWithValue("@CarColor",cr.CarColor);
        //        updateCmd.Parameters.AddWithValue("@CarModel", cr.CarModel);
        //        updateCmd.Parameters.AddWithValue("@RegisterationNumber", cr.RegisterationNumber);
        //        updateCmd.ExecuteNonQuery();

        //        con.Close();
        //        Console.WriteLine("Succesfull");
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }

        }
        public void Delete(CarRegisteration cr)
        {
            //try
            //{
            //    string connection = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=booking;Integrated Security=True;";
            //    SqlConnection con = new SqlConnection(connection);
            //    con.Open();
            //    string updateQuery = "Delete CarRegisteration  WHERE Id =@Id";
            //    SqlCommand updateCmd = new SqlCommand(updateQuery, con);
            //    updateCmd.Parameters.AddWithValue("@Id", cr.Id);
            //    updateCmd.ExecuteNonQuery();
            //    con.Close();
            //    Console.WriteLine("Succesfull");
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}

        }
        public List<CarRegisteration> GetAll()
        {
            string connection = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=booking;Integrated Security=True";
            SqlConnection connection1 = new SqlConnection(connection);
            connection1.Open();
            string query = "Select * from CarRegisteration ";
            SqlCommand command = new SqlCommand(query, connection1);
            SqlDataReader reader = command.ExecuteReader();
            List<CarRegisteration> CarReg = new List<CarRegisteration>();
            while (reader.Read())
            {

                CarReg.Add(new CarRegisteration
                {
                    Id = (int)reader[6],
                    CarName = (string)reader[0],
                    CarColor = (string)reader[1],
                    CarModel = (string)reader[2],
                    RegisterationNumber = (int)reader[3],
                    Rental = (int)reader[4],
                    ImgUrl = (string)reader[5] 
                    

                });
            }
            return CarReg;
        }


    }
}
