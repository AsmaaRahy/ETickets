using System.ComponentModel.DataAnnotations;

namespace ETickets.ViewModel
{
    public class CinemaVM
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        
        public string CinemaLogo { get; set; }
        
        public string Address { get; set; }
    }
}
