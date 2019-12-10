using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CustomerManager.Models;

namespace CustomerManager.Controllers
{
    
    public class CustomerController : Controller
    {
        private ApplicationDbContext db;
        public CustomerController()
        {
            db = new ApplicationDbContext();
        }

        // GET: Customer
        
        public ActionResult Index()
        {
            //check if the user is in the roles table for the role specified
            if (User.IsInRole("CanManageCustomers"))
                return View();
            return View("ReadOnlyIndex");
        }

        
        public ActionResult Create()
        {

            return View();
        }


        [Authorize(Roles = "CanManageCustomers")]
        public ActionResult Edit(int Id)
        {
            var customer = db.Customers.SingleOrDefault(c => c.Id == Id);
            return View(customer);
        }

        public ActionResult Detail(int id)
        {
            var customer = db.Customers.SingleOrDefault(c => c.Id == id);
            return View(customer);
        }
    }
}