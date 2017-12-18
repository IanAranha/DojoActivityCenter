using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dojoactivitycenter.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace dojoactivitycenter.Controllers
{   
    public class HomeController : Controller
    {
        private DojoActivityCenterContext _context;
 
        public HomeController(DojoActivityCenterContext context)
        {
        _context = context;
        }

        
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register(RegisterUserModel formData)
        {   
            if(ModelState.IsValid)
            {   
                User registerdedCheck = _context.Users.SingleOrDefault(dbu => dbu.Email == formData.Email);
                
                if(registerdedCheck == null)
                
                {   
                    PasswordHasher<User> Hasher = new PasswordHasher<User>();
                    
                    User user = new User();
                    user.FirstName = formData.FirstName;
                    user.LastName = formData.LastName;
                    user.Email = formData.Email;
                    user.Password = Hasher.HashPassword(user, formData.Password);
                    
                    _context.Users.Add(user);
                    _context.SaveChanges();

                    int UserId = _context.Users.Last().UserId;
                    HttpContext.Session.SetInt32("loggedPerson", UserId);
                    return RedirectToAction("Dashboard", "DojoActivity", UserId);

                    } 
                    else
                        {      
                            TempData["Unique"] = "Email already in use";
                            return RedirectToAction("Index");
                        }   
                }
            return View("Index");
        }

        [HttpGet]
        [Route("Login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginUserModel formData)
        {   
     
            HttpContext.Session.Clear();
            User loggedUser = _context.Users.Where(x => x.Email == formData.LoginEmail).SingleOrDefault();
            
            if(loggedUser == null)
            {
                TempData["emailerror"] = "Email is not registered, please register first";
                return RedirectToAction("Login");
            } 
            else 
            {
                PasswordHasher<LoginUserModel> hasher = new PasswordHasher<LoginUserModel>();    
                if(0 != hasher.VerifyHashedPassword(formData, loggedUser.Password, formData.LoginPassword))
                {
                    HttpContext.Session.SetInt32("loggedPerson", (int)loggedUser.UserId);
                    return RedirectToAction("Dashboard", "DojoActivity");
                }
                else
                {
                    return View("Index");
                }
            }
        }

        [HttpGet]
        [Route("Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return View("Index");
        }


    }
}
