using MyShopLucri.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShopLucri.Controllers
{
    
    public class CustomerController : Controller
    {
        MyShopDBEntities dbObj = new MyShopDBEntities();
        // GET: Customer
        public ActionResult Customer()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCustomer(tbl_Customers model)
        {
            tbl_Customers obj = new tbl_Customers();
            obj.CustomerID = model.CustomerID;
            obj.FirstName = model.FirstName;
            obj.Surname = model.Surname;
            obj.Phone = model.Phone;
            obj.Email = model.Email;

            dbObj.tbl_Customers.Add(obj);
            try
            {
                dbObj.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var entityValidationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationErrors.ValidationErrors)
                    {
                        Response.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                    }
                }
            }
            return View("Customer");
        }
        public ActionResult ListCustomers()
        {
            var res = dbObj.tbl_Customers.ToList();
            return View(res);
        }

        public ActionResult Delete(string id)
        {
            var res = dbObj.tbl_Customers.Where(x => x.CustomerID == id).First();
            dbObj.tbl_Customers.Remove(res);
            dbObj.SaveChanges();

            var list = dbObj.tbl_Customers.ToList();

            return View();
        }
    }

}