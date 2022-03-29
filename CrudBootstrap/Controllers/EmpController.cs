using CrudBootstrap.Database;
using CrudBootstrap.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CrudBootstrap.Controllers
{
    public class EmpController : Controller
    {
        // GET: Emp
        [Authorize]
        public ActionResult Index()
        {
            Project_AoneEntities obj = new Project_AoneEntities();

            List<EmpModel> empobj = new List<EmpModel>();

            var x = obj.Employee.ToList();
            foreach (var item in x)
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
        [Authorize]
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(EmpModel empobj)
        {
            Project_AoneEntities obj = new Project_AoneEntities();
            Employee tblobj = new Employee();
            tblobj.Id = empobj.Id;
            tblobj.Name = empobj.Name;
            tblobj.Email = empobj.Email;
            tblobj.Mobile = empobj.Mobile;
            tblobj.Address = empobj.Address;

            if (empobj.Id == 0)
            {
                obj.Employee.Add(tblobj);
                obj.SaveChanges();
            }
            else
            {
                obj.Entry(tblobj).State = System.Data.Entity.EntityState.Modified;
                obj.SaveChanges();
            }

            return RedirectToAction("EmpTable","Home");
        }

        [Authorize]
        public ActionResult Delete(int id)
        {
            Project_AoneEntities obj = new Project_AoneEntities();
            var deleteitem = obj.Employee.Where(b => b.Id == id).First();
            obj.Employee.Remove(deleteitem);
            obj.SaveChanges();
            return RedirectToAction("EmpTable","Home");
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            EmpModel empobj = new EmpModel();
            Project_AoneEntities obj = new Project_AoneEntities();
            var edititem = obj.Employee.Where(b => b.Id == id).First();
            empobj.Id = edititem.Id;
            empobj.Name = edititem.Name;
            empobj.Email = edititem.Email;
            empobj.Mobile = edititem.Mobile;
            empobj.Address = edititem.Address;


            ViewBag.id = edititem.Id;
            return View("Add", empobj);
        }
    }

}