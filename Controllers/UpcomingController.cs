using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System.IO;
using WATCHWAWE.Models;
using WATCHWAWE.Services;

namespace WATCHWAWE.Controllers
{
    public class UpcomingController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment environment;

        public UpcomingController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            this.context = context;
            this.environment = environment;
        }
        public IActionResult Index()
        {
            var Upcoming = context.Upcomings.ToList();
            return View(Upcoming);
        }

        public IActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(UpcomingDto upcomingDto)
        {
            if (upcomingDto.ImageFile == null) 
            {
                ModelState.AddModelError("ImageFile", "File Required");
            }

            if (!ModelState.IsValid) 
            {
                return View(upcomingDto);
            }

            string newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");

            newFileName += Path.GetExtension(upcomingDto.ImageFile!.FileName);

            string imageFullPath = environment.WebRootPath + "/upcomings/" + newFileName; 
            using (var stream = System.IO.File.Create(imageFullPath))
            {

                upcomingDto.ImageFile.CopyTo(stream);

            }

            Upcoming upcoming = new Upcoming()
            {
                Name = upcomingDto.Name,
                Genre = upcomingDto.Genre,
                ImageFileName = newFileName,
                CreatedAT = DateTime.Now,
            };

            context.Upcomings.Add(upcoming);
            context.SaveChanges();

            return RedirectToAction("Index","Upcoming");
        }

        public IActionResult Edit( int Id) 
        {
            var upcoming = context.Upcomings.Find(Id);

            if (upcoming == null)
            {
                return RedirectToAction("Index", "upcoming");
            }

            var upcomingDto = new UpcomingDto()
            {
                Name = upcoming.Name,
                Genre = upcoming.Genre,
            };

            ViewData["UpcomingId"] = upcoming.Id;
            ViewData["ImageFileName"] = upcoming.ImageFileName;
            ViewData["CreatedAT"] = upcoming.CreatedAT.ToString("mm/dd/yyyy");

            return View(upcomingDto);
        }

        [HttpPost]
        public IActionResult Edit(int Id, UpcomingDto upcomingDto) 
        {
            var upcoming = context.Upcomings.Find(Id);
            if (upcoming == null) 
            {
                return RedirectToAction("Index", "Upcoming");
            }

            if (!ModelState.IsValid)
            {
                ViewData["UpcomingId"] = upcoming.Id;
                ViewData["ImageFileName"] = upcoming.ImageFileName;
                ViewData["CreatedAT"] = upcoming.CreatedAT.ToString("mm/dd/yyyy");

                return View(upcomingDto);
            }

            string newFileName = upcoming.ImageFileName;

            if (upcomingDto.ImageFile != null)

            {

                newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");

                newFileName += Path.GetExtension(upcomingDto.ImageFile.FileName);

                string imageFullPath = environment.WebRootPath + "/upcomings/" + newFileName;

                using (var stream = System.IO.File.Create(imageFullPath))

                {
                    upcomingDto.ImageFile.CopyTo(stream);
                }
                // delete the old image

                string oldImageFullPath = environment.WebRootPath + "/upcomings/" + upcoming.ImageFileName; 
                System.IO.File.Delete(oldImageFullPath);

            }

            upcoming.Name = upcomingDto.Name;
            upcoming.Genre = upcomingDto.Genre;
            upcoming.ImageFileName = newFileName;

            context.SaveChanges();

            return RedirectToAction("Index", "Upcoming");
        }

        public IActionResult Delete(int Id) 
        {
            var upcoming = context.Upcomings.Find(Id);
            if (upcoming == null) 
            {
                return RedirectToAction("Index", "Upcoming");
            }

            string imageFullPath = environment.WebRootPath + "/upcomings/" + upcoming.ImageFileName;
            System.IO.File.Delete(imageFullPath);

            context.Upcomings.Remove(upcoming);
            context.SaveChanges();

            return RedirectToAction("Index", "Upcoming");
        }
    }
}
