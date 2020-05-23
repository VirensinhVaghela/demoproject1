using MvcLoginApp.Common_Classes;
using MvcLoginApp.Models;
using MvcLoginApp.Models.LoginModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcLoginApp.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        [HttpGet]
        public ActionResult Index()
        {
            RegistrationPageModel model = new RegistrationPageModel();
            List<Types> types = new List<Types>();
            types.Add(new Types { UserID = 1, UserType = "Select Type" });
            types.Add(new Types { UserID = 2, UserType = "Admin" });
            types.Add(new Types { UserID = 3, UserType = "User" });
            model.Types = types;

            return View("Login", model);
        }

        [HttpPost]
        public ActionResult Index(LoginModel model)
        {
            DataTable dtlogin = WebAppDAL.UserLogin(model);   
            LoginModel login = new LoginModel();
            if(dtlogin.Rows.Count > 0 && dtlogin != null)
            {
                login.UserID = Convert.ToInt32( dtlogin.Rows[0]["UserID"]);
                login.UserName = Convert.ToString(dtlogin.Rows[0]["Username"]);
                ProjectSession.clientID = login.UserID;
                ProjectSession.EmailAddress = model.EmailAddress;
                ProjectSession.UserName = login.UserName;
                return RedirectToAction("GetUserPost", "NewsFeed");
            }
            else
            {
                RegistrationPageModel modal = new RegistrationPageModel();
                List<Types> types = new List<Types>();
                types.Add(new Types { UserID = 1, UserType = "Select Type" });
                types.Add(new Types { UserID = 2, UserType = "Admin" });
                types.Add(new Types { UserID = 3, UserType = "User" });
                modal.Types = types;

                ModelState.AddModelError("EmailAddress", "Please Enter Valid Username Or Password");
                return View("Login", modal);
            }
        }

        #region
        //[HttpGet]
        //public ActionResult RegistrationPage()
        //{

        //    //List<SelectListItem> items = new List<SelectListItem>();
        //    //items.Add(new SelectListItem { Text = "Admin", Value = "Admin" });
        //    //items.Add(new SelectListItem { Text = "User", Value = "User" });
        //    //ViewBag.Departments = items;

        //    RegistrationPageModel model = new RegistrationPageModel();
        //    List<Types> types = new List<Types>();
        //    types.Add(new Types { UserID = 1, UserType = "Select Type" });
        //    types.Add(new Types { UserID = 2, UserType = "Admin" });
        //    types.Add(new Types { UserID = 3, UserType = "User" });
        //    model.Types = types;

        //    return View("RegistrationPage",model);
        //}
        #endregion

        [HttpPost]
        public ActionResult RegistrationPage(RegistrationPageModel model)
        { 
            DataSet dtRegistration = WebAppDAL.UserRegistration(model);
            if (dtRegistration.Tables.Count > 0)
            {
                ModelState.AddModelError("EmailAddress", "This Email Address is already registered");
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
    }
}