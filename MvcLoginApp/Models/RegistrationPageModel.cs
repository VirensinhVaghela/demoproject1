using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcLoginApp.Models
{
    public class RegistrationPageModel
    {

        public RegistrationPageModel()
        {
            Types = new List<Types>();
            SendRequestList = new List<SendRequestList>();
            FriendList = new List<FriendList>();
            FindFriendList = new List<FindFriendList>();
            RequestedList = new List<RequestedListModel>();
        }
        [Required(AllowEmptyStrings = false,ErrorMessage ="Please enter your EmailAddress")]
        public string EmailAddress { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter your Password")]
        public string Password { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter your Type")]
        public string Type { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter your Username")]
        public string UserName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter your Firstname")]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter your Lastname")]
        public string LastName { get; set; }
        public int UserID { get; set; }
        public List<Types> Types { get; set; }
        public List<SendRequestList> SendRequestList { get; set; }
        public List<FriendList> FriendList { get; set; }
        public List<FindFriendList> FindFriendList { get; set; }
        public List<RequestedListModel> RequestedList { get; set; }

    }
    public class RequestedListModel
    {
        public int UserID { get; set; }
        public string EmailAddress { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Type { get; set; }
    }

    public class SendRequestList
    {
        public int UserID { get; set; }
        public string EmailAddress { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Type { get; set; }
    }

    public class FriendList
    {
        public int UserID { get; set; }
        public string EmailAddress { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Type { get; set; }
    }

    public class FindFriendList
    {
        public int UserID { get; set; }
        public string EmailAddress { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Type { get; set; }
    }
    public class Types
    {
        public int UserID { get; set; }
        public string UserType { get; set; }
    }
}