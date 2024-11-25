using ETickets.IRepository;
using ETickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace ETickets.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMovieRepository movieRepository;
        ICinemaRepository cinemaRepository;
        ICategoryRepository categoryRepository;
        IActorRepository actorRepository;
      
        public HomeController(ILogger<HomeController> logger, IMovieRepository movieRepository, ICinemaRepository cinemaRepository, ICategoryRepository categoryRepository, IActorRepository actorRepository)
        {
            _logger = logger;
            this.movieRepository = movieRepository;
            this.cinemaRepository = cinemaRepository;
            this.categoryRepository = categoryRepository;
            this.actorRepository = actorRepository;
        }

        public IActionResult Index()
        {
            var listOfMovies = movieRepository.ReadAllWithCinemaAndCat();

            var movieStatusList = new SelectList(Enum.GetValues(typeof(ETickets.Data.Enums.MovieStatus)));

            // Pass movies and SelectList to the view

            ViewData["MovieStatusList"] = movieStatusList;

            return View("Index",listOfMovies);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
