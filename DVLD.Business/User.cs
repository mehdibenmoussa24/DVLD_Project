using DVLD.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace DVLD.Business
{
    public class User
    {
        private enum enMode { AddNew = 0, Update = 1 }
        private enMode _Mode = enMode.AddNew;

        public int UserID { get; set; }
        public int PersonID { get; set; }

        public Person PersonInfo { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }

        public User()
        {
            UserID = -1;
            PersonID = -1;
            PersonInfo = new Person();
            UserName = string.Empty;
            Password = string.Empty;
            IsActive = false;
            _Mode = enMode.AddNew;
        }

        public User(int userID, int personID, string userName, string password, bool isActive)
        {
            UserID = userID;
            PersonID = personID;
            PersonInfo = Person.Find(personID);
            UserName = userName;
            Password = password;
            IsActive = isActive;
            _Mode = enMode.Update;
        }

        private bool _AddNew()
        {
            //call DataAccess Layer 
            this.UserID = UserData.AddNewUser(this.PersonID, this.UserName,this.Password,this.IsActive);

            return (this.UserID != -1);
        }

        private bool _Update()
        {
            //call DataAccess Layer 
            return UserData.UpdateUser(this.UserID,this.PersonID,this.UserName,this.Password,this.IsActive);
        }
        public static User FindByUserID(int UserID)
        {
            int PersonID = -1;
            string UserName = "", Password = "";
            bool IsActive = false;

            bool IsFound = UserData.GetUserInfoByUserID
                                (UserID, ref PersonID, ref UserName, ref Password, ref IsActive);

            if (IsFound)
                //we return new object of that User with the right data
                return new User(UserID, PersonID, UserName, Password, IsActive);
            else
                return null;
        }
        public static User FindByPersonID(int PersonID)
        {
            int UserID = -1;
            string UserName = "", Password = "";
            bool IsActive = false;

            bool IsFound = UserData.GetUserInfoByPersonID
                                (PersonID, ref UserID, ref UserName, ref Password, ref IsActive);

            if (IsFound)
                //we return new object of that User with the right data
                return new User(UserID, UserID, UserName, Password, IsActive);
            else
                return null;
        }
        public static User FindByUsernameAndPassword(string UserName, string Password)
        {
            int UserID = -1;
            int PersonID = -1;

            bool IsActive = false;

            bool IsFound = UserData.GetUserInfoByUserNameAndPassword
                                (UserName, Password, ref UserID, ref PersonID, ref IsActive);

            if (IsFound)
                //we return new object of that User with the right data
                return new User(UserID, PersonID, UserName, Password, IsActive);
            else
                return null;
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNew:
                    {
                        if (_AddNew())
                        {
                            _Mode = enMode.Update;
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                case enMode.Update:
                    { return _Update(); }
            }
            return false;
        }
        public static DataTable GetAllUsers()
        {
            return UserData.GetAllUsers();
        }

        public static bool DeleteUser(int UserID)
        {
            return UserData.DeleteUser(UserID);
        }

        public static bool isUserExist(int UserID)
        {
            return UserData.IsUserExist(UserID);
        }

        public static bool isUserExist(string UserName)
        {
            return UserData.IsUserExist(UserName);
        }

        public static bool isUserExistForPersonID(int PersonID)
        {
            return UserData.IsUserExistForPersonID(PersonID);
        }


    }
}
