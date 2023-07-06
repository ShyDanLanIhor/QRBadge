using AdminProgram.Models;
using AdminProgram.ViewModels;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace AdminProgram.Repositories
{
    public class AdminRepository : RepositoryBase, IAdminRepository
    {
        public static bool is_SuperGlobal;
        public void Add(AdminModel adminModel)
        {
            throw new NotImplementedException();
        }

        public bool AuthenticateAdmin(NetworkCredential credential)
        {
            bool validAdmin = true, is_Super = false;
            try
            {
                using (var connection = GetConnection())
                using (var command = new MySql.Data.MySqlClient.MySqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "SELECT * FROM Admins WHERE Login=@login and Password=@password";
                    MySqlParameter loginParam = new MySqlParameter("@login", MySqlDbType.VarChar);
                    loginParam.Value = credential.UserName;
                    command.Parameters.Add(loginParam);
                    MySqlParameter passwordParam = new MySqlParameter("@password", MySqlDbType.VarChar);
                    passwordParam.Value = credential.Password;
                    command.Parameters.Add(passwordParam);
                    validAdmin = command.ExecuteScalar() == null ? false : true;
                    command.CommandText = "SELECT IS_SUPER FROM Admins WHERE Login=@login and Password=@password";
                    is_Super = Convert.ToBoolean(command.ExecuteScalar());
                    connection.Close();
                }
                is_SuperGlobal = is_Super;
            }
            catch (Exception)
            {
                validAdmin = false;
                System.Windows.Forms.MessageBox.Show("Немає підключення до бази даних!", "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return validAdmin;
        }

        public void Edit(AdminModel adminModel)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AdminModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public AdminModel GetByAdminname(string adminname)
        {
            throw new NotImplementedException();
        }

        public AdminModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(AdminModel adminModel)
        {
            throw new NotImplementedException();
        }
    }
}
