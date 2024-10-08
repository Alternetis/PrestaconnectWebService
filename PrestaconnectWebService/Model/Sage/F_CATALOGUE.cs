using PrestaconnectWebService.Core;
using PrestaconnectWebService.Model.Prestaconnect.Class;
using PrestaconnectWebService.Properties;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestaconnectWebService.Model.Sage
{
    public class F_CATALOGUE 
    {
     
        public string Intitule { get; set; }
        public string AffichageName { get; set; }
        public int ClNo { get;set; }
        public int ClNoParent { get; set; }
        public int CbMarq { get; set; }
        public List<F_CATALOGUE> Childrens { get; set; } = new List<F_CATALOGUE>();




        F_CATALOGUE() { }
        F_CATALOGUE(SqlDataReader reader, string ParentAffichageName = "")
        {
            this.Intitule = reader.IsDBNull(reader.GetOrdinal("CL_Intitule")) ? "" : (string)reader["CL_Intitule"];
            this.ClNo = reader.IsDBNull(reader.GetOrdinal("CL_No")) ? 0 : (int)reader["CL_No"];
            this.ClNoParent = reader.IsDBNull(reader.GetOrdinal("cl_NoParent")) ? 0 : (int)reader["Cl_NoParent"];
            this.CbMarq = reader.IsDBNull(reader.GetOrdinal("cbmarq")) ? 0 : (int)reader["cbmarq"];
            this.AffichageName = string.IsNullOrEmpty(ParentAffichageName) ? this.Intitule : ParentAffichageName + " > " + Intitule;
            this.Childrens = GetChildrenCatalogue(ClNo,AffichageName);
        }
        public static List<F_CATALOGUE> GetCatalogue()
        {
            List<F_CATALOGUE> catalogue = new List<F_CATALOGUE>();

            using (SqlConnection connection = new SqlConnection(Settings.Default.SAGEConnection))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand($"SELECT cbmarq,CL_No,CL_Intitule,cl_NoParent FROM F_CATALOGUE WHERE CL_Niveau = 0", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            catalogue.Add(new F_CATALOGUE(reader));
                        }
                    }
                }
            }

            return catalogue;
        }

        private  List<F_CATALOGUE> GetChildrenCatalogue(int clNoParent,string ParrentAffichagename)
        {
            List<F_CATALOGUE> catalogue = new List<F_CATALOGUE>();

            using (SqlConnection connection = new SqlConnection(Settings.Default.SAGEConnection))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand($"SELECT cbmarq,CL_No,CL_Intitule,cl_NoParent FROM F_CATALOGUE WHERE CL_NoParent = {clNoParent}", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            catalogue.Add(new F_CATALOGUE(reader, ParrentAffichagename));
                        }
                    }
                }
            }

            return catalogue;
        }

        public static F_CATALOGUE GetCatalogue(long SageId)
        {
            using (SqlConnection connection = new SqlConnection(Settings.Default.SAGEConnection))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand($"SELECT cbmarq,CL_No,CL_Intitule,cl_NoParent FROM F_CATALOGUE WHERE cbmarq = {SageId}", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                           return (new F_CATALOGUE(reader));
                        }
                    }
                }
            }
            return null;
        }


    }
}
