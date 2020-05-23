using Microsoft.Practices.EnterpriseLibrary.Data;
using MvcLoginApp.Common_Classes;
using MvcLoginApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcLoginApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            DataTable dt = WebAppDAL.GetUserDetails(ProjectSession.clientID);
            //List<RegistrationPageModel> model = new List<RegistrationPageModel>();
            RegistrationPageModel model = new RegistrationPageModel();
            if (dt != null && dt.Rows.Count > 0)
            {
                for (var i = 0; i < dt.Rows.Count; i++)
                {
                    model.FindFriendList.Add(new FindFriendList
                    {
                        UserID = Convert.ToInt32(dt.Rows[i]["UserID"]),
                        UserName = dt.Rows[i]["Username"].ToString(),
                        EmailAddress = dt.Rows[i]["EmailAddress"].ToString(),
                        Type = dt.Rows[i]["Usertype"].ToString()
                    });
                }
            }

            DataTable dtGetFriendRequestList = WebAppDAL.GetFriendRequestList(ProjectSession.clientID);
            if (dtGetFriendRequestList != null && dtGetFriendRequestList.Rows.Count > 0)
            {
                for (var i = 0; i < dtGetFriendRequestList.Rows.Count; i++)
                {
                    model.RequestedList.Add(new RequestedListModel
                    {
                        UserID = Convert.ToInt32(dtGetFriendRequestList.Rows[i]["UserID"]),
                        EmailAddress = dtGetFriendRequestList.Rows[i]["EmailAddress"].ToString(),
                        UserName = dtGetFriendRequestList.Rows[i]["Username"].ToString(),
                        Type = dtGetFriendRequestList.Rows[i]["Usertype"].ToString()
                    });
                }
            }

            DataTable dtGetFriendList = WebAppDAL.GetFriendList(ProjectSession.clientID);
            if(dtGetFriendList != null && dtGetFriendList.Rows.Count > 0)
            {
                for(var i = 0; i < dtGetFriendList.Rows.Count; i++)
                {
                    model.FriendList.Add(new FriendList
                    {
                        UserID = Convert.ToInt32(dtGetFriendList.Rows[i]["UserID"]),
                        EmailAddress = dtGetFriendList.Rows[i]["EmailAddress"].ToString(),
                        UserName = dtGetFriendList.Rows[i]["Username"].ToString(),
                        Type = dtGetFriendList.Rows[i]["Usertype"].ToString()
                    });
                }
            }

            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public ActionResult SendRequest(int id)
        {
            int bl = WebAppDAL.SendRequestStatus(id, ProjectSession.clientID);
            return RedirectToAction("Index");
        }

        public ActionResult RequestedList()
        {
            DataTable dt = WebAppDAL.GetRequestedUserList(ProjectSession.clientID);
            List<RequestedListModel> model = new List<RequestedListModel>();
            if (dt != null && dt.Rows.Count > 0)
            {
                for (var i = 0; i < dt.Rows.Count; i++)
                {
                    RequestedListModel registrationPageModel = new RequestedListModel();
                    registrationPageModel.UserID = Convert.ToInt32(dt.Rows[i]["UserID"]);
                    registrationPageModel.EmailAddress = dt.Rows[i]["EmailAddress"].ToString();
                    registrationPageModel.Type = dt.Rows[i]["Usertype"].ToString();
                    registrationPageModel.UserName = dt.Rows[i]["Username"].ToString();
                    if (ProjectSession.clientID != registrationPageModel.UserID)
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["Status"])))
                        {
                            if (Convert.ToInt32(dt.Rows[i]["Status"]) == 0)
                                model.Add(registrationPageModel);
                        }
                    }
                }
            }
            return View("Contact", model);
        }

        public ActionResult CancelSentRequest(int id)
        {
            WebAppDAL.CancelRequestStatus(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult AcceptFriendRequest(int id)
        {
            WebAppDAL.AcceptRequest(id, ProjectSession.clientID);
            return RedirectToAction("Index");
        }

        public ActionResult RejectFriendRequest(int ID)
        {
            WebAppDAL.RejectRequest(ID, ProjectSession.clientID);
            return RedirectToAction("Index");
        }

        public ActionResult UnFriend(int id)
        {
            WebAppDAL.UnFriend(id, ProjectSession.clientID);
            return RedirectToAction("Index");
        }

        public ActionResult GetMutualFriendList(int id)
        {
            DataTable dtGetMutualFriend = WebAppDAL.GetMutualFriendList(id, ProjectSession.clientID);
            ProjectSession.IsMutualFriendTab = true;
            List<RegistrationPageModel> model = new List<RegistrationPageModel>();
            if (dtGetMutualFriend != null && dtGetMutualFriend.Rows.Count > 0)
            {
                for (var i = 0; i < dtGetMutualFriend.Rows.Count; i++)
                {
                    RegistrationPageModel registration = new RegistrationPageModel();
                    registration.UserID = Convert.ToInt32(dtGetMutualFriend.Rows[i]["UserID"]);
                    registration.EmailAddress = dtGetMutualFriend.Rows[i]["EmailAddress"].ToString();
                    registration.Type = dtGetMutualFriend.Rows[i]["Usertype"].ToString();
                    registration.UserName = dtGetMutualFriend.Rows[i]["Username"].ToString();
                    model.Add(registration);
                }
            }
            return PartialView("_MutualFriendList", model);
        }
    }
}