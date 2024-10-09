namespace GymManagment_server.DTO
{
    public class GymDTO
    {
        public string Name { get; set; }
        public int GymId { get; set; }
        public int? Level { get; set; }
        public GymDTO() { }
        public GymDTO(Models.Gym gym) 
        {
            this.Name = Name;
            this.GymId=GymId;
            this.Level = Level;
        }
    }
}
