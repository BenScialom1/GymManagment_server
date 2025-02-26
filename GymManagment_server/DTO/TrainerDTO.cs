using GymManagment_server.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymManagment_server.DTO
{
    public class TrainerDTO
    {
        public int TrainerId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int GymId { get; set; }

        // Empty constructor for serialization
        public TrainerDTO() { }

        // Constructor that converts from model to DTO
        public TrainerDTO(Trainer trainer)
        {
            TrainerId = trainer.TrainerId;
            Name = trainer.Name;
            Description = trainer.Description;
            GymId = trainer.GymId;
        }
    }
}
