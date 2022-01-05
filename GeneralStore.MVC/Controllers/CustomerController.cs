using GeneralStore.MVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace GeneralStore.MVC.Controllers
{
    public class CustomerController : Controller
    {
        // Add application DB Context (link to the database)
        private ApplicationDbContext _customerDb = new ApplicationDbContext();

        // GET: Customer
        public ActionResult Index()
        {
            List<Customer> customerList = _customerDb.Customers.ToList();
            List<Customer> orderedList = customerList.OrderBy(cust => cust.FullName).ToList();
            return View(orderedList);
        }

        // Get: Customer
        public ActionResult Create()
        {
            return View();
        }
        
        // POST: Customer
        [HttpPost]
        public ActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _customerDb.Customers.Add(customer);
                _customerDb.SaveChanges();
                return RedirectToAction("IndexCustomer");
            }
            return View(customer);
        }

        // GET: Delete
        // Product/Delete/{id}
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = _customerDb.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Delete
        // Product/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            Product product = _customerDb.Products.Find(id);
            _customerDb.Products.Remove(product);
            _customerDb.SaveChanges();
            return RedirectToAction("IndexCustomer");
        }

        // GET: Edit
        // Product/Edit/{id}
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = _customerDb.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Edit// Customer/Edit/{id}
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _customerDb.Entry(customer).State = EntityState.Modified;
                _customerDb.SaveChanges();
                return RedirectToAction("IndexCustomer");
            }
            return View(customer);
        }

        // GET: Details
        // Product/Details/{id}
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = _customerDb.Customers.Find(id);

            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }       
    }
}