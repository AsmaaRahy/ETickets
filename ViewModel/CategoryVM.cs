using ETickets.Models;
using System.ComponentModel.DataAnnotations;

namespace ETickets.ViewModel
{
    public class CategoryVM
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        //public List<Movie> Movies { get; set; }
    }
}
