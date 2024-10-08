using PrestaconnectWebService.Core;
using PrestaconnectWebService.Model.Prestaconnect.Class;
using PrestaconnectWebService.Properties;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace PrestaconnectWebService.Model.Prestaconnect
{
    public class ConfigRepository
    {
        private PrestaconnectDB DBLocal = new PrestaconnectDB();

        public void Add(Config Obj)
        {
            DBLocal.Config.Add(Obj);
            Save();
        }

        public void Save()
        {
            DBLocal.SaveChanges();
        }

        public void Delete(Config Obj)
        {
            DBLocal.Config.Remove(Obj);
            Save();
        }

        public bool ExistName(string Name)
        {
            return DBLocal.Config.Any(Obj => Obj.Con_Name.ToUpper() == Name.ToUpper());
        }

        public Config ReadName(string Name)
        {
            return DBLocal.Config.FirstOrDefault(Obj => Obj.Con_Name.ToUpper() == Name.ToUpper());
        }

        public List<Config> List()
        {
            return DBLocal.Config.ToList();
        }

        public string CheckSync(string type)
        {
            using (SqlConnection connection = new SqlConnection(Settings.Default.PrestaconnectConnection))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand($"SELECT TOP 1 Con_Value FROM Config WHERE Con_Name = '" + type + "'", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return (string)reader["Con_Value"];
                        }
                    }
                }
            }
            return "false";
        }
    }
}
