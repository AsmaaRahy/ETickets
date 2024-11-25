using ETickets.Data;
using ETickets.IRepository;
using ETickets.Models;

namespace ETickets.Repository
{
    public class HomeRepository : IHomeRepository
    {
        private readonly ApplicationDbContext context;
        public List<Movie> ReadAll()
        {
            return context.Movies.ToList();
        }
    }
}
