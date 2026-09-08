using Common;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;



namespace DVLD.DataAccess
{
    public class Person
    {
       public static bool GetPersonInfoByID(int PersonID, ref string FirstName,ref string SecondName,ref string ThirdName, ref string LastName,
           ref string NationalNo, ref DateTime DateOfBirth, ref int Gender, ref string Address, ref string Phone,ref string Email,
           ref int NationalityCountryID,ref string ImagePath)
        {
            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                string query = "SELECT * FROM People WHERE PersonID = @PersonID";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.Add("@PersonID", SqlDbType.Int).Value = PersonID;

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {


                        if (reader.Read())
                        {
                            // The record was found
                            

                            FirstName = (string)reader["FirstName"];
                            SecondName = (string)reader["SecondName"];

                            //ThirdName: allows null in database so we should handle null
                            if (reader["ThirdName"] != DBNull.Value)
                            {
                                ThirdName = (string)reader["ThirdName"];
                            }
                            else
                            {
                                ThirdName = "";
                            }

                            LastName = (string)reader["LastName"];
                            NationalNo = (string)reader["NationalNo"];
                            DateOfBirth = (DateTime)reader["DateOfBirth"];
                            Gender = (byte)reader["Gendor"];
                            Address = (string)reader["Address"];
                            Phone = (string)reader["Phone"];


                            //Email: allows null in database so we should handle null
                            if (reader["Email"] != DBNull.Value)
                            {
                                Email = (string)reader["Email"];
                            }
                            else
                            {
                                Email = "";
                            }

                            NationalityCountryID = (int)reader["NationalityCountryID"];

                            //ImagePath: allows null in database so we should handle null
                            if (reader["ImagePath"] != DBNull.Value)
                            {
                                ImagePath = (string)reader["ImagePath"];
                            }
                            else
                            {
                                ImagePath = "";
                            }
                            return true;
                        }
                   

                    }
                }
                catch (Exception ex)
                {
                    Logger.LogError($"Error: {ex}");
                    return false;
                }
             

            }
                return false;

        }

        public static bool GetPersonInfoByNationalNo(string NationalNo, ref int PersonID, ref string FirstName, ref string SecondName,
       ref string ThirdName, ref string LastName, ref DateTime DateOfBirth,
        ref short Gender, ref string Address, ref string Phone, ref string Email,
        ref int NationalityCountryID, ref string ImagePath)
        {

            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                string query = "SELECT * FROM People WHERE NationalNo = @NationalNo";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.Add("@NationalNo", SqlDbType.NVarChar,20).Value = NationalNo;

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // The record was found


                            PersonID = (int)reader["PersonID"];
                            FirstName = (string)reader["FirstName"];
                            SecondName = (string)reader["SecondName"];

                            //ThirdName: allows null in database so we should handle null
                            if (reader["ThirdName"] != DBNull.Value)
                            {
                                ThirdName = (string)reader["ThirdName"];
                            }
                            else
                            {
                                ThirdName = "";
                            }

                            LastName = (string)reader["LastName"];
                            DateOfBirth = (DateTime)reader["DateOfBirth"];
                            Gender = (byte)reader["Gender"];
                            Address = (string)reader["Address"];
                            Phone = (string)reader["Phone"];

                            //Email: allows null in database so we should handle null
                            if (reader["Email"] != DBNull.Value)
                            {
                                Email = (string)reader["Email"];
                            }
                            else
                            {
                                Email = "";
                            }

                            NationalityCountryID = (int)reader["NationalityCountryID"];

                            //ImagePath: allows null in database so we should handle null
                            if (reader["ImagePath"] != DBNull.Value)
                            {
                                ImagePath = (string)reader["ImagePath"];
                            }
                            else
                            {
                                ImagePath = "";
                            }
                            return true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogError("Error: " + ex.Message);
                    return false;
                }
            }
            return false;
        }
        public static int? AddNewPerson(string FirstName, string SecondName,
           string ThirdName, string LastName, string NationalNo, DateTime DateOfBirth,
           short Gender, string Address, string Phone, string Email,
            int NationalityCountryID, string ImagePath)
        {
            //this function will return the new person id if succeeded and null if not.
            int? PersonID = null;

            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                string query = @"INSERT INTO People (FirstName, SecondName, ThirdName,LastName,NationalNo,
                                                       DateOfBirth,Gender,Address,Phone, Email, NationalityCountryID,ImagePath)
                                 VALUES (@FirstName, @SecondName,@ThirdName, @LastName, @NationalNo,
                                         @DateOfBirth,@Gender,@Address,@Phone, @Email,@NationalityCountryID,@ImagePath);
                             SELECT SCOPE_IDENTITY();";

                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    command.Parameters.Add("@FirstName", SqlDbType.NVarChar,20).Value = FirstName;
                    command.Parameters.Add("@SecondName", SqlDbType.NVarChar, 20).Value = SecondName;

                    if (ThirdName != "" && ThirdName != null)
                        command.Parameters.Add("@ThirdName", SqlDbType.NVarChar, 20).Value = ThirdName;
                    else
                        command.Parameters.Add("@ThirdName", SqlDbType.NVarChar, 20).Value = System.DBNull.Value;

                    command.Parameters.Add("@LastName", SqlDbType.NVarChar, 20).Value = LastName;
                    command.Parameters.Add("@NationalNo", SqlDbType.NVarChar, 20).Value = NationalNo;
                    command.Parameters.Add("@DateOfBirth", SqlDbType.DateTime).Value = DateOfBirth;
                    command.Parameters.Add("@Gender", SqlDbType.TinyInt).Value = Gender;
                    command.Parameters.Add("@Address", SqlDbType.NVarChar, 500).Value = Address;
                    command.Parameters.Add("@Phone", SqlDbType.NVarChar, 20).Value = Phone;

                    if (Email != "" && Email != null)
                        command.Parameters.Add("@Email", SqlDbType.NVarChar, 50).Value = Email;
                    else
                        command.Parameters.Add("@Email", SqlDbType.NVarChar, 50).Value = System.DBNull.Value;

                    command.Parameters.Add("@NationalityCountryID", SqlDbType.Int).Value = NationalityCountryID;

                    if (ImagePath != "" && ImagePath != null)
                        command.Parameters.Add("@ImagePath", SqlDbType.NVarChar, 250).Value = ImagePath;
                    else
                        command.Parameters.Add("@ImagePath", SqlDbType.NVarChar, 250).Value = System.DBNull.Value;

                    try
                    {
                        connection.Open();

                        object result = command.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int insertedID))
                        {
                            PersonID = insertedID;
                        }
                    }

                    catch (Exception ex)
                    {
                        Logger.LogError($"Error: {ex}");

                    }

                }
            }
            return PersonID;
        }

        

    }
}
