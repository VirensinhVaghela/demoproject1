using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Linq;
using System.Web;
using MvcLoginApp.Models;
using MvcLoginApp.Models.LoginModel;
using System.Data.Common;

namespace MvcLoginApp.Common_Classes
{
    public class WebAppDAL
    {
        public static DataTable UserLogin (LoginModel login)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("UserLogin");
            db.AddInParameter(dbCommand, "@Email", DbType.String, login.EmailAddress);
            db.AddInParameter(dbCommand, "@Password", DbType.String, login.Password);
            return db.ExecuteDataSet(dbCommand).Tables[0];
        }

        public static DataSet UserRegistration(RegistrationPageModel registration)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("UserRegistration");
            db.AddInParameter(dbCommand, "@Firstname", DbType.String,registration.FirstName);
            db.AddInParameter(dbCommand, "@Lastname", DbType.String, registration.LastName);
            db.AddInParameter(dbCommand, "@Username", DbType.String, registration.UserName);
            db.AddInParameter(dbCommand, "@Email", DbType.String,registration.EmailAddress);
            db.AddInParameter(dbCommand, "@Password", DbType.String,registration.Password);
            db.AddInParameter(dbCommand, "@type", DbType.String,registration.Type);
            return db.ExecuteDataSet(dbCommand);
        }
        public static DataTable GetLoginUserDetail(int ClientID)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("GetLoginUserDetail");
            db.AddInParameter(dbCommand, "@ClientID", DbType.Int32, ClientID);
            return db.ExecuteDataSet(dbCommand).Tables[0];
        }
        public static DataTable GetUserDetails(int ClientID)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("GetUserDetails");
            db.AddInParameter(dbCommand, "@ClientID", DbType.Int32, ClientID);
            return db.ExecuteDataSet(dbCommand).Tables[0];
        }

        public static int SendRequestStatus(int ID,int ClientID)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("SendRequestStatus");
            db.AddInParameter(dbCommand, "@ID", DbType.Int32, ID);
            db.AddInParameter(dbCommand, "@UserID", DbType.Int32, ClientID);
            db.AddOutParameter(dbCommand, "@Status", DbType.Int32, 0);
            db.ExecuteNonQuery(dbCommand);
            return Convert.ToInt32(dbCommand.Parameters["@Status"].Value);
        }

        public static void CancelRequestStatus(int ID)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("CancelRequest");
            db.AddInParameter(dbCommand, "@ID", DbType.Int32,ID);
            //db.AddOutParameter(dbCommand, "@Status", DbType.Int32, 0);
            db.ExecuteNonQuery(dbCommand);
        }

        public static DataTable GetRequestedUserList(int ClientID)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("GetRequestedUser");
            db.AddInParameter(dbCommand, "@ClientID", DbType.Int32, ClientID);
            return db.ExecuteDataSet(dbCommand).Tables[0];
        }
        public static DataTable GetFriendRequestList(int ClientID)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("GetFriendRequestList");
            db.AddInParameter(dbCommand, "@UserID", DbType.Int32, ClientID);
            return db.ExecuteDataSet(dbCommand).Tables[0];
        }

        public static void AcceptRequest(int ID,int clientID)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("AcceptRequest");
            db.AddInParameter(dbCommand, "@ID", DbType.Int32,ID);
            db.AddInParameter(dbCommand, "@ClientID", DbType.Int32, clientID);
            db.ExecuteNonQuery(dbCommand);
        }

        public static DataTable GetFriendList(int ClientID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommand("GetFrindsList");
                db.AddInParameter(dbCommand, "@ClientID", DbType.Int32, ClientID);
                return db.ExecuteDataSet(dbCommand).Tables[0];
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public static void RejectRequest(int id,int ClientID)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("RejectRequest");
            db.AddInParameter(dbCommand, "@ID", DbType.Int32,id);
            db.AddInParameter(dbCommand, "@ClientID", DbType.Int32, ClientID);
            db.ExecuteNonQuery(dbCommand);
        }

        public static void UnFriend(int id,int ClientID)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("GetUnfriendList");
            db.AddInParameter(dbCommand, "@ID", DbType.Int32, id);
            db.AddInParameter(dbCommand, "@ClientID", DbType.Int32, ClientID);
            db.ExecuteNonQuery(dbCommand);
        }

        public static DataTable GetMutualFriendList(int id, int ClientID)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("GetMutualFriendList");
            db.AddInParameter(dbCommand, "@ID", DbType.Int32, id);
            db.AddInParameter(dbCommand, "@ClientID", DbType.Int32, ClientID);
            return db.ExecuteDataSet(dbCommand).Tables[0];

        }

        public static void InsertNewsFeedData(int ClientID,string NewsData)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("InsertNewsFeedData");
            db.AddInParameter(dbCommand, "@ClientID", DbType.Int32, ClientID);
            db.AddInParameter(dbCommand, "@NewsData", DbType.String, NewsData);
            db.ExecuteNonQuery(dbCommand);
        }

        public static DataTable GetUserPostData(int ClientID)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("GetUserPost");
            db.AddInParameter(dbCommand, "@ClientID", DbType.Int32, ClientID);
            return db.ExecuteDataSet(dbCommand).Tables[0];
        }

        public static void AddUploadedUserImage(int ClientID, string ImagePath, string filename,string fileNameWithoutExtension)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("AddUploadedUserImage");
            db.AddInParameter(dbCommand, "@ClientID", DbType.Int32, ClientID);
            db.AddInParameter(dbCommand, "@ImagePath", DbType.String, ImagePath);
            db.AddInParameter(dbCommand, "@FileName", DbType.String, filename);
            db.AddInParameter(dbCommand, "@fileNameWithoutExtension", DbType.String, fileNameWithoutExtension);
            db.ExecuteNonQuery(dbCommand);
        }
    }
}