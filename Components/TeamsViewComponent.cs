using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mission13.Models;

namespace Mission13.Components
{
    public class TeamsViewComponent : ViewComponent
    {
        private BowlersDbContext repo { get; set; }

        public TeamsViewComponent (BowlersDbContext temp)
        {
            repo = temp;
        }

        public IViewComponentResult Invoke()
        {
            // get the team name from routedata
            ViewBag.SelectedTeam = RouteData?.Values["teamName"];

            var teams = repo.Teams
                .Select(x => x.TeamName)
                .Distinct()
                .OrderBy(x => x);

            return View(teams); 
        }
    }
}
