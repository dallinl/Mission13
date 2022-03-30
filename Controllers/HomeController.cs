using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mission13.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Mission13.Controllers
{
    public class HomeController : Controller
    {
        private BowlersDbContext _context { get; set;}
        
        public HomeController(BowlersDbContext temp)
        {
            _context = temp;
        }
        
        // Home Page
        [HttpGet]
        public IActionResult Index(string teamName)
        {
            var bowlers = _context.Bowlers
                .Include(x => x.Team)
                .Where(t => t.Team.TeamName == teamName || teamName ==null)
                .ToList();

            return View(bowlers);
        }


        // CRUD Functionality
        [HttpGet]
        public IActionResult Create()
        {

            ViewBag.Teams = _context.Teams.ToList();
            return View(); 
        }

        [HttpPost]
        public IActionResult Create(Bowler b)
        {
            b.BowlerID = (_context.Bowlers.ToList().Capacity + 1);
            _context.Add(b);
            _context.SaveChanges();
            return View("Confirmation");
        }

        [HttpGet]
        public IActionResult Edit(int bowlerid)
        {
            ViewBag.Teams = _context.Teams.ToList();

            var bowler = _context.Bowlers.Single(x => x.BowlerID == bowlerid);

            return View("Create", bowler);
        }

        [HttpPost]
        public IActionResult Edit(Bowler b)
        {
            _context.Update(b);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int bowlerid)
        {
            var b = _context.Bowlers.Single(x => x.BowlerID == bowlerid);

            return View(b);
        }

        [HttpPost]
        public IActionResult Delete(Bowler b)
        {
            _context.Bowlers.Remove(b);
            _context.SaveChanges();

            return RedirectToAction("Index");

        }

    }
}
