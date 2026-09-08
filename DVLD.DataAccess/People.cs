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



    }
}
