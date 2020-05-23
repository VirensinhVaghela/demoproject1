using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcLoginApp.Common_Classes
{
    public class ProjectSession
    {
        public static int clientID { get; set; }
        public static string EmailAddress { get; set; }
        public static bool IsMutualFriendTab { get; set; }
        public static string ServerPath { get; set; }
        public static string UserName { get; set; }

    }
}