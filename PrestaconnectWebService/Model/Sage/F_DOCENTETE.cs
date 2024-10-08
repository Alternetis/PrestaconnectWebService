using PrestaconnectWebService.Properties;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestaconnectWebService.Model.Sage
{
    public class F_DOCENTETE
    {
        public static string ExistPieceWeb(string DO_NoWeb)
        {

            using (SqlConnection connection = new SqlConnection(Settings.Default.SAGEConnection))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand($"SELECT DO_Piece FROM F_DOCENTETE WHERE DO_NoWeb = '{DO_NoWeb}'", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return reader.GetString(0);
                        }
                    }
                }
            }

            return "";
        }
    }
}
