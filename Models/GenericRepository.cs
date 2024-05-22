using Microsoft.Data.SqlClient;
using ProjectCarRental.Models.Interfaces;

namespace ProjectCarRental.Models
{
    public class GenericRepository<TEntity> : IRepository<TEntity>
    {
        private readonly string connectionString;

        public GenericRepository(string c)
        {
            connectionString = c;
        }

        public void Add(TEntity entity)
        {
            //get table anme
            var tablename = typeof(TEntity).Name;

            var properties =
                typeof(TEntity).GetProperties().Where(p => p.Name != "Id" && p.Name!="CarImage" && p.Name !="ImageName");
            var columnNames = string.Join(",", properties.Select(x => x.Name));
            var parameterName =
                string.Join(",", properties.Select(y => "@" + y.Name));

            var query = $"insert into {tablename} ({columnNames}) values({parameterName}) ";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var comm = new SqlCommand(query, connection);
                foreach (var prop in properties)
                {
                    comm.Parameters.AddWithValue("@" + prop.Name, prop.GetValue(entity));
                }
                comm.ExecuteNonQuery();
            }
        }

        public void Update(TEntity entity)
        {
            var tableName = typeof(TEntity).Name;
            var primaryKey = "Id"; 
            //var properties =
            //   typeof(TEntity).GetProperties().Where(p => p.Name != "Id" && p.Name != "CarImage" && p.Name != "ImageName");

            var properties = typeof(TEntity).GetProperties().Where(x => x.Name != primaryKey && x.Name!="CarImage" && x.Name!="ImgUrl");

            var setClause = string.Join(",", properties.Select(a => $"{a.Name}=@{a.Name}"));

            var query = $"update {tableName} set {setClause} where {primaryKey}=@{primaryKey} ";
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var comm = new SqlCommand(query, connection);
                foreach (var prop in properties)
                {
                    comm.Parameters.AddWithValue("@" + prop.Name, prop.GetValue(entity));
                }
                comm.Parameters.AddWithValue("@" + primaryKey, typeof(TEntity).GetProperty(primaryKey).GetValue(entity));
                comm.ExecuteNonQuery();
            }
        }

        public void Delete(TEntity entity)
        {
            var tablename = typeof(TEntity).Name;

            var query = $"delete from {tablename} where {"Id"} = @{"Id"}";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Id",entity.GetType().GetProperty("Id").GetValue(entity)); 
                cmd.ExecuteNonQuery();
            }
        }

        public TEntity? Get(int id)
        {
            var tablename = typeof(TEntity).Name;

            var query = $"select * from {tablename} where id = @id";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", id);
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    var entity = Activator.CreateInstance<TEntity>();
                    foreach (var prop in typeof(TEntity).GetProperties())
                    {
                        prop.SetValue(entity, Convert.ChangeType(reader[prop.Name],prop.PropertyType));
                    }
                    return entity;
                }
            }
            return default;
        }

        public List<TEntity> Get()
        {
            var tablename = typeof(TEntity).Name;

            var query = $"select * from {tablename}";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                var reader = cmd.ExecuteReader();
                var entities = new List<TEntity>();
                while (reader.Read())
                {
                    var entity = Activator.CreateInstance<TEntity>();
                    foreach (var prop in typeof(TEntity).GetProperties())
                    {
                        prop.SetValue(entity, Convert.ChangeType(reader[prop.Name], prop.PropertyType));
                    }
                    entities.Add(entity);
                }
                return entities;
            }
        }
    }
}
