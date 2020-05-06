using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EGUI_L2.Models;


namespace EGUI_L2.Controllers
{
    public class CalendarController : Controller
    {

        public IActionResult Calendar(int? year, int? month)
        {
            EventsAccess.ReadEvents();

            var date = new DateTime(year ?? DateTime.Now.Year, month ?? DateTime.Now.Month, 1);
            bool curr_month = false;
            if (DateTime.Now.Year == date.Year && DateTime.Now.Month == date.Month)
            {
                curr_month = true;
            }
            CalendarViewModel cm = new CalendarViewModel
            {
                shift = (int)date.DayOfWeek,
                header = date.ToString("MMM - yyyy"),
                days_in_month = DateTime.DaysInMonth(date.Year, date.Month),
                curr_day = curr_month ? DateTime.Now.Day : 0,
                highlight_days = EventsAccess.get_highlights(date)
            };
            return View(cm);
        }

        public IActionResult Day(int? year, int? month, int? day)
        {
            
            var date = new DateTime(year ?? DateTime.Now.Year, month ?? DateTime.Now.Month, day ?? DateTime.Now.Day);
            DayViewModel dm = new DayViewModel
            {
                header = "Events for " + date.ToString("dd.MM.yyyy"),
                events = EventsAccess.GetEvents(date).OrderBy(e => e.time).ToArray()
            };
            return View(dm);
        }

        public IActionResult Event(int? year, int? month, int? day, Guid? id)
        {
            var date = new DateTime(year ?? DateTime.Now.Year, month ?? DateTime.Now.Month, day ?? DateTime.Now.Day);
            var edited = id.HasValue;
            EventModel new_e = null;
            if (edited)
            {
                new_e = EventsAccess.GetEvent(date, id.Value);
            }
            EventViewModel em = new EventViewModel
            {
                header = date.ToString("dd.MM.yyyy"),
                time = edited ? new_e.time : DateTime.MinValue,
                desc = edited ? new_e.desc : "",
                edited = edited
            };
            return View(em);
        }

        [HttpPost]
        public IActionResult Add(DateTime time, string desc, int? year, int? month, int? day)
        {
            var date = new DateTime(year ?? DateTime.Now.Year, month ?? DateTime.Now.Month, day ?? DateTime.Now.Day, time.Hour, time.Minute, time.Second);
            
            EventsAccess.AddEvent(date, desc);
            return RedirectToAction(nameof(Day), new { year, month, day });
        }

        [HttpPost]
        public IActionResult Edit(DateTime time, string desc, int? year, int? month, int? day, Guid? id)
        {
            var date = new DateTime(year ?? DateTime.Now.Year, month ?? DateTime.Now.Month, day ?? DateTime.Now.Day, time.Hour, time.Minute, time.Second);
 
            EventsAccess.EditEvent(date, desc, id.Value);
            return RedirectToAction(nameof(Day), new { year, month, day });
        }

        [HttpPost]
        public IActionResult Delete(int? year, int? month, int? day, Guid? id)
        {
            var date = new DateTime(year ?? DateTime.Now.Year, month ?? DateTime.Now.Month, day ?? DateTime.Now.Day);
            EventsAccess.DeleteEvent(date, id.Value);
            return RedirectToAction(nameof(Day), new { year, month, day });
        }



    }

}