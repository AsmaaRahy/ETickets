using ETickets.Data.Enums;
using ETickets.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ETickets.ViewModel
{
    public class MovieVM
    {
       
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }

        public string? ImgUrl { get; set; }
        public string TrailerUrl { get; set; }

        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }

        public int CategoryId { get; set; }
        public int CinemaId { get; set; }
        [NotMapped]
        public List<int> SelectedActorIds { get; set; }



        public MovieStatus MovieStatus { get; set; }
        //public List<Actor> AllActors { get; set; }




    }


}

