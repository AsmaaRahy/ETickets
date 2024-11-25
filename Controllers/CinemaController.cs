using ETickets.IRepository;
using ETickets.Models;
using ETickets.Repository;
using ETickets.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETickets.Controllers
{
    public class CinemaController : Controller
    {
        ICinemaRepository cinemaRepository;
        public CinemaController(ICinemaRepository cinemaRepository)
        {
            this.cinemaRepository = cinemaRepository;
        }
        public IActionResult Index()
        {
            var listOfCinema = cinemaRepository.ReadAll();
            return View("Index", listOfCinema);
            
        }
        public IActionResult ShowAll(int id)
        {
            var result = cinemaRepository.ReadAllMovies(id);
            return View("PartialView/_ShowAllMoviesPartial", result);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateNew()
        {

            var cinema = new CinemaVM();

            return View(cinema);
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult SaveNew(CinemaVM cinemaVM)
        {
            if (ModelState.IsValid)
            {
                Cinema cinema = new Cinema();
                cinema.Name = cinemaVM.Name;
                cinema.Description = cinemaVM.Description;
                cinema.Address = cinemaVM.Address;
                cinema.CinemaLogo = cinemaVM.CinemaLogo;

                cinemaRepository.Create(cinema);

                return RedirectToAction("Index");

            }
            return View("CreateNew", cinemaVM);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
           Cinema? cinema = cinemaRepository.ReadById(id);

            if (cinema == null)
            {
                return RedirectToAction("Index");
            }

            CinemaVM cinemaVM = new CinemaVM() {
                Id = cinema.Id,
                Name = cinema.Name,
                Description = cinema.Description,
                Address = cinema.Address,
                CinemaLogo =cinema.CinemaLogo
            };

            return View(cinemaVM);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult SaveEdit(Cinema cinema)
        {
            if (cinema.Name == null)
            {
                return View("Edit", cinema);
            }

            cinemaRepository.Update(cinema);
            return RedirectToAction("Index");
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            cinemaRepository.Delete(id);

            return RedirectToAction("Index");


        }

    }
}
