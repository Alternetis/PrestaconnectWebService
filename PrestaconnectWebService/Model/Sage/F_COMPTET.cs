using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestaconnectWebService.Model.Sage
{
    public class F_COMPTET
    {
        public static string SearchComboTextByCbmarq(int cbmarq)
        {
            string ComboText =$"";
            using (SqlConnection ConnectionSage = new SqlConnection(Properties.Settings.Default.SAGEConnection))
            {
                ConnectionSage.Open();
                using (SqlCommand commandSage = new SqlCommand(string.Format("SELECT CT_Num, CT_Intitule FROM F_COMPTET WHERE cbMarq ='" +cbmarq +"'"), ConnectionSage))
                {
                    using (SqlDataReader reader = commandSage.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string ct_num = reader.IsDBNull(reader.GetOrdinal("CT_Num")) ? "" : (string)reader["CT_Num"];
                            string ct_intitule = reader.IsDBNull(reader.GetOrdinal("CT_Intitule")) ? "" : (string)reader["CT_Intitule"];
                            ComboText += $"{ct_num} - {ct_intitule}";
                        }
                    }
                }
            }
            return ComboText;
        }
    }
}
