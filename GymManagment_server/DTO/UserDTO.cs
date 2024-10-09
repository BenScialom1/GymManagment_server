namespace GymManagment_server.DTO
{
    public class UserDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int? Age { get; set; }
        public string Gender { get; set; }
        public UserDTO() { }
        public UserDTO(Models.User user)
        {
            this.Username = Username;
            this.Password = Password;
            this.Age = Age;
            this.Gender = Gender;
        }
    }
}
