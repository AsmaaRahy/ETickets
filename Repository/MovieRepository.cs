using ETickets.Data;
using ETickets.Data.Enums;
using ETickets.IRepository;
using ETickets.Models;
using ETickets.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ETickets.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private readonly ApplicationDbContext context;

        public MovieRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public void Create(Movie movie)
        {
            var today = DateTime.Now;
            if (movie.StartDate > today)
            {
                movie.MovieStatus = MovieStatus.Upcoming;
            }
            else if (movie.EndDate >= today)
            {
                movie.MovieStatus = MovieStatus.Available;
            }
            else
            {
                movie.MovieStatus = MovieStatus.Expired;
            }
            //var m=context.Movies.Include(e => e.Actors).ToList();
            context.Movies.Add(movie);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var movie = context.Movies.Find(id);

            if (movie != null)
            {
                context.Movies.Remove(movie);
                context.SaveChanges();
            }
        }
        

        public List<Movie> ReadAll()
        {
            return context.Movies.ToList();
        }

        

        public Movie ReadById(int id)
        {
            return context.Movies.Find(id);
        }

        public List<Movie> ReadAllWithCinemaAndCat()
        {

            return context.Movies.Include(e => e.Cinema).Include(e => e.Category).ToList();

        }

        //public List<Movie> ReadAllActor()
        //{
        //    return context.Movies.Include(e => e.Actors).ToList();
        //}

        public void Update(Movie movie)
        {
            var mov = context.Movies.Find(movie.Id);

            if (mov != null)
            {
                mov.Name = movie.Name;
                mov.Price = movie.Price;
                mov.Description = movie.Description;
                mov.StartDate = movie.StartDate;
                mov.EndDate = movie.EndDate;
                mov.CategoryId = movie.CategoryId;
                mov.CinemaId = movie.CinemaId;
                var today = DateTime.Now;
                if (movie.StartDate > today)
                {
                    movie.MovieStatus = MovieStatus.Upcoming;
                }
                else if (movie.EndDate >= today)
                {
                    movie.MovieStatus = MovieStatus.Available;
                }
                else
                {
                    movie.MovieStatus = MovieStatus.Expired;
                }
                mov.MovieStatus = movie.MovieStatus;
                context.SaveChanges();
            }

        }

        public List<Movie> ReadAllWithCinemaAndCatAndActors()
        {
            return context.Movies
             .Include(e => e.Cinema)
   .Include(e => e.Category)
   .Include(e => e.Actors)

   .ToList();
        }


        public List<Actor> GetAllActors()
        {
            return context.Actors.ToList();
        }

        public List<Movie> SearchMovies(string query)
        {
            if (string.IsNullOrEmpty(query))
                return new List<Movie>();

            return context.Movies
                .Include(m => m.Cinema) // Include related entities if needed
                .Include(m => m.Category)
                .Include(m => m.Actors)
                .Where(m => m.Name.Contains(query) ||
                            m.Description.Contains(query) ||
                            m.Cinema.Name.Contains(query) ||
                            m.Category.Name.Contains(query))
                .ToList();
        }
    }
}
