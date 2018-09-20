using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using TestProj.DAL;
using TestProj.Models;
using TestProj.ViewModels;

namespace TestProj.Controllers
{
    [Authorize]
    public class CompaniesController : Controller
    {
        private EFContext db = new EFContext();

        // GET: Companies
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page, string adminUsersOnly)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.CountrySortParm = sortOrder == "Country" ? "country_desc" : "Country";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var companies = from s in db.Companies
                            select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                companies = companies.Where(s => s.Name.Contains(searchString));
            }
            if (adminUsersOnly == "AdminUsersOnly")
            {
                companies = companies.Where(c => c.AdminUsers.Count != 0);
            }
                
            switch (sortOrder)
            {
                case "name_desc":
                    companies = companies.OrderByDescending(s => s.Name);
                    break;
                case "Country":
                    companies = companies.OrderBy(s => s.Country);
                    break;
                case "country_desc":
                    companies = companies.OrderByDescending(s => s.Country);
                    break;
                default:  // Name ascending 
                    companies = companies.OrderBy(s => s.Name);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(companies.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult GetUsers(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var company = db.Companies.Find(id);
            //var userlist = new UserListViewModel();
            //userlist.AdminUsers = company.AdminUsers;
            //userlist.EndUsers = company.EndUsers;
            return PartialView(company);
        }

        // GET: Companies/Create
        public ActionResult Create()
        {
            var adminUsers = new SelectList(db.AdminUsers, "Id", "Login");
            var endUsers = new SelectList(db.EndUsers, "Id", "Login");

            ViewBag.AdminUsers = adminUsers;
            ViewBag.EndUsers = endUsers;
            return View();
        }

        // POST: Companies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Country")] Company company, List<string> adminUserIds, List<string> endUserIds)
        {
            if (ModelState.IsValid)
            {
                company.Id = Guid.NewGuid();                                    //2xCtrl+K

                foreach(string adminUserId in adminUserIds)
                {
                    if (Guid.TryParse(adminUserId, out Guid adminUserGuid))
                    {
                        var adminUser = db.AdminUsers.FirstOrDefault(u => u.Id == adminUserGuid);
                        company.AdminUsers.Add(adminUser);
                    }
                }
                foreach (string endUserId in endUserIds)
                {
                    if (Guid.TryParse(endUserId, out Guid endUserGuid))
                    {
                        var endUser = db.EndUsers.FirstOrDefault(u => u.Id == endUserGuid);
                        company.EndUsers.Add(endUser);
                    }
                }
                db.Companies.Add(company);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(company);
        }

        // GET: Companies/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            var admins = db.AdminUsers.ToList().Except(company.AdminUsers);
            var ends = db.EndUsers.ToList().Except(company.EndUsers);

            var adminUsers = new SelectList(admins, "Id", "Login");
            var endUsers = new SelectList(ends, "Id", "Login");

            ViewBag.AdminUsers = adminUsers;
            ViewBag.EndUsers = endUsers;

            return View(company);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Country")] Company company, List<string> adminUserIds, List<string> endUserIds)
        {
            if (ModelState.IsValid)
            {
                
                db.Entry(company).State = EntityState.Modified;
                foreach (string adminUserId in adminUserIds)
                {
                    if (Guid.TryParse(adminUserId, out Guid adminUserGuid))
                    {
                        var adminUser = db.AdminUsers.FirstOrDefault(u => u.Id == adminUserGuid);
                        company.AdminUsers.Add(adminUser);
                    }
                }
                foreach (string endUserId in endUserIds)
                {
                    if (Guid.TryParse(endUserId, out Guid endUserGuid))
                    {
                        var endUser = db.EndUsers.FirstOrDefault(u => u.Id == endUserGuid);
                        company.EndUsers.Add(endUser);
                    }
                }
                db.SaveChanges();
                return RedirectToAction("Edit", new { id = company.Id });
            }
            return View(company);
        }

        // GET: Companies/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        public ActionResult DeleteUser(Guid? id, Guid? userId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companies.Find(id);

            if (company == null)
            {
                return HttpNotFound();
            }

            var adminUser = db.AdminUsers.FirstOrDefault(u => u.Id == userId);
            if (adminUser != null)
                company.AdminUsers.Remove(adminUser);
            else
                company.EndUsers.Remove(db.EndUsers.FirstOrDefault(u => u.Id == userId));
            db.SaveChanges();

            return RedirectToAction("Edit", new { id });
        }

        // POST: Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Company company = db.Companies.Find(id);
            db.Companies.Remove(company);
            db.SaveChanges();
            return RedirectToAction("Index");
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
