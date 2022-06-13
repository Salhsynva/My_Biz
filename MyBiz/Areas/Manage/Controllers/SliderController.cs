using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MyBiz.DAL;
using MyBiz.Helpers;
using MyBiz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBiz.Areas.Manage.Controllers
{
    [Area("manage")]
    public class SliderController : Controller
    {
        private readonly MybizDbContext _context;
        private readonly IWebHostEnvironment _web;

        public SliderController(MybizDbContext context, IWebHostEnvironment web)
        {
            this._context = context;
            this._web = web;
        }
        
        public IActionResult Index()
        {
            List<Slider> sliders = _context.Sliders.ToList();
            return View(sliders);
        }
    
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Slider slider)
        {

            if (slider.ImageFile!=null)
            {
                if(slider.ImageFile.ContentType != "image/png" && slider.ImageFile.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("ImageFile", "image type must be png or jpeg");
                }
                if(slider.ImageFile.Length > 2097152)
                {
                    ModelState.AddModelError("ImageFile", "image lenth must be less than 2mb");
                }
            }
            else
            {
                ModelState.AddModelError("ImageFile", "image required");
            }


            if (!ModelState.IsValid)
            {
                return View();
            }

            slider.Image = FileManager.Save(_web.WebRootPath, "uploads/sliders", slider.ImageFile);

            _context.Sliders.Add(slider);
            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Edit(int id)
        {
            Slider slider = _context.Sliders.FirstOrDefault(x => x.Id == id);
            if (slider == null)
                return Content("error");

            return View(slider);
        }

        [HttpPost]
        public IActionResult Edit(Slider slider)
        {
            Slider existSl = _context.Sliders.FirstOrDefault(x => x.Id == slider.Id);
            if (existSl == null)
                return Content("error");

            if (slider.ImageFile != null)
            {
                if (slider.ImageFile.ContentType != "image/png" && slider.ImageFile.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("ImageFile", "image type must be png or jpeg");
                }
                if (slider.ImageFile.Length > 2097152)
                {
                    ModelState.AddModelError("ImageFile", "image lenth must be less than 2mb");
                }

                if (!ModelState.IsValid)
                    return View();

                existSl.Image = FileManager.Save(_web.WebRootPath, "uploads/sliders", slider.ImageFile);

            }
            existSl.Title = slider.Title;
            existSl.Desc = slider.Desc;
            existSl.BtnUrl = slider.BtnUrl;
            existSl.BtnText = slider.BtnText;
            existSl.Title = slider.Title;

            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Delete(int id)
        {
            Slider slider = _context.Sliders.FirstOrDefault(x => x.Id == id);
            if (slider == null)
                return Content("errror");

            return View(slider);
        }

        [HttpPost]
        public IActionResult Delete(Slider slider)
        {
            Slider existSl = _context.Sliders.FirstOrDefault(x => x.Id == slider.Id);
            if (existSl == null)
                return Content("error");

            FileManager.Delete(_web.WebRootPath, "uploads/sliders", existSl.Image);

            _context.Sliders.Remove(existSl);
            _context.SaveChanges();

            return RedirectToAction("index");
        }




    }
}
