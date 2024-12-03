
using System.ComponentModel.DataAnnotations;

namespace GymManagment_server.DTO

{
    public class UserDTO
    {
        public string? Username { get; set; }

        
        public string Email { get; set; } = null!;

        
        public string? Password { get; set; }

        
        public int Id { get; set; }

        public DateOnly? BirthDate { get; set; }

        
        public string Address { get; set; } = null!;

        
        public string Difficulty { get; set; } = null!;

        public int? GenderId { get; set; }

        public bool IsManager { get; set; }

        public UserDTO() { } 
        public UserDTO(Models.User user)
        {
            this.Username = user.Username;
            this.Email = user.Email;
            this.Password = user.Password;
            this.Id = user.Id;
            this.BirthDate = user.BirthDate;
            this.Address = user.Address;
            this.Difficulty = user.Difficulty;
            this.GenderId = user.GenderId;
            this.IsManager = user.IsManager;
        }

    }
}
