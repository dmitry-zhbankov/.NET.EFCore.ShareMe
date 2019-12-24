using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ShareMe.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Process(string username, string password)
        {
            if (username != null && password != null && username.Equals("admin") && password.Equals("123"))
            {
                HttpContext.Session.SetString("username", username);
                return RedirectToAction("Index", "Home", new{area="Admin"});
            }
            else
            {
                ViewBag.Error = "Invalid";
                return View("Index");
            }
        }
    }
}