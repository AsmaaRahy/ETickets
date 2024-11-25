using ETickets.Data;
using ETickets.IRepository;
using Microsoft.AspNetCore.Mvc;
using ETickets.Data.Enums;

using Microsoft.AspNetCore.Mvc.Rendering;
using ETickets.ViewModel;
using ETickets.Models;
using ETickets.Repository;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace ETickets.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMovieRepository movieRepository;
        ICinemaRepository cinemaRepository;
        ICategoryRepository categoryRepository;
        IActorRepository actorRepository;
        public MovieController(IMovieRepository movieRepository, ICinemaRepository cinemaRepository, ICategoryRepository categoryRepository, IActorRepository actorRepository)
        {
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

            return View("Index", listOfMovies);
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            var movie = movieRepository.ReadAllWithCinemaAndCatAndActors().First(e => e.Id == id);
            return View(movie);



        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateNew()
        {

            var movie = new MovieVM();
            ViewData["listOfCategories"] = categoryRepository.ReadAllCat();
            ViewData["listOfCinema"] = cinemaRepository.ReadAll();
            ViewData["listOfActors"] = actorRepository.ReadAll();
            //movie.Actors = actorRepository.ReadAll();



            return View(movie);
        }

        public IMovieRepository GetMovieRepository()
        {
            return movieRepository;
        }

        [HttpPost]

        public IActionResult SaveNew(MovieVM movieVM, List<Actor> m)
        {
            if (string.IsNullOrEmpty(movieVM.ImgUrl))
            {
                movieVM.ImgUrl = "~/images/movies/movie2";
            }
            if (ModelState.IsValid)
            {
                Movie movie = new Movie();
                movie.Name = movieVM.Name;
                movie.Description = movieVM.Description;
                movie.Price = movieVM.Price;
                movie.StartDate = movieVM.StartDate;
                movie.EndDate = movieVM.EndDate;
                movie.ImgUrl = movieVM.ImgUrl;
                movie.TrailerUrl = movieVM.TrailerUrl;
                movie.CategoryId = movieVM.CategoryId;
                movie.CinemaId = movieVM.CinemaId;
                
                movie.Actors = movieRepository.GetAllActors().Where(a => movieVM.SelectedActorIds.Contains(a.Id)).ToList() ;
                //movieVM.AllActors = movieRepository.GetAllActors().ToList();
                //movieVM.AllActors = context.Actors.ToList();


                //movie.Actors = actorRepository.ReadAll().Where(a => movieVM.SelectedActorIds.Contains(a.Id)).ToList();


                movieRepository.Create(movie);

                return RedirectToAction("Index");

            }
            ViewData["listOfCategories"] = categoryRepository.ReadAllCat();
            ViewData["listOfCinema"] = cinemaRepository.ReadAll();
            ViewData["listOfActors"] = actorRepository.ReadAll();

            return View("CreateNew", movieVM);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            movieRepository.Delete(id);

            return RedirectToAction("Index");


        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id, Movie mov)
        {
            Movie? movie = movieRepository.ReadById(id);

            if (movie == null)
            {
                return RedirectToAction("Index");
            }

            MovieVM movieVM = new MovieVM()
            {
                Id = movie.Id,
                Name = movie.Name,
                Description = movie.Description,
                Price = movie.Price,
                StartDate = movie.StartDate,
                EndDate = movie.EndDate,
                ImgUrl = movie.ImgUrl,
                TrailerUrl = movie.TrailerUrl,
                CategoryId = movie.CategoryId,
                CinemaId = movie.CinemaId,
                //SelectedActorIds = movie.Actors.Select(a => a.Id).ToList()

              //SelectedActorIds = movieRepository.GetAllActors().Where(a => movie.Actors.Contains(a.Id)).ToList()


            };
            //movieVM.MovieStatus = CalculateMovieStatus(movieVM.StartDate, movieVM.EndDate);
            ViewData["listOfCategories"] = categoryRepository.ReadAllCat();
            ViewData["listOfCinema"] = cinemaRepository.ReadAll();
            //ViewData["listOfActors"] = actorRepository.ReadAll();

            return View(movieVM);
        }

        [HttpPost]
        public IActionResult SaveEdit(Movie movie)
        {
            if(movie.Name== null)
            {
                ViewData["listOfCategories"] = categoryRepository.ReadAllCat();
                ViewData["listOfCinema"] = cinemaRepository.ReadAll();
                return View("Edit", movie);

            }
            movieRepository.Update(movie);
            return RedirectToAction("Index");

        }

        //private MovieStatus CalculateMovieStatus(DateTime startDate, DateTime endDate)
        //{
        //    DateTime today = DateTime.Today;

        //    if (endDate > today)
        //    {
        //        return MovieStatus.Available;
        //    }
        //    else if (startDate > today)
        //    {
        //        return MovieStatus.Upcoming;
        //    }
        //    else
        //    {
        //        return MovieStatus.Expired;
        //    }
        //}

        [HttpGet]
        public IActionResult Search(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                return RedirectToAction("Index");
            }

            // Search for movies based on the query
            var results = movieRepository.ReadAllWithCinemaAndCatAndActors()
                .Where(m => m.Name.Contains(query, StringComparison.OrdinalIgnoreCase))
                .ToList();

            // If no movies are found, return to the search view with a message
            if (results.Count == 0)
            {
                ViewBag.Message = "No movies found.";
                return View("Search", results);
            }

            // If one movie is found, redirect to its Details page
            if (results.Count == 1)
            {
                return RedirectToAction("Details", new { id = results.First().Id });
            }

            // If multiple movies are found, return the Search view
            return View("Search", results);
        }



    }
}
