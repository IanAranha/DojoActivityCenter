using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using dojoactivitycenter.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


namespace dojoactivitycenter.Controllers
{
    public class DojoActivityController : Controller
    {

        private DojoActivityCenterContext _context;
       
        public DojoActivityController(DojoActivityCenterContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("Dashboard")]
        public IActionResult Dashboard()
        {
                int? loggedperson = HttpContext.Session.GetInt32("loggedPerson");
                if (loggedperson == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                var allActivity = _context.Activities.Include(a => a.Planner).Include(b => b.Participants).OrderBy(c => c.ActivityDate).OrderBy(d => d.ActivityTime).ToList();
                ViewBag.activities = allActivity;
                User findtheperson = _context.Users.SingleOrDefault(x => x.UserId == loggedperson);
                ViewBag.User = findtheperson;
                return View();
            }
        }

        [HttpGet]
        [Route("CreateActivity")]
        public IActionResult CreateActivity()
        {
            return View();
        }

        [HttpPost]
        [Route("AddActivity")]
        public IActionResult AddActivity(Activity activityData)
        {
            int? loggedPerson = HttpContext.Session.GetInt32("loggedPerson");
            if (loggedPerson == null)
            {
                return RedirectToAction("Index", "Home");
            } 
            else 
            if(ModelState.IsValid)
            {
                Activity newActivity = new Activity
                {
                    ActivityTitle = activityData.ActivityTitle,
                    ActivityTime = activityData.ActivityTime,
                    ActivityDate = activityData.ActivityDate,
                    ActivityDuration = activityData.ActivityDuration,
                    Duration =activityData.Duration,
                    ActivityDescription=activityData.ActivityDescription,
                    UserId=(int)HttpContext.Session.GetInt32("loggedPerson") 
                };
                _context.Activities.Add(newActivity);
                _context.SaveChanges();
                
                return RedirectToAction("ShowOneActivityPage");
            }

            return View("CreateActivity");
        }

        [HttpGet]
        [Route("showOne")]
        public IActionResult ShowOneActivityPage()
        {
            var cur_Id = _context.Activities.Last().ActivityId;
            var oneActivity = _context.Activities.Include(w => w.Participants).Include(x => x.Planner).Where(y => y.ActivityId == cur_Id).SingleOrDefault();
            ViewBag.oneactivity = oneActivity;
            return View("AddActivity");
        }

        [HttpGet]
        [Route("show/{ActivityId}")]
        public IActionResult Show(int ActivityId)
        {
            var oneActivity = _context.Activities.Include(w => w.Participants).Include(x => x.Planner).Where(y => y.ActivityId == ActivityId).SingleOrDefault();
            ViewBag.oneactivity = oneActivity;
            return View("AddActivity");
        }


        [HttpGet]
        [Route("AddActivity")]
        public IActionResult AddActivity()
        {
            int? loggedPerson = HttpContext.Session.GetInt32("loggedPerson");
            if (loggedPerson == null)
            {
                return RedirectToAction("Index", "Home");
            } 
            else 
            return View("AddActivity");
            }


        [HttpGet]
        [Route("deleteactivity/{ActivityId}")]
        public IActionResult Delete( int ActivityId)
        {
            int? loggedPerson = HttpContext.Session.GetInt32("loggedPerson");
            if (loggedPerson == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                var deleteActivity = _context.Activities.Include(w => w.Participants).ThenInclude(g => g.Participant).Where(w => w.ActivityId == ActivityId).SingleOrDefault();
                _context.Remove(deleteActivity);
                _context.SaveChanges();
                return RedirectToAction("Dashboard");
            }
        }

        [HttpGet]
        [Route("joinactivity/{ActivityId}")]
        public IActionResult Join(int ActivityId)
        {   
            int? loggedPerson = HttpContext.Session.GetInt32("loggedPerson");
            if (loggedPerson == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {   
                var happeningAt = _context.Activities.Include(a => a.Planner).ToList();
                UserActivity activity = new UserActivity 
            {                
                UserId = (int) loggedPerson,
                ActivityId = ActivityId
            };

            _context.UsersActivities.Add(activity);
            _context.SaveChanges();
            return RedirectToAction("Dashboard");
            }
        }

        [HttpGet]
        [Route("leaveactivity/{ActivityId}")]
        public IActionResult Leave(int ActivityId)
        {   
            int? loggedPerson = HttpContext.Session.GetInt32("loggedPerson");
            if (loggedPerson == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                var leave = _context.UsersActivities.Where(w => w.UserActivitiesId == ActivityId).Where(g => g.UserId == loggedPerson).SingleOrDefault();
                _context.UsersActivities.Remove(leave);
                _context.SaveChanges();
                return RedirectToAction("Dashboard");
            }
        }
    }
}
    