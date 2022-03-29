using CrudBootstrap.Database;
using CrudBootstrap.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CrudBootstrap.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        
        public ActionResult Index(UserInfoModel userobj)
        {
            Project_AoneEntities obj = new Project_AoneEntities();
            var UserRes = obj.user_info.Where(a => a.Email == userobj.Email).FirstOrDefault();

            if (UserRes == null)
            {
                TempData["Invalid"] = "Email not found or Invalid Username";
            }
            else
            {
                if(UserRes.Email==userobj.Email && UserRes.Passward == userobj.Passward)
                {
                    FormsAuthentication.SetAuthCookie(UserRes.Email, false);

                    Session["username"] = UserRes.Name;
                    Session["useremail"] = UserRes.Email;
                    return RedirectToAction("IndexDashboard", "Home");
                }
                else
                {
                    TempData["Wrong"] = "Wrong Password Please Enter Valid Password";
                    return View();
                }
            }
            return View();
        }

        [Authorize]
        public ActionResult IndexDashBoard()
        {

            return View();
        }

        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }

        public ActionResult EmpTable()
        {
            Project_AoneEntities obj = new Project_AoneEntities();

            List<EmpModel> empobj = new List<EmpModel>();

            var res = obj.Employee.ToList();
            foreach (var item in res)
            {
                empobj.Add(new EmpModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    Email = item.Email,
                    Mobile = item.Mobile,
                    Address = item.Address
                });
            }

            return View(empobj);
        }

        [HttpGet]
        public ActionResult UserReg()
        {
            return View();
        }


        [HttpPost]
        public ActionResult UserReg(UserInfoModel useobj)
        {
            Project_AoneEntities obj = new Project_AoneEntities();
            user_info infoobj = new user_info();
            infoobj.Id = useobj.Id;
            infoobj.Name = useobj.Name;
            infoobj.Email = useobj.Email;
            infoobj.Passward = useobj.Passward;

            obj.user_info.Add(infoobj);
            obj.SaveChanges();

            return RedirectToAction("Index", "Home");
            //return View();
        }











        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [Authorize]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

    }
}