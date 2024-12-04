using GymManagment_server.Models;
using Microsoft.EntityFrameworkCore;
namespace GymManagment_server.Models
{
    public partial class BenDBContext:DbContext
    {
        public User GetUser(string email)
        {
            return this.Users.Where(u => u.Email == email).FirstOrDefault();
        }
    }
}
