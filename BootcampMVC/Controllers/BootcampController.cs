﻿using BootcampMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace BootcampMVC.Controllers
{
    public class BootcampController : Controller
    {
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("list","bootcamp");
            }

            var kurs = Repository.GetById(id);

            return View(kurs);
        }
        public IActionResult List()
        {

            return View(Repository.Bootcamps);
        }

    }
}
