using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebClinica.Models;
using WebClinica.Models.DBClinica;
using System.Web;

namespace WebClinica.Controllers
{
    public class LoginController : Controller
    {
        private readonly DB_CLINICAContext _context = new DB_CLINICAContext();

        public LoginController()//DB_CLINICAContext context)
        {
            //_context = context;
        }

        // GET: Login
        public async Task<IActionResult> Index()
        {
            //var dB_CLINICAContext = _context.LoginUsers.Include(l => l.IdUserNavigation);
            //return View(await dB_CLINICAContext.ToListAsync());
            return View();
        }
        
        public IActionResult LogIn(string user, string pass)
        {
            Users u = new Users();
            ViewBag.islogged = u.UserType.Value;
            return RedirectToAction("Index", "Home");
        }

        // GET: Login/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loginUsers = await _context.LoginUsers
                .Include(l => l.IdUserNavigation)
                .SingleOrDefaultAsync(m => m.IdLoginUser == id);
            if (loginUsers == null)
            {
                return NotFound();
            }

            return View(loginUsers);
        }

        // GET: Login/Create
        public IActionResult Create()
        {
            ViewData["IdUser"] = new SelectList(_context.Users, "IdUser", "IdUser");
            return View();
        }

        // POST: Login/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LoginUser,UserPassword")] LoginUsers loginUsers, string name, string primerapellido, string segundoapellido, string descripcion, int idtype, string idnumbercitizen)
        {
            if (ModelState.IsValid)
            {
                
                Users user = new Users();
                user.UserName = name;
                user.UserLastname1 = primerapellido;
                user.UserLastname2 = segundoapellido;
                user.UserDescription = descripcion;
                user.IdCitizen = idnumbercitizen;
                user.UserType = idtype;
                _context.Add(user);
                await _context.SaveChangesAsync();
                loginUsers.IdUser = user.IdUser;
                _context.Add(loginUsers);
                await _context.SaveChangesAsync();
                ViewData["user"] = loginUsers.LoginUser;
            }
            ViewData["IdUser"] = new SelectList(_context.Users, "IdUser", "IdUser", loginUsers.IdUser);
            return View(loginUsers);
        }

        // GET: Login/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loginUsers = await _context.LoginUsers.SingleOrDefaultAsync(m => m.IdUser == id);
            var user = await _context.Users.SingleOrDefaultAsync(m => m.IdUser == id);
            if (loginUsers == null)
            {
                return NotFound();
            }
            ViewData["IdUser"] = new SelectList(_context.UsersType, "IduserType", "UserTypeDesc", user.UserType.Value);
            return View(loginUsers);
        }

        // POST: Login/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdLoginUser,LoginUser,UserPassword,IdUser")] LoginUsers loginUsers, string name, string primerapellido, string segundoapellido, string descripcion, int idtype, string idnumbercitizen)
        {
            if (id != loginUsers.IdLoginUser)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Users user = new Users();
                    user.IdUser = loginUsers.IdUser.Value;
                    user.UserName = name;
                    user.UserLastname1 = primerapellido;
                    user.UserLastname2 = segundoapellido;
                    user.UserDescription = descripcion;
                    user.IdCitizen = idnumbercitizen;
                    user.UserType = idtype;
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                    loginUsers.IdUser = user.IdUser;
                    _context.Update(loginUsers);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoginUsersExists(loginUsers.IdLoginUser))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdUser"] = new SelectList(_context.Users, "IdUser", "IdUser", loginUsers.IdUser);
            return View(loginUsers);
        }

        // GET: Login/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loginUsers = await _context.LoginUsers
                .Include(l => l.IdUserNavigation)
                .SingleOrDefaultAsync(m => m.IdLoginUser == id);
            if (loginUsers == null)
            {
                return NotFound();
            }

            return View(loginUsers);
        }

        // POST: Login/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var loginUsers = await _context.LoginUsers.SingleOrDefaultAsync(m => m.IdLoginUser == id);
            _context.LoginUsers.Remove(loginUsers);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoginUsersExists(int id)
        {
            return _context.LoginUsers.Any(e => e.IdLoginUser == id);
        }
    }
}
