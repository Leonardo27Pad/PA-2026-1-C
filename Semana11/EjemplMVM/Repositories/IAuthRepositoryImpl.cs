using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Text;

namespace EjemplMVM.Repositories
{
    public class AuthRepositoryImpl : IAuthRepository
    {
        string cadena = string.Empty;
        public AuthRepositoryImpl()
        {
            cadena = ConfigurationManager.ConnectionStrings["EjemplMVM.Properties.Settings.NorthwindDB"].ConnectionString;
        }
        public bool ValidarUsuario(string username, string password)
        {
            using (SqlConnection con = new SqlConnection(cadena))
            {
                con.Open();
                string query = "select count(1) from Employees WHERE LastName=@usuario AND Extension=@password";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.Add("@usuario", SqlDbType.NVarChar, 20).Value = username;
                    cmd.Parameters.Add("@password", SqlDbType.NVarChar, 20).Value = password;
                    int cantidad = Convert.ToInt32(cmd.ExecuteScalar());
                    return cantidad > 0;
                }
            }
        }
    }
}