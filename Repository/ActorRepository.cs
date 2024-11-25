using ETickets.Data;
using ETickets.IRepository;
using ETickets.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ETickets.Repository
{
    public class ActorRepository : IActorRepository
    {
        ApplicationDbContext context;
        public ActorRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void Create(Actor actor)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Actor> ReadAll()
        {
            return context.Actors.ToList();
        }
        public List<SelectListItem> GetActorsForDropdown()
        {
            return context.Actors.Select(actor => new SelectListItem
            {
                Value = actor.Id.ToString(),
                Text = actor.FirstName + " " + actor.LastName
            }).ToList();
        }
        public Actor ReadById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Actor actor)
        {
            throw new NotImplementedException();
        }
    }
}
