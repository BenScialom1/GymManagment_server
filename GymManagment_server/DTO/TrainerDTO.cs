using System.ComponentModel.DataAnnotations;

namespace GymManagment_server.DTO
{
    public class TrainerDTO
    {
        public string? Name { get; set; }

        
        public int TrainerId { get; set; }

        public int? NumOfClasses { get; set; }
        public TrainerDTO() { }
        public TrainerDTO(Models.Trainer trainer)
        {
            this.Name = Name;
            this.TrainerId = trainer.TrainerId;
            this.NumOfClasses = trainer.NumOfClasses;
        }
    }
}
