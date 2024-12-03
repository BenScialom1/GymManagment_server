using GymManagment_server.Models;
namespace GymManagment_server.ModelsBL
{
    public class BenDBContext
    {
        public User GetUser(string email)
        {
            return this.User.Where(u => u.Email == email)
                                .Include(u => u.UserTasks)
                                .ThenInclude(t => t.TaskComments)
                                .FirstOrDefault();
        }
    }
}
