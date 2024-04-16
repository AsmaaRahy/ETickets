using ETickets.IRepository;
using ETickets.Models;
using Microsoft.AspNetCore.Mvc;

namespace ETickets.Controllers
{
    public class CategoryController : Controller
    {
        ICategoryRepository categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository) {
            this.categoryRepository = categoryRepository;
        }
        public IActionResult Index()
        {
            var listOfCategories = categoryRepository.ReadAll();
            return View("Index" ,listOfCategories);
        }

        public IActionResult ShowAll(int id)
        {
            var movies = categoryRepository.ReadAllMovies(id);
            
            //var moviesInCat = categoryRepository.ReadAllMovies(id);
            return View("ShowAll", movies);
        }

    }
}
