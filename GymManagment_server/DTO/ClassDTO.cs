using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GymManagment_server.DTO
{
    public class ClassDTO
    {
        public string? Name { get; set; }

       
        public int ClassId { get; set; }

        [Column("DIFFICULTY")]
        public int? Difficulty { get; set; }
        public ClassDTO() { }
        public ClassDTO(Models.Class @class)
        {
            Name = @class.Name;
            ClassId = @class.ClassId;
            Difficulty = @class.Difficulty;

        }
    }
}
