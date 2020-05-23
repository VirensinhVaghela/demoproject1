using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using MvcLoginApp.Common_Classes;
using MvcLoginApp.Models;
using Newtonsoft.Json;

namespace MvcLoginApp.Controllers
{
    public class NewsFeedController : Controller
    {
        // GET: NewsFeed
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PostNews(NewsFeed newsFeed)
        {
            WebAppDAL.InsertNewsFeedData(ProjectSession.clientID, newsFeed.NewsFeeds);
            return RedirectToAction("GetUserPost");
        }

        [HttpGet]
        public ActionResult GetUserPost()
        {
            string FolderPath;
            string ServerPath;

            //if (string.IsNullOrEmpty(FolderPath))
            //{
                FolderPath = Constant_UploadImagePath.UserUploadImage;
            //}
            //if(string.IsNullOrEmpty(ServerPath))
            //{
                 ServerPath = string.Format("{0}://{1}:{2}{3}", Request.Url.Scheme, Request.Url.Host, Request.Url.Port, (Request.ApplicationPath == "/" ? "" : Request.ApplicationPath));
            //}
            var tempFolderPath = FolderPath.Replace("~/", ServerPath + "/" );
            if (Directory.Exists(Server.MapPath(FolderPath)));
            {
                var ImageInfos = new DirectoryInfo(Server.MapPath(FolderPath)).GetFiles().ToList();
            }
            DataTable dtGetUserPost = WebAppDAL.GetUserPostData(ProjectSession.clientID);

            NewsFeed newsFeed = new NewsFeed();
            if (dtGetUserPost != null && dtGetUserPost.Rows.Count > 0)
            {
                for (var i = 0; i < dtGetUserPost.Rows.Count; i++)
                {
                    //NewsFeed data = new NewsFeed();
                    //data.UserPostData = Convert.ToString(dtGetUserPost.Rows[i]["NewsFeed"]);
                    if (!string.IsNullOrEmpty(Convert.ToString(dtGetUserPost.Rows[i]["Imagename"])))
                    {
                        newsFeed.Data.Add(new UserData
                        {
                            UserPostData = Convert.ToString(dtGetUserPost.Rows[i]["NewsFeed"]),
                            UserName = Convert.ToString(dtGetUserPost.Rows[i]["Username"]),
                            UserID = Convert.ToInt32(dtGetUserPost.Rows[i]["UserID"]),
                            DateTime = Convert.ToDateTime(dtGetUserPost.Rows[i]["CreatedDateTime"]),
                            ImagePath = tempFolderPath + Convert.ToInt32(dtGetUserPost.Rows[i]["UserID"]) + "/" + Convert.ToString(dtGetUserPost.Rows[0]["Imagename"]),
                        });
                    }
                    else
                    {
                        newsFeed.Data.Add(new UserData
                        {
                            UserPostData = Convert.ToString(dtGetUserPost.Rows[i]["NewsFeed"]),
                            UserName = Convert.ToString(dtGetUserPost.Rows[i]["Username"]),
                            UserID = Convert.ToInt32(dtGetUserPost.Rows[i]["UserID"]),
                            DateTime = Convert.ToDateTime(dtGetUserPost.Rows[i]["CreatedDateTime"]),
                            //ImagePath = tempFolderPath + "/" + Convert.ToString(dtGetUserPost.Rows[i]["Imagename"]),
                        });
                    }

                    //model.Add(new SelectListItem {
                    //    Value = Convert.ToString(dtGetUserPost.Rows[i]["NewsFeed"]),
                    //    Text = Convert.ToString(dtGetUserPost.Rows[i]["NewsFeed"]),
                    //});
                }
            }
            //dynamic mymodel = new ExpandoObject();
            //mymodel.UserData = model;
            //ViewBag.NewsFeed = model;
            return View("Index", newsFeed);
        }

        public ActionResult OpenUploadFileDialog()
        {
            DataTable dtGetLoginUserDetail = WebAppDAL.GetLoginUserDetail(ProjectSession.clientID);
            NewsFeed newsFeed = new NewsFeed();
            if (dtGetLoginUserDetail != null && dtGetLoginUserDetail.Rows.Count > 0)
            {
                for (var i = 0; i < dtGetLoginUserDetail.Rows.Count; i++)
                {
                    newsFeed.Data.Add(new UserData()
                    {
                        UserID = Convert.ToInt32(dtGetLoginUserDetail.Rows[i]["UserID"]),
                        UserName = Convert.ToString(dtGetLoginUserDetail.Rows[i]["Username"]),
                        Email = Convert.ToString(dtGetLoginUserDetail.Rows[i]["EmailAddress"]),
                        FirstName = Convert.ToString(dtGetLoginUserDetail.Rows[i]["Firstname"]),
                        LastName = Convert.ToString(dtGetLoginUserDetail.Rows[i]["Lastname"])
                    });
                }
            }
            return View();
        }

