using ETickets.Data;
using ETickets.IRepository;
using ETickets.Models;
using Microsoft.EntityFrameworkCore;

namespace ETickets.Repository
{
    public class CinemaRepository : ICinemaRepository
    {
        ApplicationDbContext context;
        public CinemaRepository(ApplicationDbContext context)
        {
            this.context= context;
        }

        public void Create(Cinema cinema)
        {
            context.Cinemas.Add(cinema);
            context.SaveChanges();
        }

        public void Update(Cinema cinema)
        {
            var cinema1 = context.Cinemas.Find(cinema.Id);

            if (cinema1 != null)
            {
                cinema1.Name = cinema.Name;
                context.SaveChanges();
            }


        }

        public List<Cinema> ReadAll()
        {
            return context.Cinemas.ToList();
        }

        public List<Movie> ReadAllMovies(int id)
        {

            var ListOfMovies = context.Movies.Include(e => e.Cinema).Include(e => e.Category).Where(e => e.CinemaId == id).ToList();

            return ListOfMovies;


        }



        public Cinema ReadById(int id)
        {
            return context.Cinemas.Find(id);
        }

        public void Delete(int id)
        {
            var cinema = context.Cinemas.Find(id);

            if (cinema != null)
            {
                context.Cinemas.Remove(cinema);
                context.SaveChanges();
            }
        }

        
    }
}
