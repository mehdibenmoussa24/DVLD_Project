using Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD.DataAccess
{
    public class UserData
    {
        public static bool GetUserInfoByUserID(int userID,ref int personID,ref string userName, ref string password,ref bool isActive)
        {

            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                string query = "SELECT * FROM Users WHERE UserID=@userID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@userID", SqlDbType.Int).Value = userID;


                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                personID = (int)reader["PersonID"];
                                userName = (string)reader["UserName"];
                                password = (string)reader["Password"];
                                isActive = (bool)reader["IsActive"];
                                return true;
                            }
                            else { return false; }
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.LogError($"Error: {ex.Message}");
                    }

                }
            }
            return false;
        }

        public static bool GetUserInfoByPersonID(int personID, ref int userID, ref string userName, ref string password, ref bool isActive)
        {

            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                string query = "SELECT * FROM Users WHERE PersonID=@personID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@personID", SqlDbType.Int).Value = personID;


                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                userID = (int)reader["UserID"];
                                userName = (string)reader["UserName"];
                                password = (string)reader["Password"];
                                isActive = (bool)reader["IsActive"];
                                return true;
                            }
                            else { return false; }
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.LogError($"Error: {ex.Message}");
                    }

                }
            }
            return false;
        }

        public static bool GetUserInfoByUserNameAndPassword(string userName, string password, ref int userID, ref int personID, ref bool isActive)
        {

            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                string query = "SELECT * FROM Users WHERE UserName=@userName and Password = @password";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@userName", SqlDbType.NVarChar,20).Value = userName;
                    command.Parameters.Add("@password", SqlDbType.NVarChar,20).Value = password;


                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                userID = (int)reader["UserID"];
                                personID = (int)reader["PersonID"];
                                isActive = (bool)reader["IsActive"];
                                return true;
                            }
                            else { return false; }
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.LogError($"Error: {ex.Message}");
                    }

                }
            }
            return false;
        }

        public static int AddNewUser(int personID, string userName,
             string password, bool isActive)
        {
            //this function will return the new user id if succeeded and -1 if not.
            int UserID = -1;

            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                string query = @"INSERT INTO Users (PersonID,UserName,Password,IsActive)
                             VALUES (@personID, @userName,@password,@isActive);
                             SELECT SCOPE_IDENTITY();";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@personID", SqlDbType.Int).Value = personID;
                    command.Parameters.Add("@userName", SqlDbType.NVarChar, 20).Value = userName;
                    command.Parameters.Add("@password", SqlDbType.NVarChar, 20).Value = password;
                    command.Parameters.Add("@isActive", SqlDbType.Bit).Value = isActive;

                    try
                    {
                        connection.Open();

                        object result = command.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int insertedID))
                        {
                            UserID = insertedID;
                        }
                    }

                    catch (Exception ex)
                    {
                        Logger.LogError("Error: " + ex.Message);
                    }
                }
            }

            return UserID;
        }


        public static bool UpdateUser(int userID, int personID, string userName,
             string password, bool isActive)
        {

            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                 string query = @"Update  Users  
                            set PersonID = @personID,
                                UserName = @userName,
                                Password = @password,
                                IsActive = @isActive
                                where UserID = @userID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@personID",SqlDbType.Int).Value = personID;
                    command.Parameters.Add("@userName", SqlDbType.NVarChar,20).Value = userName;
                    command.Parameters.Add("@password", SqlDbType.NVarChar,20).Value = password;
                    command.Parameters.Add("@isActive", SqlDbType.Bit).Value = isActive;
                    command.Parameters.Add("@userID", SqlDbType.Int).Value = userID;


                    try
                    {
                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();

                    }
                    catch (Exception ex)
                    {
                        Logger.LogError("Error: " + ex.Message);
                        return false;
                    }

                }
            }
                return (rowsAffected > 0);
        }
        public static DataTable GetAllUsers()
        {

            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {

                string query = @"SELECT  Users.UserID, Users.PersonID,
                            FullName = People.FirstName + ' ' + People.SecondName + ' ' + ISNULL( People.ThirdName,'') +' ' + People.LastName,
                             Users.UserName, Users.IsActive
                             FROM  Users INNER JOIN
                                    People ON Users.PersonID = People.PersonID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    try
                    {
                        connection.Open();

                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.HasRows)

                        {
                            dt.Load(reader);
                        }

                        reader.Close();


                    }

                    catch (Exception ex)
                    {
                       Logger.LogError("Error: " + ex.Message);
                    }
               
                }
            }
            return dt;
        }

        public static bool DeleteUser(int userID)
        {

            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                string query = @"Delete Users 
                                where UserID = @userID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@userID", SqlDbType.Int).Value = userID;
                    try
                    {
                        connection.Open();

                        rowsAffected = command.ExecuteNonQuery();

                    }
                    catch (Exception ex)
                    {
                        Logger.LogError("Error: " + ex.Message);
                    }
                }
            }
            return (rowsAffected > 0);
        }

        public static bool IsUserExist(int userID)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                string query = "SELECT Found=1 FROM Users WHERE UserID = @userID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@userID", SqlDbType.Int).Value = userID;

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            isFound = reader.HasRows;
                        }
                        return isFound;
                    }
                    catch (Exception ex)
                    {
                        Logger.LogError("Error: " + ex.Message);
                    }
                }
            }
            return isFound;
        }

        public static bool IsUserExist(string userName)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                string query = "SELECT Found=1 FROM Users WHERE UserName = @userName";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@userName", SqlDbType.NVarChar,20).Value = userName;

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            isFound = reader.HasRows;
                        }
                        return isFound;
                    }
                    catch (Exception ex)
                    {
                        Logger.LogError("Error: " + ex.Message);
                    }
                }
            }
            return isFound;
        }

        public static bool IsUserExistForPersonID(int personID)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                string query = "SELECT Found=1 FROM Users WHERE PersonID = @personID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@personID", SqlDbType.Int).Value = personID;
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            isFound = reader.HasRows;
                        }
                            return true;
                    }
                    catch (Exception ex)
                    {
                        Logger.LogError("Error: " + ex.Message);
                    }
                }
            }
            return false;
        }
        public static bool ChangePassword(int userID, string newPassword)
        {

            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                string query = @"Update  Users  
                            set Password = @newPassword
                            where UserID = @userID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@userID", SqlDbType.Int).Value = userID;
                    command.Parameters.Add("@newPassword", SqlDbType.NVarChar, 20).Value = newPassword;

                    try
                    {
                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();

                    }
                    catch (Exception ex)
                    {
                        Logger.LogError("Error: " + ex.Message);
                        return false;
                    }
                }
            }
            return (rowsAffected > 0);
        }



    }
}
