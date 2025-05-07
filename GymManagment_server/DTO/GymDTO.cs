using System.ComponentModel.DataAnnotations;

namespace GymManagment_server.DTO
{
    public class GymDTO
    {
        public string? Name { get; set; }
        [Key]
        public int GymId { get; set; }
        public int? Level { get; set; }
        [StringLength(100)]
        public string Address { get; set; } = null!;
        public int Price { get; set; }
        [StringLength(15)]
        public string PhoneNumber { get; set; } = null!;
        public int? GymManager { get; set; }

        // Empty constructor needed for serialization/deserialization
        public GymDTO()
        {
        }

        public GymDTO(Models.Gym gym)
        {
            this.Name = gym.Name;
            this.GymId = gym.GymId;
            this.Level = gym.Level;
            this.Address = gym.Address;
            this.Price = gym.Price;
            this.PhoneNumber = gym.PhoneNumber;
            this.GymManager = gym.GymManager;
        }
    }
}