        public ActionResult UploadImage()
        {
            try
            {
                var result = new object();
                //check if path exist or not
                checkifPathExistForCurrentUser();
                var pathofUploadImage = "";

                pathofUploadImage = Server.MapPath(Constant_UploadImagePath.UserUploadImage + ProjectSession.clientID.ToString());

                foreach (string filname in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[filname];
                    var filename1 = Path.GetFileName(file.FileName);
                    filename1 = filename1.Replace(' ', '_');
                    file.SaveAs(Path.Combine(pathofUploadImage, filename1));
                    pathofUploadImage = Path.Combine(pathofUploadImage, filename1);
                    var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filename1);
                    WebAppDAL.AddUploadedUserImage(ProjectSession.clientID, pathofUploadImage,filename1,fileNameWithoutExtension);
                }

                result = new { Message = "Successfully Uploaded Images", IsSuccess = true };

                string JsonResult = JsonConvert.SerializeObject(result);
                return new ContentResult { Content = JsonResult, ContentType = "application/json" };
            }
            catch (Exception ex)
            {
                var result = new { Message = "Uploading fail.", IsSuccess = false };
                string JsonResult = Newtonsoft.Json.JsonConvert.SerializeObject(result);
                return new ContentResult { Content = JsonResult, ContentType = "application/json" };
                
            }
        }

        public ActionResult GetUplodedImage()
        {
            string FolderPath;
            string ServerPath;
            string result;

            FolderPath = Constant_UploadImagePath.UserUploadImage;
            ServerPath = string.Format("{0}://{1}:{2}{3}", Request.Url.Scheme, Request.Url.Host, Request.Url.Port, (Request.ApplicationPath == "/" ? "" : Request.ApplicationPath));

            var tempFolderPath = FolderPath.Replace("~/", ServerPath + "/");
            if (Directory.Exists(Server.MapPath(FolderPath))) ;
            {
                var ImageInfos = new DirectoryInfo(Server.MapPath(FolderPath)).GetFiles().ToList();
            }
            DataTable dtGetUserPost = WebAppDAL.GetUserPostData(ProjectSession.clientID);

            NewsFeed newsFeed = new NewsFeed();
            if (dtGetUserPost != null && dtGetUserPost.Rows.Count > 0)
            {
                for (var i = 0; i < 1; i++)
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(dtGetUserPost.Rows[0]["Imagename"])))
                    {
                        newsFeed.Data.Add(new UserData
                        {
                            UserPostData = Convert.ToString(dtGetUserPost.Rows[0]["NewsFeed"]),
                            UserName = Convert.ToString(dtGetUserPost.Rows[0]["Username"]),
                            DateTime = Convert.ToDateTime(dtGetUserPost.Rows[i]["CreatedDateTime"]),
                            ImagePath = tempFolderPath + Convert.ToInt32(dtGetUserPost.Rows[0]["UserID"]) + "/" + Convert.ToString(dtGetUserPost.Rows[0]["Imagename"]),
                        });
                    }
                    else
                    {
                        newsFeed.Data.Add(new UserData
                        {
                            UserPostData = Convert.ToString(dtGetUserPost.Rows[0]["NewsFeed"]),
                            UserName = Convert.ToString(dtGetUserPost.Rows[0]["Username"]),
                            UserID = Convert.ToInt32(dtGetUserPost.Rows[0]["UserID"]),
                            DateTime = Convert.ToDateTime(dtGetUserPost.Rows[0]["CreatedDateTime"]),
                        });
                    }
                }
            }
            string jsonResult = JsonConvert.SerializeObject(newsFeed);
            return new ContentResult { Content = jsonResult, ContentType = "application/json" };
        }
        public void checkifPathExistForCurrentUser()
        {
            // Check Path for specific user exist or not. If not exists then create
            var pathofUploadImage = Server.MapPath(Constant_UploadImagePath.UserUploadImage + ProjectSession.clientID.ToString());
            if (!System.IO.File.Exists(pathofUploadImage))
            {
                DirectorySecurity sec = new DirectorySecurity();
                SecurityIdentifier everyone = new SecurityIdentifier(WellKnownSidType.WorldSid, null);
                sec.AddAccessRule(new FileSystemAccessRule(everyone, FileSystemRights.Modify | FileSystemRights.Synchronize, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow));
                Directory.CreateDirectory(pathofUploadImage, sec);
            }
        }
    }
}