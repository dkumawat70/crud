using BALayer;
using ClassLiabraryForAppplication;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
//using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        BusinessLayer bal = new BusinessLayer();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Getdata()
        {
            //var data = db.userInfos.ToList();
            //return Json(data, JsonRequestBehavior.AllowGet);
            List<UserInfo> u = bal.GetUserDetails();
            return Json(u, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult AddUser(int? id)
        {
            UserInfo1 u = new UserInfo1();

            if (id > 0)
            {
                Hashtable hstable = new Hashtable();
                hstable.Add("@id", id);
                List<UserInfo> dtedit = bal.getEditUser(hstable);
                if(dtedit!=null)
                {
                    foreach (var item in dtedit)
                    {
                        u.id = item.id;
                        u.firstname = item.firstname;
                        u.lastname = item.lastname;
                        u.email = item.email;
                        u.salary = item.salary;
                        u.mobile = item.mobile;
                        u.image = item.image;
                    }
                }               
            }
            return View(u);

        }
        [HttpPost]
        public JsonResult AddUser()
        {

            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;

                    for (int i = 0; i < files.Count; i++)
                    {
                        //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                        //string filename = Path.GetFileName(Request.Files[i].FileName);  

                        HttpPostedFileBase file = files[i];
                        string fname;

                        // Checking for Internet Explorer  
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                        }

                        // Get the complete folder path and store the file inside it.  
                        fname = Path.Combine(Server.MapPath("../Uploads/"), fname);
                        file.SaveAs(fname);


                        UserInfo u = new UserInfo();
                        u.id = Convert.ToInt32(Request.Form["id"].ToString());
                        u.firstname = Request.Form["firstname"].ToString();
                        u.lastname = Request.Form["lastname"].ToString();
                        u.email = Request.Form["email"].ToString();
                        u.mobile = Request.Form["mobile"].ToString();
                        u.salary = Convert.ToDecimal(Request.Form["salary"].ToString());
                        u.image = "../Uploads/" + file.FileName;
                        if (u.id > 0)
                        {
                            Hashtable hstbale = new Hashtable();
                            hstbale.Add("@firstname", u.firstname);
                            hstbale.Add("@lastname", u.lastname);
                            hstbale.Add("@email", u.email);
                            hstbale.Add("@mobile", u.mobile);
                            hstbale.Add("@salary", u.salary);
                            hstbale.Add("@image", u.image);
                            bal.UpdateUserInfo(hstbale);
                           

                        }
                        else
                        {
                            //db.userInfos.Add(u);
                            //db.SaveChanges();
                            Hashtable hstbale = new Hashtable();
                            hstbale.Add("@firstname",u.firstname);
                            hstbale.Add("@lastname", u.lastname);
                            hstbale.Add("@email", u.email);
                            hstbale.Add("@mobile", u.mobile);
                            hstbale.Add("@salary", u.salary);
                            hstbale.Add("@image", u.image);
                            bal.InsertUserInfo(hstbale);

                        }
                    }
                    // Returns message that successfully uploaded  
                    return Json("File Uploaded Successfully!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
            //return json(1);
        }
        [HttpGet]
        public ActionResult EditUser(int? id)
        {
            return View();
        }
        [HttpGet]
        public JsonResult DeleteUser(int? id)
        {
            var result = 0;
            //UserInfo u = new UserInfo();
            //var delete = db.userInfos.SingleOrDefault(a => a.id == id);
            //if (delete != null)
            //{
            //    db.userInfos.Remove(delete);
            //    result = db.SaveChanges();
            //}
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}