using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcLoginApp.Models
{
    public class NewsFeed
    {
        public NewsFeed()
        {
            Data = new List<UserData>();
        }
        public string NewsFeeds { get; set; }
        public int UserID { get; set; }
        public List<UserData> Data { get; set; }
        public string UserPostData { get; set; }
    }

    public class UserData
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string UserPostData { get; set; }
        public int UserID { get; set; }
        public DateTime DateTime { get; set; }
        public string ImageName { get; set; }
        public string ImagePath { get; set; }

    }
}