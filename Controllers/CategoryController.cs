using ETickets.IRepository;
using ETickets.Models;
using ETickets.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETickets.Controllers
{
    public class CategoryController : Controller
    {
        ICategoryRepository categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }
        public IActionResult Index()
        {
            var listOfCategories = categoryRepository.ReadAllCat();
            return View("Index", listOfCategories);
        }

        public IActionResult ShowAll(int id)
        {
            var result = categoryRepository.ReadAllMovies(id);
            return View("PartialView/_ShowAllMoviesPartial", result);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateNew()
        {

            var category = new CategoryVM();

            return View(category);
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult SaveNew(CategoryVM categoryVM)
        {
            if (ModelState.IsValid)
            {
                Category cat = new Category();
                cat.Name = categoryVM.Name;


                categoryRepository.Create(cat);

                

                return RedirectToAction("Index");

            }
            return View("CreateNew", categoryVM);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            Category? category = categoryRepository.ReadById(id);

            if (category == null)
            {
                return RedirectToAction("Index");
            }

            CategoryVM categoryVM = new CategoryVM() { Id = category.Id,Name = category.Name};

            return View(categoryVM);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult SaveEdit(Category category)
        {
            if (category.Name == null)
            {
                return View("Edit", category);
            }

            categoryRepository.Update(category);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            categoryRepository.Delete(id);

            return RedirectToAction("Index");

            
        }

    }
}
