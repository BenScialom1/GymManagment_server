using GymManagment_server.Models;

namespace GymManagment_server.DTO
{
    public class CommentDTO
    {
        public int CommentId { get; set; }
        public int UserId { get; set; }
        public int GymId { get; set; }
        public int Rank { get; set; }
        public string Description { get; set; }
        public DateTime CommentDate { get; set; }
        public UserDTO? User { get; set; }

        public CommentDTO() { }

        public CommentDTO(Comment comment)
        {
            CommentId = comment.CommentId;
            UserId = comment.UserId;
            GymId = comment.GymId;
            Rank = comment.Rank;
            Description = comment.Description;
            CommentDate = comment.Date;
            if (comment.User != null) 
            {
                this.User = new UserDTO(comment.User);
            }
        }

        public Comment GetModel()
        {
            return new Comment()
            {
                UserId = this.UserId,
                GymId = this.GymId,
                Rank = this.Rank,
                Description = this.Description,
                Date = DateTime.Now
            };
        }
    }
}

