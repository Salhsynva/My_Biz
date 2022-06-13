using Microsoft.AspNetCore.Mvc;
using MyBiz.DAL;
using MyBiz.Models;
using MyBiz.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBiz.Controllers
{
    public class HomeController : Controller
    {
        private readonly MybizDbContext _context;

        public HomeController(MybizDbContext context)
        {
            this._context = context;
        }
        public IActionResult Index()
        {
            List<Slider> sliders = _context.Sliders.ToList();

            HomeViewModels homeVM = new HomeViewModels
            {
                Sliders = sliders
            };
            return View(homeVM);
        }
    }
}
