using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Models.Controllers
{
    public class HomeController : Controller
    {
        private TaskFormContext blahContext { get; set; }
        public HomeController(TaskFormContext someName)
        {
            blahContext = someName;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddEdit ()
        {

            ViewBag.Categories = blahContext.Categories.ToList();

            return View();

        }
        [HttpPost]
        public IActionResult AddEdit (TaskResponse tr)
        {
            if (ModelState.IsValid)
            {
                blahContext.Add(tr);
                blahContext.SaveChanges();

                return View("TaskConfirmation", tr);
            }
            else //If Invalid
            {
                ViewBag.Categories = blahContext.Categories.ToList();

                return View(tr);
            }
        }

        //[HttpGet]
        //public IActionResult TaskList ()
        //{
        //    var tasks = blahContext.responses
        //        .Where(blah => blah.Completed == false)
        //        .Include(x => x.Category)
        //        .ToList();

        //    return View(tasks);
        //}

        public IActionResult Quadrants()
        {
            var taskList = blahContext.responses
                .Include(x => x.Category)
                .Where(x => x.Completed == false)
                .ToList();
            return View(taskList);
        }

        [HttpGet]
        public IActionResult Edit(int taskid)
        {
            ViewBag.Categories = blahContext.Categories.ToList();

            var form = blahContext.responses.Single(x => x.TaskId == taskid);

            return View("AddEdit", form);
        }

        [HttpPost]
        public IActionResult Edit (TaskResponse blah)
        {
            blahContext.Update(blah);
            blahContext.SaveChanges();

            return RedirectToAction("Quadrants");
        }

        [HttpGet]
        public IActionResult Delete(int taskid)
        {
            var form = blahContext.responses.Single(x => x.TaskId == taskid);

            return View(form);
        }

        [HttpPost]
        public IActionResult Delete(TaskResponse tr)
        {
            blahContext.responses.Remove(tr);
            blahContext.SaveChanges();

            return RedirectToAction("Quadrants");
        }

        public IActionResult Completed(int taskid)
        {
            var task = blahContext.responses.Single(x => x.TaskId == taskid);
            task.Completed = true;
            blahContext.responses.Update(task);
            blahContext.SaveChanges();
            return RedirectToAction("Quadrants");
        }

    }
}
