using ETickets.Data;
using ETickets.IRepository;
using ETickets.Models;
using Microsoft.EntityFrameworkCore;

namespace ETickets.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
         ApplicationDbContext context;

        public CategoryRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public void Create(Category category)
        {
            context.Categories.Add(category);
            context.SaveChanges();
        }

        public void Update(Category category)
        {
            var cat = context.Categories.Find(category.Id);

            if (cat != null)
            {
                cat.Name = category.Name;
                context.SaveChanges();
            }


        }

        public List<Category> ReadAllCat()
        {
            return context.Categories.ToList();
        }

        public List<Movie> ReadAllMovies(int id)
        {

            var ListOfMovies = context.Movies.Include(e => e.Cinema).Include(e => e.Category).Where(e => e.CategoryId == id).ToList();

            return ListOfMovies;


        }



        public Category ReadById(int id)
        {
            return context.Categories.Find(id);
        }

        public void Delete(int id)
        {
            var cat = context.Categories.Find(id);

            if (cat != null)
            {
                context.Categories.Remove(cat);
                context.SaveChanges();
            }
        }

        
        
        
    }
}
