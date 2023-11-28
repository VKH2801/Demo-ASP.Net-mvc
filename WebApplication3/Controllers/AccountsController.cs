using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication3.Controllers
{
    public class AccountsController : Controller
    {
        Demo0Entities data = new Demo0Entities();
        // GET: Accounts
        public ActionResult Index()
        {
            return View(data.Accounts.ToList());
        }

        //This is the action for open the create view
        public ActionResult Create()
        {
            return View();
        }

        // This action is to add data to DB
        [HttpPost]
        public ActionResult Create(Account acc)
        {
            try
            {
                Account accAdd = new Account
                {
                    id = acc.id,
                    name = acc.name,
                    Age = acc.Age,
                    Address = acc.Address,
                    password = acc.password,
                    gender = acc.gender,
                    Phone_Number = acc.Phone_Number
                };

                data.Accounts.Add(accAdd);
                data.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var entityValidationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationErrors.ValidationErrors)
                    {
                        Console.WriteLine($"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}");
                    }
                }
                throw;  // Rethrow the exception after logging the details.
            }
        }


        // This to show data by id of Account
        public ActionResult Edit(int id)
        {
            Account acc = data.Accounts.Find(id);
            return View(acc); // This is the model object return to the Edit View
        }

        // This action to update data from controller to DB
        [HttpPost]
        public ActionResult Edit(Account viewModel)
        {
            if (!ModelState.IsValid)
            {
                // Handle invalid data, perhaps return the view with validation errors.
                return View(viewModel);
            }

            // Retrieve the existing account from the database.
            Account accUpdate = data.Accounts.Find(viewModel.id);

            if (accUpdate == null)
            {
                // Handle the case where the account is not found.
                return HttpNotFound();
            }

            // Update the account properties.
            accUpdate.name = viewModel.name;
            accUpdate.password = viewModel.password;
            accUpdate.gender = viewModel.gender;
            accUpdate.Address = viewModel.Address;
            accUpdate.Age = viewModel.Age;
            accUpdate.Phone_Number = viewModel.Phone_Number;

            // Save changes to the database.
            data.SaveChanges();

            // Redirect to another action (e.g., details page or list page).
            return RedirectToAction("Index");
        }

        // This to show the details of Account Info
        public ActionResult Details(int id)
        {
            // Retrieve the account from the database based on the provided ID.
            Account account = data.Accounts.Find(id);

            if (account == null)
            {
                // Handle the case where the account is not found.
                return HttpNotFound();
            }

            // Pass the account to the view for display.
            return View(account);
        }



        //This is the Actions to show the delete Account
        public ActionResult Delete(int id)
        {
            // Retrieve the account from the database based on the provided ID.
            Account account = data.Accounts.Find(id);

            if (account == null)
            {
                // Handle the case where the account is not found.
                return HttpNotFound();
            }

            // Pass the account to the view for confirmation.
            return View(account);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            // Retrieve the account from the database based on the provided ID.
            Account account = data.Accounts.Find(id);

            if (account == null)
            {
                // Handle the case where the account is not found.
                return HttpNotFound();
            }

            // Remove the account from the database.
            data.Accounts.Remove(account);
            data.SaveChanges();

            // Redirect to another action (e.g., list page) after deletion.
            return RedirectToAction("Index");
        }
    }

    
}