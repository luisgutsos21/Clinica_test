using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebClinica.Models;
using WebClinica.Models.DBClinica;
using Microsoft.EntityFrameworkCore;

namespace WebClinica.Controllers
{
    public class HomeController : Controller
    {
        DB_CLINICAContext ctx = new DB_CLINICAContext();
        public IActionResult Index()
        {
            var dB_CLINICAContext = ctx.Citas.Include(c => c.IdTipoCitaNavigation);
            return View(dB_CLINICAContext.ToList());
        }

        public IActionResult About()
        {
            ViewData["Message"] = "";
            Users user = ctx.Users.FirstOrDefault(i => i.UserName == "Rogelio");
            userinfo uinfo = new userinfo();
            uinfo.nombre = user.UserName;
            uinfo.apellidos = user.UserLastname1 + " " + user.UserLastname2;
            uinfo.informacion = user.UserDescription;
            return View(uinfo);
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public JsonResult GetUserRole(string user)
        {
            int id = ctx.Users.FirstOrDefault(c => c.UserName == "Rogelio").IdUser;
            return new JsonResult(new { idrole = id });
        }
    }
}
